namespace Vail_Engineering.Models
{
    public class WasteCategory
    {
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Description { get; set; }

        public IList<WasteChapter>? WasteChapters { get; set; }


    }
}
