using AutoMapper;
using Data;
using Entity;
using Shared;

namespace Business;

public class ArtorManager : IArtorService
{
    protected private IUnitOfWork? _unitOfWork;
    protected private IMapper? _mapper;
    public ArtorManager(IUnitOfWork? unitOfWork,IMapper? mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 
    public SuccessResponse? SuccessResponse { get ; set ; }
    public ErrorResponse? ErrorResponse { get ; set ; }

    public Task<bool> Create(Artor entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Artor entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Artor entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> GetAll()
    {
        var artorList = await _unitOfWork!.Artors.GetAll();

        if(artorList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var artors = artorList.Select(e => _mapper!.Map<ArtorSummaryDTO>(e));

        SuccessResponse = new SuccessResponse 
        {
            Data = artors
        };

        return true;

    }

    public async Task<bool> GetAllWithWorks()
    {
        var artorList = await _unitOfWork!.Artors.GetAllWithWorks();

        if(artorList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var artors = artorList.Select(e => new ArtorDTO {
            Name = e.Name,
            ImageUrl = e.ImageUrl,
            Url = e.Url,
            Works = e.ArtorWorks.Select(i=> _mapper!.Map<WorkDTO>(i.Work)).ToList()
        });

        SuccessResponse = new SuccessResponse 
        {
            Data = artors
        };

        return true;
    }

    public async Task<bool> GetById(int id)
    {
        var artor = await _unitOfWork!.Artors.GetById(id);

        if(artor is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Artor id hatası."
            };
            return false;
        }

        var artorDTO =  new ArtorDTO {
            Name = artor.Name,
            ImageUrl = artor.ImageUrl,
            Url = artor.Url,
            Works = artor.ArtorWorks.Select(i=> _mapper!.Map<WorkDTO>(i.Work)).ToList()
        };

        SuccessResponse = new SuccessResponse 
        {
            Data = artorDTO
        };

        return true;
    }
}
