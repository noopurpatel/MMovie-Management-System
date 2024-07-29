using Module6.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module6
{
	// Movie class
	public class Movie
	{
		// Properties
		public int MovieID { get; set; }
		public string Title { get; set; }
		public Genre Genre { get; set; }
		public int ReleaseYear { get; set; }
		public string Description { get; set; }
		public int Duration { get; set; }
		public double Rating { get; set; }
		public string Director { get; set; }
		public ObservableCollection<Cast> Cast { get; set; }
		public bool IsAvailable { get; set; }

		// Constructor
		public Movie(int movieId, string title, Genre genre, int releaseYear, string description, 
			int duration, double rating, string director, ObservableCollection<Cast> cast, bool isAvailable)
		{
			MovieID = movieId;
			Title = title;
			Genre = genre;
			ReleaseYear = releaseYear;
			Description = description;
			Duration = duration;
			Rating = rating;
			Director = director;
			Cast = cast;
			IsAvailable = isAvailable;
		}

        public Movie()
        {
            
        }

        // Method to display movie details
        public void DisplayDetails()
		{
			Console.WriteLine($"Title: {Title}");
			Console.WriteLine($"Genre: {Genre}");
			Console.WriteLine($"Release Year: {ReleaseYear}");
			Console.WriteLine($"Description: {Description}");
			Console.WriteLine($"Duration: {Duration} minutes");
			Console.WriteLine($"Rating: {Rating}");
			Console.WriteLine($"Director: {Director}");
			Console.WriteLine($"Cast: {string.Join(", ", Cast)}");
			Console.WriteLine($"Availability: {(IsAvailable ? "Available" : "Not Available")}");
		}
	}
}
