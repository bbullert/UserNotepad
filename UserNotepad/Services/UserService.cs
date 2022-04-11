using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Data;
using UserNotepad.Extensions.Methods;
using UserNotepad.Models;
using UserNotepad.ViewModels;

namespace UserNotepad.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IPhoneNumberService phoneNumberService;

        public UserService(IUnitOfWork unitOfWork, IPhoneNumberService phoneNumberService)
        {
            this.unitOfWork = unitOfWork;
            this.phoneNumberService = phoneNumberService;
        }

        public async Task MockDataAsync()
        {
            if (await unitOfWork.Users.CountAsync() == 0)
            {
                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());

                var file = System.IO.File.ReadAllText("mockData.json");
                var models = System.Text.Json.JsonSerializer.Deserialize<List<User>>(file, options);

                await unitOfWork.Users.AddRangeAsync(models);
                await unitOfWork.CommitAsync();
            }
        }

        public async Task<IEnumerable<User>> GetUsers(int currentPage = 1, string filter = null, int pageSize = 0)
        {
            Expression<Func<User, bool>> predicate = string.IsNullOrWhiteSpace(filter) ?
                x => true :
                x => x.FirstName.Contains(filter) ||
                    x.LastName.Contains(filter) ||
                    x.PhoneNumber.Contains(filter) ||
                    x.Email.Contains(filter);

            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = query => query
                .OrderBy(user => user.FirstName)
                .ThenBy(user => user.LastName);

            var users = await unitOfWork.Users.GetAsync(
                selector: x => x,
                predicate: predicate,
                orderBy: orderBy,
                skip: (currentPage - 1) * pageSize,
                take: pageSize);

            int count = await unitOfWork.Users.CountAsync(predicate);

            return users.ToPagedList(currentPage, count, pageSize);
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await unitOfWork.Users.GetByIdAsync(id);
        }

        public async Task<Guid> CreateUser(User user)
        {
            await unitOfWork.Users.AddAsync(user);
            await unitOfWork.CommitAsync();

            return user.Id;
        }

        public async Task UpdateUser(Guid id, User updated)
        {
            unitOfWork.Users.Update(updated);
            await unitOfWork.CommitAsync();
        }

        public async Task RemoveUser(Guid id)
        {
            var user = await unitOfWork.Users.GetByIdAsync(id);
            unitOfWork.Users.Remove(user);
            await unitOfWork.CommitAsync();
        }

        public async Task<ReportFileSpecification> GenerateReport(UserExportViewModel model)
        {
            var users = await GetUsers();
            var generator = new UserReportGenerator();
            var report = generator.Generate(users, model.FileName, model.FileType);

            return report;
        }
    }
}
