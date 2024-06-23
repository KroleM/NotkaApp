# Aplikacja Notka

## Baza danych

Tabela `Login` - zawiera wiele wpisów o jednym Userze, aby udokumentować, na których urządzeniach był/jest zalogowany. Śledzenie, na których urządzeniach nastąpiło kiedykolwiek logowanie mogłoby również odbywać się poprzez lokalnie zapisywane pliki (np. xml)

`EF Core`:
* `List<T>` jest wydajne dla niewielkiej liczby powiązanych tabel i zachowuje stabilną kolejność
* `HashSet<T>` ma bardziej wydajne wyszukiwanie dla dużej liczby tabel, ale nie zachowuje stabilnej kolejności. Jeśli nawigacja jest zapisana w kodzie jako `IEnumerable`, `ICollection` albo `ISet`, to zostanie zainicjalizowana jako `HashSet`
* Pairing of relationships only works when there is a single relationship between two types. Multiple relationships between two types must be configured explicitly.
* Generowanie skryptów BD albo konkretnych obiektów w [MS SQL Server Management Studio][link1].
* Operacje PUT - [modyfikowanie rekordów w BD][link4].
* `SaveChanges(Async)` wysyła wartości właściwości danej encji do bazy danych.
* [Serwer vs. Client][link9] - rozwiązywanie kwerend IQueryable vs. IEnumerable.
* Usunięcie ostatniej migracji można wykonać poleceniem `Remove-Migration`

`Swagger`:
* Właściwości będące typami referencyjnymi nie są standardowo udostępniane jako `nullable`, ale właściwości będące typami wartościowymi (np. int?, DateTime?) mogą być "nulowalne". Istotą problemu jest brak nullowalności typów referencyjnych. Aby to zmienić należy w konfiguracji projektu Web API użyć opcji `UseAllOfToExtendReferenceSchemas`:
    ```c#
    builder.Services.AddSwaggerGen(options =>
    {
        options.UseAllOfToExtendReferenceSchemas();
    });
    ```
    "This method is used to extend reference schemas when using the allOf keyword in your API models. It ensures that the generated Swagger document accurately represents the inheritance hierarchy and relationships between different models."\
    Źródło:\
    https://stackoverflow.com/questions/40920441/how-to-specify-a-property-can-be-null-or-a-reference-with-swagger \
    https://github.com/domaindrivendev/Swashbuckle.AspNetCore/issues/1589
* W kontrolerach można tworzyć własne metody CRUD z dodatkowymi parametrami. Metodę należy poprzedzić odpowiednim atrybutem określającym typ operacji CRUD i strukturę URL. Nazwa metody po stronie API może być dowolna, ale żeby `Swagger` przypisał niestandardową nazwę do metody trzeba to określić w atrybucie, np `[HttpGet("{email}/{hash}", Name = "GetUserWithAuth")]`

`ASP.NET Web API`:
* Istnieje [kilka sposobów na przekazywanie parametrów][link7] do metod CRUD w Web API. Swagger tworzy kontrolery wykorzystujące `Route Parameters`. Przykłady:
    - Query parameters: `https://localhost:7089/api/Tag?userId=1&id=2`
    - Route parameters: `https://localhost:7089/api/Tag/1/2`
    - kombinacja obydwu powyższych, np.: `https://localhost:7089/api/Tag/1?id=2`
    - Request Body - przydatny atrybut [FromBody]
    - Header Parameter - przydatny atrybut [FromHeader] - dobry sposób na wysyłanie wrażliwych danych, np. związanych z uwierzytelnianiem.
* Metody typu `IActionResult`:
    - OK => returns the 200 status code
    - NotFound => returns the 404 status code
    - BadRequest => returns the 400 status code
    - NoContent => returns the 204 status code
    - Created, CreatedAtRoute, CreatedAtAction => returns the 201 status code
    - Unauthorized => returns the 401 status code
    - Forbid => returns the 403 status code
    - StatusCode => returns the status code we provide as input
* Trochę o generowanym pliku `swaggerClient.cs` - [LINK][link11], [LINK][link12]


## .NET MAUI

* `MAUI` automatycznie konwertuje obrazy .svg na .png, dlatego w kodzie należy się odwoływać do pliku z rozszerzenim `.png`, mimo że w folderze `Images` będzie tylko plik .svg.
* Problem ze zbyt dużym poborem pamięci [(wyciek)][link8]: pomóc może zastąpienie w widokach AllPage `CollectionView` przez `ListView` (patrz dokumentacja ListView).
* Drugi możliwy powód: DataStory są (na razie) singletonami i one mogą blokować garbage collection widoków i ich view-modeli.
* Trzeci możliwy powód: użycie `Command` zamiast `RelayCommand` [LINK][link5]
* [Nawigacja w `Shell`][link2]:
    - `GoToAsync()` operuje na ścieżkach, które są układane na stosie, czyli tak jakby strony są singletonami. Np. moje notatki to zawsze ta sama strona (ten sam VM), ale detale notatki to nowe strony (i nowe VM). Z detali nie można zrobić `GoToAsync(Notes)`, tylko trzeba cofać się na stosie `GoToAsync(..)`. 
    - `View-first` vs. [`ViewModel-first`][link3] Navigation
    - `await Shell.Current.GoToAsync("../../route");` podwójna nawigacja wstecz i do konkretnej ścieżki
    - W nawigacji przez Shell można **przekazywać** dowolne argumenty (np. obiekty), również we wstecznej nawigacji.\
        - Prosta nawigacja z argumentem typu string:
            ```c#
            await Shell.Current.GoToAsync($"elephantdetails?name={elephantName}");
            ```
        - Nawigacja z przekazaniem wielu argumentów odbywa się dzięki przeciążeniu metody `GoToAsync`, która jako argument przyjmuje `IDictionary<string, object>`:
            ```c#
            var navigationParameter = new Dictionary<string, object>
            {
                { "Bear", animal }
            };
            await Shell.Current.GoToAsync($"beardetails", navigationParameter);
            ```
            `animal` jest typu Animal.\
            Uwaga: dane przekazane jako `IDictionary<string, object>` są przechowywane w pamięci przez całe życie strony (do momentu usunięcia z nawigacyjnego stosu). Jeśli nie jest to pożądane, to wspomniane IDictionary należy wyczyścić metodą `Clear` po otrzymaniu przez daną stronę.\
        - `GoToAsync` może również przyjąć argument typu `ShellNavigationQueryParameters` i wtedy przekazywany obiekt jest czyszczony po wykonaniu nawigacji.
    - **Otrzymywanie** danych nawigacyjnych może następować na 2 sposoby:
        - Klasę, która reprezentuje stronę, do której następuje przejście, lub klasę przypisywaną do BindingContext tej strony, należy poprzedzić `QueryPropertyAttribute` dla każdego (dowolnego typu) parametru zapytania, np.:
            ```c#
            [QueryProperty(nameof(Bear), "Bear")]
            ```
            gdzie pierwszy argument to nazwa właściwości, która otrzyma dane; drugi argument to id parametru.\
            Otrzymane stringowe argumenty są automatycznie dekodowane na sposób URL.
        - Klasa, która reprezentuje stronę, do której następuje przejście, lub klasę przypisywaną do BindingContext tej strony, implementuje interfejs `IQueryAttributable`. Zaletą tego podejścia jest to, że dane nawigacyjne mogą być przetwarzane przy użyciu jednej metody, co może być przydatne w przypadku wielu elementów danych nawigacyjnych, które wymagają przetwarzania jako całość.
    - Przekazywanie wielu stringowych argumentów:
        ```c#
        await Shell.Current.GoToAsync($"elephantdetails?name={elephantName}&location={elephantLocation}");
        ```
* [Formatowanie daty][link10] w warstwie widoku.
* Ciekawą funkcją widoku jest `ActivityIndicator` - może pokazywać ładowanie danych. Przykład jest w projekcie `MonkeyFinder` (dotnet-maui-workshop-main).
* Wszystkie daty zapisywane w BD powinny być według uniwersalnego czasu, zaś ich ładowanie w aplikacji powinno konwertować je na czas lokalny (problem związany z lokalizacją). W BD użyty jest typ `DateTimeOffset`, a przy odczycie w aplikacji należy użyć konwersji `CreatedDate.LocalDateTime`.
* `SearchBar` na Androidzie i iOS nie odpala komendy dla pustej wartości. [Obejście][link13]
* `Bottom sheet` - okienko wyjeżdżające z dołu ekranu. Są w internecie różne darmowe implementacje: [Link1][link14], [Link2][link15]
* Ustawianie `SelectedItem` dla `CollectionView` programistycznie (w VM) nie działa w oczekiwany sposób - `VisualStateManager` nie zmienia stanu elementów i wybrany obiekt nie zmienia się. Link do [śledzenia tej sprawy][link17] i [drugi][link16].
* Jeśli `CollectionView` jest zagnieżdżone w `StackLayout`, to może się niepoprawnie scrollować. Należy umieszczać je w Gridzie.


## [MediatR][link6]


## Czego się dziś nauczyłem?
 * 2024.02.19\
    Do solucji `NotkaAPI` dodałem osobny projekt typu `Class Library`, aby współdzielić go z `NotkaMobile`, w której to (jako że jest w osobnej solucji) musiałem najpierw dodać do solucji `ApiSharedClasses` jako 'external project', a następnie referencję do tego projektu z `NotkaMobile`.
 * 2024.03.03\
    Vertical/HorizontalStackLayout gryzie się z `CollectionView` i uniemożliwia scrollowanie. Sposób: użyć Grid.\
    W WebAPI należy nadawać nazwy metod kontrolerów. W przeciwnym razie mogą zostać nadane dość losowe nazwy. Przykład:
    ```
    [HttpGet("/orders/{id}", Name = nameof(GetOrder))]
    ```
    ConnectedServices -> Service References -> `swaggerClient.cs` - wygenerowany kod można rozszerzać poprzez utworzenie własnej klasy częściowej (partial) o tej samej nazwie. Daje to pewność, że w razie aktualizacji klienta HTTP wprowadzony kod nie zostanie utracony.
* 2024.06.19 Gdy w API dwie ścieżki tego samego typu (Routes), np. GET, mają taką samą liczbę argumentów, to rozróżnić je można po `route constraint`. Atrybut ma wówczas taką postać: `[HttpGet("{userId:int}")]`


[link1]: https://learn.microsoft.com/en-us/sql/ssms/scripting/generate-and-publish-scripts-wizard?view=sql-server-ver16
[link2]: https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-8.0
[link3]: https://learn.microsoft.com/en-us/dotnet/architecture/maui/navigation
[link4]: https://learn.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state
[link5]: https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm-community-toolkit-features
[link6]: https://cezarywalenciuk.pl/blog/programing/mediatr-cqrs-i-wzorzec-projektowy-mediator-w-aspnet-core
[link7]: https://code-maze.com/aspnetcore-pass-parameters-to-http-get-action/
[link8]: https://learn.microsoft.com/en-us/dotnet/core/diagnostics/debug-memory-leak
[link9]: https://learn.microsoft.com/en-us/ef/core/querying/client-eval
[link10]: https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
[link11]: https://stevetalkscode.co.uk/openapireference-commands
[link12]: https://devblogs.microsoft.com/dotnet/creating-discoverable-http-apis-with-asp-net-core-5-web-api/
[link13]: https://stackoverflow.com/questions/76818812/searchbar-in-maui-doesnt-fire-the-searchcommand-if-the-text-is-empty-in-ios
[link14]: https://blogs.xgenoapps.com/post/2022/07/23/maui-bottom-sheet
[link15]: https://docs.devexpress.com/MAUI/DevExpress.Maui.Controls.BottomSheet?v=23.1
[link16]: https://stackoverflow.com/questions/75593079/programmatically-setting-the-selecteditem-of-a-collectionview-is-not-working-on
[link17]: https://github.com/dotnet/maui/issues/18933