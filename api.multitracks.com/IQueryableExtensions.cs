using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace api.multitracks.com
{
    public static partial class IQueryableExtensions
    {
        public static Task<PagedResultAsync<T>> GetPagedResultAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            if (pageSize < 1)
            {
                pageSize = 10;
            }

            if (pageIndex < 0)
            {
                pageIndex = 0;
            }

            return InternalGetPagedResultAsync(query, pageIndex, pageSize);
        }
        private static async Task<PagedResultAsync<T>> InternalGetPagedResultAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            var result = new PagedResultAsync<T>
            {
                CurrentPageIndex = pageIndex,
                PageSize = pageSize,
                RowCount = query.Count()
            };

            double pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = pageIndex * pageSize;
            result.Results = await query.Skip(skip).Take(pageSize).ToArrayAsync();

            return result;
        }
        public class PagedResultAsync<T>
        {
            public int CurrentPageIndex { get; internal set; }
            public int PageSize { get; internal set; }
            public int RowCount { get; internal set; }
            public int PageCount { get; internal set; }
            public T[] Results { get; internal set; }
        }
    }
}
