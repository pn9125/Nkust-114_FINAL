using WebApp.Models;

namespace WebApp.ViewModels
{
    public class BirthIndexViewModel
    {
        public List<BirthData> DataList { get; set; } = new List<BirthData>();
        public int? SearchYear { get; set; }
        
        // Statistics
        public double? StdDevPrev { get; set; } // Standard Deviation (Year-2, Year-1, Year)
        public double? StdDevNext { get; set; } // Standard Deviation (Year, Year+1, Year+2)
        public BirthData? SelectedData { get; set; }
    }
}
