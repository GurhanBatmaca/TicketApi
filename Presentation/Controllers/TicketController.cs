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
    [Route("All")]
    public async Task<IActionResult> All(int page=1)
    {
        var ticketList = await _ticketService.GetAll(page,PageSize);
        var totalItems = await _ticketService.GetAllCount();      
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        return Ok( new { Tickects = ticketList, PageInfo = pageInfo } );
    }

    [HttpGet]
    [Route("AllWithDetails")]
    public async Task<IActionResult> AllWithDetails(int page=1)
    {
        var ticketList = await _ticketService.GetAllWithDetails(page,PageSize);
        var totalItems = await _ticketService.GetAllWithDetailsCount();      
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        return Ok( new { Tickects = ticketList, PageInfo = pageInfo } );
    }


    [HttpGet]
    [Route("Filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterModel model,int page=1)
    {
        var ticketList = await _ticketService.GetFilterResult(model,page,PageSize);
        var totalItems = await _ticketService.GetFilterResultCount(model);
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        return Ok( new { Tickects = ticketList, PageInfo = pageInfo } );

    }

    [HttpGet]
    [Route("FilterWithDetails")]
    public async Task<IActionResult> FilterWithDetails([FromQuery] FilterModel model,int page=1)
    {
        var ticketList = await _ticketService.GetFilterResultWithDetails(model,page,PageSize);
        var totalItems = await _ticketService.GetFilterResultWithDetailsCount(model);
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        return Ok( new { Tickects = ticketList, PageInfo = pageInfo } );

    }


    [HttpGet]
    [Route("Search")]
    public async Task<IActionResult> Search([FromQuery] SearchModel model,int page=1)
    {
        var ticketList = await _ticketService.GetSearchResult(model,page,PageSize);
        var totalItems = await _ticketService.GetSearchResultCount(model);
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        return Ok( new { Tickects = ticketList, PageInfo = pageInfo } );

    }

    [HttpGet]
    [Route("SearchWithDetails")]
    public async Task<IActionResult> SearchWithDetails([FromQuery] SearchModel model,int page=1)
    {
        var ticketList = await _ticketService.GetSearchResultWithDetails(model,page,PageSize);
        var totalItems = await _ticketService.GetSearchResultWithDetailsCount(model);
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        return Ok( new { Tickects = ticketList, PageInfo = pageInfo } );

    }


}