namespace BookSale.Managerment.Application.DTOs
{
  public class RequestFilterModel
  {
    public string search { get; set; }
    public int Limit { get; set; }
    public  int Offset { get; set; } 
  }
}