using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HotelEasy.Services.Common;
using Microsoft.AspNetCore.Http;

namespace HotelEasy.Services;

public interface IImageService
{
    Task<ServiceResult<string>> UploadAsync(IFormFile file, string folder);
    Task<ServiceResult<List<string>>> UploadManyAsync(List<IFormFile> files, string folder);
}

public class CloudinaryImageService : IImageService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryImageService(Cloudinary cloudinary)
    {
        _cloudinary = cloudinary;
    }

    public async Task<ServiceResult<string>> UploadAsync(IFormFile file, string folder)
    {
        if (file == null || file.Length == 0)
            return ServiceResult<string>.Failure("No file uploaded.");

        using var stream = file.OpenReadStream();
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(file.FileName, stream),
            Folder = folder
        };

        var uploadResult = await _cloudinary.UploadAsync(uploadParams);

        return ServiceResult<string>.SuccessResult(uploadResult.SecureUrl.ToString());
    }

    public async Task<ServiceResult<List<string>>> UploadManyAsync(List<IFormFile> files, string folder)
    {
        var urls = new List<string>();

        foreach (var file in files)
        {
            var result = await UploadAsync(file, folder);
            if (!result.Success)
                return ServiceResult<List<string>>.Failure(result.ErrorMessage!);

            urls.Add(result.Data!);
        }

        return ServiceResult<List<string>>.SuccessResult(urls);
    }
}
