using Bogus;
using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.FakeData
{
  public class FakeData
  {
    public List<UserDto> FakeUser(int count)
    {
      var fakeUsers = new Faker<UserDto>()
          .StrictMode(true)//OrderId is deterministic
          .RuleFor(u => u.Id, f => f.Random.String2(10))
          .RuleFor(u => u.UserName, f => f.Name.FindName())
          .RuleFor(u => u.FullName, f => f.Name.FindName())
          .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FullName))
          .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
          .RuleFor(u => u.IsActive, f => true);
      return fakeUsers.Generate(count);
    }
  }
}