using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vail_Engineering.Models;

namespace Vail_Engineering.Pages.Reports
{
    public class IndexModel : PageModel
    {
        public ChartData BarChartData { get; set; }
        public ChartData BinDataReport { get; set; }

        private readonly Data.Vail_EngineeringContext _context;

        public IndexModel(Data.Vail_EngineeringContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            // Retrieve the chart data from your data source
            BarChartData = GetBarChartData();
            await BinTypeDataProcess();
        }


        public async Task BinTypeDataProcess()
        {
            var quantitiesPerBinTypePerDay = await _context.Record
                .GroupBy(r => new { r.Bin.BinType, r.Date.Date })
                .Select(g => new
                {
                    BinType = g.Key.BinType,
                    Date = g.Key.Date,
                    Quantity = g.Sum(r => r.Bin.Quantity)
                })
                .ToListAsync();

            BinDataReport = new ChartData
            {
                Labels = quantitiesPerBinTypePerDay.Select(q => q.Date.ToString("yyyy-MM-dd")).Distinct().ToList(),
                Datasets = quantitiesPerBinTypePerDay
                    .GroupBy(q => q.BinType)
                    .Select(g => new ChartDataset
                    {
                        Label = g.Key.ToString(),
                        Data = g.Select(q => q.Quantity).ToList(),
                        BackgroundColor = g.Select(q => GetRandomColor(q.BinType.ToString())).ToList()
                    })
                    .ToList()
            };
        }

        public string GetRandomColor(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                Random random = new Random();
                byte[] rgb = new byte[3];
                random.NextBytes(rgb);

                return string.Format("#{0:X2}{1:X2}{2:X2}", rgb[0], rgb[1], rgb[2]);
            }
            else
            {
                int hashCode = input.GetHashCode();
                Random random = new Random(hashCode);
                byte[] rgb = new byte[3];
                random.NextBytes(rgb);

                return string.Format("#{0:X2}{1:X2}{2:X2}", rgb[0], rgb[1], rgb[2]);
            }
        }


        private ChartData GetBarChartData()
        {
            // Fetch and return the chart data from your data source
            // You can populate the chart data according to your project requirements
            return new ChartData
            {
                Labels = new List<string> { "Label 1", "Label 2", "Label 3" },
                Datasets = new List<ChartDataset>
            {
                new ChartDataset
                {
                    Label = "Bar Chart",
                    Data = new List<int> { 10, 20, 30 },
                    BackgroundColor = new List<string> { "red", "green", "blue" }
                }
            }
            };
        }


        public List<string> GetRandomColors(int count)
        {
            Random random = new Random();
            List<string> colors = new List<string>();

            for (int i = 0; i < count; i++)
            {
                byte[] rgb = new byte[3];
                random.NextBytes(rgb);

                string color = string.Format("#{0:X2}{1:X2}{2:X2}", rgb[0], rgb[1], rgb[2]);
                colors.Add(color);
            }

            return colors;
        }


    }

    public class ChartData
    {
        public List<string> Labels { get; set; }
        public List<ChartDataset> Datasets { get; set; }
    }

    public class ChartDataset
    {
        public string Label { get; set; }
        public List<int> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
    }
}
