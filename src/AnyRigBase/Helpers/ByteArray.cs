using System;
using System.Text;

namespace AnyRigBase.Helpers
{
    public class ByteArray
    {
        public static byte[] BytesAnd(byte[] a1, byte[] a2)
        {
            byte[] result = new byte[a1.Length];

            if (a1.Length != a2.Length)
                throw new Exception();

            for (int i = 0; i < a1.Length; i++)
            {
                result[i] = (byte)(a1[i] & a2[i]);
            }

            return result;

        }

        public static bool BytesEqual(byte[] a1, byte[] a2)
        {

            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
            {
                if (a1[i] != a2[i])
                    return false;
            }

            return true;

        }

        public static byte[] Copy(byte[] a1, int start, int lenght)
        {
            byte[] result = new byte[lenght];
            Array.Copy(a1, start, result, 0, lenght);
            return result;
        }

        public static string ByteToHex(byte[] buff)
        {
            if (buff == null)
                return "<null>";
            string result = "";
            foreach (byte b in buff)
                result += $"{b:X2} ";
            return result;
        }

        public static byte[] StrToBytes(string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static string StrToHex(string s)
        {
            byte[] buff = StrToBytes(s);
            return ByteToHex(buff);
        }

        public static string BytesToStr(byte[] buff)
        {
            if (buff == null)
                return null;
            return Encoding.UTF8.GetString(buff);
        }
    }
}
