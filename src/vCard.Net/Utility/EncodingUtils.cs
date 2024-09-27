using System;
using System.Text;

namespace vCard.Net.Utility;

/// <summary>
/// This class contains some static utility methods used to encode and decode data
/// </summary>
public static class EncodingUtils
{
    /// <summary>
    /// This method is used to Base 64 encode the specified string using the iso-8859-1
    /// encoding (Western European (ISO), Windows code page 1252) which works well for
    /// 8-bit binary data.
    /// </summary>
    /// <param name="encode">The string to encode.</param>
    /// <param name="foldWidth">
    /// If greater than zero, line folds are inserted at the specified interval with
    /// one leading space.
    /// </param>
    /// <param name="appendBlankLine">
    /// If true, an extra carriage return and line feed are appended to the end of the
    /// encoded data to satisfy the requirements of some specifications such as vCard
    /// 2.1. If false, they are not appended.
    /// </param>
    /// <returns>The Base 64 encoded string</returns>
    public static string ToBase64(this string encode, int foldWidth, bool appendBlankLine)
    {
        if (encode == null || encode.Length == 0)
        {
            return encode;
        }

        Encoding encoding = Encoding.GetEncoding("iso-8859-1");
        byte[] bytes = encoding.GetBytes(encode);
        StringBuilder stringBuilder = new StringBuilder(Convert.ToBase64String(bytes));
        if (foldWidth > 0)
        {
            for (int i = foldWidth - 1; i < stringBuilder.Length; i += foldWidth + 2)
            {
                stringBuilder.Insert(i, "\r\n ");
            }
        }

        if (appendBlankLine)
        {
            stringBuilder.Append("\r\n");
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// This method is used to decode the specified Base 64 encoded string using the
    /// iso-8859-1 encoding (Western European (ISO), Windows code page 1252) which works
    /// well for 8-bit binary data.
    /// </summary>
    /// <param name="decode">The string to decode</param>
    /// <returns>The decoded data as a string. This may or may not be a human-readable string.</returns>
    public static string FromBase64(this string decode)
    {
        if (decode == null || decode.Length == 0)
        {
            return decode;
        }

        Encoding encoding = Encoding.GetEncoding("iso-8859-1");
        return encoding.GetString(Convert.FromBase64String(decode));
    }

    /// <summary>
    /// This method is used to replace carriage returns, line feeds, commas, semi-colons,
    /// and backslashes within the string with an appropriate escape sequence (\r \n
    /// \, \; \\).
    /// </summary>
    /// <param name="escapeText">The string to escape</param>
    /// <returns>The escaped string</returns>
    public static string Escape(this string escapeText)
    {
        if (escapeText == null || escapeText.Length == 0 || escapeText.IndexOfAny(new char[5] { '\r', '\n', ',', ';', '\\' }) == -1)
        {
            return escapeText;
        }

        StringBuilder stringBuilder = new StringBuilder(escapeText, escapeText.Length + 100);
        stringBuilder.Replace("\\", "\\\\");
        stringBuilder.Replace(";", "\\;");
        stringBuilder.Replace(",", "\\,");
        stringBuilder.Replace("\r\n", "\\n");
        stringBuilder.Replace("\r", "\\n");
        stringBuilder.Replace("\n", "\\n");
        return stringBuilder.ToString();
    }

    /// <summary>
    /// This method is used to replace carriage returns, line feeds, and backslashes
    /// within the string with an appropriate escape sequence (\r \n \\). Commas and
    /// semi-colons are not escaped by this method.
    /// </summary>
    /// <param name="escapeText">The string to escape</param>
    /// <returns>The escaped string</returns>
    /// <remarks>
    /// This is mainly for vCard 2.1 properties in which commas and semi-colons should
    /// not be escaped.
    /// </remarks>
    public static string RestrictedEscape(this string escapeText)
    {
        if (escapeText == null || escapeText.Length == 0 || escapeText.IndexOfAny(new char[3] { '\r', '\n', '\\' }) == -1)
        {
            return escapeText;
        }

        StringBuilder stringBuilder = new StringBuilder(escapeText, escapeText.Length + 100);
        stringBuilder.Replace("\\", "\\\\");
        stringBuilder.Replace("\r\n", "\\n");
        stringBuilder.Replace("\r", "\\n");
        stringBuilder.Replace("\n", "\\n");
        return stringBuilder.ToString();
    }

    /// <summary>
    /// This method is used to unescape carriage returns, line feeds, commas, semi-colons,
    /// and backslashes within the string by replacing them with their literal characters.
    /// </summary>
    /// <param name="unescapeText">The string to unescape</param>
    /// <returns>The unescaped string</returns>
    /// <remarks>
    /// If any escaped single quotes, double quotes, or colons are encountered, they
    /// are also unescaped. The specifications do not state that they have to be escaped,
    /// but some implementations do, so they are handled here too just in case. However,
    /// they will not be escaped when written back out.
    /// </remarks>
    public static string Unescape(this string unescapeText)
    {
        if (unescapeText == null || unescapeText.Length == 0)
        {
            return null;
        }

        if (unescapeText.IndexOf('\\') == -1)
        {
            return unescapeText;
        }

        StringBuilder stringBuilder = new StringBuilder(unescapeText, unescapeText.Length + 100);
        stringBuilder.Replace("\\r\\n", "\r\n");
        stringBuilder.Replace("\\R\\N", "\r\n");
        stringBuilder.Replace("\\r", "\r\n");
        stringBuilder.Replace("\\n", "\r\n");
        stringBuilder.Replace("\\R", "\r\n");
        stringBuilder.Replace("\\N", "\r\n");
        stringBuilder.Replace("\\,", ",");
        stringBuilder.Replace("\\;", ";");
        stringBuilder.Replace("\\\\", "\\");
        stringBuilder.Replace("\\\"", "\"");
        stringBuilder.Replace("\\'", "'");
        stringBuilder.Replace("\\:", ":");
        return stringBuilder.ToString();
    }

    /// <summary>
    /// This method is used to encode the specified string as Quoted-Printable text
    /// </summary>
    /// <param name="encode">The string to encode.</param>
    /// <param name="foldWidth">If greater than zero, line folds with soft line breaks are inserted at the specified interval.</param>
    /// <returns>The Quoted-Printable encoded string</returns>
    /// <remarks>
    /// Character values 9, 32-60, and 62-126 go through as-is. All others are encoded
    /// as "=XX" where XX is the 2 digit hex value of the character (i.e. =0D=0A for
    /// a carriage return and line feed).
    /// </remarks>
    public static string ToQuotedPrintable(this string encode, int foldWidth)
    {
        if (encode == null || encode.Length == 0)
        {
            return encode;
        }

        StringBuilder stringBuilder = new StringBuilder(encode.Length + 100);
        foldWidth--;
        int i = 0;
        int num = 0;
        for (; i < encode.Length; i++)
        {
            if (encode[i] == '\t' || (encode[i] > '\u001f' && encode[i] < '=') || (encode[i] > '=' && encode[i] < '\u007f') || encode[i] > 'ÿ')
            {
                if (foldWidth > 0 && num + 1 > foldWidth)
                {
                    stringBuilder.Append("=\r\n");
                    num = 0;
                }

                stringBuilder.Append(encode[i]);
                num++;
            }
            else
            {
                if (foldWidth > 0 && num + 3 > foldWidth)
                {
                    stringBuilder.Append("=\r\n");
                    num = 0;
                }

                stringBuilder.AppendFormat("={0:X2}", (int)encode[i]);
                num += 3;
            }
        }

        return stringBuilder.ToString();
    }

    /// <summary>
    /// This method is used to decode the specified Quoted-Printable encoded string
    /// </summary>
    /// <param name="decode">The string to decode</param>
    /// <returns>The decoded data as a string</returns>
    public static string FromQuotedPrintable(this string decode)
    {
        if (decode == null || decode.Length == 0 || decode.IndexOf('=') == -1)
        {
            return decode;
        }

        StringBuilder stringBuilder = new StringBuilder(decode.Length);
        string text = "0123456789ABCDEF";
        for (int i = 0; i < decode.Length; i++)
        {
            if (decode[i] == '=' && i + 2 <= decode.Length)
            {
                if (decode[i + 1] == '\r' && decode[i + 2] == '\n')
                {
                    i += 2;
                    continue;
                }

                int num = text.IndexOf(decode[i + 1]);
                int num2 = text.IndexOf(decode[i + 2]);
                if (num != -1 && num2 != -1)
                {
                    i += 2;
                    stringBuilder.Append((char)(num * 16 + num2));
                }
            }
            else
            {
                stringBuilder.Append(decode[i]);
            }
        }

        return stringBuilder.ToString();
    }
}