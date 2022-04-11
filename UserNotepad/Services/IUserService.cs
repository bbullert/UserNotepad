using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Models;
using UserNotepad.ViewModels;

namespace UserNotepad.Services
{
    public interface IUserService
    {
        Task MockDataAsync();
        Task<User> GetUserById(Guid id);
        Task<IEnumerable<User>> GetUsers(int currentPage = 1, string filter = null, int pageSize = 0);
        Task<Guid> CreateUser(User user);
        Task RemoveUser(Guid id);
        Task UpdateUser(Guid id, User updated);
        Task<ReportFileSpecification> GenerateReport(UserExportViewModel model);
    }
}
