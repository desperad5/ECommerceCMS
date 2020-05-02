using AutoMapper;
using log4net;
using ECommerceCMS.Data.Abstract;
using ECommerceCMS.Data.Entity;
using ECommerceCMS.Helpers;
using ECommerceCMS.Models;
using ECommerceCMS.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceCMS.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(UserService));

        public UserService(IUserRepository userRepository, ITenantRepository tenantRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _tenantRepository = tenantRepository;
            _mapper = mapper;
        }

        public ServiceResult<List<UserViewModel>> FetchAllUsers()
        {
            ServiceResult<List<UserViewModel>> result = new ServiceResult<List<UserViewModel>>();
            try
            {
                var users = _userRepository
                    .AllIncluding(u => u.Tenant)
                    .Where(a => a.IsDeleted == false && a.Tenant != null)
                    .Select(u => { u.Password = null; return u; })
                    .ToList();

                result.data = _mapper.Map<List<UserViewModel>>(users);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@FetchAllUsers: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }
        public ServiceResult<UserViewModel> CreateOrEdit(UserViewModel model)
        {
            ServiceResult<UserViewModel> result = new ServiceResult<UserViewModel>();

            if (model.Tenant?.Id == 0)
            {
                result.message = "Tenant is required.";
                result.resultType = ServiceResultType.Fail;
                return result;
            }

            try
            {
                var tenantEntity = _tenantRepository.GetSingle(t => t.Id == model.TenantId);

                if (model.Id > 0)
                {
                    var userEntity = _userRepository.GetSingle(a => a.Id == model.Id);
                    userEntity.Name = model.Name;
                    userEntity.Surname = model.Surname;
                    userEntity.EmailAddress = model.EmailAddress;
                    userEntity.TenantId = model.TenantId;
                    _userRepository.Update(userEntity);
                    userEntity.Tenant = tenantEntity;
                    result.data = _mapper.Map<UserViewModel>(userEntity);

                }
                else
                {
                    string salt = HashCalculator.GenerateSalt();
                    string password = HashCalculator.HashPasswordWithSalt(model.Password, salt);
                    var user = _mapper.Map<CMSUser>(model);
                    user.PasswordSalt = salt;
                    user.Password = password;
                    var userEntity = _userRepository.AddWithCommit(user);
                    userEntity.Tenant = tenantEntity;
                    result.data = _mapper.Map<UserViewModel>(userEntity);
                }
                result.resultType = ServiceResultType.Success;
                _userRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@CreateOrEdit: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }

            return result;

        }


        public ServiceResult<UserViewModel> DeleteUserById(int id)
        {
            ServiceResult<UserViewModel> result = new ServiceResult<UserViewModel>();

            try
            {
                var userEntity = _userRepository.FindBy(a => a.Id == id).FirstOrDefault();
                userEntity.IsDeleted = true;
                _userRepository.Update(userEntity);
                result.data = _mapper.Map<UserViewModel>(userEntity);
                result.resultType = ServiceResultType.Success;
                _userRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@DeleteUserById: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;
            }
            return result;

        }

        public ServiceResult<UserViewModel> ChangePassword(UserViewModel model)
        {
            ServiceResult<UserViewModel> result = new ServiceResult<UserViewModel>();
            try
            {
                var user = _userRepository.GetSingle(u => u.Id == model.Id);
                string salt = HashCalculator.GenerateSalt();
                string password = HashCalculator.HashPasswordWithSalt(model.Password, salt);
                user.Password = password;
                user.PasswordSalt = salt;
                _userRepository.Update(user);
                _userRepository.Commit();
                result.data = _mapper.Map<UserViewModel>(user);
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@ChangePassword: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;

            }

            return result;

        }

        public ServiceResult<UserViewModel> ForgotPasswordWithEmail(string email)
        {
            throw new NotImplementedException();
        }

        public ServiceResult<UserViewModel> Login(UserViewModel model)
        {
            ServiceResult<UserViewModel> result = new ServiceResult<UserViewModel>();
            try
            {
                var user = _userRepository.GetSingle(u => u.EmailAddress == model.EmailAddress);

                if (user != null)
                {
                    string hashedPassword = HashCalculator.HashPasswordWithSalt(model.Password, user.PasswordSalt);
                    if (hashedPassword == user.Password)
                    {
                        result.resultType = ServiceResultType.Success;
                        result.data = _mapper.Map<UserViewModel>(user);
                    }
                    else
                    {
                        result.resultType = ServiceResultType.Fail;
                        result.message = "INVALID_COMBINATION";
                    }

                }
                else
                {
                    result.resultType = ServiceResultType.Fail;
                    result.message = "INVALID_COMBINATION";
                }

            }
            catch (Exception e)
            {
                logger.Error("Error@Login: ", e);
                result.message = e.ToString();
                result.resultType = ServiceResultType.Fail;

            }

            return result;

        }

        public ServiceResult<LoginViewModel> ForgotPasswordSendEmail(string email)
        {
            var user = _userRepository.GetSingle(cu => cu.EmailAddress == email);

            if (user == null)
                return new ServiceResult<LoginViewModel>()
                {
                    resultType = ServiceResultType.Fail,
                    message = "Maile ait kullanıcı mevcut değil."
                };

            user.ForgotPasswordToken = Guid.NewGuid().ToString();
            var result = new ServiceResult<LoginViewModel>();
            try
            {
                _userRepository.Update(user);
                var url = "localhost:5486/auth/change-password?" + "code=" + user.ForgotPasswordToken + "&" + "email=" + email;
                var content = "<html><body>Üyeliğinizi tamamlamak için <a href='" + url + "'>linke</a> tıklayınız." + url + "</body></html>";

                MailSender.SendMail(email, "ETicaret Paketi CMS Şifremi Unuttum", content);

                result.resultType = ServiceResultType.Success;
                _userRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@ForgotPasswordSendEmail: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }

        public ServiceResult<LoginViewModel> ChangePasswordWithCode(UserViewModel model)
        {
            var user = _userRepository.GetSingle(cu => cu.EmailAddress == model.EmailAddress);

            if (user == null)
                return new ServiceResult<LoginViewModel>()
                {
                    resultType = ServiceResultType.Fail,
                    message = "Invalid User."
                };

            if (model.Code != user.ForgotPasswordToken)
                return new ServiceResult<LoginViewModel>()
                {
                    resultType = ServiceResultType.Fail,
                    message = "Invalid Code."
                };
            string salt = HashCalculator.GenerateSalt();
            string password = HashCalculator.HashPasswordWithSalt(model.Password, salt);
            user.Password = password;
            user.PasswordSalt = salt;
            user.ForgotPasswordToken = null;
            var result = new ServiceResult<LoginViewModel>();
            try
            {
                _userRepository.Update(user);
                _userRepository.Commit();
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@ChangePasswordWithCode: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;

        }


    }
}
