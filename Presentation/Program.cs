using Business;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<StoreContext>(opt => {
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConnectionString"),e=> e.MigrationsAssembly("Presentation"));
});

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddScoped<ITicketService,TicketManager>();

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

builder.Services.AddControllers();
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

app.Run();