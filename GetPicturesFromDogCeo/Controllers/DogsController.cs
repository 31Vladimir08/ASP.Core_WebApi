using System.Threading.Tasks;
using System.Web.Http.Filters;

using DogCeoService.Interfaces;

using GetPicturesFromDogCeo.Filters;
using GetPicturesFromDogCeo.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace GetPicturesFromDogCeo.Controllers
{
    [ApiController]
    [NotImplExceptionFilter]
    [Route("api/dogs")]
    public class DogsController : ControllerBase
    {
        private readonly IDogService _dogService;

        public DogsController(IDogService dogService)
        {
            _dogService = dogService;
        }

        [HttpPost]
        [NotImplExceptionFilter]
        public async Task<IActionResult> GetDogPictures([FromBody] DogsQueryViewModel dogsQueryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dogs = await _dogService.GetDogsAsync(dogsQueryViewModel.Count, dogsQueryViewModel.Breads);
            return Ok();
        }
    }
}
