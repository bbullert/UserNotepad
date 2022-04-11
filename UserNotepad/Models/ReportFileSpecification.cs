using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotepad.Models
{
    public class ReportFileSpecification
    {
        public string FileName { get; set; }

        public string FileType { get; set; }

        public string FullFileName => $"{FileName}.{FileType}";

        public string ContentType { get; set; }

        public byte[] Content { get; set; }
    }
}
