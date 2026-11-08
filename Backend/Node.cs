namespace Backend;

// Nodo de la lista doblemente ligada: guarda un valor y apunta al siguiente y al anterior.
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
