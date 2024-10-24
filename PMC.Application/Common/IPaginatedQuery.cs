namespace PMC.Application.Common
{
    public interface IPaginatedQuery
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
    }
}
