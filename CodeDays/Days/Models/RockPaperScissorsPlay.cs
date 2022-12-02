using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Days.Models
{
    public partial class RockPaperScissorsPlay : IDataServiceModel
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique]
        public int Seq { get; set; }

        public string OpponentCode { get; set; }

        public string PlayerCode { get; set; }

        [Ignore]
        public int RoundScore { get; set; }

        [Ignore]
        public string PlayedCodes
        {
            get { return OpponentCode + " - " + PlayerCode; }
        }
    }
}
