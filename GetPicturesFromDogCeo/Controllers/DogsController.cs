using System.Threading;
using System.Threading.Tasks;

using DogCeoService.Interfaces;

using GetPicturesFromDogCeo.Interfaces.WebServices;
using GetPicturesFromDogCeo.ViewModels;

using Microsoft.AspNetCore.Mvc;

namespace GetPicturesFromDogCeo.Controllers
{
    [ApiController]
    [Route("api/dogs")]
    public class DogsController : ControllerBase
    {
        private readonly IDogWebService _dogWebService;

        public DogsController(IDogWebService dogWebService)
        {
            _dogWebService = dogWebService;
        }

        [HttpPost]
        public async Task<IActionResult> GetDogPictures([FromBody] DogsQueryViewModel dogsQueryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dogs = await _dogWebService.GetDogsAsync(dogsQueryViewModel.Count, new CancellationTokenSource().Token, dogsQueryViewModel.Breads);

            return Ok();
        }
    }
}
