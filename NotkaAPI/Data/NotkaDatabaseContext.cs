using Microsoft.EntityFrameworkCore;
using Notka.Database.Data.General;
using Notka.Database.Data.Notes;
using Notka.Database.Data.Users;

namespace Notka.Database.Data
{
	public sealed class NotkaDatabaseContext : DbContext
	{
		#region Constructors
		/// <summary>
		/// This is a standard constructor, however migrations in this project use the parameterless one.
		/// </summary>
		/// <param name="options"></param>
		public NotkaDatabaseContext(DbContextOptions<NotkaDatabaseContext> options) : base(options)
		{
		}
		/// <summary>
		/// This parameterless constructor is used for the design-time DbContext creation (for the needs of migration). 
		/// Using it requires defining the OnConfiguring method.
		/// </summary>
		//public NotkaDatabaseContext() { }
		#endregion

		#region DbSets
		public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Picture> Picture { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<List> List { get; set; }
        //public DbSet<ListType> ListType { get; set; }
        public DbSet<Notes.Task> Task { get; set; }
        public DbSet<ListElement> ListElement { get; set; }
		#endregion

		#region Methods
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<User>()
				.HasMany(e => e.RequestSender)
				.WithMany(e => e.RequestReceiver)
				.UsingEntity<Request>(
					l => l.HasOne(e => e.Receiver).WithMany(e => e.Receivers).HasForeignKey(e => e.ReceiverId),
					r => r.HasOne(e => e.Sender).WithMany(e => e.Senders).HasForeignKey(e => e.SenderId));
		}
		/*
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			base.OnConfiguring(optionsBuilder);

			optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NotkaContext;Trusted_Connection=True;MultipleActiveResultSets=true");
		}
		*/
		#endregion
	}
}
