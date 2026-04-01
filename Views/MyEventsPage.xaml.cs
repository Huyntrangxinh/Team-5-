using Campus.ViewModels;

namespace Campus.Views;

public partial class MyEventsPage : ContentPage
{
	public MyEventsPage()
	{
		InitializeComponent();
		BindingContext = new MyEventsViewModel();
	}
}