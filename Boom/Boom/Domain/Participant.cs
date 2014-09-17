using System;

namespace Boom.Domain
{
    public class Participant : EntityBase
    {
        public Survey Survey { get; set; }

        public long SurveyId { get; set; }

        public string Name { get; set; }
    }
}