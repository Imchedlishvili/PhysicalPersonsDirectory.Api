using PhysicalPersonsDirectory.Services.Models.Base;

namespace PhysicalPersonsDirectory.Services.Models.Paging
{
    public class PagingBaseResponseModel : ResponseBaseModel
    {
        public int TotalCount { get; set; }
        public int CurrentElementStart { get; set; }
        public int CurrentElementEnd { get; set; }
    }
}
