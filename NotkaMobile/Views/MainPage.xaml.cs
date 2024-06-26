﻿using NotkaMobile.ViewModels;

namespace NotkaMobile.Views
{
	public partial class MainPage : ContentPage
	{
		int count = 0;

		public MainPage(MainViewModel vm)
		{
			InitializeComponent();
			BindingContext = vm;
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			GC.Collect();
			GC.WaitForPendingFinalizers();

			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}
	}

}
