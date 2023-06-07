namespace CroudFundingApi.Dto
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string phone { get; set; }
        public IFormFile imageFile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
       
    }
}
