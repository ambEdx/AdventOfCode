using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.Models
{
    public partial class Food : IDataServiceModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Calories { get; set; }

        public int ElfId { get; set; }
    }
}
