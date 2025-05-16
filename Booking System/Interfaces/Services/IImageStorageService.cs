namespace Booking_System.Interfaces.Services
{
    public interface IImageStorageService
    {
        Task UploadImageAsync(Stream imageStream, string fileName);
    }
}
