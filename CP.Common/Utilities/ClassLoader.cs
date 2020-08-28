/* 
 * Copyright (C) Pope Games, Inc - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Author: Chandler Pope-Lewis <c.popelewis@gmail.com>
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CP.Common.Utilities
{
    public static class ClassLoader
    {
        public static List<T> Load<T>()
        {
            List<T> objects = new List<T>();

            if(typeof(T).IsInterface)
            {
                foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract))
                {
                    objects.Add((T)Activator.CreateInstance(type));
                }
            } else
            {
                foreach (Type type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes()).Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(T))))
                {
                    objects.Add((T)Activator.CreateInstance(type));
                }
            }


            return objects;
        }
    }
}
