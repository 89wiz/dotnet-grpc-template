using gRPCTemplate.Application.Requests.Common;

namespace gRPCTemplate.Application.Responses.Common;

public class PagedResponse<T> where T : class
{
    public IQueryable<T> Data { get; set; }
    public int Rows { get; set; }

    public PagedResponse(IQueryable<T> data, GetPagedQuery request)
    {
        Rows = data.Count();
        Data = data
            .Skip(request.Skip)
            .Take(request.Take);
    }
}
