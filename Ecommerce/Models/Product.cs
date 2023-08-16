using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    internal class Product
    {
        public string Id {get; set;}
        public string Name {get; set;}=string.Empty;
        public string Price {get;set;}=string.Empty;
    }
}
