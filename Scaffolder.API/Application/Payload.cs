using System;

namespace Scaffolder.API.Application
{
    public class Payload
    {
        public String TableName { get; set; }
        public dynamic Entity { get; set; }
    }
}
