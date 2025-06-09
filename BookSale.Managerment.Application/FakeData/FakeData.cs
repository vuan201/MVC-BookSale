using Bogus;
using BookSale.Managerment.Application.DTOs;

namespace BookSale.Managerment.Application.FakeData
{
    public static class FakeData
    {
        public static List<UserDto> FakeUser(int count)
        {
            var fakeUsers = new Faker<UserDto>()
                .StrictMode(true)
                .RuleFor(u => u.Id, f => f.Random.String2(10))
                .RuleFor(u => u.UserName, f => f.Name.FindName())
                .RuleFor(u => u.FullName, f => f.Name.FindName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FullName))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.IsActive, f => true);
            return fakeUsers.Generate(count);
        }
        public static List<BookDTO> GenerateFakeBooks(int count)
        {
            var tagId = 1;
            var tagFaker = new Faker<TagDTO>()
                .RuleFor(t => t.Id, f => tagId++)
                .RuleFor(t => t.Name, f => f.Commerce.ProductAdjective() + " " + f.Commerce.Product())
                .RuleFor(t => t.Description, f => f.Lorem.Sentence());

            var genres = new[] { "Fiction", "Non-fiction", "Science Fiction", "Fantasy", "Mystery", "Romance" };
            var authors = new[] { "John Doe", "Jane Austen", "Isaac Asimov", "Stephen King", "Agatha Christie" };

            var bookId = 1;
            var bookFaker = new Faker<BookDTO>()
                .RuleFor(b => b.Id, f => bookId++)
                .RuleFor(b => b.Code, f => "BOOK-" + f.Random.AlphaNumeric(6).ToUpper())
                .RuleFor(b => b.Name, f => f.Commerce.ProductName())
                .RuleFor(b => b.Description, f => f.Lorem.Paragraph())
                .RuleFor(b => b.Quantity, f => f.Random.Int(0, 100))
                .RuleFor(b => b.Price, f => Math.Round(f.Random.Decimal(5, 100), 2))
                .RuleFor(b => b.GenreName, f => f.PickRandom(genres))
                .RuleFor(b => b.AuthorName, f => f.PickRandom(authors))
                .RuleFor(b => b.BookTags, f => tagFaker.Generate(f.Random.Int(1, 3)))
                .RuleFor(b => b.CreatedDate, f => f.Date.Past(2))
                .RuleFor(b => b.UpdatedDate, (f, b) => f.Date.Between(b.CreatedDate, DateTime.Now));

            return bookFaker.Generate(count);
        }
    }
}