namespace Doamin.Service.Users
{
    using Doamin.Service.Security;
    using Domain.Model.Users;
    using Infrastructure.Domain;
    using System;
    using System.Linq;

    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        private readonly IRepository<UserRole> userRoleRepository;

        private readonly IEncryptionService encryptionService;

        private readonly IUnitOfWork unitOfWork;

        public UserService(IRepository<User> userRepository,
            IRepository<UserRole> userRoleRepository,
            IEncryptionService encryptionService, IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.encryptionService = encryptionService;
            this.unitOfWork = unitOfWork;
        }

        public User GetUserByName(string userName)
        {
            var users = this.userRepository.FindAll(u => u.Name == userName);

            return users.SingleOrDefault();
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            this.userRepository.Update(user);
            this.unitOfWork.Commit();
        }

        public UserLoginResults ValidateUser(string userName, string password)
        {
            var user = this.GetUserByName(userName);

            if (user == null)
            {
                return UserLoginResults.UserNotExist;
            }

            if (user.Deleted)
            {
                return UserLoginResults.Deleted;
            }
            if (!user.Active)
            {
                return UserLoginResults.NotActive;
            }

            var pwd = this.encryptionService.CreatePasswordHash(password, user.PasswordSalt, "SHA1");

            if (pwd != user.Password)
            {
                return UserLoginResults.WrongPassword;
            }
            user.LastLoginDate = DateTime.Now;
            this.UpdateUser(user);
            return UserLoginResults.Successful;
        }

        public UserRole GetUserRoleBySystemName(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
            {
                return null;
            }

            var query = this.userRoleRepository.FindAll(ur => ur.SystemName == systemName).OrderBy(cr => cr.Id);
            return query.FirstOrDefault();
        }

        public UserRole GetUserRoleById(int roleId)
        {
            return roleId <= 0 ? null : this.userRoleRepository.GetByKey(roleId);
        }

        public void InsertUserRole(UserRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            this.userRoleRepository.Add(role);
            this.unitOfWork.Commit();
        }
    }
}