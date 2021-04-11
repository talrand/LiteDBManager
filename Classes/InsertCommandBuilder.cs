using System;
using System.Text;
using Talrand.Core;

namespace LiteDBManager.Classes
{
    public class InsertCommandBuilder : CommandBuilder
    {
        public override string ToString()
        {            
            Type fieldType = null;
            string valueJson = "";

            // Construct Json for values
            using (var jsonWriter = new JsonWriter())
            {
                jsonWriter.WriteObjectStartElement("root");

                foreach (var field in _fields)
                {
                    fieldType = field.Value.GetType();

                    // Write Json element based on value type
                    if (fieldType.Equals(typeof(string)) || fieldType.Equals(typeof(DateTime)))
                    {
                        jsonWriter.WriteStringElement(field.Key, field.Value.ToString());
                        continue;
                    }

                    if (fieldType.Equals(typeof(bool)))
                    {
                        jsonWriter.WriteBooleanElement(field.Key, (bool)field.Value);
                        continue;
                    }

                    if (fieldType.Equals(typeof(int)) || fieldType.Equals(typeof(byte)))
                    {
                        jsonWriter.WriteNumberElement(field.Key, (int)field.Value);
                        continue;
                    }

                    if (fieldType.Equals(typeof(decimal))||fieldType.Equals(typeof(double)))
                    {
                        jsonWriter.WriteNumberElement(field.Key, (decimal)field.Value);
                        continue;
                    }
                }

                jsonWriter.WriteEndElement(); // root

                valueJson = jsonWriter.ToString();
            }

            return $"INSERT INTO {_tableName} VALUES {valueJson}";
        }
    }
}
