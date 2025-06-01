namespace Domain.Entities;
// TODO find if i need to delete this class and move to DTO
public class PageInfo(int i = 1, int count = 10)
{
    public int PageNumber { get; set; } = 1;

    public int PageSize { get; set; } = 10;
}