namespace Domain.Entites
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Email { get; set; }
        public List<MovieDom> ListMovies { get; set; }
        
    }
}