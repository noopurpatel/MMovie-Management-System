using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Module6
{
	/// <summary>
	/// Interaction logic for AddUpdateMovieDialog.xaml
	/// </summary>
	public partial class AddUpdateMovieDialog : Window
	{
		public AddUpdateMovieDialog(Movie movie = null)
		{
			InitializeComponent();
			this.DataContext = new AddUpdateMovieViewModel(movie);
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
        }

		private void SaveMovie_Click(object sender, RoutedEventArgs e)
		{
			var vm = this.DataContext as AddUpdateMovieViewModel;
			if(vm != null)
			{
				vm.SaveCommand.Execute(this);
				this.Close();
			}
		}

		private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
		{
			var vm = this.DataContext as AddUpdateMovieViewModel;
			if (vm != null)
			{
				vm.CastList.RemoveAt(vm.SelectedCastIndex);
			}
		}
	}
}
