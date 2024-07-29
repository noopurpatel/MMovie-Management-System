using Module4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Module6
{
	public class MainWindowViewModel : BaseViewModel
	{
		MovieLogic movieLogic = new MovieLogic();

		private ObservableCollection<Movie> _MovieCollection;

		public ObservableCollection<Movie> MovieCollection
		{
			get { return _MovieCollection; }
			set 
			{ 
				_MovieCollection = value;
				OnPropertyRaised("MovieCollection");
			}
		}

		private Movie _SelectedMovie;

		public Movie SelectedMovie
		{
			get { return _SelectedMovie; }
			set 
			{ 
				_SelectedMovie = value;
				OnPropertyRaised("SelectedMovie");
			}
		}

        public ICommand AddCommand { get; set; }
		public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public MainWindowViewModel()
        {
			AddCommand = new RelayCommand(AddCommandHandler, CanAddCommand);
			UpdateCommand = new RelayCommand(UpdateCommandHandler, CanUpdateCommand);
			DeleteCommand = new RelayCommand(DeleteCommandHandler, CanDeleteCommand);
			MovieCollection = movieLogic.moviesCollection;
		}

		private void DeleteCommandHandler(object obj)
		{
			if(SelectedMovie !=null)
			{
				movieLogic.DeleteMovie(SelectedMovie.MovieID);
				MovieCollection=movieLogic.GetMovies();
			}
		}

		private bool CanDeleteCommand(object obj)
		{
			return true;
		}

		private void AddCommandHandler(object obj)
		{
			AddUpdateMovieDialog addUpdateMovieDialog = new AddUpdateMovieDialog(SelectedMovie);
			var dailog = addUpdateMovieDialog.ShowDialog();
			var vm = addUpdateMovieDialog.DataContext as AddUpdateMovieViewModel;
			MovieCollection = movieLogic.GetMovies();
		}

		private bool CanAddCommand(object obj)
		{
			return true;
		}


		private void UpdateCommandHandler(object obj)
		{
			AddUpdateMovieDialog addUpdateMovieDialog = new AddUpdateMovieDialog(SelectedMovie);
			var dailog = addUpdateMovieDialog.ShowDialog();
			var vm = addUpdateMovieDialog.DataContext as AddUpdateMovieViewModel;
			MovieCollection = movieLogic.GetMovies();
		}

		private bool CanUpdateCommand(object obj)
		{
			return true;
		}
	}
}
