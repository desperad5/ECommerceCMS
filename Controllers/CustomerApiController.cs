using ECommerceCMS.Data;
using Microsoft.AspNetCore.Mvc;
using ECommerceCMS.Models.Api;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ECommerceCMS.Helpers;
using ECommerceCMS.Service.Abstract;
using ECommerceCMS.Services;
using ECommerceCMS.Data.Entity;

namespace ECommerceCMS.Controllers
{

    [Route("api/[controller]")]
    public class CustomerApiController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly ICustomerService _customerService;

        public CustomerApiController(ApplicationDbContext db, ICustomerService customerService)
        {
            _db = db;
            _customerService = customerService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> LoginWithSocialMedia([FromBody] SocialLoginViewModel userdata)
        {

            if (!ModelState.IsValid)
                return BadRequest(new JsonResult(ModelState.Values.First().Errors));

            var alreadySavedData = _db.Customers.Where(Uid => Uid.SocialAuthId == userdata.UserId).FirstOrDefault();
            JwtSecurityToken token;
            if (alreadySavedData != null)
            {
                token = CreateJwt(alreadySavedData);

                return Ok(new
                {
                    id = alreadySavedData.Id,
                    message = "User data has already been saved",
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    username = alreadySavedData.FirstName,
                    pictureUrl = alreadySavedData.PictureUrl,
                    userRole = "user",
                });
            }

            var user = new Customer
            {
                SocialAuthId = userdata.UserId,
                FirstName = userdata.FirstName,
                LastName = userdata.LastName,
                PictureUrl = userdata.PictureUrl,
                EmailAddress = userdata.EmailAddress,
                Provider = userdata.Provider
            };

            await _db.AddAsync(user);

            await _db.SaveChangesAsync();
            token = CreateJwt(user);

            return Ok(new
            {
                id = user.Id,
                message = "User Login successful",
                username = user.FirstName,
                pictureUrl = user.PictureUrl,
                userRole = "user",
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });


        }

        private static JwtSecurityToken CreateJwt(Customer user)
        {

            var claims = new[]{
                        new Claim(JwtRegisteredClaimNames.Sub, user.FirstName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim("ApplicationName","SoruBankFE" )
                    };


            var loginKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"));

            var token = new JwtSecurityToken(
                issuer: "Antasya",
                audience: "Antasya",
                expires: DateTime.UtcNow.AddDays(1),
                claims: claims,
                signingCredentials: new SigningCredentials(loginKey, SecurityAlgorithms.HmacSha256)
                );
            return token;
        }

        [HttpPost("[action]")]

        public ActionResult LoginWithEmail([FromBody] Models.LoginViewModel userdata)
        {
            var user = _db.Customers.Where(u => u.EmailAddress == userdata.EmailAddress && u.IsDeleted == false).FirstOrDefault();

            if (user == null)
                return BadRequest("User not found!");

            string salt = user.PasswordSalt;
            string hashedPassword = HashCalculator.HashPasswordWithSalt(userdata.Password, salt);
            if (user.Password != hashedPassword)
                return BadRequest("Invalid user combination.");

            JwtSecurityToken token = CreateJwt(user);

            return Ok(new
            {
                id = user.Id,
                message = "User data has already been saved",
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                username = user.FirstName,
                pictureUrl = user.PictureUrl,
                userRole = "user",
            });


        }

        [HttpPost("[action]")]

        public ActionResult CheckRegistrationCode([FromBody]RegistrationRequestModel model)
        {
            var lead = _db.Leads.Any(l => l.Email == model.Email && l.Code == model.Code && l.HasRegistered == false);
            if (lead)
                return Ok(model);
            return BadRequest("Geçersiz mail ve kod.");

        }


        [HttpPost("[action]")]

        public ActionResult SaveUser([FromBody]RegistrationRequestModel model)
        {

            var user = _db.Customers.Where(u => u.EmailAddress == model.Email).FirstOrDefault();
            if (user != null)
                return BadRequest(model.Email + "'e ait kullanıcı mevcut. Lütfen şifremi unuttum alanına gidiniz.");

            var lead = _db.Leads.Where(l => l.Email == model.Email && l.Code == model.Code && l.HasRegistered == false).FirstOrDefault();

            if (lead != null)
            {
                try
                {
                    string salt = HashCalculator.GenerateSalt();
                    string hashedPassword = HashCalculator.HashPasswordWithSalt(model.Password, salt);
                    var newUser = new Customer()
                    {
                        Password = hashedPassword,
                        PasswordSalt = salt,
                        EmailAddress = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName

                    };
                    _db.Customers.Add(newUser);

                    lead.HasRegistered = true;
                    _db.Leads.Update(lead);
                    _db.SaveChanges();
                    return Ok(model);

                }
                catch (Exception e)
                {
                    return BadRequest(e.ToString());

                    throw;
                }

            }
            return BadRequest("Geçersiz mail ve kod.");


        }


        [HttpPost("[action]")]

        public ActionResult SendCodeToEmailAddress([FromBody]RegistrationRequestModel model)
        {
            Lead lead;
            lead = _db.Leads.Where(l => l.Email == model.Email).FirstOrDefault();
            if (lead != null)
            {
                if (lead.HasRegistered)
                    return BadRequest("Sistemde üyeliğiniz var.");
            }
            else
            {
                try
                {
                    lead = new Lead()
                    {
                        Code = Guid.NewGuid().ToString(),
                        Email = model.Email
                    };
                    _db.Leads.Add(lead);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    return BadRequest("Databasede hata oluştu.");
                }
            }
            var url = "http://localhost:4200/auth/sign-up" + "?code=" + lead.Code + "&" + "email=" + lead.Email;
            var content = "<html><body>Üyeliğinizi tamamlamak için <a href='" + url + "'>linke</a> tıklayınız. </body></html>";
            MailSender.SendMail(lead.Email, "Sorubank Üyelik Aktivasyonu", content);
            return Ok("Mailiniz tarafımıza yönlendirildi.");
        }


        [HttpPost("[action]")]

        public ActionResult ForgotPasswordSendEmail([FromBody]RegistrationRequestModel model)
        {

            var result = _customerService.ForgotPasswordSendEmail(model.Email);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);

        }

        [HttpPost("[action]")]

        public ActionResult ChangePasswordWithCode([FromBody]ECommerceCMS.Models.LoginViewModel model)
        {

            var result = _customerService.ChangePasswordWithCode(model);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);

        }

        [HttpPost("[action]")]

        public ActionResult ChangePassword([FromBody]ECommerceCMS.Models.LoginViewModel model)
        {
            var result = _customerService.ChangePassword(model);

            if (result.resultType == ServiceResultType.Success)
                return Ok(result.data);

            return BadRequest(result.message);

        }

    }
}
