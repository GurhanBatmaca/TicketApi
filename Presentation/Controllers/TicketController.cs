using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/")]
public class TicketController: ControllerBase
{
    protected ITicketService _ticketService;
    public TicketController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpGet]
    [Route("get")]
    public async Task<IActionResult> GetTest(int page=1)
    {
        var pageSize = 2;
        var tickets = await _ticketService.GetAllTickets(page,pageSize);
        var ticketCount = await _ticketService.GetAllTicketsCount();

        var pageInfo = new PageInfo {
            TotalItems = ticketCount,
            ItemPerPage = pageSize,
            CurrentPage = page,
            TotalPage = (int)Math.Ceiling((decimal)ticketCount/pageSize)
        };

        return Ok(new ResponseModel {Content = tickets});

        // return Ok(new {pageInfo=pageInfo,tickets=tickets});
    }
}