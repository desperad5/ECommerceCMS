using SorubankCMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using SorubankCMS.Service.Abstract;
using SorubankCMS.Services;
using SorubankCMS.Models;
using SorubankCMS.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System;
using AutoMapper;

namespace SorubankCMS.Controllers
{

    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(ApplicationDbContext db, IUserService userService,IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("[action]")]
        public IActionResult FetchAllUsers()
        {
            var result = _userService.FetchAllUsers();

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult CreateOrEdit([FromBody] UserViewModel user)
        {
            var result = _userService.CreateOrEdit(user);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult ChangePassword([FromBody] UserViewModel user)
        {
            var result = _userService.ChangePassword(user);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult DeleteUserById([FromBody] int id)
        {
            var result = _userService.DeleteUserById(id);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);
        }

        private static JwtSecurityToken CreateJwt(CMSUser user)
        {
            var tenantId = user.TenantId.HasValue ? user.TenantId.ToString() : string.Empty;
            
            var claims = new[]{
                        new Claim("TenantId",tenantId),
                        new Claim("UserId",user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.Id.ToString() ),
                        new Claim("ApplicationName","ECommerceCMS" )
                    };


            var loginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

            var token = new JwtSecurityToken(
                issuer: "ECommerceTeam",
                audience: "ECommerceTeam",
                expires: DateTime.UtcNow.AddYears(1),
                claims: claims,
                signingCredentials: new SigningCredentials(loginKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }
        [HttpPost("[action]")]
        public ActionResult Login([FromBody] UserViewModel user)
        {
            var result = _userService.Login(user);
            

            if (result.resultType == ServiceResultType.Success)
            {
                var cmsUser = _mapper.Map<CMSUser>(result.data);
                var token = CreateJwt(cmsUser);

                return Ok(new
                {
                    id = user.Id,
                    isAdmin=cmsUser.TenantId==0,
                    message = "User Login successful",
                    username = user.UserName,
                    pictureUrl = user.PictureUrl,
                    userRole = "user",
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }

            return BadRequest(result.message);
        }

        [HttpPost("[action]")]
        public ActionResult ForgotPasswordSendEmail([FromBody]UserViewModel model)
        {

            var result = _userService.ForgotPasswordSendEmail(model.EmailAddress);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);

        }

        [HttpPost("[action]")]

        public ActionResult ChangePasswordWithCode([FromBody]UserViewModel model)
        {

            var result = _userService.ChangePasswordWithCode(model);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);

        }
    }
}
