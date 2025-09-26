using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Reader
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ReaderBook> ReaderBooks { get; set; } = new List<ReaderBook>();
    }
}
