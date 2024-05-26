namespace gRPCTemplate.Domain.Common
{
    public class ValidationError
    {
        public int ValidationErrorId { get; private set; }
        public string Message { get; set; }
        public ValidationError(string message)
        {
            Message = message;
        }
    }
}