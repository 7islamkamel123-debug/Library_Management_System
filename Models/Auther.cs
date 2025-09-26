using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Auther
    {

        public int Id { get; set; }
        public string Name { get; set; }


        public virtual ICollection<Book> Books { get; set; } = new List<Book>();


    }
}
