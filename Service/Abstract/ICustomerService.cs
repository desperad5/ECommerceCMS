using ECommerceCMS.Models;
using ECommerceCMS.Services;
using System.Collections.Generic;

namespace ECommerceCMS.Service.Abstract
{
    public interface ICustomerService
    {
        ServiceResult<LoginViewModel> ForgotPasswordSendEmail(string email);
        ServiceResult<LoginViewModel> ChangePasswordWithCode(LoginViewModel model);
        ServiceResult<LoginViewModel> ChangePassword(LoginViewModel model);


    }
}
