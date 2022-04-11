using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Data.Repositories;

namespace UserNotepad.Data
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }

        Task CommitAsync();
    }
}
