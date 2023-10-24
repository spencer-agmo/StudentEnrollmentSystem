namespace StudentEnrollmentSystem
{
    public class ServiceResponse<T>
    {
        private List<object> result;

        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ServiceResponse(T data, string message = null)
        {
            Success = true;
            Data = data;
            Message = message;
        }

        public ServiceResponse(string message)
        {
            Success = false;
            Message = message;
        }

        public ServiceResponse(List<object> result)
        {
            this.result = result;
        }
    }
}
