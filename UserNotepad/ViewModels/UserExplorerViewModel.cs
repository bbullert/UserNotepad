using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;

namespace UserNotepad.ViewModels
{
    public class UserExplorerViewModel
    {
        public string Filter { get; set; }

        public IEnumerable<User> Users { get; set; }
    }
}
