using System;

namespace Scaffolder.Core.Base
{
    public class BaseObject
    {
        public BaseObject()
        {
            Name = String.Empty;
            Title = String.Empty;
            Description = String.Empty;
        }

        public String Name { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }        
    }
}
