/*
Copyright © 2005 - 2016 Annpoint, s.r.o.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

-------------------------------------------------------------------------

NOTE: Reuse requires the following acknowledgement (see also NOTICE):
This product includes DayPilot (http://www.daypilot.org) developed by Annpoint, s.r.o.
*/


using System;
using System.Text;
using System.IO;

namespace DayPilot.Web.Mvc.Json
{
    /// <summary>
    /// Just for parsing short strings (inefficient for large data).
    /// </summary>
    public class SimpleJsonDeserializer
    {
        private int i = 0;
        private string input = null;
        //private IDictionary result;
        //private object current;

        /// <summary>
        /// Deserialized a string.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static JsonData Deserialize(string input)
        {
            SimpleJsonDeserializer sjd = new SimpleJsonDeserializer(input);
            return sjd.getNextValue();
        }

        public static JsonData Deserialize(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
            {
                string request = reader.ReadToEnd();
                if (!request.StartsWith("JSON"))
                {
                    throw new InvalidDataException("The request must start with 'JSON' string.");
                }
                return Deserialize(request.Substring(4));
            }
        }

        private SimpleJsonDeserializer(string input)
        {
            this.input = input;
        }


        private JsonData getNextValue()
        {
            skipWhiteSpace();
            ValueType vt = getNextValueType();

            switch (vt)
            {
                case ValueType.String:
                    return getString();
                case ValueType.Number:
                    return getNumber();
                case ValueType.Object:
                    return getObject();
                case ValueType.Array:
                    return getArray();
                case ValueType.True:
                    skipToken();
                    return true;
                case ValueType.False:
                    skipToken();
                    return false;
                case ValueType.Null:
                    skipToken();
                    return null;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void skipToken()
        {
            // assuming that it was detected properly
            while (!Detector.IsValueSeparator(input[i]))
            {
                i++;
            }
        }

        private JsonData getString()
        {
            bool escaped = false;
            StringBuilder sb = new StringBuilder();

            if (input[i] != '"')
                throw new Exception("The string doesn't start with '\"'.");

            i++;

            while (input[i] != '"' || escaped)
            {
                escaped = false;
                if (input[i] == '\\')
                    escaped = true;

                sb.Append(input[i]);

                i++;
            }

            i++;

            return new JsonData(unescape(sb.ToString()));
        }

        private static char fromUniHex(string hex)
        {
            if (hex.Length != 4)
            {
                throw new ArgumentException("A four-character string expected. Characters supplied: " + hex.Length);
            }
            int unii = Convert.ToInt32(hex, 16);
            char unic = char.ConvertFromUtf32(unii).ToCharArray()[0];
            return unic;
        }

        private static string unescape(string source)
        {
            bool escaped = false;
            StringBuilder sb = new StringBuilder();
            string uniHex = null;

            foreach (char c in source)
            {
                if (!escaped && c == '\\')
                {
                    escaped = true;
                }
                else
                {
                    if (uniHex != null)
                    {
                        uniHex += c;
                        if (uniHex.Length == 4)
                        {
                            //char uni = Char.Parse("\\u" + uniHex);
                            char uni = fromUniHex(uniHex);
                            sb.Append(uni);
                            uniHex = null;
                        }
                    }
                    else if (escaped)
                    {
                        switch (c)
                        {
                            case '"':
                                sb.Append('\"');
                                break;
                            case '\\':
                                sb.Append('\\');
                                break;
                            case 'b':
                                sb.Append('\b');
                                break;
                            case 'f':
                                sb.Append('\f');
                                break;
                            case 'n':
                                sb.Append('\n');
                                break;
                            case 'r':
                                sb.Append('\r');
                                break;
                            case 't':
                                sb.Append('\t');
                                break;
                            case 'u':
                                uniHex = "";
                                break;
                            default:
                                throw new Exception("Unsupported escape sequence : \\" + c);
                        }
                    }
                    else
                    {
                        sb.Append(c);
                    }
                    escaped = false;
                }

            }

            return sb.ToString();
        }


        private JsonData getNumber()
        {
            //bool escaped = false;
            StringBuilder sb = new StringBuilder();

            while (!Detector.IsValueSeparator(input[i]) && !Detector.IsWhiteSpace(input[i]))
            {
                sb.Append(input[i]);
                i++;
            }

            return new JsonData(Convert.ToDouble(sb.ToString()));
        }

        private JsonData getObject()
        {
            if (input[i] != '{')
                throw new Exception("The object doesn't start with '{'.");

            i++;

            //Hashtable result = new Hashtable();
            JsonData result = new JsonData();

            do
            {
                skipWhiteSpace();

                if (input[i] == '}')
                {
                    i++;
                    break;
                }

                ValueType vt = getNextValueType();
                if (vt != ValueType.String)
                {
                    throw new Exception("Object can't be parsed: Key is not a string.");
                }

                string key = (string)getString();

                skipWhiteSpace();

                if (input[i] != ':')
                {
                    throw new Exception("Colon expected as key/value separator.");
                }

                i++;

                skipWhiteSpace();

                result[key] = getNextValue();

                skipWhiteSpace();

            } while (input[i++] == ',');


            if (input[i - 1] != '}')
            {
                throw new Exception("'}}' expected.");
                //throw new Exception(String.Format("'}}' expected ({0}).", input.Substring(i-1, 30)));
            }

            return result;

        }

        private JsonData getArray()
        {
            if (input[i] != '[')
                throw new Exception("The array doesn't start with '['.");

            i++;

            JsonData result = new JsonData();


            do
            {
                skipWhiteSpace();

                if (input[i] == ']')
                {
                    i++;
                    break;
                }

                result.Add(getNextValue());

                skipWhiteSpace();

            } while (input[i++] == ',');

            if (input[i - 1] != ']')
            {
                /*
                int start = 0;
                if (i > 30)
                {
                    start = i - 30;
                }
                string sample = input.Substring(start, i - start);
                char c = input[i - 1];

                throw new Exception(String.Format("']' expected. '{0}' supplied. Here: {1}", c, sample));
                 */
                throw new Exception(String.Format("']' expected."));

            }

            return result;

        }


        private ValueType getNextValueType()
        {
            // assumes whitespaces skipped

            // keep the position at the first character
            char c = input[i];
            switch (c)
            {
                case '{':
                    return ValueType.Object;
                case '[':
                    return ValueType.Array;
                case '"':
                    return ValueType.String;
            }

            if (Detector.IsNumber(c))
            {
                return ValueType.Number;
            }

            if (isToken("true"))
            {
                return ValueType.True;
            }

            if (isToken("false"))
            {
                return ValueType.False;
            }

            if (isToken("null"))
            {
                return ValueType.Null;
            }

            throw new Exception(String.Format("Unrecognized value type ({0})", input.Substring(i, 30)));

        }

        private void skipWhiteSpace()
        {
            while (Detector.IsWhiteSpace(input[i]))
            {
                i++;
            }
        }

        private bool isToken(string token)
        {
            string buffer = String.Empty;

            int tempI = i;

            // read without moving the position
            while (!Detector.IsValueSeparator(input[tempI]) && !Detector.IsWhiteSpace(input[tempI]))
            {
                buffer += input[tempI];
                tempI++;

                if (tempI >= input.Length) // not found, too long
                {
                    return false;
                }

                if (tempI - i > token.Length) // not found, EOF
                {
                    return false;
                }
            }

            return buffer == token;

        }



        internal class Detector
        {
            internal static bool IsWhiteSpace(char c)
            {
                switch (c)
                {
                    case ' ':
                    case '\t':
                    case '\r':
                    case '\n':
                        return true;
                    default:
                        return false;
                }
            }

            internal static bool IsValueSeparator(char c)
            {
                switch (c)
                {
                    case ',':
                    case '}':
                    case ']':
                        return true;
                    default:
                        return false;
                }
            }

            internal static bool IsNumber(char c)
            {
                switch (c)
                {
                    case '0':
                    case '1':
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        return true;
                    default:
                        return false;
                }
            }


        }

        internal enum ValueType
        {
            String,
            Number,
            Object,
            Array,
            True,
            False,
            Null
        }

    }
}
