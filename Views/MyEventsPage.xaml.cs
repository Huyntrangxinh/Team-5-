using Campus.ViewModels;

namespace Campus.Views;

public partial class MyEventsPage : ContentPage
{
	public MyEventsPage(EventViewModels vm)
	{
		InitializeComponent();
		BindingContext = vm;

		
	}
	protected override void OnAppearing()
	{
		base.OnAppearing();

		if (BindingContext is EventViewModels vm)
		{
			vm.LoadMyEventsCommand.Execute(null);
		}
	}
}