using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;

namespace UserNotepad.Data.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {

    }
}
