using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Saudi_Auctions.Models;

namespace Saudi_Auctions.DAL
{
    public class AttachmentContext :DbContext
    {
        public AttachmentContext() 
        : base("AttachmentContext") 
        {
             Database.Initialize(true);
        }
        public DbSet<Attachment> Attachments { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
