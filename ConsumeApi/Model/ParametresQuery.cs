namespace ConsumeApi.Model
{
    public class ParametresQuery
    {
        public string MovieToFind { get; set; }
        public int Page { get; set; } = 1;
        public bool? IncludeAdulte { get; set; } = false;
        public string Region { get; set; } 
        public int? Year { get; set; }
        public int? PrimaryReleaseYear { get; set; }
    }
}