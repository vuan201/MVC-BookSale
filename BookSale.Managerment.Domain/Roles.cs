using System.Reflection;
namespace BookSale.Managerment.Domain
{
    public static class Roles
    {
        public const string SupperAdmin = "SupperAdmin";
        public const string Admin = "Admin";
        public const string User = "User";

        public static List<string> GetRoles()
        {
            return typeof(Roles).GetFields(BindingFlags.Public | BindingFlags.Static)
                                .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
                                .Select(f => f.GetValue(null)?.ToString())
                                .Where(value => value is not null)
                                .Select(value => value!) // Dùng ! vì đã lọc null rồi
                                .ToList();
        }
    }
}