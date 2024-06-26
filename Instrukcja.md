# Instrukcja

## Dodawanie nowego widoku

### API

Na potrzeby instrukcji zakładam, że tworzę obsługę tabeli `Note`:
1. `NoteForView` i zależne klasy (np. `TagForView`)
2. Stworzenie odpowiedniej metody w `ModelConverters.cs`, np. `ConvertToNoteForView`
2. `NoteParameters`
3. `INoteRepository`
4. `NoteRepository` - implementacja interfejsu i przerobienie metod na asynchroniczne (async)
5. Wpis do `IRepositoryWrapper` + `RepositoryWrapper`
6. `NoteController`


### Aplikacja mobilna
1. DataStore - stworzenie nowego dziedzczącego z AListDataStore (przerobienie metod na asynchroniczne)
2. ViewModel
3. View

### Aplikacja desktopowa
1. DataStore
2. Dodać wpisy do enuma `MainWindowView` tożsame ze stronami, na które można przechodzić.
3. ViewModele (Lista, New, Edit)
4. View (dziedziczenie z klasy generycznej?)
5. Wpis do `MainWindowViewModel.cs` odnośnie tworzonej zakładki (tworzenie przycisku, nawigacja)