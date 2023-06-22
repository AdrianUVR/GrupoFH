using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class TicketController : Controller
    {
        [HttpGet]
        [Route("api/Ticket/GetAll")]
        public ActionResult GetAll()
        {
            ML.Ticket ticket = new ML.Ticket();
            ML.Result result = BL.Ticket.GetAllTicket();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

    }
}
