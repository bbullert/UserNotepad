using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;

namespace UserNotepad.Services
{
    public class ExcelUserExporter : IUserExporter
    {
        public byte[] Export(IEnumerable<User> users)
        {
            MemoryStream stream;

            using (stream = new MemoryStream())
            {
                using (XLWorkbook wb = new XLWorkbook())
                {
                    DataTable dt = new DataTable("Users");
                    dt.Columns.AddRange(new DataColumn[9] {
                        new DataColumn("Id"),
                        new DataColumn("Title"),
                        new DataColumn("FirstName"),
                        new DataColumn("LastName"),
                        new DataColumn("Gender"),
                        new DataColumn("BirthDate"),
                        new DataColumn("Age"),
                        new DataColumn("Email"),
                        new DataColumn("PhoneNumber")
                    });

                    foreach (var user in users)
                    {
                        dt.Rows.Add(user.Id, user.Title, user.FirstName, user.LastName, user.Gender, user.BirthDate, user.Age, user.Email, user.PhoneNumber);
                    }

                    var wsDep = wb.Worksheets.Add(dt, "Users");
                    wsDep.Columns().AdjustToContents();

                    wb.SaveAs(stream);
                }
            }

            return stream.ToArray();
        }
    }
}
