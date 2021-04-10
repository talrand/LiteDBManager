using System;
using LiteDB;

namespace LiteDBManager.Classes
{
    public static class BsonTypeMapper
    {
        public static Type ToSystemType(this BsonType type)
        {
            switch (type)
            {
                case BsonType.Int32: return Type.GetType("System.Int32");
                case BsonType.Int64: return Type.GetType("System.Int64");
                case BsonType.Boolean: return Type.GetType("System.Boolean");
                case BsonType.Decimal: return Type.GetType("System.Decimal");
                case BsonType.DateTime: return Type.GetType("System.DateTime");
                case BsonType.Double: return Type.GetType("System.Double");
                case BsonType.String:
                case BsonType.ObjectId:
                    return Type.GetType("System.String");
                default:
                    return Type.GetType("System.Object");
            }
        }
    }
}