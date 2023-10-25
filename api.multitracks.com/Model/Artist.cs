using System;

namespace api.multitracks.com.Model
{
    public class Artist
    {
        public int artistID { get; set; }
        public string title { get; set; }
        public string biography { get; set; }
        public string imageURL { get; set; }
        public string heroURL { get; set; }
        public DateTime dateCreation { get; set; }
    }
}
