using System.Collections.Generic;

namespace Repository
{
    public partial class Category
    {
        public Category()
        {
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string EventName { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Location { get; set; }

    }
}