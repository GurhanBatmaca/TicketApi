using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;
namespace Presentation;

[ApiController]
[Route("api/tickets")]
public class TicketController: ControllerBase
{
    protected private ITicketService _ticketService;
    protected private IConfiguration _configuration;
    public int PageSize => Int32.Parse(_configuration["PageSize"]!);
    public TicketController(ITicketService ticketService,IConfiguration configuration)
    {
        _ticketService = ticketService;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All(int page=1)
    {
        var tickets = await _ticketService.GetAll(page,PageSize);
        var totalItems = await _ticketService.GetAllCount();      
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        if(page > pageInfo.TotalPages)
        {
            return BadRequest( new ErrorResponse { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new SuccessResponse{ Data = tickets!, PageInfo = pageInfo } );
    }


    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterModel model,int page=1)
    {
        var tickets = await _ticketService.GetFilterResult(model,page,PageSize);
        var totalItems = await _ticketService.GetFilterResultCount(model);
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        if(page > pageInfo.TotalPages)
        {
            return BadRequest( new ErrorResponse { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new SuccessResponse { Data = tickets, PageInfo = pageInfo } );

    }


    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search([FromQuery] SearchModel model,int page=1)
    {
        var tickets = await _ticketService.GetSearchResult(model,page,PageSize);
        var totalItems = await _ticketService.GetSearchResultCount(model);
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        if(page > pageInfo.TotalPages)
        {
            return BadRequest( new ErrorResponse { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new SuccessResponse { Data = tickets, PageInfo = pageInfo } );

    }

    [HttpGet]
    [Route("ticket/{id}")]
    public async Task<IActionResult> TicketDetails(int id)
    {
        var ticket = await _ticketService.GetById(id);

        if(ticket is null)
        {
            return BadRequest( new ErrorResponse { Error = "Ticket id hatası" } );
        }

        // if(string.IsNullOrEmpty(ticket!.Name))
        // {
        //     return BadRequest( new ErrorResponse { Error = "Ticket id hatası" } );
        // }
        return Ok( new SuccessResponse { Data = ticket } );

    }

}