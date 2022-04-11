using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;

namespace UserNotepad.Services
{
    public interface IUserReportGenerator
    {
        ReportFileSpecification Generate(IEnumerable<User> users, string fileName, string fileType);
    }
}
