using api.multitracks.com.DataAccess;
using api.multitracks.com.Model;
using System;
using System.Threading.Tasks;

namespace api.multitracks.com.QueryHandler
{
    public class AddArtistQueryHandler
    {
        private readonly MTDBContext _ctx;

        public AddArtistQueryHandler(MTDBContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> ExecuteAsync(AddArtistCommand command) {
            if (command == null) throw new ArgumentNullException(nameof(command));
            else return await ExecuteInternalAsync(command);
        }
        private async Task<bool> ExecuteInternalAsync(AddArtistCommand command)
        {
            try { 
                await _ctx.Artist.AddAsync(await ConvertToArtist(command));
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex) {
                throw ex.InnerException;
            }
        }
        //converting the creation class to the actual class
        private async Task<Artist> ConvertToArtist(AddArtistCommand command) {
            return new Artist()
            {
                dateCreation = DateTime.Now,
                title = command.title,
                biography = command.biography,
                imageURL = command.imageURL,
                heroURL = command.heroURL
            };
        }
    }
}
