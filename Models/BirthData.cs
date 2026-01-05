namespace WebApp.Models
{
    public class BirthData
    {
        public int Year { get; set; }
        public int Male { get; set; }
        public int Female { get; set; }
        public int Total => Male + Female;
    }
}
