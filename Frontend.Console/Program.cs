using ListasLigadas;

Console.WriteLine("=================MENU=================");
Console.WriteLine("================================");

new ListMenu<Dato>((string text, out Dato value) =>
{
    value = new Dato(text);
    return value.Text.Length > 0;
}).Run();
