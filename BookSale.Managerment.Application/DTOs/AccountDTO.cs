namespace BookSale.Managerment.Application.DTOs
{
  public class AccountDTO
  {
    public string Id { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsAdmin { get; set; }
  }
}