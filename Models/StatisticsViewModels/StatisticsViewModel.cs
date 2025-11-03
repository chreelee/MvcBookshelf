namespace MvcBookshelf.Models.StatisticsViewModels
{
    public class StatisticsViewModel
    {
        public int BookCount { get; set; }
        public int AuthorCount { get; set; }
        public int GenreCount { get; set; }
        public int PagesCount { get; set; }
        // create new variables to hold the totals of each from autogenerate
        public int TotalBooks { get; set; }
        public int TotalAuthors { get; set; }
        public int TotalGenres { get; set; }
        public int TotalPages   { get; set; }

    }
}