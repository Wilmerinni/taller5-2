using ListasLigadas;

Console.WriteLine("Lista doblemente ligada genérica");
Console.WriteLine("wilmerholguin");
Console.WriteLine("================================");

new ListMenu<Dato>((string text, out Dato value) =>
{
    value = new Dato(text);
    return value.Text.Length > 0;
}).Run();
