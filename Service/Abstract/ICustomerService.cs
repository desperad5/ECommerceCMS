using SorubankCMS.Models;
using SorubankCMS.Services;
using System.Collections.Generic;

namespace SorubankCMS.Service.Abstract
{
    public interface ICustomerService
    {
        ServiceResult<LoginViewModel> ForgotPasswordSendEmail(string email);
        ServiceResult<LoginViewModel> ChangePasswordWithCode(LoginViewModel model);
        ServiceResult<LoginViewModel> ChangePassword(LoginViewModel model);


    }
}
