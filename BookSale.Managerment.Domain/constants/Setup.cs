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

    }
}
