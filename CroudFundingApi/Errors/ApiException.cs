namespace CroudFundingApi.Errors
{
    public class ApiException : ApiResponse
    {
        public ApiException(int statusCode, string message = null,string details=null) : base(statusCode, message)
        {
            _details = details;
        }

        public string _details { get; set; }
    }
}
