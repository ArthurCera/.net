using api.multitracks.com.DataAccess;
using api.multitracks.com.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static api.multitracks.com.IQueryableExtensions;

namespace api.multitracks.com.QueryHandler
{
    public class GetSongListQueryHandler
    {
        private readonly MTDBContext _ctx;

        public GetSongListQueryHandler(MTDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<PagedResultAsync<Song>> ExecuteAsync(int pageIndex, int pageSize, string search, string orderBy, string orderDir)
        {
            try
            {
                var baseQuery = (from p in _ctx.Song
                                 select p);

                baseQuery = ApplySearch(search, baseQuery);
                baseQuery = ApplySort(orderBy, orderDir, baseQuery);

                return (await (from query in baseQuery select query).GetPagedResultAsync(pageIndex, pageSize));

            }
            catch(Exception ex) { 
                throw ex.InnerException;
            }
        }

        private IQueryable<Song> ApplySearch(string search, IQueryable<Song> query)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return query;
            }
            search = search.ToLower();
            //only searching for the parameters that are not ID or bit
            return (from p in query
                    where p.title.ToLower().Contains(search)
                    select p);
        }
        private IQueryable<Song> ApplySort(string orderBy, string orderDir, IQueryable<Song> query)
        {
            if (orderBy == null) return query.OrderBy(c => c.artistID);
            var ascending = string.Equals(orderDir, "asc", StringComparison.OrdinalIgnoreCase);
            if (ascending)
            {
                if (string.Equals(orderBy, "title", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(c => c.title);
                }
                else if (string.Equals(orderBy, "bpm", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(c => c.bpm);
                }
                else if (string.Equals(orderBy, "dateCreation", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(c => c.dateCreation);
                }
                else if (string.Equals(orderBy, "timeSignature", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderBy(c => c.timeSignature);
                }
                else query = query.OrderBy(c => c.artistID);
            }
            else {
                if (string.Equals(orderBy, "title", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(c => c.title);
                }
                else if (string.Equals(orderBy, "bpm", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(c => c.bpm);
                }
                else if (string.Equals(orderBy, "dateCreation", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(c => c.dateCreation);
                }
                else if (string.Equals(orderBy, "timeSignature", StringComparison.OrdinalIgnoreCase))
                {
                    query = query.OrderByDescending(c => c.timeSignature);
                }
                else query = query.OrderByDescending(c => c.artistID);
            }
            return query;
        }


    }
}
