using System;

namespace Boom.Domain
{
    public class Participant : EntityBase
    {
        public Survey Survey { get; set; }

        public string Name { get; set; }
    }
}