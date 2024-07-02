using CommunityToolkit.Mvvm.ComponentModel;
using NotkaDesktop.ViewModels.Abstract;

namespace NotkaDesktop.ViewModels
{
	public partial class MainPageViewModel : BaseViewModel
	{
		[ObservableProperty]
		string _headline = "Witaj w Intranecie!";

		[ObservableProperty]
		string _text = "Tutaj możesz zarządzać aplikacją 'Notka', która służy do zapisywania notatek, list zadań i zarządzania swoim portfolio akcji." +
			" Do każdej akcji możesz dopisywać swoje notatki, aby żadne ważne przemyślenie nie uciekło.";  
	}
}
