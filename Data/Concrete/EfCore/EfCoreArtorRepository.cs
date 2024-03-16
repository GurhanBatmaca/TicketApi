using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class EfCoreArtorRepository: EfCoreGenericRepository<Artor>,IArtorRepository
{
    public EfCoreArtorRepository(StoreContext context):base(context)
    {
    }
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<ArtorSummaryDTO>> GetAll()
    {
        var artorList = Context!.Artors.AsQueryable();

        if(artorList is null)
        {
            return [];
        }

        var artors = artorList.Select(e => _mapper!.Map<ArtorSummaryDTO>(e));

        return await artors.ToListAsync();
    }

    public async Task<List<ArtorDTO>> GetAllWithWorks()
    {
        var artorList = Context!.Artors
                                    .Include(e=>e.ArtorWorks)
                                    .ThenInclude(e=> e.Work)
                                    .AsQueryable();

        if(artorList is null)
        {
            return [];
        }

        var artors = artorList.Select(e => new ArtorDTO {
            Name = e.Name,
            ImageUrl = e.ImageUrl,
            Url = e.Url,
            Works = e.ArtorWorks.Select(i=> _mapper!.Map<WorkDTO>(i.Work)).ToList()
        });

        return await artors.ToListAsync();
    }

    public async Task<ArtorDTO> GetById(int id)
    {
        var artor = await Context!.Artors
                                    .Include(e=>e.ArtorWorks)
                                    .ThenInclude(e=> e.Work)
                                    .FirstOrDefaultAsync(e=> e.Id == id);

        if(artor is null)
        {
            return new ArtorDTO();
        }

        var artorDTO =  new ArtorDTO {
            Name = artor.Name,
            ImageUrl = artor.ImageUrl,
            Url = artor.Url,
            Works = artor.ArtorWorks.Select(i=> _mapper!.Map<WorkDTO>(i.Work)).ToList()
        };

        return artorDTO;
    }
}
