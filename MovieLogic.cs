using Microsoft.SqlServer.Server;
using Module6.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Module6
{
	// Movie Management System class
	public class MovieLogic : IMovie
	{
		public static string ConnectionString = string.Empty;
        public ObservableCollection<Movie> moviesCollection; // Using linked list to store movies
		private Stack<string> actionHistory; // Using stack to maintain action history
		private Dictionary<int, Movie> movieDictionary; // Using dictionary for fast movie lookup by ID

		// Constructor
		public MovieLogic()
		{
			ConnectionString = ConfigurationManager.ConnectionStrings["MovieCS"].ConnectionString;
			moviesCollection = new ObservableCollection<Movie>();
			actionHistory = new Stack<string>();
			movieDictionary = new Dictionary<int, Movie>();
			moviesCollection=GetMovies();
		}

		public ObservableCollection<Movie> GetMovies()
		{
			ObservableCollection<Movie> movies = new ObservableCollection<Movie>();
			List<Cast> casts = new List<Cast>();
			try
			{
				using (var context = new MovieContext())
				{
					foreach (var movie in context.Movies)
					{
						movies.Add(new Movie
						{
							MovieID = movie.MovieID,
							Title = movie.Title,
							Genre = (Genre)movie.Genre,
							ReleaseYear = movie.ReleaseYear,
							Description = movie.Description,
							Duration = movie.Duration,
							Rating = movie.Rating,
							Director = movie.Director,
							IsAvailable = movie.IsAvailable,

						});
					}
				}

				foreach (var item in movies)
				{
					item.Cast = new ObservableCollection<Cast>();
					using (var context = new MovieContext())
					{
						var existingCasts = context.Casts.Where(c => c.MovieId == item.MovieID);
						if (existingCasts != null && existingCasts.Any())
						{
							foreach (var c in existingCasts)
							{
								item.Cast.Add(new Cast { CastId = c.CastId, MovieId = c.MovieId, Name = c.Name });
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
			}
			return movies;
		}

		// Method to add a movie
		public void AddMovie(Movie movie)
		{
			DataAccess.Movie movieDTO=null;
			if (movie !=null)
			{
				movieDTO = new DataAccess.Movie();
				movieDTO.Title = movie.Title;
				movieDTO.Genre = (int)movie.Genre;
				movieDTO.ReleaseYear = movie.ReleaseYear;
				movieDTO.Description = movie.Description;
				movieDTO.Duration = movie.Duration;
				movieDTO.Rating = movie.Rating;
				movieDTO.Director = movie.Director;
				movieDTO.IsAvailable = movie.IsAvailable;
			}
			using(var context = new MovieContext())
			{
				context.Movies.Add(movieDTO);
				context.SaveChanges();
			}
			foreach (var item in movie.Cast)
			{
				DataAccess.Cast castDTO = new DataAccess.Cast();
				castDTO.Name = item.Name;
				castDTO.MovieId = movieDTO.MovieID;
				using (var context = new MovieContext())
				{
					context.Casts.Add(castDTO);
					context.SaveChanges();
				}
			}
		}

		// Method to view all movies
		public void ViewAllMovies()
		{
			foreach (var movie in moviesCollection)
			{
				movie.DisplayDetails();
				Console.WriteLine();
			}
		}

		// Method to update a movie
		public void UpdateMovie(Movie movie)
		{
			DataAccess.Movie movieToUpdate = new DataAccess.Movie();
			if (movie != null)
			{
				using (var context = new MovieContext())
				{
					movieToUpdate = context.Movies.FirstOrDefault(m => m.MovieID == movie.MovieID);
					movieToUpdate.Title = movie.Title;
					movieToUpdate.Genre = (int)movie.Genre;
					movieToUpdate.ReleaseYear = movie.ReleaseYear;
					movieToUpdate.Description = movie.Description;
					movieToUpdate.Duration = movie.Duration;
					movieToUpdate.Rating = movie.Rating;
					movieToUpdate.Director = movie.Director;
					movieToUpdate.IsAvailable = movie.IsAvailable;
					context.Movies.AddOrUpdate(movieToUpdate);
					context.SaveChanges();
				}
				if (movie.Cast != null && movie.Cast.Any())
				{
					using (var context = new MovieContext())
					{
						var existingCasts = context.Casts.Where(c => c.MovieId == movie.MovieID);
						if (existingCasts != null && existingCasts.Any())
						{
							context.Casts.RemoveRange(existingCasts);
							context.SaveChanges();
						}
					}
					foreach (var item in movie.Cast)
					{
						DataAccess.Cast castDTO = new DataAccess.Cast();
						using (var context = new MovieContext())
						{
							castDTO.Name = item.Name;
							castDTO.MovieId = movieToUpdate.MovieID;
							context.Casts.Add(castDTO);
							context.SaveChanges();
						}
					}
				}
			}
			else
			{
				Console.WriteLine("Movie not found.");
			}
		}

		// Method to delete a movie
		public void DeleteMovie(int movieID)
		{
			if(movieID !=null && movieID > 0) 
			{
				using (var context = new MovieContext())
				{
					var existingCasts = context.Casts.Where(c => c.MovieId == movieID);
					context.Casts.RemoveRange(existingCasts);
					var existingMovie = context.Movies.Where(c => c.MovieID == movieID);
					context.Movies.RemoveRange(existingMovie);
					context.SaveChanges();
				}
			}
		}

		// Method to search for a movie by title
		public void SearchMovieByTitle(string title)
		{
			Movie movie = moviesCollection.FirstOrDefault(m => m.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
			if (movie != null)
			{
				movie.DisplayDetails();
			}
			else
			{
				Console.WriteLine("Movie not found.");
			}
		}

		// Method to search for movies by genre
		public void SearchMoviesByGenre(Genre genre)
		{
			var matchingMovies = moviesCollection.Where(m => m.Genre == genre);
			if (matchingMovies != null && matchingMovies.Any())
			{
				foreach (var movie in matchingMovies)
				{
					movie.DisplayDetails();
					Console.WriteLine();
				}
			}
			else
			{
				Console.WriteLine("No movies found for the specified genre.");
			}
		}

		// Method to display action history
		public void DisplayActionHistory()
		{
			Console.WriteLine("Action History:");
			foreach (var action in actionHistory)
			{
				Console.WriteLine(action);
			}
		}
	}
}
