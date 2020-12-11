namespace PhysicalPersonsDirectory.Services.Models.Paging
{
    public class PagingBaseRequestModel
    {
        public PagingModel Paging { get; set; }
        public SortingModel Sorting { get; set; }

        public PagingBaseRequestModel()
        {
            Paging = new PagingModel();
            Sorting = new SortingModel();
        }
    }

    public class PagingModel
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 25;
    }

    public class SortingModel
    {
        public string SortBy { get; set; } = "Id";
        public string SortDir { get; set; }
    }
}
