namespace DogCeoService.EntitiesDto
{
    public class DogDto
    {
        public DogDto()
        {
            DogPictures = new List<DogPictureDto>();
        }

        public string? Breed { get; set; }
        public List<DogPictureDto> DogPictures { get; set; }
    }
}
