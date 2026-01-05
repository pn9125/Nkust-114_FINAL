using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class BirthController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public BirthController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult Index(int? searchYear)
        {
            var dataList = GetDataList();
            var viewModel = new BirthIndexViewModel
            {
                DataList = dataList,
                SearchYear = searchYear
            };

            if (searchYear.HasValue)
            {
                var target = dataList.FirstOrDefault(d => d.Year == searchYear.Value);
                if (target != null)
                {
                    viewModel.SelectedData = target;
                    
                    // Calculate StdDev for [Year-2, Year-1, Year]
                    // If "前年" means Year-2.
                    var prevRange = dataList.Where(d => d.Year >= searchYear.Value - 2 && d.Year <= searchYear.Value).ToList();
                    // Ensure we have 3 points for meaningful "Year-2 to Year" stat, or just calculate whatever is available
                    if (prevRange.Any())
                    {
                        viewModel.StdDevPrev = CalculateStdDev(prevRange.Select(d => (double)d.Total));
                    }

                    // Calculate StdDev for [Year, Year+1, Year+2]
                    var nextRange = dataList.Where(d => d.Year >= searchYear.Value && d.Year <= searchYear.Value + 2).ToList();
                    if (nextRange.Any())
                    {
                        viewModel.StdDevNext = CalculateStdDev(nextRange.Select(d => (double)d.Total));
                    }
                }
            }

            return View(viewModel);
        }

        private List<BirthData> GetDataList()
        {
            var dataList = new List<BirthData>();
            var path = Path.Combine(_hostingEnvironment.WebRootPath, "data", "japan_birth.csv");

            if (System.IO.File.Exists(path))
            {
                var lines = System.IO.File.ReadAllLines(path);
                for (int i = 1; i < lines.Length; i++)
                {
                    var parts = lines[i].Split(',');
                    if (parts.Length >= 5)
                    {
                        if (int.TryParse(parts[1], out int year) &&
                            int.TryParse(parts[3], out int birth_male) &&
                            int.TryParse(parts[4], out int birth_female))
                        {
                            dataList.Add(new BirthData
                            {
                                Year = year,
                                Male = birth_male,
                                Female = birth_female
                            });
                        }
                    }
                }
            }
            return dataList.OrderBy(d => d.Year).ToList();
        }

        private double CalculateStdDev(IEnumerable<double> values)
        {
            var count = values.Count();
            if (count < 2) return 0;

            var avg = values.Average();
            var sum = values.Sum(d => Math.Pow(d - avg, 2));
            
            // Using Sample Standard Deviation (N-1)
            return Math.Sqrt(sum / (count - 1));
        }
    }
}
