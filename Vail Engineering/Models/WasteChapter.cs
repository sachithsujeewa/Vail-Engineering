namespace Vail_Engineering.Models
{
    public class WasteChapter
    {
        public int Id{ get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public WasteCategory? Category { get; set; }

    }
}
