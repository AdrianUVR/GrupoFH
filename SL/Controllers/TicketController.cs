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

   

        [HttpPost]
        [Route("api/Ticket/Add")]
        public ActionResult Add([FromBody] ML.Ticket ticket)
        {
            ML.Result result = BL.Ticket.Add(ticket);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpGet]
        [Route("api/Empleado/GetAllByIdTicket/{IdTicket}")]
        public ActionResult GetById(int IdTicket)
        {

            ML.Result result = BL.Ticket.GetAllByIdTicket(IdTicket);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [HttpPost]
        [Route("api/Ticket/Update")]
        public ActionResult Update([FromBody] ML.Ticket ticket)
        {
            ML.Result result = BL.Ticket.Update(ticket);

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
