using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using TravelPackerAPI.Models;

namespace TravelPackerAPI.Data {
	public class TravelPackerDbContext : IdentityDbContext {

		public DbSet<User> Users { get; set; }
		public DbSet<Travel> Travels { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ItineraryItem> ItineraryItems { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Task> Tasks { get; set; }

		public TravelPackerDbContext(DbContextOptions<TravelPackerDbContext> options) : base(options) {
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>();

			modelBuilder.Entity<Travel>();

			modelBuilder.Entity<Category>();
			modelBuilder.Entity<ItineraryItem>();

			modelBuilder.Entity<Item>();
			modelBuilder.Entity<Task>();

		}

	}
}
