using Microsoft.AspNetCore.Mvc;
using NumberClassification.Application.Interface;
using NumberClassification.API.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NumberClassification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ValidateNumberQueryParameter]
    public class NumberController : ControllerBase
    {
        private readonly IClassifyNumber _classifyNumberUseCase;

        public NumberController(IClassifyNumber classifyNumberUseCase)
        {
            _classifyNumberUseCase = classifyNumberUseCase;
        }

        [HttpGet("classify-number")]
        public async Task<IActionResult> ClassifyNumber([FromQuery] string number)
        {
            if (!int.TryParse(number, out int parsedNumber))
            {
                return BadRequest(new { number = "alphabet", error = true });
            }

            var result = await _classifyNumberUseCase.ExecuteAsync(parsedNumber);
            return Ok(result);
        }
    }
}
