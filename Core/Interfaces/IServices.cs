namespace CroudFundingApi.Services
{
    public interface IServices
    {
        public Task<bool> SendEmailConfirmAsync(string userEmail,string body);
        public string GenerateEmailVerificationToken(string email);
        public bool ValidateEmailVerificationToken(string token,out string email);
    }

}
