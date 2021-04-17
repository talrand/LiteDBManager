using System;
using LiteDB;

namespace LiteDBManager.Classes
{
    /// <summary>
    /// Maps BsonType to SystemType (see https://www.litedb.org/docs/data-structure/)
    /// </summary>
    public static class BsonTypeMapper
    {
        public static Type ToSystemType(this BsonType type)
        {
            switch (type)
            {
                case BsonType.Null: return null;
                case BsonType.Int32: return Type.GetType("System.Int32");
                case BsonType.Int64: return Type.GetType("System.Int64");
                case BsonType.Boolean: return Type.GetType("System.Boolean");
                case BsonType.Decimal: return Type.GetType("System.Decimal");
                case BsonType.DateTime: return Type.GetType("System.DateTime");
                case BsonType.Double: return Type.GetType("System.Double");
                case BsonType.Document: return Type.GetType("System.Collection.Generic.Dictionary<string, BsonValue>");
                case BsonType.Array: return Type.GetType("System.Collection.Generic.List<BsonValue>");
                case BsonType.Binary: return Type.GetType("System.Byte[]");
                case BsonType.Guid: return Type.GetType("System.Guid");
                case BsonType.String:
                case BsonType.ObjectId:
                    return Type.GetType("System.String");
                default:
                    return Type.GetType("System.Object");
            }
        }
    }
}