using System.Collections.Generic;

namespace Boom.Domain
{
    public class Backlog : EntityBase
    {
        public string Name { get; set; }

        public IList<BacklogOption> Options { get; set; }
    }
}