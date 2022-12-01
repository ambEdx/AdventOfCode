using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.Models
{
    public partial class Elf : IDataServiceModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }

        private List<Food> _foods;
        [Ignore]
        public List<Food> Foods
        {
            get
            {
                return _foods;
            }
            set
            {
                _foods = value;
                TotalCalories = Foods.Sum(f => f.Calories);
            }
        }

        [Ignore]
        public int TotalCalories { get; set; }
    }
}
