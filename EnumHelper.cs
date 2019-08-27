/// <summary>
/// Helper to manipule enum linked attributes
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// Get data of description attribute linked an enum value
    /// </summary>
    /// <param name="enumType">Type of Enumerator.</param>
    /// <param name="value">Enum item.</param>
    /// <returns>Description of attribute.</returns>
    public static string Description(this Type enumType, int value)
    {
        if ((enumType == null) || !enumType.GetTypeInfo().IsEnum)
        {
            throw new Exception("The Type should be a Enum object.");
        }

        var customAttributes = enumType.GetMember(Enum.GetName(enumType, value))[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        if ((customAttributes != null) && (customAttributes.Count() > 0))
        {
            foreach (object obj2 in customAttributes)
            {
                if (obj2.GetType() == typeof(DescriptionAttribute))
                {
                    return ((DescriptionAttribute)obj2).Description;
                }
            }
        }

        return null;
    }

    /// <summary>
    /// Get attribute description from enum.
    /// </summary>
    /// <param name="value">Enumerator.</param>
    /// <returns>Description of attribute.</returns>
    public static string GetDescription(this System.Enum value)
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(
            typeof(DescriptionAttribute),
            false);

        if (attributes != null &&
            attributes.Length > 0)
            return attributes[0].Description;
        return value.ToString();
    }
}