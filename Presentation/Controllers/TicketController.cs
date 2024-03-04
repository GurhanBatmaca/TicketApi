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
    public TicketController(ITicketService ticketService,IConfiguration configuration)
    {
        _ticketService = ticketService;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("All")]
    public async Task<IActionResult> GetAllTickets(int page=1)
    {
        var pageSize = Int32.Parse(_configuration["PageSize"]!);
        var ticketList = await _ticketService.GetAllTickets(page,pageSize);
        var totalItems = await _ticketService.GetAllTicketsCount();
        
        var pageInfo = new PageInfo {
            TotalItems = totalItems,
            ItemPerPage = pageSize,
            CurrentPage = page,
            TotalPage = (int)Math.Ceiling((decimal)totalItems/pageSize)
        };

        if(ticketList!.Count > 0)
            return Ok( new {Tickects = ticketList, PageInfo = pageInfo});

        return BadRequest( new {Message = "Listenin boyutu aşıldı.", PageInfo = pageInfo});

    }

    [HttpPost]
    [Route("Filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterModel model,int page=1)
    {
        // var pageSize = Int32.Parse(_configuration["PageSize"]!);
        // var ticketList = await _ticketService.GetTicketsByActivity(page,pageSize,UrlConverter.Convert(activity));
        // var totalItems = await _ticketService.GetTicketsByActivityCount(UrlConverter.Convert(activity));
        
        // var pageInfo = new PageInfo {
        //     TotalItems = totalItems,
        //     ItemPerPage = pageSize,
        //     CurrentPage = page,
        //     TotalPage = (int)Math.Ceiling((decimal)totalItems/pageSize)
        // };

        // if(ticketList!.Count > 0)
        //     return Ok( new {Tickects = ticketList, PageInfo = pageInfo});

        // return BadRequest( new {Message = "Listenin boyutu aşıldı.", PageInfo = pageInfo});
        return Ok();

    }

    // [HttpGet]
    // [Route("Search")]
    // public async Task<IActionResult> Search(DateTime date,string searcString="",int page=1)
    // {
    //     var pageSize = Int32.Parse(_configuration["PageSize"]!);
    //     var ticketList = await _ticketService.GetSearchResult(page,pageSize,UrlConverter.Convert(searcString),date);
    //     var totalItems = await _ticketService.GetTicketsByActivityCount(UrlConverter.Convert(searcString));
        
    //     var pageInfo = new PageInfo {
    //         TotalItems = totalItems,
    //         ItemPerPage = pageSize,
    //         CurrentPage = page,
    //         TotalPage = (int)Math.Ceiling((decimal)totalItems/pageSize)
    //     };

    //     if(ticketList!.Count > 0)
    //         return Ok( new {Tickects = ticketList, PageInfo = pageInfo});

    //     return BadRequest( new {Message = "Listenin boyutu aşıldı.", PageInfo = pageInfo});

    // }


}