using PhysicalPersonsDirectory.Services.Models.Paging;

namespace PhysicalPersonsDirectory.Services.Models.Base
{
    public class PagingBaseRequest
    {
        public PagingBaseRequestModel PagingModel { get; set; }

        public PagingBaseRequest()
        {
            PagingModel = new PagingBaseRequestModel();
        }
    }
}
