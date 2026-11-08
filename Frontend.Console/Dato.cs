namespace ListasLigadas;

// What the user enters in the menu is compared as a number if both inputs are numeric
// (so 2 ​​comes before 10), and as text in all other cases (Yellow, Blue...).
// If the two types are mixed, numbers come before text.
public class Dato : IComparable<Dato>
{
    // Fields
    private readonly string _text;
    private readonly bool _isNumber;
    private readonly double _number;

    // Constructors
    public Dato(string text)
    {
        _text = text.Trim();
        _isNumber = double.TryParse(_text, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out _number)
            && double.IsFinite(_number);
    }

    // Properties
    public string Text
    {
        get => _text;
    }

    // Public Methods
    public int CompareTo(Dato? other)
    {
        if (other == null)
        {
            return 1;
        }
        if (_isNumber && other._isNumber)
        {
            return _number.CompareTo(other._number);
        }
        if (_isNumber)
        {
            return -1;
        }
        if (other._isNumber)
        {
            return 1;
        }
        return string.Compare(_text, other._text, StringComparison.CurrentCultureIgnoreCase);
    }

    public override string ToString()
    {
        return _text;
    }
}
