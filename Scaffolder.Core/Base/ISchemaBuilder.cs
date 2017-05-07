using System.Collections.Generic;
using Scaffolder.Core.Meta;

namespace Scaffolder.Core.Base
{
    public interface ISchemaBuilder
    {
        IEnumerable<Table> Build();
    }
}
