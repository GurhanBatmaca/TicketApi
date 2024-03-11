﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Data;

public class UnitOfWork : IUnitOfWork
{
    protected private StoreContext? _context;
    protected readonly IMapper? _mapper;
    private readonly SignInManager<AuthUser> _signInManager;
    private readonly UserManager<AuthUser> _userManager;
    private readonly IConfiguration _configuration;
    public UnitOfWork(StoreContext? context,IMapper? mapper,SignInManager<AuthUser> signInManager,UserManager<AuthUser> userManager,IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }
    protected private EfCoreTicketRepository? ticketRepository;
    protected private EfCoreActivityRepository? activityRepository;
    protected private EfCoreAddressRepository? addressRepository;
    protected private EfCoreArtorRepository? artorRepository;
    protected private EfCoreSignRepository? signRepository;
    protected private EfCoreUserRepository? userRepository;
    public ITicketRepository Tickets => ticketRepository ?? new EfCoreTicketRepository(_context!,_mapper);

    public IActivityRepository Activities => activityRepository ?? new EfCoreActivityRepository(_context!,_mapper);

    public IAddressRepository Addresses => addressRepository ??  new EfCoreAddressRepository(_context!,_mapper);

    public IArtorRepository Artors => artorRepository ?? new EfCoreArtorRepository(_context!,_mapper);

    public ISignRepository Signs => signRepository ?? new EfCoreSignRepository(_signInManager,_userManager,_configuration);

    public IUserRepository Users => userRepository ?? new EfCoreUserRepository(_signInManager,_userManager,_configuration);

    public void Dispose()
    {
        _context!.Dispose();
    }
}
