namespace Gigo;


/*************************************
 * Bennett, Neta (netab)
 * CSHP-310
 *************************************/

public class Gigo
{
    public event Action DecimalDetected = delegate { };
    private int _id;
    public Gigo(int i)
    {
        _id = i;
    }

    public object? Consume(object? paramObj)
    {
        if (paramObj == null) return null;

        var dataType = paramObj.GetType().ToString();

        switch(dataType)
        {
            case "System.Int32":
                {
                    return ConsumeInt32((int)paramObj);
                }
            case "System.String":
                {
                    return ConsumeString((string)paramObj);
                }
            case "System.Character":
                {
                    return ConsumeCharacter((char)paramObj);
                }
            case "System.Double":
                {
                    return ConsumeDouble((double)paramObj);
                }
            case "System.Decimal":
                {
                    return ConsumeDecimal((decimal)paramObj);
                }
            default:
                {
                    return null;
                }
        }
    }

    private object? ConsumeDecimal(decimal paramObj)
    {
        DecimalDetected();
        return null;
    }

    private object ConsumeDouble(double paramObj)
    {
        throw new ArgumentException("We don't take doubles at the Gigo.");
    }

    private object? ConsumeCharacter(char paramObj)
    {
        return ConsumeString(paramObj.ToString());
    }

    private object? ConsumeString(string paramObj)
    {
        object? returnObj = null;
        if (paramObj.Length == 1)
        {
            var vowels = new string[] { "a", "e", "i", "o", "u" };
            if (vowels.Contains(paramObj))
            {
                returnObj = paramObj.ToUpper();
            }
        } 
        else if ("Answer".Equals(paramObj))
        {
            returnObj = 42;
        }

        return returnObj;
    }

    private object? ConsumeInt32(int paramObj)
    {
        object? returnObj = null;

        if (paramObj == 0) returnObj = 0;

        if (paramObj >= 1 && paramObj <= 100)
        {
            returnObj = isEven(paramObj) ? 0 : paramObj;
        }

        return returnObj;
    }

    private bool isEven(int paramObj)
    {
        return paramObj % 2 == 0;
    }
}