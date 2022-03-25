using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CategoryService.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public bool IsHeader { get; set; }
        public bool IsMainPage { get; set; }
    }
}
