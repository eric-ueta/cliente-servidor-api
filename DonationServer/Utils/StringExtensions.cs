using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace DonationServer.Utils
{
    public static partial class Extensions
    {
        #region Methods

        public static byte[] ConvertHexStringToByteArray(this string hexString)
        {
            try
            {
                hexString = hexString.Trim();
                hexString = hexString.Replace("-", "");

                if (hexString.Length % 2 != 0)
                {
                    //erro
                    return new byte[1];
                }

                byte[] HexAsBytes = new byte[hexString.Length / 2];

                for (int index = 0; index < HexAsBytes.Length; index++)
                {
                    string byteValue = hexString.Substring(index * 2, 2);
                    HexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                }

                return HexAsBytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string ConvertHexToASCII(this String hexString)
        {
            try
            {
                hexString = hexString.Replace("-", "");
                string ascii = string.Empty;

                for (int i = 0; i < hexString.Length; i += 2)
                {
                    string hs = string.Empty;

                    hs = hexString.Substring(i, 2);
                    uint decval = Convert.ToUInt32(hs, 16);
                    char character = Convert.ToChar(decval);
                    ascii += character;
                }

                return ascii;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string FromHex(this string hex)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(hex))
                {
                    return string.Empty;
                }

                string[] hexs = hex.Split('-');
                string result = "";

                for (int i = 0; i < hexs.Length; i++)
                {
                    int decValue = int.Parse(hexs[i], System.Globalization.NumberStyles.HexNumber);
                    result += decValue.ToString();
                }

                return ((char)int.Parse(result)).ToString();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static byte[] HexStringToByteArray(this String hex)
        {
            try
            {
                hex = hex.Trim();
                hex = hex.Replace("-", "");
                int NumberChars = hex.Length;

                if (hex.Length % 2 != 0)
                    return null;

                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                    bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                return bytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool IsAValidSerialComunication(this string input)
        {
            try
            {
                var regex = new Regex(@"[^0-9a-zA-Z@|[\]\\\/:\r\n\s'\x14]+");

                return !regex.IsMatch(input);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static object NullTreatment(this string input)
        {
            try
            {
                return string.IsNullOrWhiteSpace(input)
                    ? DBNull.Value
                    : input;
            }
            catch
            {
                return DBNull.Value;
            }
        }

        public static string RemoveEmptySpaces(this string input)
        {
            return Regex.Replace(input, @"\s+", "");
        }

        public static string RemoveSpecialCharacters(this string input)
        {
            return Regex.Replace(input, "[^0-9a-zA-Z]+", string.Empty);
        }

        public static string Reverse(this string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);

            return new string(charArray);
        }

        public static byte[] StringToByteArray(this string data)
        {
            return Encoding.ASCII.GetBytes(data);
        }

        public static string ToCamelCase(this string input)
        {
            if (!string.IsNullOrEmpty(input) && input.Length > 1)
                return char.ToLowerInvariant(input[0]) + input[1..];

            return input;
        }

        public static string ToHex(this byte data)
        {
            return BitConverter.ToString(new byte[] { data });
        }

        public static string ToHex(this byte[] data)
        {
            return BitConverter.ToString(data);
        }

        public static string ToHex(this string data)
        {
            return BitConverter.ToString(Encoding.ASCII.GetBytes(data));
        }

        #endregion Methods
    }
}