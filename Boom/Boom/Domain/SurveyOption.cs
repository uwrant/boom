using System;

namespace Boom.Domain
{

    public class SurveyOption : EntityBase
    {
        public Survey Survey { get; set; }

	    public string Description { get; set; }
    }
}