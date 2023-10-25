using System;

namespace api.multitracks.com.Model
{
    public class AddArtistCommand
    {
        public string title { get; set; }
        public string biography { get; set; }
        public string imageURL { get; set; }
        public string heroURL { get; set; }
    }
}
