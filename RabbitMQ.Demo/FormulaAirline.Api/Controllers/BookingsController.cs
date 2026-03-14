using FormulaAirline.Api.Models;
using FormulaAirline.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.Api.Controllers
{
    [ApiController]
    [Route("controller")]
    public class BookingsController : ControllerBase
    {
        public static readonly List<Booking> _bookings = new();
        private readonly ILogger<BookingsController> _logger;
        private readonly IMessageProducer _messageProducer;


        public BookingsController(ILogger<BookingsController> logger, IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public IActionResult CreateBooking(Booking newBooking)
        {
            if(ModelState.IsValid) return BadRequest();
            
            _bookings.Add(newBooking);
            _messageProducer.SendingMessages<Booking>(newBooking);
            return Ok();
        }
    }
}