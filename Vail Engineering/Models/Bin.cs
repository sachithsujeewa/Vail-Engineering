namespace Vail_Engineering.Models
{

    public enum BinTypes
    {
        None = 0,
        Small = 1,
        Medium = 2,
        Large = 3
    }

    public class Bin
    {
        public int Id { get; set; }

        public int Quantity { get; set; }

        public BinTypes BinType { get; set; } = BinTypes.None;

    }
}
