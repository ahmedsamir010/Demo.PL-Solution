using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string UploadFile(IFormFile file, string folderName)
        {
            // 1.Get Located Folder Path 
            //  var folderPath= @"F:\Demo.PL Solution\Demo.PL\wwwroot\Images\"
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName);

            // 2. Get File Name
            var fileName = $"{Guid.NewGuid()}{Path.GetFileName(file.FileName)}";


            // 3. get File Path
            var filePath = Path.Combine(folderPath, fileName);

            // 4. Save File as Streams

            using var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;



        }
        public static void DeleteFile(string folderName, string fileName)
        {
            if (fileName != null && folderName != null)
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", folderName, fileName);
                if (File.Exists(filePath))
                    File.Delete(filePath);
            }


        }

    }
}
