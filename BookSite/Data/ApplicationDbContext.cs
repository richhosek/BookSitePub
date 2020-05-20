using System;
using System.Collections.Generic;
using System.Text;
using BookSite.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookSite.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //builder.Entity<Collaborator>().HasOne(c => c.CoAuthor).WithMany(a => a.Collaborators).HasForeignKey(c => c.CoAuthorId);
            builder.Entity<Collaborator>()
                .HasOne(c => c.Author)
                .WithMany(a => a.Collaborators)
                .HasForeignKey(c => c.AuthorId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<BookSiteAccount> Accounts { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<CoverArt> Covers { get; set; }
        public DbSet<CoverImage> Images { get; set; }
        public DbSet<BookAuthor> BookAuthors { get; set; }
        public DbSet<Collaborator> Collaborators { get; set; }
        public DbSet<Edition> Editions { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Listing> Listings { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Blurb> Blurbs { get; set; }
    }
}
