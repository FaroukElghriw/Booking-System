using Amazon.S3;
using Amazon.S3.Model;

namespace Booking_System.Services
{
    public class ImageStorageService:ImageStorageService
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public ImageStorageService(IAmazonS3 s3Client, IConfiguration configuration)
        {
            _s3Client = s3Client;
            _bucketName = configuration["AWS:S3:BucketName"];
        }

        public async Task<string> UploadImageAsync(Stream imageStream, string fileName)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(fileName)}";
            var request = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = $"images/{uniqueFileName}",
                InputStream = imageStream,
                ContentType = MimeTypeMap.GetMimeType(Path.GetExtension(fileName))
            };

            await _s3Client.PutObjectAsync(request);
            return $"https://{_bucketName}.s3.amazonaws.com/images/{uniqueFileName}";
        }
    }
}
