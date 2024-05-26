namespace gRPCTemplate.Domain.Common;

public class ErrorMessage
{
    public int ErrorCode { get; set; }
    public string Message { get; set; }
    public List<ErrorMessage> ErrorDetails { get; set; }
}
