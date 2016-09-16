using System;

namespace Calculator.Common
{
    public static class Extensions
    {
        //public static string ToHex(this decimal input)
        //{
        //    return Convert.ToString(Convert.ToInt32(input), 16);
        //}

        //public static decimal ToDecimal(this string input)
        //{
        //    return decimal.Parse(input, System.Globalization.NumberStyles.HexNumber);
        //}

        //public static string ToBinary(this decimal input)
        //{
        //    return Convert.ToString(Convert.ToInt32(input), 2);

        //}

        //public static string ToOct(this decimal input)
        //{
        //    return Convert.ToString(Convert.ToInt32(input), 8);

        //}

        public static T ToEnum<T>(this object o)
        {
            T enumVal = (T)Enum.Parse(typeof(T), o.ToString());
            return enumVal;
        }

    }
}
