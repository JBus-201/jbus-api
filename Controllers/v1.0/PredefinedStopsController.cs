using API.Controllers.v1;
using API.DTOs;
using API.Interfaces;
using API.Validations;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [CustomAuthorize("PASSENGER", "SUPER_ADMIN", "ADMIN")]
    public class PredefinedStopsController : BaseApiController
    {
        private readonly IPredefinedStopsRepository _predefinedStopsRepository;

        public PredefinedStopsController(
            IPredefinedStopsRepository predefinedStopsRepository
            )
        {
            _predefinedStopsRepository = predefinedStopsRepository;
        }

        [HttpPost]
        public async Task<ActionResult<PointDto>> CreatePredefinedStops(PredefinedStopsCreateDto predefinedStopsCreateDto)
        {
            var predefinedStops = _predefinedStopsRepository.CreatePredefinedStops(predefinedStopsCreateDto);
            if(!await _predefinedStopsRepository.SaveChanges())
                    return StatusCode(500, "Server Error1");
            return Ok(predefinedStops);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PredefinedStopsDto>> getPredefinedStopsById(int id)
        {
            PredefinedStopsDto? predefinedStops = await _predefinedStopsRepository.GetPredefinedStopById(id);
            
            if(predefinedStops == null)
                return NotFound("No Predefined Stops Defined");

            return Ok(predefinedStops);
        }
    }
}