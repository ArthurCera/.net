using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class artistDetails : MultitracksPage
{
    private int artistID = 1;
    private DataTable artistDT = new DataTable();
    private DataTable albumsDT = new DataTable();
    private DataTable songsDT = new DataTable();

    public Artist artist;
    public List<Album> albums;
    public List<Song> songs;
    public class Artist { 
        public int artistID { get;set;}
        public string title { get;set;}
        public string biography { get;set;}
        public string imageURL { get;set;}
        public string heroURL { get;set;}
    }

    public class Album { 
        public int albumID { get; set; }
        public int artistID { get; set; }
        public string title { get; set; }
        public string imageURL { get; set; }
        public int year { get; set; }
        public string artistName { get; set; }
    }

    public class Song
    { 
        public int songID { get; set; }
        public int albumID { get; set; }
        public string albumName { get; set; }
        public string albumURL { get; set; }
        public int artistID { get; set; }
        public string title { get; set; }
        public decimal bpm { get; set; }
        public string timeSignature { get; set; }
        public bool multitracks { get; set; }
        public bool customMix {  get; set; }
        public bool chart {  get; set; }
        public bool rehearsalMix { get; set; }
        public bool patches { get; set; }
        public bool songSpecificPatches { get; set; }
        public bool proPresenter { get; set; }

    }

    //function to load the values inside the necessary variables
    protected async Task LoadValuesAsync(DataSet data)
    {
        try {
            artistDT = data.Tables[data.Tables.IndexOf("table")];
            albumsDT = data.Tables[data.Tables.IndexOf("table1")];
            songsDT = data.Tables[data.Tables.IndexOf("table2")];

            artist = artistDT.AsEnumerable().Select(row =>
            new Artist
            {
                artistID = row.Field<int>("artistID"),
                title = row.Field<string>("title"),
                biography = row.Field<string>("biography"),
                heroURL = row.Field<string>("heroURL"),
                imageURL = row.Field<string>("imageURL")
            }).FirstOrDefault();

            albums = albumsDT.AsEnumerable().Select(row =>
            new Album
            {
                albumID = row.Field<int>("albumID"),
                artistID = row.Field<int>("artistID"),
                title = row.Field<string>("title"),
                year = row.Field<int>("year"),
                imageURL = row.Field<string>("imageURL")
            }).ToList();

            songs = songsDT.AsEnumerable().Select(row =>
            new Song
            {
                albumID = row.Field<int>("albumID"),
                artistID = row.Field<int>("artistID"),
                songID = row.Field<int>("songID"),
                title = row.Field<string>("title"),
                bpm = row.Field<decimal>("bpm"),
                timeSignature = row.Field<string>("timeSignature"),
                multitracks = row.Field<bool>("multitracks"),
                customMix = row.Field<bool>("customMix"),
                chart = row.Field<bool>("chart"),
                rehearsalMix = row.Field<bool>("rehearsalMix"),
                patches = row.Field<bool>("patches"),
                songSpecificPatches = row.Field<bool>("songSpecificPatches"),
                proPresenter = row.Field<bool>("proPresenter")
            }).ToList();
            SetAlbumNames();
            BindValuesToPage();
        }
        catch(Exception ex)
        {
            //if there's any errors while loading the data, refresh the page with a "default" id            
            Response.Redirect("/artistDetails.aspx?artistID=1");
        }
    }
    //set the right album name and url inside the song list
    protected void SetAlbumNames()
    {
        try {
            foreach (var song in songs)
            {
                song.albumName = albums.Where(x => x.albumID == song.albumID).FirstOrDefault().title;
                song.albumURL = albums.Where(x => x.albumID == song.albumID).FirstOrDefault().imageURL;
            }
        }
        catch(Exception ex)
        {
            foreach (var song in songs)
            {
                song.albumName = "";
                song.albumURL = "";
            }
        }
    }
    //bind the loaded values to the page
    protected void BindValuesToPage() {
        try {
            //artist values
            profileImage.ImageUrl = artist.imageURL ?? "";
            bannerImage.ImageUrl = artist.heroURL ?? "";
            artistName.Text = artist.title ?? "";
            artist.biography = ProcessBiography(artist.biography);

            //biography
            biography.InnerHtml = artist.biography;

            //album values
            mediaRepeater.DataSource = albums;
            mediaRepeater.DataBind();

            //song values
            songRepeater.DataSource = songs;
            songRepeater.DataBind();
        } catch {
            //if there's any errors while loading the data, refresh the page with a "default" id            
            Response.Redirect("/artistDetails.aspx?artistID=1");
        }
    }
    private string ProcessBiography(string biography)
    {
        try {
            if (!biography.Contains("<!-- read more -->")) return biography;

            string[] segments = biography.Split(new[] { "<!-- read more -->" }, StringSplitOptions.None);

            if (segments.Length > 1 && !segments[1].Any(x => char.IsLetter(x))) return biography;
            segments[0] = "<p>" + segments[0];
            if (segments.Length > 1)
            {
                for (int i = 0; i < segments.Length; i++)
                {
                    if (i + 1 >= segments.Length)
                    {
                        segments[i] = "<p id='more" + i + "' hidden>" + segments[i] + "</p>";
                    }
                    else
                    {

                        segments[i] = "<p id='more" + i + "'>" + segments[i] + "</p>";
                        segments[i] += "<p><a onClick='readMore(more" + (i + 1).ToString() + ", this)'>.....  Read More</a></p>";
                    }
                }
            }

            segments[0] = segments[0] + "</p>";
            return string.Join("", segments);
        }
        catch(Exception e) { 
            return biography;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["artistID"] != null)
        {
            artistID = Int32.TryParse(Request.QueryString["artistID"], out int result) ? Int32.Parse(Request.QueryString["artistID"]) : 1;
        }

        var sql = new SQL();

        try
        {
            sql.Parameters.Add("@artistID", artistID);
            var data = sql.ExecuteStoredProcedureDS("GetArtistDetails");

            LoadValuesAsync(data);            
        }
        catch (Exception ex) {
            throw ex.InnerException;
        }
    }
}