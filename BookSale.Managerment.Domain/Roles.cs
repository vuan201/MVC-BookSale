using BookSale.Managerment.Domain.Extension;
using System.Reflection;
namespace BookSale.Managerment.Domain
{
    public static class Roles
    {
        public const string SupperAdmin = "SupperAdmin";
        public const string Admin = "Admin";
        public const string User = "User";

        public static List<string> GetRoles() => LoadVariableValue.GetAllConstValue(typeof(Roles));
    }
}