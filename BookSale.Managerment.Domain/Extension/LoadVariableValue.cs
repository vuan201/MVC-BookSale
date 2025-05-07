using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookSale.Managerment.Domain.Extension
{
    public static class LoadVariableValue
    {
        public static List<string> GetAllConstValue(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static)
                              .Where(f => f.IsLiteral && !f.IsInitOnly && f.FieldType == typeof(string))
                              .Select(f => f.GetValue(null)?.ToString())
                              .Where(value => value is not null)
                              .Select(value => value!)
                              .ToList();
        }
    }
}
