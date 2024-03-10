﻿using Data;
using Entity;
using Shared;

namespace Business;

public class ArtorManager : IArtorService
{

    protected private IUnitOfWork? _unitOfWork;
    public ArtorManager(IUnitOfWork? unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public string? Message { get; set; }

    public Task Create(Artor entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Artor entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Artor entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ArtorSummaryDTO>> GetAll()
    {
        return await _unitOfWork!.Artors.GetAll();
    }

    public async Task<List<ArtorDTO>> GetAllWithWorks()
    {
        return await _unitOfWork!.Artors.GetAllWithWorks();
    }


}