namespace Backend;

// Lista doblemente ligada genérica que se mantiene ordenada al insertar.
// T debe poder compararse (IComparable<T>) para saber dónde va cada elemento.
public class DoublyLinkedList<T> where T : IComparable<T>
{
    // Fields
    private Node<T>? _head;
    private Node<T>? _tail;
    private int _count;
    private bool _isAscending;

    // Constructors
    public DoublyLinkedList()
    {
        _head = null;
        _tail = null;
        _count = 0;
        _isAscending = true;
    }

    // Properties
    public Node<T>? Head
    {
        get => _head;
    }

    public Node<T>? Tail
    {
        get => _tail;
    }

    public int Count
    {
        get => _count;
    }

    public bool IsEmpty
    {
        get => _count == 0;
    }

    public bool IsAscending
    {
        get => _isAscending;
    }

    // Public Methods

    // Inserta el valor directamente en la posición que le corresponde.
    // Si ya hay valores iguales, el nuevo queda después de ellos.
    public void Add(T value)
    {
        var node = new Node<T>(value);

        if (_head == null)
        {
            _head = node;
            _tail = node;
            _count++;
            return;
        }

        // Se busca el primer nodo que debe quedar DESPUÉS del nuevo.
        var current = _head;
        while (current != null && Compare(current.Value, value) <= 0)
        {
            current = current.Next;
        }

        if (current == null)
        {
            // Va al final.
            node.Previous = _tail;
            _tail!.Next = node;
            _tail = node;
        }
        else if (current == _head)
        {
            // Va al inicio.
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }
        else
        {
            // Va en medio, justo antes de current.
            node.Previous = current.Previous;
            node.Next = current;
            current.Previous!.Next = node;
            current.Previous = node;
        }
        _count++;
    }

    // Recorre la lista desde la cabeza hasta la cola.
    public List<T> GetForward()
    {
        var result = new List<T>();
        var current = _head;
        while (current != null)
        {
            result.Add(current.Value);
            current = current.Next;
        }
        return result;
    }

    // Recorre la lista desde la cola hasta la cabeza, usando los enlaces "anterior".
    public List<T> GetBackward()
    {
        var result = new List<T>();
        var current = _tail;
        while (current != null)
        {
            result.Add(current.Value);
            current = current.Previous;
        }
        return result;
    }

    // Cambia el orden real de la lista (no solo la muestra al revés):
    // como la lista siempre está ordenada, invertir los enlaces de cada nodo
    // la deja en orden descendente. Las inserciones siguientes respetan ese orden.
    public void SortDescending()
    {
        if (!_isAscending)
        {
            return;
        }

        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            current.Next = current.Previous;
            current.Previous = next;
            current = next;
        }
        (_head, _tail) = (_tail, _head);
        _isAscending = false;
    }

    // Cuántas veces aparece cada valor, en el orden en que están en la lista.
    // Como la lista está ordenada, los repetidos quedan juntos.
    public List<(T Value, int Count)> GetOccurrences()
    {
        var result = new List<(T Value, int Count)>();
        var current = _head;
        while (current != null)
        {
            var value = current.Value;
            var count = 0;
            while (current != null && value.CompareTo(current.Value) == 0)
            {
                count++;
                current = current.Next;
            }
            result.Add((value, count));
        }
        return result;
    }

    // La moda es el valor que más veces aparece; si hay empate, devuelve todas.
    public (List<T> Modes, int Frequency) GetModes()
    {
        var modes = new List<T>();
        var frequency = 0;
        foreach (var (value, count) in GetOccurrences())
        {
            if (count > frequency)
            {
                frequency = count;
                modes.Clear();
                modes.Add(value);
            }
            else if (count == frequency)
            {
                modes.Add(value);
            }
        }
        return (modes, frequency);
    }

    // Dice si existe al menos un elemento con ese valor.
    public bool Exists(T value)
    {
        return FindNode(value) != null;
    }

    // Elimina solo la primera ocurrencia encontrada.
    public bool RemoveFirst(T value)
    {
        var node = FindNode(value);
        if (node == null)
        {
            return false;
        }
        RemoveNode(node);
        return true;
    }

    // Elimina todas las ocurrencias y devuelve cuántas eliminó.
    public int RemoveAll(T value)
    {
        var removed = 0;
        var current = _head;
        while (current != null)
        {
            var next = current.Next;
            if (current.Value.CompareTo(value) == 0)
            {
                RemoveNode(current);
                removed++;
            }
            current = next;
        }
        return removed;
    }

    // Private Methods

    // Compara respetando el orden actual de la lista (ascendente o descendente).
    private int Compare(T first, T second)
    {
        return _isAscending ? first.CompareTo(second) : second.CompareTo(first);
    }

    private Node<T>? FindNode(T value)
    {
        var current = _head;
        while (current != null)
        {
            if (current.Value.CompareTo(value) == 0)
            {
                return current;
            }
            current = current.Next;
        }
        return null;
    }

    // Desconecta el nodo: el anterior y el siguiente pasan a apuntarse entre sí.
    private void RemoveNode(Node<T> node)
    {
        if (node.Previous != null)
        {
            node.Previous.Next = node.Next;
        }
        else
        {
            _head = node.Next;
        }

        if (node.Next != null)
        {
            node.Next.Previous = node.Previous;
        }
        else
        {
            _tail = node.Previous;
        }

        node.Next = null;
        node.Previous = null;
        _count--;
    }
}
