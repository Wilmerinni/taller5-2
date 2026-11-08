namespace Backend;

// // A generic doubly linked list that remains sorted upon insertion.
// T must be comparable (IComparable<T>) so that the position of each element can be determined.
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

    // IInserts the value directly into its corresponding position. 
// If identical values ​​already exist, the new one is placed after them.
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

        // Find the first node that should come AFTER the new one.
        var current = _head;
        while (current != null && Compare(current.Value, value) <= 0)
        {
            current = current.Next;
        }

        if (current == null)
        {
            // to finish
            node.Previous = _tail;
            _tail!.Next = node;
            _tail = node;
        }
        else if (current == _head)
        {
            // to start
            node.Next = _head;
            _head.Previous = node;
            _head = node;
        }
        else
        {
            // to middle
            node.Previous = current.Previous;
            node.Next = current;
            current.Previous!.Next = node;
            current.Previous = node;
        }
        _count++;
    }

    // Traverse the list from head to tail.
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

    // Traverse the list from tail to head.
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

    // Changes the actual order of the list (not just its display order):
    // since the list is always sorted, reversing the links of each node
    // puts it in descending order. Subsequent insertions will respect this order.
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

    // How many times each value appears, in the order in which they occur in the list. 
    // Since the list is sorted, repeated values ​​are grouped together.
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

    // If there is a tie, return all of them.
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

    // there exists at least one element with that value.
    public bool Exists(T value)
    {
        return FindNode(value) != null;
    }

    // Removes the first occurrence of the value and returns true if it was found and removed, or false if it was not found.
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

    // Removes all occurrences of the value and returns how many were removed.
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

    // Compares two values ​​according to the current order of the list.
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

    // Removes the node from the list and updates the links of its neighbors.
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
