using System;
using System.Collections;
using UnityEngine;

public class ConvertType {

    public static object Convert(object source, Type destinationType)
    {
        if (source == null)
        {
            return null;
        }

        var sourceType = source.GetType();

        // unwrap nullable types
        var nullableType = Nullable.GetUnderlyingType(destinationType);
        if (nullableType != null)
        {
            destinationType = nullableType;
        }

        nullableType = Nullable.GetUnderlyingType(sourceType);
        if (nullableType != null)
        {
            sourceType = nullableType;
        }


        var implicitCastMethod =
            destinationType.GetMethod("op_Implicit",
                                 new[] { sourceType });

        if (implicitCastMethod == null)
        {
            return null;
        }

        return implicitCastMethod.Invoke(null, new[] { source });
    }
}
