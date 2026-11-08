using ListasLigadas;

Console.WriteLine("Taller 5 - Lista doblemente ligada genérica");
Console.WriteLine("Puedes ingresar números (enteros o decimales) o texto (palabras, colores...).");

// La lista es genérica: aquí se usa con Dato, que sabe compararse como número o como texto.
new ListMenu<Dato>((string text, out Dato value) =>
{
    value = new Dato(text);
    return value.Text.Length > 0;
}).Run();
