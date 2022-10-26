using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Tutorial_UploadSamples.Services
{
    public interface IStreamFileUploadService
    {
        Task<bool> UploadFile(MultipartReader reader, MultipartSection section);
    }

    public class StreamFileUploadLocalService : IStreamFileUploadService
    {
        public async Task<bool> UploadFile(MultipartReader reader, MultipartSection? section)
        {
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(
                    section.ContentDisposition, out var contentDisposition
                );

                if (hasContentDispositionHeader)
                {
                    if (contentDisposition.DispositionType.Equals("form-data") &&
                   (!string.IsNullOrEmpty(contentDisposition.FileName.Value) ||
                   !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value)))
                    {
                        var _targetFilePath = @"C:\Temp\upload";
                        var filePath = Path.Combine(_targetFilePath, contentDisposition.FileName.Value);

                        using (var fileStream = File.Create(filePath))
                        {
                            using (var streamWriter = new StreamWriter(fileStream))
                            {
                                await section.Body.CopyToAsync(fileStream);
                            }
                        }
                    }
                }
                section = await reader.ReadNextSectionAsync();
            }
            return true;
        }
    }
}
