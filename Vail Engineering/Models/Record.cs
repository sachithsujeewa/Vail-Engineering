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

        public WasteCategory WasteCategory { get; set; }

        public bool IsHazzard { get; set; }

        public bool BioDegradable { get; set; }

        public Bin Bin { get; set; }

        public Outlets? Outlet { get; set; } = Outlets.None;
        public Location Location { get; set; }

        public string Comment { get; set; }
    }
}
