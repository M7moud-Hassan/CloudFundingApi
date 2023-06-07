namespace CroudFundingApi.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode,String message=null)
        {
            _statusCode = statusCode;
            _message = message??GetDeffultMessageForStatusCode(statusCode);
            
        }

        public int _statusCode { get; set; }
        public string _message { get; set; }
        private string GetDeffultMessageForStatusCode( int statusCode)
        {
            return statusCode switch
            {
                400 => "bad Request",
                404 => "not found",
                500 => "server error",
                200 => "ok",
                _ => null
            };
        }
    }
}
