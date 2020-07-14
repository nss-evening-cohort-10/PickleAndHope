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
            var boundary = GetBoundary(MediaTypeHeaderValue.Parse(contentType), DefaultFormOptions.MultipartBoundaryLengthLimit);

            var reader = new MultipartReader(boundary, requestBody);

            var section = reader.ReadNextSectionAsync().Result;
            
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader)
                {
                    if (HasFileContentDisposition(contentDisposition))
                    {
                        using (var targetStream = new MemoryStream())
                        {
                            section.Body.CopyTo(targetStream);
                            
                            return new UploadedFile
                            {
                                Content = targetStream.ToArray(),
                                ContentType = section.ContentType,
                                Size = targetStream.Length,
                                Filename = contentDisposition.FileName.Value
                            };
                        }
                    }
                }
                section = reader.ReadNextSectionAsync().Result;
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
        public byte[] Content { get; set; }
        
        public long Size { get; set; }
        
        public string ContentType { get; set; }
        
        public string Filename { get; set; }
    }
}
