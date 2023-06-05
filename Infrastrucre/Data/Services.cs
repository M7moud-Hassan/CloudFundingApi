using CroudFundingApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Runtime.CompilerServices;

namespace Infrastructure.Data
{
    public class Services : IServices
    {
        private readonly IConfiguration _configuration;
        public Services(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendRegistrationEmailAsync(string userEmail,string token)
        {
            var fromAddress = new MailAddress(_configuration["EmailSettings:FromAddress"], _configuration["EmailSettings:Name"]);
            var toAddress = new MailAddress(userEmail);
            var fromPassword = _configuration["EmailSettings:Password"];
            
            var body = "\r\n<!DOCTYPE html>\r\n<html>\r\n  <head>\r\n    <title>Please confirm your e-mail</title>\r\n    <meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <style type=\"text/css\">\r\n      body,table,td,a{\r\n      -webkit-text-size-adjust:100%;\r\n      -ms-text-size-adjust:100%;\r\n      }\r\n      table,td{\r\n      mso-table-lspace:0pt;\r\n      mso-table-rspace:0pt;\r\n      }\r\n      img{\r\n      -ms-interpolation-mode:bicubic;\r\n      }\r\n      img{\r\n      border:0;\r\n      height:auto;\r\n      line-height:100%;\r\n      outline:none;\r\n      text-decoration:none;\r\n      }\r\n      table{\r\n      border-collapse:collapse !important;\r\n      }\r\n      body{\r\n      height:100% !important;\r\n      margin:0 !important;\r\n      padding:0 !important;\r\n      width:100% !important;\r\n      }\r\n      a[x-apple-data-detectors]{\r\n      color:inherit !important;\r\n      text-decoration:none !important;\r\n      font-size:inherit !important;\r\n      font-family:inherit !important;\r\n      font-weight:inherit !important;\r\n      line-height:inherit !important;\r\n      }\r\n      a{\r\n      color:#00bc87;\r\n      text-decoration:underline;\r\n      }\r\n      * img[tabindex=0]+div{\r\n      display:none !important;\r\n      }\r\n      @media screen and (max-width:350px){\r\n      h1{\r\n      font-size:24px !important;\r\n      line-height:24px !important;\r\n      }\r\n      }   div[style*=margin: 16px 0;]{\r\n      margin:0 !important;\r\n      }\r\n      @media screen and (min-width: 360px){\r\n      .headingMobile {\r\n      font-size: 40px !important;\r\n      }\r\n      .headingMobileSmall {\r\n      font-size: 28px !important;\r\n      }\r\n      }\r\n    </style>\r\n  </head>\r\n  <body bgcolor=\"#ffffff\" style=\"background-color: #ffffff; margin: 0 !important; padding: 0 !important;\">\r\n    <div style=\"display: none; font-size: 1px; color: #fefefe; line-height: 1px; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; max-height: 0px; max-width: 0px; opacity: 0; overflow: hidden;\"> - to finish signing up, you just need to confirm that we got your e-mail right within 48 hours. To confirm please click the VERIFY button.</div>\r\n    <center>\r\n      <table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" valign=\"top\">\r\n        <tbody>\r\n          <tr>\r\n            <td>\r\n              <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" align=\"center\" valign=\"top\" bgcolor=\"#ffffff\" style=\"padding: 0 20px !important;max-width: 500px;width: 90%;\">\r\n                <tbody>\r\n                  <tr>\r\n                    <td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 10px 0 0px 0;\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"350\">\r\n<tr>\r\n<td align=\"center\" valign=\"top\" width=\"350\">\r\n<![endif]-->\r\n                      <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 500px;border-bottom: 1px solid #e4e4e4 ;\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td bgcolor=\"#ffffff\" align=\"left\" valign=\"middle\" style=\"padding: 0px; color: #111111; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; line-height: 62px;padding:0 0 15px 0;\"><a href=\"https://app.croundFunding.com\" target=\"_blank\"><img width=\"19\" height=\"25\" alt=\"logo\" src=\"https://s3-eu-west-1.amazonaws.com/avocode-mailing/mailing-app/img/logo.png\"></a></td>\r\n                            <td bgcolor=\"#ffffff\" align=\"right\" valign=\"middle\" style=\"padding: 0px; color: #111111; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; line-height: 48px;padding:0 0 15px 0;\"><a href=\"https://app.croundFunding.com/login/\" target=\"_blank\" style=\"font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif;color: #797979;font-size: 12px;font-weight:400;-webkit-font-smoothing:antialiased;text-decoration: none;\">Login to croundFunding.com</a></td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table><!--[if (gte mso 9)|(IE)]></td></tr></table>\r\n<![endif]-->\r\n                    </td>\r\n                  </tr>\r\n                  <tr>\r\n                    <td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 0;\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"350\">\r\n<tr>\r\n<td align=\"center\" valign=\"top\" width=\"350\">\r\n<![endif]-->\r\n                      <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 500px;border-bottom: 1px solid #e4e4e4;\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td bgcolor=\"#ffffff\" align=\"left\" style=\"padding: 20px 0 0 0; color: #666666; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400;-webkit-font-smoothing:antialiased;\">\r\n                                                <p class=\"headingMobile\" style=\"margin: 0;color: #171717;font-size: 26px;font-weight: 200;line-height: 130%;margin-bottom:5px;\">Verify your e-mail to finish signing up for croundFunding</p>\r\n                            </td>\r\n                          </tr>\r\n                                            <tr>\r\n                                              <td height=\"20\"></td>\r\n                                            </tr>\r\n                          <tr>\r\n                            <td bgcolor=\"#ffffff\" align=\"left\" style=\"padding:0; color: #666666; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400;-webkit-font-smoothing:antialiased;\">\r\n                                                <p style=\"margin:0;color:#585858;font-size:14px;font-weight:400;line-height:170%;\">Thank you for choosing croundFunding.</p>\r\n                                                <p style=\"margin:0;margin-top:20px;line-height:0;\"></p>\r\n                                                <p style=\"margin:0;color:#585858;font-size:14px;font-weight:400;line-height:170%;\">Please confirm that <b>" + userEmail+"</b> is your e-mail address by clicking on the button below or use this link <a style='color: #00bc87;text-decoration: underline;' target='_blank' href='https://croundFunding.com/'>https://croundFunding.com/confirm-email/?token="+token+"</a> within 48&nbsp;hours.</p>\r\n                            </td>\r\n                          </tr>\r\n                                            <tr>\r\n                                              <td align=\"center\">\r\n                                                <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">\r\n                                                  <tr>\r\n                                                    <td align=\"center\" style=\"padding: 33px 0 33px 0;\">\r\n                                                      <table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"100%\">\r\n                                                        <tr>\r\n                                                          <td align=\"center\" style=\"border-radius: 4px;\" bgcolor=\"#00bc87\"><a href=\"https://croundFunding.com/confirmEmail/?token="+token+"\" style=\"text-transform:uppercase;background:#00bc87;font-size: 13px; font-weight: 700; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none !important; padding: 20px 25px; border-radius: 4px; border: 1px solid #00bc87; display: block;-webkit-font-smoothing:antialiased;\" target=\"_blank\"><span style=\"color: #ffffff;text-decoration: none;\">Verify</span></a></td>\r\n                                                        </tr>\r\n                                                      </table>\r\n                                                    </td>\r\n                                                  </tr>\r\n                                                </table>\r\n                                              </td>\r\n                                            </tr>\r\n                        </tbody>\r\n                      </table><!--[if (gte mso 9)|(IE)]></td></tr></table>\r\n<![endif]-->\r\n                    </td>\r\n                  </tr>\r\n                  <tr>\r\n                    <td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 0;\"><!--[if (gte mso 9)|(IE)]><table align=\"center\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" width=\"350\">\r\n<tr>\r\n<td align=\"center\" valign=\"top\" width=\"350\">\r\n<![endif]-->\r\n                      <table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"max-width: 500px;\">\r\n                        <tbody>\r\n                          <tr>\r\n                            <td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 30px 0 30px 0; color: #666666; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 18px;\">\r\n                                                <p style=\"margin: 0;color: #585858;font-size: 12px;font-weight: 400;-webkit-font-smoothing:antialiased;line-height: 170%;\">Need help? Ask at <a href=\"mailto:team@croundFunding.com\" style=\"color: #00bc87;text-decoration: underline;\" target=\"_blank\">team@croundFunding.com</a> or visit our <a href=\"https://help.croundFunding.com/en/\" style=\"color: #00bc87;text-decoration: underline;\" target=\"_blank\">Help Center</a></p>\r\n                                                <tr>\r\n                                                  <td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 0; color: #666666; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 18px;\">\r\n                                                    <p style=\"margin: 0;color: #585858;font-size: 12px;font-weight: 400;-webkit-font-smoothing:antialiased;line-height: 170%;\"></p>\r\n                                                  </td>\r\n                                                </tr>\r\n                                                <tr>\r\n                                                  <td bgcolor=\"#ffffff\" align=\"center\" style=\"padding: 15px 0 30px 0; color: #666666; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; font-size: 16px; font-weight: 400; line-height: 18px;\">\r\n                                                    <p style=\"margin: 0;color: #585858;font-size: 12px;font-weight: 400;-webkit-font-smoothing:antialiased;line-height: 170%;\">croundFunding, Inc.<br> 330 East 59th Street, 7th Floor<br> New York, NY 10022, USA</p>\r\n                                                  </td>\r\n                                                </tr>\r\n                            </td>\r\n                          </tr>\r\n                        </tbody>\r\n                      </table><!--[if (gte mso 9)|(IE)]></td></tr></table>\r\n<![endif]-->\r\n                    </td>\r\n                  </tr>\r\n                </tbody>\r\n              </table>\r\n            </td>\r\n          </tr>\r\n        </tbody>\r\n      </table>\r\n    </center>\r\n\r\n  \r\n  </body>\r\n</html>\r\n";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "verfy yout email to croudfunding website",
                Body = body,
                IsBodyHtml = true
            })
            {
                await smtp.SendMailAsync(message);
            }
        }
       
        public string GenerateEmailVerificationToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["EmailSettings:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Email, email)
            }),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encodedToken = tokenHandler.WriteToken(token);

            return encodedToken;
        }

        public bool ValidateEmailVerificationToken(string token, out string email)
        {

            email = null;

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["EmailSettings:SecretKey"]);

                // Verify token
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);

                // Access claims from the validated token
                email = principal.FindFirst(ClaimTypes.Email)?.Value;

                return true;
            }
            catch
            {
                return false;
            }

           
        }
    }
}
