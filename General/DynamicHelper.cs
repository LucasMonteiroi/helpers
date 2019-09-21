
/// <summary>
/// Conversion dynamic object Helper and properties include
/// </summary>
public static class DynamicHelper
{
    /// <summary>
    /// Dynamic object conversion
    /// </summary>
    /// <param name="obj">Any Object</param>
    /// <returns>Dynamic Object (ExpandoObject)</returns>
    public static ExpandoObject ConvertToDynamicObject(object obj)
    {
        //Get Properties Using Reflections
        BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
        PropertyInfo[] properties = obj.GetType().GetProperties(flags);

        //Add Them to a new Expando
        ExpandoObject expando = new ExpandoObject();
        foreach (PropertyInfo property in properties)
        {
            AddProperty(expando, property.Name, property.GetValue(obj));
        }

        return expando;
    }

    /// <summary>
    /// Add Property on dynamic object
    /// </summary>
    /// <param name="expando">Dynamic Object</param>
    /// <param name="propertyName">Property name</param>
    /// <param name="propertyValue">Property value</param>
    public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
    {
        //Take use of the IDictionary implementation
        var expandoDict = expando as IDictionary<string, object>;
        if (expandoDict.ContainsKey(propertyName))
            expandoDict[propertyName] = propertyValue;
        else
            expandoDict.Add(propertyName, propertyValue);
    }

    public static void RemoveProperty(ExpandoObject expando, string propertyName)
    {
        //Take use of the IDictionary implementation
        var expandoDict = expando as IDictionary<string, object>;
        expandoDict.Remove(propertyName);
    }
}
