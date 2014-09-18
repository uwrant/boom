using System;
using System.Collections.Generic;

namespace Boom.Domain
{
    public class Vote : EntityBase
    {
        public IDictionary<SurveyOption, int> Options { get; set; }

        public Participant Participant { get; set; }
    }
}