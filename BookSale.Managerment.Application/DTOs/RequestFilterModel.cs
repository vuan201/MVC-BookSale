namespace BookSale.Managerment.Application.DTOs
{
  public class RequestFilterModel
  {
    public string searchText { get; set; }
    public int pageNumber { get; set; } = 1;
    public  int pageSize { get; set; } = 50;
  }
}