using App.Core;
using App.Core.Domain.Users;
using App.Services.Events;
using App.Services.Security;
using System;

namespace App.Services.Users
{
    /// <summary>
    /// Customer registration service
    /// </summary>
    public partial class UserRegistrationService : IUserRegistrationService
    {
        #region Fields

        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;
        private readonly IEventPublisher _eventPublisher;
        private readonly IWorkContext _workContext;


        #endregion

        #region Ctor

        public UserRegistrationService(IUserService customerService,
            IEncryptionService encryptionService,
            IEventPublisher eventPublisher,
            IWorkContext workContext
            )
        {
            _userService = customerService;
            _encryptionService = encryptionService;
            _eventPublisher = eventPublisher;
            _workContext = workContext;
        }

        #endregion

        #region Utilities

        #endregion

        #region Methods

        /// <summary>
        /// Validate customer
        /// </summary>
        /// <param name="usernameOrEmail">Username or email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        public virtual UserLoginResults ValidateUser(string usernameOrEmail, string password)
        {
            var user = _userService.GetUserByUsername(usernameOrEmail);

            if (user == null)
                return UserLoginResults.UserNotExist;
            if (user.Deleted)
                return UserLoginResults.Deleted;
            if (!user.Active)
                return UserLoginResults.NotActive;
            
            //check whether a customer is locked out
            if (user.CannotLoginUntilDateUtc.HasValue && user.CannotLoginUntilDateUtc.Value > DateTime.UtcNow)
                return UserLoginResults.LockedOut;

            string pwd = _encryptionService.EncryptText(password);
            if (pwd != user.Password)
            {
                //wrong password
                //user.FailedLoginAttempts++;
                //if (_customerSettings.FailedPasswordAllowedAttempts > 0 &&
                //    user.FailedLoginAttempts >= _customerSettings.FailedPasswordAllowedAttempts)
                //{
                //    //lock out
                //    user.CannotLoginUntilDateUtc = DateTime.UtcNow.AddMinutes(_customerSettings.FailedPasswordLockoutMinutes);
                //    //reset the counter
                //    user.FailedLoginAttempts = 0;
                //}

                _userService.UpdateUser(user);

                return UserLoginResults.WrongPassword;
            }

            //update login details
            user.FailedLoginAttempts = 0;
            user.CannotLoginUntilDateUtc = null;
            user.RequireReLogin = false;
            user.LastLoginDateUtc = DateTime.UtcNow;
            _userService.UpdateUser(user);

            return UserLoginResults.Successful;
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Result</returns>
        public virtual ChangePasswordResult ChangePassword(ChangePasswordRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var result = new ChangePasswordResult();
            if (string.IsNullOrWhiteSpace(request.Email))
            {
                result.AddError("EmailIsNotProvided");
                return result;
            }

            if (string.IsNullOrWhiteSpace(request.NewPassword))
            {
                result.AddError("PasswordIsNotProvided");
                return result;
            }

            var user = _userService.GetUserByEmail(request.Email);
            if (user == null)
            {
                result.AddError("EmailNotFound");
                return result;
            }

            //request isn't valid
            if (request.ValidateRequest && _encryptionService.EncryptText(request.OldPassword) == user.Password)
            {
                result.AddError("OldPasswordDoesntMatch");
                return result;
            }

            user.Password = _encryptionService.EncryptText(request.NewPassword);

            _userService.UpdateUser(user);

            //publish event
            //_eventPublisher.Publish(new CustomerPasswordChangedEvent(customerPassword));

            return result;
        }

        #endregion
    }
}
