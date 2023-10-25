using System;

namespace api.multitracks.com.Model
{
    public class Album
    {
        public int albumID { get; set; }
        public int artistID { get; set; }
        public string title { get; set; }
        public string imageURL { get; set; }
        public int year { get; set; }
        public DateTime dateCreation {get;set;}
    }
}
