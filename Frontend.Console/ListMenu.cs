using Backend;

namespace ListasLigadas;

// A delegate that tries to parse a string into a value of type T.
public delegate bool TryParse<T>(string text, out T value);

// A menu for interacting with a generic doubly linked list.
public class ListMenu<T> where T : IComparable<T>
{
    // Fields
    private readonly DoublyLinkedList<T> _list;
    private readonly TryParse<T> _tryParse;

    // Constructors
    public ListMenu(TryParse<T> tryParse)
    {
        _list = new DoublyLinkedList<T>();
        _tryParse = tryParse;
    }

    // Public Methods
    public void Run()
    {
        var option = -1;
        while (option != 0)
        {
            ShowMenu();
            option = ReadOption();
            Console.WriteLine();

            switch (option)
            {
                case 1:
                    Add();
                    break;
                case 2:
                    ShowForward();
                    break;
                case 3:
                    ShowBackward();
                    break;
                case 4:
                    SortDescending();
                    break;
                case 5:
                    ShowModes();
                    break;
                case 6:
                    ShowGraph();
                    break;
                case 7:
                    Exists();
                    break;
                case 8:
                    RemoveOne();
                    break;
                case 9:
                    RemoveAll();
                    break;
                case 0:
                    Console.WriteLine("¡Hasta luego!");
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }

            if (option != 0)
            {
                Console.WriteLine();
                Console.Write("Presiona Enter para continuar...");
                Console.ReadLine();
            }
        }
    }
    //====MENU METHODS====
    // Private Methods
    private void ShowMenu()
    {
        Console.WriteLine();
        Console.WriteLine("===== LISTA DOBLEMENTE LIGADA =====");
        Console.WriteLine("1. Adicionar.                 6. Mostrar gráfico.");
        Console.WriteLine("2. Mostrar hacia adelante.    7. Existe.");
        Console.WriteLine("3. Mostrar hacia atrás.       8. Eliminar una ocurrencia.");
        Console.WriteLine("4. Ordenar descendentemente.  9. Eliminar todas las ocurrencias.");
        Console.WriteLine("5. Mostrar la(s) moda(s).     0. Salir.");
        Console.Write("Opción: ");
    }

    private int ReadOption()
    {
        return int.TryParse(Console.ReadLine(), out var option) ? option : -1;
    }

    private void Add()
    {
        if (!ReadValue("Elemento a adicionar: ", out var value))
        {
            return;
        }
        _list.Add(value);
        Console.WriteLine($"Se adicionó {Format(value)}. Lista: {FormatList(_list.GetForward())}");
    }

    private void ShowForward()
    {
        if (IsEmptyMessage())
        {
            return;
        }
        Console.WriteLine($"Hacia adelante: {FormatList(_list.GetForward())}");
    }

    private void ShowBackward()
    {
        if (IsEmptyMessage())
        {
            return;
        }
        Console.WriteLine($"Hacia atrás: {FormatList(_list.GetBackward())}");
    }

    private void SortDescending()
    {
        if (IsEmptyMessage())
        {
            return;
        }
        if (!_list.IsAscending)
        {
            Console.WriteLine("La lista ya está ordenada descendentemente.");
            return;
        }
        _list.SortDescending();
        Console.WriteLine($"Lista ordenada descendentemente: {FormatList(_list.GetForward())}");
    }

    private void ShowModes()
    {
        if (IsEmptyMessage())
        {
            return;
        }
        var (modes, frequency) = _list.GetModes();
        if (modes.Count == 1)
        {
            Console.WriteLine($"La moda es {Format(modes[0])} (aparece {frequency} veces).");
        }
        else
        {
            var text = string.Join(", ", modes.Take(modes.Count - 1).Select(Format)) + " y " + Format(modes[^1]);
            Console.WriteLine($"Las modas son {text} (cada una aparece {frequency} veces).");
        }
    }

    private void ShowGraph()
    {
        if (IsEmptyMessage())
        {
            return;
        }
        foreach (var (value, count) in _list.GetOccurrences())
        {
            Console.WriteLine($"{Format(value)} {new string('*', count)}");
        }
    }

    private void Exists()
    {
        if (!ReadValue("Elemento a buscar: ", out var value))
        {
            return;
        }
        if (_list.Exists(value))
        {
            Console.WriteLine($"Sí existe el {Format(value)} en la lista.");
        }
        else
        {
            Console.WriteLine($"No existe el {Format(value)} en la lista.");
        }
    }

    private void RemoveOne()
    {
        if (!ReadValue("Elemento a eliminar (ocurrencia): ", out var value))
        {
            return;
        }
        if (_list.RemoveFirst(value))
        {
            Console.WriteLine($"Se eliminó una ocurrencia de {Format(value)}. Lista: {FormatList(_list.GetForward())}");
        }
        else
        {
            Console.WriteLine($"No existe el {Format(value)}, no se eliminó nada.");
        }
    }

    private void RemoveAll()
    {
        if (!ReadValue("Elemento a eliminar (todas las ocurrencias): ", out var value))
        {
            return;
        }
        var removed = _list.RemoveAll(value);
        if (removed > 0)
        {
            Console.WriteLine($"Se eliminaron {removed} ocurrencias de {Format(value)}. Lista: {FormatList(_list.GetForward())}");
        }
        else
        {
            Console.WriteLine($"No existe el {Format(value)}, no se eliminó nada.");
        }
    }

    private bool ReadValue(string message, out T value)
    {
        Console.Write(message);
        var text = Console.ReadLine();
        if (text != null && _tryParse(text, out value))
        {
            return true;
        }
        value = default!;
        Console.WriteLine("Valor inválido: escribe al menos un carácter.");
        return false;
    }

    private bool IsEmptyMessage()
    {
        if (_list.IsEmpty)
        {
            Console.WriteLine("La lista está vacía.");
            return true;
        }
        return false;
    }

    private string Format(T item)
    {
        return Convert.ToString(item, System.Globalization.CultureInfo.InvariantCulture) ?? string.Empty;
    }

    private string FormatList(List<T> items)
    {
        return string.Join(", ", items.Select(Format));
    }
}
