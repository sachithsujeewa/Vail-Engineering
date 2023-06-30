using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Vail_Engineering.Models;

namespace Vail_Engineering.Pages.Reports
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public DateTime DateTime { get; set; } = DateTime.Now;
        public ChartData BarChartData { get; set; }
        public ChartData BinDataReport { get; set; }
        public ChartData WastePercentage { get; set; }
        public ChartData HazardChart { get; set; }
        public ChartData DegradableChart { get; set; }
        public ChartData LandfillReuseChart { get; set; }
        public ChartData CostAnalysischart { get; set; }

        private readonly Data.Vail_EngineeringContext _context;

        public IndexModel(Data.Vail_EngineeringContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync()
        {
            // Retrieve the chart data from your data source
            BarChartData = GetBarChartData();
            //await BinTypeDataProcess();
        }
        public async Task OnPostAsync()
        {
            await BinTypeDataProcess(DateTime);
            await WastePercentageProcess(DateTime);
            await HazardChartProcess(DateTime);
            await DegradableProcess(DateTime);
            await LandfillReuseProcess(DateTime);
            await CostAnalysisProcess(DateTime);
        }

        public async Task BinTypeDataProcess(DateTime dateTime)
        {
            var quantitiesPerBinTypePerDay = await _context.Record
                .Where(r => r.Date.Date == dateTime.Date)
                .GroupBy(d => d.Bin.BinType)
                .Select(g => new
                {
                    BinType = g.Key,
                    Date = g.First().Date,
                    Quantity = g.Sum(r => r.Bin.Quantity)
                })
                .ToListAsync();

            var labelList = quantitiesPerBinTypePerDay.Select(q => q.BinType.ToString()).Distinct().ToList();
            var colorList = labelList.Select(l => GetRandomColor(l)).ToList();
            var dataList = quantitiesPerBinTypePerDay.Select(q => (double)q.Quantity).ToList();

            BinDataReport = new ChartData
            {
                Labels = labelList,
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Bar Chart",
                        Data = dataList,
                        BackgroundColor = colorList
                    }
                }
            };
        }


        public async Task WastePercentageProcess(DateTime dateTime)
        {
            var wastageByOutlet = await _context.Record.Include(x=> x.Bin)
                .Where(r => r.Date.Date == dateTime.Date)
                .GroupBy(d => d.Outlet)
                .ToListAsync();

            var wastageByOutlet2 = wastageByOutlet.Select(g => new
            {
                Outlet = g.Key,
                Date = g.First().Date,
                Quantity = g.Sum(r => r.Bin.Volume)
            });

            var labelList = wastageByOutlet2.Select(q => q.Outlet.ToString()).ToList();
            var colorList = labelList.Select(l => GetRandomColor(l)).ToList();
            var dataList = wastageByOutlet2.Select(q => q.Quantity).ToList();

            int totalSum = dataList.Sum();

            var percentages  = dataList.Select(val => (double)val / totalSum * 100).ToList();

            WastePercentage = new ChartData
            {
                Labels = labelList,
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Bar Chart",
                        Data = percentages,
                        BackgroundColor = colorList
                    }
                }
            };
        }

        public async Task CostAnalysisProcess(DateTime dateTime)
        {
            var wastageByOutlet = await _context.Record.Include(x => x.Bin).Where(r => r.Outlet != Outlets.Elimination)
                .Where(r => r.Date.Date == dateTime.Date)
                .GroupBy(d => d.Outlet)
                .ToListAsync();

            var wastageByOutlet2 = wastageByOutlet.Select(g => new
            {
                Outlet = g.Key,
                Date = g.First().Date,
                Quantity = g.Sum(r => r.Bin.Volume* r.Cost)
            });

            var labelList = wastageByOutlet2.Select(q => {
                if (q.Outlet == Outlets.EnergyRecovery)
                {
                    return "Compostable Waste";
                } else {

                    return q.Outlet.ToString();
                }  }).ToList();
            var colorList = labelList.Select(l => GetRandomColor(l)).ToList();
            var dataList = wastageByOutlet2.Select(q => (double)q.Quantity).ToList();

            //int totalSum = dataList.Sum();

            //var percentages = dataList.Select(val => (double)val / totalSum * 100).ToList();

            CostAnalysischart = new ChartData
            {
                Labels = labelList,
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Reuse",
                        Data = dataList,
                        BackgroundColor = colorList
                    }
                }
            };
        }

        public async Task LandfillReuseProcess(DateTime dateTime)
        {
            var wastageByOutlet = await _context.Record.Include(x => x.Bin)
                .Where(r => r.Date.Date == dateTime.Date)
                .GroupBy(d => d.Outlet)
                .ToListAsync();

            var wastageByOutlet2 = wastageByOutlet.Select(g => new
            {
                Outlet = g.Key,
                Date = g.First().Date,
                Quantity = g.Sum(r => r.Bin.Quantity)
            });

            var labelList = wastageByOutlet2.Where(r=>r.Outlet == Outlets.Reuse || r.Outlet == Outlets.Landfill ).Select(q => q.Outlet.ToString()).ToList();
            var colorList = labelList.Select(l => GetRandomColor(l)).ToList();
            var dataList = wastageByOutlet2.Select(q => (double)q.Quantity).ToList();

           

            LandfillReuseChart = new ChartData
            {
                Labels = labelList,
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Reuse",
                        Data = dataList,
                        BackgroundColor = colorList
                    }
                }
            };
        }



        public async Task HazardChartProcess(DateTime dateTime)
        {
            var hazardList = await _context.Record
                .Where(r => r.Date.Date == dateTime.Date)
                .ToListAsync();

            int trueCount = hazardList.Count(item => item.IsHazzard == true);
            int falseCount = hazardList.Count(item => item.IsHazzard == false);

            HazardChart = new ChartData
            {
                Labels = new List<string> { "Hazardous", "Non Hazardous" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "hazardous count",
                        Data = new List<double> { trueCount, falseCount },
                        BackgroundColor = new List<string> { "red", "green",}
                    }
                }
            };
        }

        public async Task DegradableProcess(DateTime dateTime)
        {
            var dayRecordList = await _context.Record
                .Where(r => r.Date.Date == dateTime.Date)
                .ToListAsync();

            int trueCount = dayRecordList.Count(item => item.BioDegradable == true);
            int falseCount = dayRecordList.Count(item => item.BioDegradable == false);

            DegradableChart = new ChartData
            {
                Labels = new List<string> { "Degradable", "Non Degradable" },
                Datasets = new List<ChartDataset>
                {
                    new ChartDataset
                    {
                        Label = "Bio degradable count",
                        Data = new List<double> { trueCount, falseCount },
                        BackgroundColor = new List<string> { "red", "green"}
                    }
                }
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
                    Data = new List<double> { 10, 20, 30 },
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
        public List<double> Data { get; set; }
        public List<string> BackgroundColor { get; set; }
    }
}
