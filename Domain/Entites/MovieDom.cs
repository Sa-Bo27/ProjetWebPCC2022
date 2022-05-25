namespace Domain.Entites
{
    public class MovieDom
    {
        public bool Adult { get; set; }
        public string Backdrop_path { get; set; }
        public int Id { get; set; }
        public string Original_language { get; set; }
        public string Original_title { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        public string Poster_path { get; set; }
        public string Release_date { get; set; }
        public string Title { get; set; }
        
        public List<User> Users {get; set;}
    }
}