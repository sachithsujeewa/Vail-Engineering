namespace Vail_Engineering.Models
{
    public class WasteChapter
    {
        public int Id{ get; set; }
        public string WasteChapterCode { get; set; }
        public string WasteChapterDescription { get; set; }

        public WasteCategory? Category { get; set; }

    }
}
