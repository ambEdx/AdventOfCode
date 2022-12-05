using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.Models
{
    public partial class RawInput : IDataServiceModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Input { get; set; }

        [Unique]
        public int Day { get; set; }
    }
}
