using System;
using Microsoft.Data.Entity;
using Boom.Domain;
using Microsoft.Framework.OptionsModel;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Metadata;
using System.Linq;

namespace Boom
{
    /// <summary>
    /// DbContext for Boom.Domain
    /// </summary>
    public class BoomContext : DbContext
    {
        public BoomContext(IServiceProvider serviceProvider, IOptionsAccessor<DbContextOptions> optionsAccessor)
            : base(serviceProvider, optionsAccessor.Options)
        {
        }

        public DbSet<Backlog> Backlogs { get; set; }
        public DbSet<BacklogOption> BacklogOptions { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<SurveyOption> SurveyOptions { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Backlog -> BacklogOption

            builder.Entity<BacklogOption>()
                .ForeignKeys(o =>
                {
                    o.ForeignKey<Backlog>(b => b.BacklogId);
                });

            var backlog = builder.Model.GetEntityType(typeof(Backlog));
            var backlogOption = builder.Model.GetEntityType(typeof(BacklogOption));
            var backlogFk = backlogOption.ForeignKeys.Single(f => f.Properties.Any(p => p.Name == "BacklogId"));
            backlog.AddNavigation(new Navigation(backlogFk, "Options", pointsToPrincipal: false));
            backlogOption.AddNavigation(new Navigation(backlogFk, "Backlog", pointsToPrincipal: true));



            // Survey -> SurveyOption

            builder.Entity<SurveyOption>()
                .ForeignKeys(o =>
                {
                    o.ForeignKey<Survey>(b => b.SurveyId);
                });

            var survey = builder.Model.GetEntityType(typeof(Survey));
            var surveyOption = builder.Model.GetEntityType(typeof(SurveyOption));
            var surveyFk = surveyOption.ForeignKeys.Single(f => f.Properties.Any(p => p.Name == "SurveyId"));
            survey.AddNavigation(new Navigation(surveyFk, "Options", pointsToPrincipal: false));
            surveyOption.AddNavigation(new Navigation(surveyFk, "Survey", pointsToPrincipal: true));
        }
    }

}