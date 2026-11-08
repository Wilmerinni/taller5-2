namespace Backend;

// A generic doubly linked list that remains sorted upon insertion.
public class Node<T>
{
    // Fields
    private T _value;
    private Node<T>? _next;
    private Node<T>? _previous;

    // Constructors
    public Node(T value)
    {
        _value = value;
        _next = null;
        _previous = null;
    }

    // Properties
    public T Value
    {
        get => _value;
        set => _value = value;
    }

    public Node<T>? Next
    {
        get => _next;
        set => _next = value;
    }

    public Node<T>? Previous
    {
        get => _previous;
        set => _previous = value;
    }
}
