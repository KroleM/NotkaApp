﻿using ApiSharedClasses.QueryParameters;
using ApiSharedClasses.SortValues;
using NotkaMobile.Helpers;
using NotkaMobile.Service.Reference;
using NotkaMobile.Services;
using NotkaMobile.ViewModels.Abstract;
using System.Diagnostics;

namespace NotkaMobile.ViewModels.ListVM
{
	public partial class ListSortFilterViewModel : ASortFilterViewModel<ListForView, ListParameters>
	{
		public ListSortFilterViewModel(ListDataStore dataStore)
			: base("Sortowanie", dataStore)
		{
			LoadSelectedSortValue();
		}

		protected override void CreateSortItems()
		{
			SortItems.Clear();
			SortItems.Add(new SortClass(NoteSortValue.FromAtoZ, "Od A do Z"));
			SortItems.Add(new SortClass(NoteSortValue.FromZtoA, "Od Z do A"));
			SortItems.Add(new SortClass(NoteSortValue.ByCreationDateAscending, "Według daty utworzenia rosnąco"));
			SortItems.Add(new SortClass(NoteSortValue.ByCreationDateDescending, "Według daty utworzenia malejąco"));
			SortItems.Add(new SortClass(NoteSortValue.ByModificationDateAscending, "Według daty modyfikacji rosnąco"));
			SortItems.Add(new SortClass(NoteSortValue.ByModificationDateDescending, "Według daty modyfikacji malejąco"));
		}
		public override async Task OnExecute()
		{
			DataStore.Params.SortOrder = SelectedSortValue?.SortEnum.ToString() ?? string.Empty;
			await base.OnExecute();
		}
		public override async Task OnClear()
		{
			CreateSortItems();
			SelectedSortValue = null;
		}

		/// <summary>
		/// Loads initial value of SelectedSortValue from DataStore. The delay allows for correct loading of SelectedItem in CollectionView
		/// </summary>
		/// <returns></returns>
		private async Task LoadSelectedSortValue()
		{
			//FIXME think about moving this method to ASortFilterViewModel (as virtual)
			await Task.Delay(100);

			SelectedSortValue = SortItems.LastOrDefault();
			if (!string.IsNullOrWhiteSpace(DataStore.Params.SortOrder))
			{
				NoteSortValue localEnum = new();
				Enum.TryParse(DataStore.Params.SortOrder, out localEnum);
				SelectedSortValue = SortItems.FirstOrDefault(x => (NoteSortValue)x.SortEnum == localEnum);
			}
		}
	}
}
