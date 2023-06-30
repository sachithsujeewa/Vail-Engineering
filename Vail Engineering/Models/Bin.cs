using System.Drawing.Drawing2D;

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

        public int Volume 
        {
            get
            {
                switch (BinType)
                {
                    case BinTypes.None:
                        return Quantity * 0;
                        break;
                    case BinTypes.Small:
                        return Quantity * 60;
                        break;
                    case BinTypes.Medium:
                        return Quantity * 250;
                        break;
                    case BinTypes.Large:
                        return Quantity * 1000;
                        break;
                    default:
                        return 0;
                        break;
                }
            }
        }

    }
}
