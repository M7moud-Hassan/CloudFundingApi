namespace CroudFundingApi.Services
{
    public interface IServices
    {
        public Task SendRegistrationEmailAsync(string userEmail,string token);
        public string GenerateEmailVerificationToken(string email);
        public bool ValidateEmailVerificationToken(string token,out string email);
    }

}
