using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace PickleAndHope.Helpers
{
    public class FileUploadHelper
    {
        static readonly FormOptions DefaultFormOptions = new FormOptions();
        
        public UploadedFile GetFileUploadContent(string contentType, Stream requestBody)
        {
            return InternalGetFileUploadContent(contentType, requestBody).Result;
        }

        static async Task<UploadedFile> InternalGetFileUploadContent(string contentType, Stream requestBody)
        {
            var boundary = GetBoundary(MediaTypeHeaderValue.Parse(contentType),
                DefaultFormOptions.MultipartBoundaryLengthLimit);

            var reader = new MultipartReader(boundary, requestBody);

            var section = await reader.ReadNextSectionAsync();

            while (section != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (HasFileContentDisposition(contentDisposition))
                    {
                        using var targetStream = new MemoryStream();
                        await section.Body.CopyToAsync(targetStream);

                        return new UploadedFile
                        {
                            FileContent = targetStream.ToArray(),
                            FileContentType = section.ContentType,
                            FileLength = targetStream.Length,
                            FileName = contentDisposition.FileName.Value
                        };
                    }
                }

                section = await reader.ReadNextSectionAsync();
            }

            throw new Exception("something went wrong during file upload.");
        }


        // Content-Type: multipart/form-data; boundary="----WebKitFormBoundarymx2fSWqWSd0OxQqq"
        // The spec says 70 characters is a reasonable limit.
        static string GetBoundary(MediaTypeHeaderValue contentType, int lengthLimit)
        {
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);
            if (string.IsNullOrWhiteSpace(boundary.Value))
            {
                throw new InvalidDataException("Missing content-type boundary.");
            }

            if (boundary.Length > lengthLimit)
            {
                throw new InvalidDataException($"Multipart boundary length limit {lengthLimit} exceeded.");
            }

            return boundary.Value;
        }

        static bool HasFormDataContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="key";
            return contentDisposition != null
                   && contentDisposition.DispositionType.Equals("form-data")
                   && string.IsNullOrEmpty(contentDisposition.FileName.Value)
                   && string.IsNullOrEmpty(contentDisposition.FileNameStar.Value);
        }
        
        static bool HasFileContentDisposition(ContentDispositionHeaderValue contentDisposition)
        {
            // Content-Disposition: form-data; name="myfile1"; filename="Misc 002.jpg"
            return contentDisposition != null
                   && contentDisposition.DispositionType.Equals("form-data")
                   && (!string.IsNullOrEmpty(contentDisposition.FileName.Value)
                       || !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value));
        }
    }

    public class UploadedFile
    {
        public byte[] FileContent { get; set; }
        
        public long FileLength { get; set; }
        
        public string FileContentType { get; set; }
        
        public string FileName { get; set; }
    }
}
