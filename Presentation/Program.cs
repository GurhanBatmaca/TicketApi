using System.Text;
using Business;
using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StoreContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString"),e=> e.MigrationsAssembly("Presentation"));
});

builder.Services.AddDbContext<IdentityContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString"),e=> e.MigrationsAssembly("Presentation"));
});

builder.Services.AddIdentity<AuthUser,IdentityRole>().AddEntityFrameworkStores<IdentityContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequireDigit = true; 
    options.Password.RequireLowercase = true; 
    options.Password.RequireUppercase = true; 
    options.Password.RequiredLength = 6; 
    options.Password.RequireNonAlphanumeric = true; 

    options.Lockout.MaxFailedAccessAttempts = 5; 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); 
    options.Lockout.AllowedForNewUsers = true; 

    options.User.RequireUniqueEmail = true; 
    options.SignIn.RequireConfirmedEmail = true; 
    options.SignIn.RequireConfirmedPhoneNumber = false; 
});


builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddScoped<ITicketService,TicketManager>();
builder.Services.AddScoped<IActivityService,ActivityManager>();
builder.Services.AddScoped<IAddressService,AddressManager>();
builder.Services.AddScoped<IArtorService,ArtorManager>();
builder.Services.AddScoped<ISignService,SignManager>();
builder.Services.AddScoped<IUserService,UserManager>();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: "_myAllowOrigins",
        builder => {
            builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        }
    );
});

builder.Services.AddAuthentication(auth => {
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options => {
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JwtSettings:Audince"],
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };
});

builder.Services.AddControllers();
                // .AddJsonOptions(options => options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull);  
                // null response obje prop'u önlemek için


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});




var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("_myAllowOrigins");

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

IdentitySeed.Seed(app,builder.Configuration);

app.Run();