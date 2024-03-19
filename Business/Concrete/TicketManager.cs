using AutoMapper;
using Data;
using Entity;
using Shared;
using Shared.Helpers;

namespace Business;

public class TicketManager : ITicketService
{
    protected private IUnitOfWork? _unitOfWork;
    protected private IMapper? _mapper;
    public TicketManager(IUnitOfWork? unitOfWork,IMapper? mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 
    public SuccessResponse? SuccessResponse { get ; set ; }
    public ErrorResponse? ErrorResponse { get ; set ; }

    public async Task<bool> GetAll(int page, int pageSize)
    {
        var ticketList =  await _unitOfWork!.Tickets.GetAll(page,pageSize);

        if(ticketList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var tickets = ticketList.Select(e => new TicketSummaryDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = e.Activity!.Name,
            Address = e.Address!.Title,
            City = e.Address.City!.Name
        });

        var pageInfo = new PageInfo 
        {
            TotalItems = await _unitOfWork.Tickets.GetAllCount(),
            ItemPerPage = pageSize,
            CurrentPage = page
        };

        if(page > pageInfo.TotalItems)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Index hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse 
        {
            Data = tickets,
            PageInfo = pageInfo
        };

        return true;
    }

    public async Task<bool> GetFilterResult(FilterModel model,int page,int pageSize)
    {
        model.Activity = UrlConverter.Edit(model.Activity!);
        model.Address = UrlConverter.Edit(model.Address!);
        model.Artor = UrlConverter.Edit(model.Artor!);

        var ticketList = await _unitOfWork!.Tickets.GetFilterResult(model,page,pageSize);

        if(ticketList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

         var tickets = ticketList.Select(e => new TicketSummaryDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = e.Activity!.Name,
            Address = e.Address!.Title,
            City = e.Address.City!.Name
        });

        var pageInfo = new PageInfo 
        {
            TotalItems = await _unitOfWork.Tickets.GetFilterResultCount(model),
            ItemPerPage = pageSize,
            CurrentPage = page
        };

        if(page > pageInfo.TotalItems)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Index hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse 
        {
            Data = tickets,
            PageInfo = pageInfo
        };

        return true;
    }

    public async Task<bool> GetSearchResult(SearchModel model,int page,int pageSize)
    {
        model.Query = UrlConverter.Edit(model.Query);
        var ticketList = await _unitOfWork!.Tickets.GetSearchResult(model,page,pageSize);

        if(ticketList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var tickets = ticketList.Select(e => new TicketSummaryDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = e.Activity!.Name,
            Address = e.Address!.Title,
            City = e.Address.City!.Name
        });

        var pageInfo = new PageInfo 
        {
            TotalItems = await _unitOfWork.Tickets.GetSearchResultCount(model),
            ItemPerPage = pageSize,
            CurrentPage = page
        };

        if(page > pageInfo.TotalItems)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Index hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse 
        {
            Data = tickets,
            PageInfo = pageInfo
        };

        return true;
    }

    public async Task<bool> GetById(int id)
    {
        var ticket = await _unitOfWork!.Tickets.GetById(id);

        if(ticket is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Ticket id hatası."
            };
            return false;
        }

        var ticketDTO = new TicketDTO
        {
            Name = ticket!.Name,
            Price = ticket.Price,
            Url = ticket.Url,
            EventDate = ticket.EventDate,
            ImageUrl = ticket.ImageUrl,
            Activity = new ActivityDTO {
                Name = ticket.Activity!.Name,
                Url = ticket.Activity.Url,
                ImageUrl = ticket.Activity.ImageUrl,
                Categories = ticket.Activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList() 
            },
            Address = new AddressDTO {
                Title = ticket.Address!.Title,
                ImageUrl = ticket.Address.ImageUrl,
                City = new CityDTO {
                    Name = ticket.Address.City!.Name,
                    ImageUrl = ticket.Address.City!.ImageUrl,
                    Url = ticket.Address.City!.Url
                }
            },
            Artors = ticket.TicketArtors.Select( i=> _mapper!.Map<ArtorDTO>(i.Artor)).ToList()
        };

        SuccessResponse = new SuccessResponse 
        {
            Data = ticketDTO
        };

        return true;
    }

    public async Task<bool> Create(TicketModel model)
    {
        if(model.Price <= 0 || model.Price > 99999)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Price hatası."
            };
            return false;
        }

        if(model.Limit <= 0 || model.Limit > 50000)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Limit hatası."
            };
            return false;
        }

        if(string.IsNullOrEmpty(model.Name))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Name hatası."
            };
            return false;
        }

        var chechAddress = await _unitOfWork!.Addresses.GetById(model.AddressId);

        if(chechAddress is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Var olmayan address hatası."
            };
            return false;
        }

        var chechActivity = await _unitOfWork!.Activities.GetById(model.ActivityId);

        if(chechActivity is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Var olmayan activity hatası."
            };
            return false;
        }

        foreach (var id in model.ArtorsIds!)
        {
            var chechartor = await _unitOfWork!.Artors.GetById(id);

            if(chechartor is null)
            {
                ErrorResponse = new ErrorResponse 
                {
                    Error = "Var olmayan artor hatası."
                };
                return false;
            }
        }        
        
        var ticket = new Ticket
        {
            Limit = model.Limit,
            Name = model.Name,
            Price = model.Price,
            Url = UrlConverter.Edit(model.Name),
            EventDate = model.EventDate,
            AddressId = model.AddressId,
            ActivityId = model.ActivityId,
            TicketArtors = model.ArtorsIds!.Select(ai => new TicketArtor {
                TicketId = model.Id,
                ArtorId = ai

            }).ToList()
        };

        if(model.Image is null)
        {
            ticket.ImageUrl = "defaultImage.jpg";
        }
        else
        {
            var extention = Path.GetExtension(model!.Image!.FileName);
            var randomName = string.Format($"{Guid.NewGuid()}{extention}");
            var path = Path.Combine(Directory.GetCurrentDirectory(),"..\\Presentation\\wwwroot\\images", randomName);

            using (var stream = new FileStream(path,FileMode.Create))
            {
                await model!.Image.CopyToAsync(stream);
            }
            
            ticket.ImageUrl = randomName;           
        } 

        await _unitOfWork!.Tickets.Create(ticket);

        SuccessResponse = new SuccessResponse 
        {
            Message = "Ticket eklendi."
        };
        return true;
    }

    public async Task<bool> Update(TicketModel model)
    {
        if(model.Price <= 0 || model.Price > 99999)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Price hatası."
            };
            return false;
        }

        if(model.Limit <= 0 || model.Limit > 50000)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Limit hatası."
            };
            return false;
        }

        if(string.IsNullOrEmpty(model.Name))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Name hatası."
            };
            return false;
        }

        var chechAddress = await _unitOfWork!.Addresses.GetById(model.AddressId);

        if(chechAddress is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Var olmayan address hatası."
            };
            return false;
        }

        var chechActivity = await _unitOfWork!.Activities.GetById(model.ActivityId);

        if(chechActivity is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Var olmayan activity hatası."
            };
            return false;
        }

        foreach (var id in model.ArtorsIds!)
        {
            var chechartor = await _unitOfWork!.Artors.GetById(id);

            if(chechartor is null)
            {
                ErrorResponse = new ErrorResponse 
                {
                    Error = "Var olmayan artor hatası."
                };
                return false;
            }
        }        
        
        var ticket = await _unitOfWork!.Tickets.GetById(model.Id);

        ticket!.Name = model.Name;
        ticket.Url = UrlConverter.Edit(model.Name);
        ticket.Limit = model.Limit;
        ticket.Price = model.Price;
        ticket.EventDate = model.EventDate;
        ticket.AddressId = model.AddressId;
        ticket.ActivityId = model.ActivityId;
        ticket.TicketArtors = model.ArtorsIds!.Select(ai=> new TicketArtor{
            TicketId = model.Id,
            ArtorId = ai
        }).ToList();

        if(model.Image is not null)
        {
            var extention = Path.GetExtension(model!.Image!.FileName);
            var randomName = string.Format($"{Guid.NewGuid()}{extention}");
            var path = Path.Combine(Directory.GetCurrentDirectory(),"..\\Presentation\\wwwroot\\images", randomName);

            using (var stream = new FileStream(path,FileMode.Create))
            {
                await model!.Image.CopyToAsync(stream);
            }

            if(ticket!.ImageUrl != "defaultImage.jpg")
                {
                    var exPath = Path.Combine(Directory.GetCurrentDirectory(),"..\\Presentation\\wwwroot\\images",ticket!.ImageUrl!);
                    System.IO.File.Delete(exPath);

                    ticket.ImageUrl = randomName;
                }
            
            ticket.ImageUrl = randomName;  
        }

        await _unitOfWork.Tickets.Update(ticket);

        SuccessResponse = new SuccessResponse {
            Message = "Ticket güncellendi"
        };
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var ticket = await _unitOfWork!.Tickets.GetById(id);

        if(ticket is null || id < 1)
        {
            ErrorResponse = new ErrorResponse {
                Error = "Ticket id hatası."
            };
            return false;
        }

        await _unitOfWork.Tickets.Delete(ticket);

        SuccessResponse = new SuccessResponse {
            Message = "Ticket silindi"
        };
        return true;
    }
}