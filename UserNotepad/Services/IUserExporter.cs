using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;

namespace UserNotepad.Services
{
    public interface IUserExporter
    {
        byte[] Export(IEnumerable<User> users);
    }
}
