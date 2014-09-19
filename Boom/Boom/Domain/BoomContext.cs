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
        public DbSet<SurveyOptionVote> SurveyOptionVotes { get; set; }
        
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
                    o.ForeignKey<Survey>(s => s.SurveyId);
                });

            var survey = builder.Model.GetEntityType(typeof(Survey));
            var surveyOption = builder.Model.GetEntityType(typeof(SurveyOption));
            var surveyOptionFk = surveyOption.ForeignKeys.Single(f => f.Properties.Any(p => p.Name == "SurveyId"));
            survey.AddNavigation(new Navigation(surveyOptionFk, "Options", pointsToPrincipal: false));
            surveyOption.AddNavigation(new Navigation(surveyOptionFk, "Survey", pointsToPrincipal: true));


            // Survey -> Participant

            builder.Entity<Participant>()
                .ForeignKeys(p =>
                {
                    p.ForeignKey<Survey>(s => s.SurveyId);
                });

            var participant = builder.Model.GetEntityType(typeof(Participant));
            var surveyParticipantFk = participant.ForeignKeys.Single(f => f.Properties.Any(p => p.Name == "SurveyId"));
            survey.AddNavigation(new Navigation(surveyParticipantFk, "Participants", pointsToPrincipal: false));
            participant.AddNavigation(new Navigation(surveyParticipantFk, "Survey", pointsToPrincipal: true));

            // Vote -> Participant

            builder.Entity<Vote>()
                .ForeignKeys(p =>
                {
                    p.ForeignKey<Participant>(v => v.ParticipantId);
                });

            var vote = builder.Model.GetEntityType(typeof(Vote));
            var voteParticipantFk = vote.ForeignKeys.Single(f => f.Properties.Any(p => p.Name == "ParticipantId"));
            vote.AddNavigation(new Navigation(voteParticipantFk, "Participant", pointsToPrincipal: true));

            // Vote -> SurveyOption

            // SurveyOption -> SurveyOptionVote
        }
    }

}