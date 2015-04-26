namespace Doamin.Service.Security
{
    using Doamin.Service.Users;
    using Domain.Model.Security;
    using Domain.Model.Users;
    using EasyErp.Core;
    using Infrastructure.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Permission service
    /// </summary>
    public class PermissionService : IPermissionService
    {
        private readonly IRepository<PermissionRecord> permissionRecordRepository;

        private readonly IUnitOfWork unitOfWork;

        private readonly IUserService userService;

        private readonly IWorkContext workContext;

        public PermissionService(
            IRepository<PermissionRecord> permissionRecordRepository,
            IUnitOfWork unitOfWork,
            IWorkContext workContext,
            IUserService userService)
        {
            this.permissionRecordRepository = permissionRecordRepository;
            this.unitOfWork = unitOfWork;
            this.workContext = workContext;
            this.userService = userService;
        }

        public void DeletePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException("permission");
            }
            permissionRecordRepository.Remove(permission);
            unitOfWork.Commit();
        }

        public PermissionRecord GetPermissionRecordById(int permissionId)
        {
            if (permissionId <= 0)
            {
                return null;
            }

            return permissionRecordRepository.GetByKey(permissionId);
        }

        public PermissionRecord GetPermissionRecordBySystemName(string systemName)
        {
            if (string.IsNullOrEmpty(systemName))
            {
                return null;
            }

            var records = permissionRecordRepository.FindAll(r => r.SystemName == systemName);
            return records.FirstOrDefault();
        }

        public IList<PermissionRecord> GetAllPermissionRecords()
        {
            return permissionRecordRepository.FindAll(r => r.Id > 0).ToList();
        }

        public void InsertPermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException("permission");
            }

            permissionRecordRepository.Add(permission);
            unitOfWork.Commit();
        }

        public void UpdatePermissionRecord(PermissionRecord permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException("permission");
            }

            permissionRecordRepository.Update(permission);
            unitOfWork.Commit();
        }

        public virtual void InstallPermissions(IPermissionProvider permissionProvider)
        {
            //install new permissions
            var permissions = permissionProvider.GetPermissions();
            foreach (var permission in permissions)
            {
                var permission1 = GetPermissionRecordBySystemName(permission.SystemName);
                if (permission1 != null)
                {
                    continue;
                }

                //new permission (install it)
                permission1 = new PermissionRecord
                {
                    Name = permission.Name,
                    SystemName = permission.SystemName,
                    Category = permission.Category
                };

                //default customer role mappings
                var defaultPermissions = permissionProvider.GetDefaultPermissions();
                foreach (var defaultPermission in defaultPermissions)
                {
                    var userRole = userService.GetUserRoleBySystemName(defaultPermission.UserRoleSystemName);
                    if (userRole == null)
                    {
                        //new role (save it)
                        userRole = new UserRole
                        {
                            Name = defaultPermission.UserRoleSystemName,
                            Active = true,
                            SystemName = defaultPermission.UserRoleSystemName
                        };
                        userService.InsertUserRole(userRole);
                    }

                    var defaultMappingProvided = (from p in defaultPermission.PermissionRecords
                                                  where p.SystemName == permission1.SystemName
                                                  select p).Any();
                    var mappingExists = (from p in userRole.PermissionRecords
                                         where p.SystemName == permission1.SystemName
                                         select p).Any();
                    if (defaultMappingProvided && !mappingExists)
                    {
                        permission1.CustomerRoles.Add(userRole);
                    }
                }

                //save new permission
                InsertPermissionRecord(permission1);
            }
        }

        public bool Authorize(PermissionRecord permission)
        {
            return Authorize(permission, workContext.CurrentUser);
        }

        public bool Authorize(PermissionRecord permission, User user)
        {
            if (permission == null)
            {
                return false;
            }

            if (user == null)
            {
                return false;
            }

            return Authorize(permission.SystemName, user);
        }

        public bool Authorize(string permissionRecordSystemName)
        {
            return Authorize(permissionRecordSystemName, workContext.CurrentUser);
        }

        public bool Authorize(string permissionRecordSystemName, User user)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
            {
                return false;
            }

            if (user == null)
            {
                return false;
            }

            var customerRoles = user.UserRoles.Where(cr => cr.Active);
            foreach (var role in customerRoles)
            {
                if (Authorize(permissionRecordSystemName, role))
                {
                    return true;
                }
            }

            //no permission found
            return false;
        }

        protected virtual bool Authorize(string permissionRecordSystemName, UserRole userRole)
        {
            if (String.IsNullOrEmpty(permissionRecordSystemName))
            {
                return false;
            }

            foreach (var permission1 in userRole.PermissionRecords)
            {
                if (permission1.SystemName.Equals(
                    permissionRecordSystemName,
                    StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}