using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ConferenceManagementSystem.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ConferenceManagementSystem.DataAccessLayer
{
    public class ConferenceManagementContext : DbContext
    {
        public ConferenceManagementContext()
            : base("ConferenceManagementContext")
        {
            Database.SetInitializer<ConferenceManagementContext>(new CreateDatabaseIfNotExists<ConferenceManagementContext>()); //set to null allow DataAnnotations to function in models folder
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<NotificationEmail> NotificationEmails { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Fee> Fees { get; set; }
        public DbSet<DateDealine> DateDealines { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Paper> Papers { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }
        public DbSet<ReviewerType> ReviewerTypes { get; set; }
        public DbSet<InvitationEmailStatus> InvitationEmailStatuses { get; set; }
        public DbSet<InvitationStatus> InvitationStatuses { get; set; }
        public DbSet<ReviewItem> ReviewItems { get; set; }
        public DbSet<AbstractFileFormat> AbstractFileFormats { get; set; }
        public DbSet<FontName> FontNames { get; set; }
        public DbSet<Alignment> Alignments { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
         public DbSet<EmailTag> EmailTags { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<ConferenceManagementSystem.Models.PaymentStatus> PaymentStatus { get; set; }
    }
}