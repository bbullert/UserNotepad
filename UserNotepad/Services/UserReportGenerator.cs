using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;

namespace UserNotepad.Services
{
    public class UserReportGenerator : IUserReportGenerator
    {
        public ReportFileSpecification Generate(IEnumerable<User> users, string fileName, string fileType)
        {
            var fileProvider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
            if (!fileProvider.TryGetContentType('.' + fileType, out string contentType))
            {
                throw new ArgumentOutOfRangeException();
            }

            IUserExporter exporter = fileType switch
            {
                "json" => new JsonUserExporter(),
                "xlsx" => new ExcelUserExporter(),
                _ => throw new NotImplementedException()
            };

            byte[] byteArray = exporter.Export(users);

            return new ReportFileSpecification
            {
                FileName = fileName,
                FileType = fileType,
                ContentType = contentType,
                Content = byteArray
            };
        }
    }
}
