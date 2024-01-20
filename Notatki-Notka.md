# Aplikacja Notka

## Baza danych

Tabela `Login` - zawiera wiele wpisów o jednym Userze, aby udokumentować, na których urządzeniach był/jest zalogowany. Śledzenie, na których urządzeniach nastąpiło kiedykolwiek logowanie mogłoby również odbywać się poprzez lokalnie zapisywane pliki (np. xml)

`EF Core`:
* `List<T>` jest wydajne dla niewielkiej liczby powiązanych tabel i zachowuje stabilną kolejność
* `HashSet<T>` ma bardziej wydajne wyszukiwanie dla dużej liczby tabel, ale nie zachowuje stabilnej kolejności. Jeśli nawigacja jest zapisana w kodzie jako `IEnumerable`, `ICollection` albo `ISet`, to zostanie zainicjalizowana jako `HashSet`
* Pairing of relationships only works when there is a single relationship between two types. Multiple relationships between two types must be configured explicitly.
* Generowanie skryptów BD albo konkretnych obiektów w [MS SQL Server Management Studio][link1].

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


`MAUI`
* MAUI automatycznie konwertuje obrazy .svg na .png, dlatego w kodzie należy się odwoływać do pliku z rozszerzenim `.png`, mimo że w folderze `Images` będzie nadal tylko plik .svg.



[link1]: https://learn.microsoft.com/en-us/sql/ssms/scripting/generate-and-publish-scripts-wizard?view=sql-server-ver16