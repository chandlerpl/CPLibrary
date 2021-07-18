using System;
using System.Collections.Generic;
using System.Text;

namespace CP.Common.Commands
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class OverrideCommandAttribute : Attribute
    { 

    }
}
