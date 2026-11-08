using ListasLigadas;

Console.WriteLine("Lista doblemente ligada genérica");
Console.WriteLine("wilmerholguin");
Console.WriteLine("================================");

// La lista es genérica: aquí se usa con Dato, que sabe compararse como número o como texto.
new ListMenu<Dato>((string text, out Dato value) =>
{
    value = new Dato(text);
    return value.Text.Length > 0;
}).Run();
