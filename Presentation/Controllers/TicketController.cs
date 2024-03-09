using AutoMapper;
using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;
using Shared.Helpers;

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
            return BadRequest( new { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new { Tickects = tickets, PageInfo = pageInfo } );
    }

    [HttpGet]
    [Route("allWithDetails")]
    public async Task<IActionResult> AllWithDetails(int page=1)
    {
        var tickets = await _ticketService.GetAllWithDetails(page,PageSize);
        var totalItems = await _ticketService.GetAllWithDetailsCount();      
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        if(page > pageInfo.TotalPages)
        {
            return BadRequest( new { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new { Tickects = tickets, PageInfo = pageInfo } );
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
            return BadRequest( new { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new { Tickects = tickets, PageInfo = pageInfo } );

    }

    [HttpGet]
    [Route("filterWithDetails")]
    public async Task<IActionResult> FilterWithDetails([FromQuery] FilterModel model,int page=1)
    {
        var tickets = await _ticketService.GetFilterResultWithDetails(model,page,PageSize);
        var totalItems = await _ticketService.GetFilterResultWithDetailsCount(model);
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        if(page > pageInfo.TotalPages)
        {
            return BadRequest( new { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new { Tickects = tickets, PageInfo = pageInfo } );

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
            return BadRequest( new { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new { Tickects = tickets, PageInfo = pageInfo } );

    }

    [HttpGet]
    [Route("searchWithDetails")]
    public async Task<IActionResult> SearchWithDetails([FromQuery] SearchModel model,int page=1)
    {
        var tickets = await _ticketService.GetSearchResultWithDetails(model,page,PageSize);
        var totalItems = await _ticketService.GetSearchResultWithDetailsCount(model);
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        if(page > pageInfo.TotalPages)
        {
            return BadRequest( new { Error = "İndex hatası,liste boyutu aşıldı." } );
        }

        return Ok( new { Tickects = tickets, PageInfo = pageInfo } );

    }


    [HttpGet]
    [Route("ticket/{id}")]
    public async Task<IActionResult> TicketDetails(int id)
    {
        var ticket = await _ticketService.GetById(id);

        if(!string.IsNullOrEmpty(ticket!.Name))
        {
            return Ok( new { Ticket = ticket } );
        }
        else
        {
            return BadRequest( new { Error = "Ticket id hatası" } );
        }
    }

}