using System;

namespace LiteDBManager.Classes.Database
{
    public static class TableFieldTypes
    {
        public struct String
        {
            public const string Name = "String";
            public const string DefaultValue = "";
        }

        public struct Integer
        {
            public const string Name = "Integer";
            public const int DefaultValue = 0;
        }

        public struct Decimal
        {
            public const string Name = "Decimal";
            public const decimal DefaultValue = (decimal)0.00;

        }

        public struct Boolean
        {
            public const string Name = "Boolean";
            public const bool DefaultValue = false;
        }

        public struct Date
        {
            public const string Name = "DateTime";
            public static DateTime DefaultValue { get => new DateTime(1970, 1, 1); }
        }
    }
}