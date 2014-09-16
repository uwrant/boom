using System;

namespace Boom.Domain
{
    public class Vote
    {
        public Option[] Options { get; set; }

        public Participant Participant { get; set; }
    }
}