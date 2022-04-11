using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Data.Repositories;

namespace UserNotepad.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        private IUserRepository userRepository;

        public UnitOfWork(AppDbContext context)
        {
            this.context = context;
            userRepository = new UserRepository(context);
        }

        public IUserRepository Users => userRepository;

        public async Task CommitAsync()
        {
            await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            context.Dispose();
        }
    }
}
