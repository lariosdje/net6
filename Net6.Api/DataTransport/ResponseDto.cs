namespace Net6.Api.DataTransport
{
    public class ResponseDto<T>
    {
        public ResponseDto()
        {
            IsCorrect = true;
        }

        public bool IsCorrect { get; set; }
        public string Message { get; set; }
        public T ObjectResponse { get; set; }
        public List<T> ListResponse { get; set; }
    }
}
