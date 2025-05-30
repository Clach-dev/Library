namespace Domain.Entities;
// TODO find if i need to delete this class and move to DTO
public class PageInfo(int pageNumber = 1, int pageSize = 10)
{
    public int PageNumber { get; set; }

    public int PageSize { get; set; }
}