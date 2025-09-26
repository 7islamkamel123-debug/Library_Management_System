using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class ReaderBook
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int ReaderId { get; set; }
        public virtual Reader Reader { get; set; }
    }
}
