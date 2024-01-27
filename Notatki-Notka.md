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
* W kontrolerach można tworzyć własne metody CRUD z dodatkowymi parametrami. Metodę należy poprzedzić odpowiednim atrybutem określającym typ operacji CRUD i strukturę URL. Nazwa metody po stronie API może być dowolna, ale `Swagger` nie przekaże tej metody dalej i nada swoją generyczną, np. w przypadku dwóch getów zwracających `User` - `UserGET2Async`.


## .NET MAUI

* `MAUI` automatycznie konwertuje obrazy .svg na .png, dlatego w kodzie należy się odwoływać do pliku z rozszerzenim `.png`, mimo że w folderze `Images` będzie tylko plik .svg.
* Problem ze zbyt dużym poborem pamięci: pomóc może zastąpienie w widokach AllPage `CollectionView` przez `ListView` (patrz dokumentacja ListView).
* Drugi możliwy powód: DataStory są (na razie) singletonami i one mogą blokować garbage collection widoków i ich view-modeli.
* Trzeci możliwy powód: użycie `Command` zamiast `RelayCommand` [LINK][link5]
* [Nawigacja w `Shell`][link2]:
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

## [MediatR][link6]


## Czego się dziś nauczyłem?
1. 


[link1]: https://learn.microsoft.com/en-us/sql/ssms/scripting/generate-and-publish-scripts-wizard?view=sql-server-ver16
[link2]: https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-8.0
[link3]: https://learn.microsoft.com/en-us/dotnet/architecture/maui/navigation
[link4]: https://learn.microsoft.com/en-us/ef/ef6/saving/change-tracking/entity-state
[link5]: https://learn.microsoft.com/en-us/dotnet/architecture/maui/mvvm-community-toolkit-features
[link6]: https://cezarywalenciuk.pl/blog/programing/mediatr-cqrs-i-wzorzec-projektowy-mediator-w-aspnet-core