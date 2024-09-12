// Copyright (c) BIM Consulting s.r.o. (www.bimcon.cz)
// All rights reserved.
// Developed by BIM Consulting s.r.o. (www.bimcon.cz)

namespace IfcModelValidator.Helpers;
public class StringHelpers
{
    public static string FirstLetterToLower(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return string.Empty;
        }
        var first = input.First().ToString().ToLower();
        var rest = input[1..];
        return first + rest;
    }
}
