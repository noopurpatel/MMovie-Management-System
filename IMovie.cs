using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Module6
{
	public interface IMovie
	{
		void AddMovie(Movie movie);
		void ViewAllMovies();
		void UpdateMovie(Movie movie);
		void DeleteMovie(int movieID);
		void SearchMovieByTitle(string title);
		void SearchMoviesByGenre(Genre genre);

	}
}
