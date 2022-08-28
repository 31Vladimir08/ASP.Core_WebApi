using GetPicturesFromDogCeo.ViewModels;
using GetPicturesFromDogCeo.WebServices.HostServices;

using Microsoft.AspNetCore.Mvc;

namespace GetPicturesFromDogCeo.Controllers
{
    [ApiController]
    [Route("api/dogs")]
    public class DogsController : ControllerBase
    {
        private readonly DogWebHostService _dogWebHostService;

        public DogsController(DogWebHostService dogWebHostService)
        {
            _dogWebHostService = dogWebHostService;
        }

        [HttpPost]
        public IActionResult GetDogPictures([FromBody] DogsQueryViewModel dogsQueryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var isRun = _dogWebHostService.StartEventExecute(dogsQueryViewModel);
            return isRun
                ? Ok(new {Status = "ok"})
                : Ok(new { Status = "run" });
        }
    }
}
