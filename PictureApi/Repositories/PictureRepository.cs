using Microsoft.Extensions.Options;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;

using PictureApi.Interfaces.Repositories;
using PictureApi.Models;
using PictureApi.Models.Settings;

namespace PictureApi.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly IGridFSBucket _gridFS;
        public PictureRepository(
            IOptions<FileMongoDbSettings> categoryMongoDbSettings,
            IMongoClient mongoClient)
        {
            var db = mongoClient.GetDatabase(categoryMongoDbSettings.Value.DatabaseName);
            _gridFS = new GridFSBucket(db);
        }

        public async Task CreatePictureAsync(Picture picture)
        {
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    { "categoryId", picture.CategoryId }
                }
            };
            await _gridFS.UploadFromBytesAsync(picture.FileName, picture.File, options);
        }

        public async Task<Picture?> GetPictureAsync(ObjectId fileId)
        {
            var file = await _gridFS.DownloadAsBytesAsync(fileId);
            var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Id, fileId);
            var fileInfo = await _gridFS.Find(filter).FirstOrDefaultAsync();
            return fileInfo == null
                ? null
                : new Picture()
                {
                    File = file,
                    FileId = fileId,
                    FileName = fileInfo.Filename,
                    CategoryId = fileInfo.Metadata["categoryId"].AsString
                };
        }

        public async Task<IEnumerable<Picture>?> GetCategoryPicturesAsync(string categoryId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Metadata, new BsonDocument
                {
                    { "categoryId", categoryId }
                });
            var fileInfo = await _gridFS.Find(filter).ToListAsync();
            var pictures = new List<Picture>();
            foreach (var item in fileInfo)
            {
                var file = await _gridFS.DownloadAsBytesAsync(item.Id);
                pictures.Add(new Picture()
                {
                    FileId = item.Id,
                    FileName = item.Filename,
                    CategoryId = categoryId,
                    File = file
                });
            }

            return pictures;
        }

        public async Task<IEnumerable<Picture>> GetPicturesAsync()
        {
            throw new NotImplementedException();
        }

        public async Task RemovePictureAsync(ObjectId fileId)
        {
            await _gridFS.DeleteAsync(fileId);
        }

        public async Task RemoveCategoryPictureAsync(string categoryId)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(info => info.Metadata, new BsonDocument
                {
                    { "categoryId", categoryId }
                });
            var fileInfo = await _gridFS.Find(filter).ToListAsync();
            if (fileInfo == null)
                return;
            foreach (var item in fileInfo)
            {
                await _gridFS.DeleteAsync(item.Id);
            }
        }

        public async Task UpdatePictureAsync(Picture picture)
        {
            var options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
                {
                    { "categoryId", picture.CategoryId }
                }
            };
            await _gridFS.UploadFromBytesAsync(picture.FileName, picture.File, options);
        }
    }
}
