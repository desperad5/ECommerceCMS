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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private static readonly ILog logger = Logger.GetLogger(typeof(CustomerService));

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public ServiceResult<LoginViewModel> ForgotPasswordSendEmail(string email)
        {
            var user = _customerRepository.GetSingle(cu => cu.EmailAddress == email);

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
                _customerRepository.Update(user);
                var url = "http://localhost:4200/auth/change-password" + "?code=" + user.ForgotPasswordToken + "&" + "email=" + email;
                var content = "<html><body>Üyeliğinizi tamamlamak için <a href='" + url + "'>linke</a> tıklayınız. </body></html>";

                MailSender.SendMail(email, "ETicaret Paketi Şifremi Unuttum", content);

                result.resultType = ServiceResultType.Success;
                _customerRepository.Commit();
            }
            catch (Exception e)
            {
                logger.Error("Error@ForgotPasswordSendEmail: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }

            return result;
        }


        public ServiceResult<LoginViewModel> ChangePasswordWithCode(LoginViewModel model)
        {
            var user = _customerRepository.GetSingle(cu => cu.EmailAddress == model.EmailAddress);

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
            string hashedPassword = HashCalculator.HashPasswordWithSalt(model.Password, salt);
            user.Password = hashedPassword;
            user.PasswordSalt = salt;
            user.ForgotPasswordToken = null;
            var result = new ServiceResult<LoginViewModel>();
            try
            {
                _customerRepository.Update(user);
                _customerRepository.Commit();
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

        public ServiceResult<LoginViewModel> ChangePassword(LoginViewModel model)
        {
            var user = _customerRepository.GetSingle(cu => cu.EmailAddress == model.EmailAddress);

            if (user == null)
                return new ServiceResult<LoginViewModel>()
                {
                    resultType = ServiceResultType.Fail,
                    message = "Invalid User."
                };
            string salt = HashCalculator.GenerateSalt();
            string hashedPassword = HashCalculator.HashPasswordWithSalt(model.Password, salt);
            user.Password = hashedPassword;
            user.PasswordSalt = salt;
            user.ForgotPasswordToken = null;
            var result = new ServiceResult<LoginViewModel>();
            try
            {
                _customerRepository.Update(user);
                _customerRepository.Commit();
                result.resultType = ServiceResultType.Success;
            }
            catch (Exception e)
            {
                logger.Error("Error@ChangePassword: ", e);
                result.resultType = ServiceResultType.Fail;
                result.message = e.ToString();
            }
            return result;

        }
    }
}
