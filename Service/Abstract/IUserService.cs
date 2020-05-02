using ECommerceCMS.Models;
using ECommerceCMS.Services;
using System.Collections.Generic;

namespace ECommerceCMS.Service.Abstract
{
    public interface IUserService
    {
        ServiceResult<List<UserViewModel>> FetchAllUsers();
        ServiceResult<UserViewModel> CreateOrEdit(UserViewModel model);
        ServiceResult<UserViewModel> DeleteUserById(int id);
        ServiceResult<UserViewModel> ChangePassword(UserViewModel model);
        ServiceResult<UserViewModel> ForgotPasswordWithEmail(string email);
        ServiceResult<UserViewModel> Login(UserViewModel model);
        ServiceResult<LoginViewModel> ForgotPasswordSendEmail(string email);
        ServiceResult<LoginViewModel> ChangePasswordWithCode(UserViewModel model);


    }
}
