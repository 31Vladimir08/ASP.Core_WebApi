using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogCeoService.EntitiesDto
{
    public class DogDto
    {
        public DogDto()
        {
            DogPictures = new List<DogPictureDto>();
        }

        public string Bread { get; set; }
        public List<DogPictureDto> DogPictures { get; set; }
    }
}
