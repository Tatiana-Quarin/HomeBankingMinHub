using HomebankingMindHub.Models;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Linq;

namespace HomebankingMindHub.Models
{
    public class Loan
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double MaxAmount { get; set; }

        public string Payments{ get; set; }

        public  ICollection<ClientLoan>ClientLoans { get; set; }
    }
}
