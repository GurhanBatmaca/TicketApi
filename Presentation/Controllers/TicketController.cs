using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/")]
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
    [Route("AllTickets")]
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
}