using System;
using System.Collections.Generic;

namespace Spark
{
    internal class ServiceBindingModel
    {
        public readonly Type BaseType;
        public readonly List<Type> ServiceTypeList = new List<Type>();

        public ServiceBindingModel(Type baseType)
        {
            BaseType = baseType;
        }
    }
}