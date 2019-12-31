using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiColumnRowReader.Models
{

    public class PersonActivityReport
    {
        public string PersonName { get; set; }
        public decimal WeightInKgs { get; set; }

        public CsvDataRecord[] DayReport { get; set; }
    }

    public class CsvDataRecord
    {
        public int DayNo { get; set; }
        public string Activity { get; set; }
        public string Task { get; set; }
        public List<int> TotalHours { get; set; }
    }

 
}
