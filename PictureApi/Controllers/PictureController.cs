using Microsoft.AspNetCore.Mvc;

using PictureApi.Interfaces.Repositories;
using PictureApi.Models;

namespace PictureApi.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class PictureController : ControllerBase
    {
        private readonly IPictureRepository _pictureRepository;

        public PictureController(IPictureRepository pictureRepository)
        {
            _pictureRepository = pictureRepository;
        }

        [HttpPost]
        public async Task<IActionResult> SetPicture([FromBody] Picture picture)
        {
            await _pictureRepository.CreatePictureAsync(picture);
            return Ok();
        }
    }
}
