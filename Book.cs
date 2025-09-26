using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management_System.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public int AutherId { get; set; }
        public virtual Auther Auther { get; set; }//Navigation Property

        public int PublisherId { get; set; }
        public virtual Publisher Publisher{    get; set; }
        

        public virtual ICollection<ReaderBook> ReaderBook { get; set; } = new List<ReaderBook>();
    }
}
