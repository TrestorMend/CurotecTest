namespace CurotecTest.Controllers.Base
{
    public class RequestResult
    {
        public List<RequestResultErrorItem> Errors { get; set; }
        public object? Data { get; private set; }
        public bool Success { get; set; }

        public RequestResult()
        {
            Errors = new List<RequestResultErrorItem>();
        }

        public RequestResult(Exception ex)
        {
            Success = false;
            Errors = new List<RequestResultErrorItem>
            {
                new RequestResultErrorItem()
                {
                    Message = ex.Message + " - " + ex.InnerException?.ToString()
                }
            };
        }
    }

    public class RequestResultErrorItem
    {
        public string Key { get; set; }
        public int? Code { get; set; }
        public string Message { get; set; }
        public string PropertyName { get; set; }
    }
}
