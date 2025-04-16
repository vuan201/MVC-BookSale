using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.constants
{
    public static class Setup
    {
        public static string EnvPath {get { return Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.FullName, ".env"); } }
        public const string UserName = "SupperAdmin";
        public const string password = "Sa@12345!";
        public const string FullName = "Supper Admin";
        public const string Email = "supperadmin@gmail.com";
        public const string PhoneNumber = "0989771234";
    }
}
