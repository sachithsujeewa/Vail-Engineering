using System.Drawing.Drawing2D;

namespace Vail_Engineering.Models
{
    public enum Outlets
    {
        None=0,
        Reuse=1, 
        Recycle=2,
        Landfill=3,
        EnergyRecovery=4,
        Elimination=5
    }

    public class Record
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public WasteChapter WasteChapter { get; set; }

        public bool IsHazzard { get; set; }

        public bool BioDegradable { get; set; }

        public Bin Bin { get; set; }

        public Outlets? Outlet { get; set; } = Outlets.None;
        public Location Location { get; set; }

        public string Comment { get; set; }

        public double Cost
        {
            get
            {
                switch (Outlet)
                {
                    case Outlets.None:
                        return 0;
                        break;
                    case Outlets.Elimination:
                        return 0.0000;
                        break;
                    case Outlets.Recycle:
                        return 0.012;
                        break;
                    case Outlets.Reuse:
                        return  0.300;
                        break;
                    case Outlets.Landfill:
                        return -0.060;
                        break;
                    case Outlets.EnergyRecovery:
                        return 0.050;
                        break;
                    default:
                        return 0;
                        break;
                }
            }
        }
    }
}
