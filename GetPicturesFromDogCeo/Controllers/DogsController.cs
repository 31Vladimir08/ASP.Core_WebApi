using GetPicturesFromDogCeo.Interfaces.WebServices;
using GetPicturesFromDogCeo.ViewModels;
using GetPicturesFromDogCeo.WebServices.HostServices;

using Microsoft.AspNetCore.Mvc;

namespace GetPicturesFromDogCeo.Controllers
{
    [ApiController]
    [Route("api/dogs")]
    public class DogsController : ControllerBase
    {
        private readonly IDogWebService _dogWebService;
        private readonly DogWebHostService _dogWebHostService;

        public DogsController(IDogWebService dogWebService, DogWebHostService dogWebHostService)
        {
            _dogWebService = dogWebService;
            _dogWebHostService = dogWebHostService;
        }

        [HttpPost]
        public IActionResult GetDogPictures([FromBody] DogsQueryViewModel dogsQueryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            int i = 1;
            var isRun = _dogWebHostService.StartEventExecute(dogsQueryViewModel, null);
            return isRun
                ? Ok(new {Status = "ok"})
                : Ok(new { Status = "run" });
        }
    }
}
