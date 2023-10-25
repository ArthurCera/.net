using api.multitracks.com.DataAccess;
using api.multitracks.com.Model;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace api.multitracks.com.QueryHandler
{
    public class GetArtistQueryHandler
    {

        private readonly MTDBContext _ctx;

        public GetArtistQueryHandler(MTDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Artist> ExecuteAsync(string name) {
            if (name == null || name == "" || name == " ") throw new ArgumentNullException(name);
            else return await ExecuteInternalAsync(name);
        }

        private async Task<Artist> ExecuteInternalAsync(string name) {
            try { 
                return await (from artist in _ctx.Artist where artist.title.ToLower().Contains(name.ToLower()) select artist).FirstOrDefaultAsync();
            
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}
