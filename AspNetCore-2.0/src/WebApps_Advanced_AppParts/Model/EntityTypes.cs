﻿using System.Collections.Generic;
using System.Reflection;

namespace WebApps_Advanced_AppParts.Model
{
    public static class EntityTypes
    {
        public static IReadOnlyList<TypeInfo> Types => new List<TypeInfo>()
        {
            typeof(Sprocket).GetTypeInfo(),
            typeof(Widget).GetTypeInfo(),
        };

        public class Sprocket { }
        public class Widget { }
    }
}
