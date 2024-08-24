// IfcModelValidator validates IFC models against given data standard.
// Copyright (C) 2023 Lukas Kohout

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IfcModelValidator.Helpers;
public class StringHelpers
{
    public static string FirstLetterToLower(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;

        var first = input.First().ToString().ToLower();
        var rest = input[1..];
        return first + rest;
    }

}
