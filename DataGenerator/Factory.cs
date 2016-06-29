﻿using System;
using DataGenerator.Types;
using DataGenerator.Types.Name;

namespace DataGenerator
{
    internal static class Factory
    {
        public static Name Name(NameType dataType) {
            switch (dataType) {
                case NameType.First:
                    return new FirstName();
                case NameType.Last:
                    return new LastName();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}