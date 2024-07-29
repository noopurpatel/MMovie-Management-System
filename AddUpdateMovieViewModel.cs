using Module4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Module6
{
	public class AddUpdateMovieViewModel: BaseViewModel
	{
		private int _MovieID;
		private string _Title;
		private int _ReleaseYear;
		private string _Description;
		private int _Duration;
		private double _Rating;
		private string _Director;
		private bool _IsAvailable = false;

		private ObservableCollection<Cast> _CastList = new ObservableCollection<Cast>();
		private int _SelectedCastIndex;
		private List<Genre> _GenreList;
		public Movie AddedMovie { get; set; } = new Movie();
        private Genre _SelectedGenre;
		private Movie selectedMovie;
		private MovieLogic movieLogic;

		public int MovieID
		{
			get { return _MovieID; }
			set 
			{
				_MovieID = value;
				OnPropertyRaised("MovieID");
			}
		}

		public string Title
		{
			get { return _Title; }
			set 
			{ 
				_Title = value; 
				OnPropertyRaised("Title");
			}
		}

		public int ReleaseYear
		{
			get { return _ReleaseYear; }
			set 
			{ 
				_ReleaseYear = value;
				OnPropertyRaised("ReleaseYear");
			}
		}

		public string Description
		{
			get { return _Description; }
			set 
			{ 
				_Description = value; 
				OnPropertyRaised("Description");
			}
		}

		public int Duration
		{
			get { return _Duration; }
			set 
			{ 
				_Duration = value;
				OnPropertyRaised("Duration");
			}
		}

		public double Rating
		{
			get { return _Rating; }
			set 
			{ 
				_Rating = value;
				OnPropertyRaised("Rating");
			}
		}

		public string Director
		{
			get { return _Director; }
			set 
			{ 
				_Director = value;
				OnPropertyRaised("Director");
			}
		}

		public bool IsAvailable
		{
			get { return _IsAvailable; }
			set 
			{ 
				_IsAvailable = value;
				OnPropertyRaised("IsAvailable");
			}
		}


		public List<Genre> GenreList
		{
			get { return _GenreList; }
			set 
			{ 
				_GenreList = value;
				OnPropertyRaised("GenreList");
			}
		}

		public Genre SelectedGenre
		{
			get { return _SelectedGenre; }
			set 
			{
				_SelectedGenre = value;
				OnPropertyRaised("SelectedGenre");
			}
		}

		public ObservableCollection<Cast> CastList
		{
			get { return _CastList; }
			set 
			{
				_CastList = value;
				OnPropertyRaised("CastList");
			}
		}

		public int SelectedCastIndex
		{
			get { return _SelectedCastIndex; }
			set 
			{
				_SelectedCastIndex = value; 
				OnPropertyRaised("SelectedCastIndex");
			}
		}


		private string _CastName;

		public string CastName
		{
			get { return _CastName; }
			set 
			{
				_CastName = value;
				OnPropertyRaised("CastName");
			}
		}


		public ICommand AddCastMember { get; set; }
		public ICommand SaveCommand { get; set; }	


		public AddUpdateMovieViewModel(Movie movie)
        {
			CastList=new ObservableCollection<Cast>();
			movieLogic = new MovieLogic();
			AddCastMember = new RelayCommand(AddCastMemberHandler, CanAddCastMember);
			SaveCommand = new RelayCommand(SaveCommandHandler, CanSaveCommand);
			selectedMovie = movie;
			if (selectedMovie !=null)
			{
				MovieID = selectedMovie.MovieID;
				Title = selectedMovie.Title;
				ReleaseYear = selectedMovie.ReleaseYear;
				Description = selectedMovie.Description;
				Duration = selectedMovie.Duration;
				Rating = selectedMovie.Rating;
				Director = selectedMovie.Director;
				IsAvailable = selectedMovie.IsAvailable;
				if(selectedMovie.Cast != null && selectedMovie.Cast.Any())
				{
					CastList = new ObservableCollection<Cast>(selectedMovie.Cast);
				}
			}
		}

		private void AddCastMemberHandler(object obj)
		{
			CastList.Add(new Cast { Name = CastName});
			CastName = "";
		}

		private bool CanAddCastMember(object obj)
		{
			return true;
		}

		private void SaveCommandHandler(object obj)
		{
			var _movie = new Movie();
			 
			_movie.MovieID = MovieID;
			_movie.Title = Title;
			_movie.Genre = SelectedGenre;
			_movie.ReleaseYear = ReleaseYear;
			_movie.Description = Description;
			_movie.Duration = Duration;
			_movie.Rating = Rating;
			_movie.Director = Director;
			_movie.Cast = CastList;
			_movie.IsAvailable = IsAvailable;
			if (selectedMovie is null)
			{
				movieLogic.AddMovie(_movie);
			}
			else
			{
				movieLogic.UpdateMovie(_movie);
			}
			AddedMovie = _movie;
		}

		private bool CanSaveCommand(object obj)
		{
			return true;
		}
	}
}
