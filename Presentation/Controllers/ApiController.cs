using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProductResultDto), StatusCodes.Status400BadRequest)]
    public class ApiController : ControllerBase
    {
    }
}
