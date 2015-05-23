namespace Doamin.Service.Users
{
    using System;
    using System.Linq;
    using Doamin.Service.Security;
    using Domain.Model.Users;
    using Infrastructure.Domain;

    public class UserService : IUserService
    {
        private readonly IEncryptionService encryptionService;

        private readonly IUnitOfWork unitOfWork;

        private readonly IRepository<User> userRepository;

        private readonly IRepository<UserRole> userRoleRepository;

        public UserService(
            IRepository<User> userRepository,
            IRepository<UserRole> userRoleRepository,
            IEncryptionService encryptionService,
            IUnitOfWork unitOfWork)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.encryptionService = encryptionService;
            this.unitOfWork = unitOfWork;
        }

        public User GetUserByName(string userName)
        {
            var users = userRepository.FindAll(u => u.Name == userName);

            return users.SingleOrDefault();
        }

        public UserLoginResults ValidateUser(string userName, string password)
        {
            var user = GetUserByName(userName);

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

            var pwd = encryptionService.CreatePasswordHash(password, user.PasswordSalt, "SHA1");

            if (pwd != user.Password)
            {
                return UserLoginResults.WrongPassword;
            }
            user.LastLoginDate = DateTime.Now;
            UpdateUser(user);
            return UserLoginResults.Successful;
        }

        public UserRole GetUserRoleBySystemName(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
            {
                return null;
            }

            var query = userRoleRepository.FindAll(ur => ur.SystemName == systemName).OrderBy(cr => cr.Id);
            return query.FirstOrDefault();
        }

        public UserRole GetUserRoleById(int roleId)
        {
            return roleId <= 0 ? null : userRoleRepository.GetByKey(roleId);
        }

        public void InsertUserRole(UserRole role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            userRoleRepository.Add(role);
            unitOfWork.Commit();
        }

        public void ResetCheckoutData(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            UpdateUser(user);
        }

        public void UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }

            userRepository.Update(user);
            unitOfWork.Commit();
        }
    }
}