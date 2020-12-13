using PhysicalPersonsDirectory.Services.Models.Paging;
using System.Collections.Generic;
using System.Linq;

namespace PhysicalPersonsDirectory.Services.Services.Helpers
{
    public static class Paging
    {
        public static List<T> PaginatedList<T>(IQueryable<T> source, PagingBaseRequestModel pagingModel)
        {
            if (pagingModel == null)
            {
                pagingModel = new PagingBaseRequestModel();
            }

            var prop = typeof(T).GetProperty(pagingModel.Sorting?.SortBy);
            if (prop != null)
            {
                var isAscending = pagingModel.Sorting?.SortDir == "asc";
                source = source.OrderByAsQueryable(prop.Name, isAscending);
            }

            var pagedList = source.Skip(pagingModel.Paging.Offset).Take(pagingModel.Paging.Limit).ToList();

            return pagedList;
        }
    }
}
