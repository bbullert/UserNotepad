using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;

namespace UserNotepad.Services
{
    public class JsonUserExporter : IUserExporter
    {
        public byte[] Export(IEnumerable<User> users)
        {
            System.Text.Json.JsonSerializerOptions options = new()
            {
                WriteIndented = true
            };

            var json = System.Text.Json.JsonSerializer.Serialize(users, options);
            byte[] byteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(json);

            return byteArray;
        }
    }
}
