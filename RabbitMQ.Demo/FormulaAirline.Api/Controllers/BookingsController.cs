using FormulaAirline.Api.Models;
using FormulaAirline.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaAirline.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingsController : ControllerBase
    {
        public static readonly List<Booking> _bookings = new();

        private readonly ILogger<BookingsController> _logger;
        private readonly IMessageProducer _messageProducer;

        public BookingsController(
            ILogger<BookingsController> logger,
            IMessageProducer messageProducer)
        {
            _logger = logger;
            _messageProducer = messageProducer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] Booking newBooking)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _bookings.Add(newBooking);

            await _messageProducer.SendMessage(newBooking);

            return Ok(new
            {
                message = "Booking sent to queue",
                booking = newBooking
            });
        }
    }
}
