using Microsoft.EntityFrameworkCore;
using NotkaAPI.Models.CMS;
using NotkaAPI.Models.General;
using NotkaAPI.Models.Investments;
using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;

namespace NotkaAPI.Data
{
	public sealed class NotkaDatabaseContext : DbContext
	{
		#region Constructors
		public NotkaDatabaseContext(DbContextOptions<NotkaDatabaseContext> options) : base(options)
		{
		}
		#endregion

		#region DbSets
		public DbSet<User> User { get; set; } = default!;
		public DbSet<Role> Role { get; set; }
		public DbSet<Login> Login { get; set; }
		public DbSet<Feed> Feed { get; set; }
		public DbSet<Picture> Picture { get; set; }
		public DbSet<Request> Request { get; set; }
		public DbSet<Tag> Tag { get; set; }
		public DbSet<Note> Note { get; set; }
		public DbSet<List> List { get; set; }
		public DbSet<TaskClass> Task { get; set; }
		public DbSet<ListElement> ListElement { get; set; }
		public DbSet<ListTag> ListTag { get; set; }
		public DbSet<NoteTag> NoteTag { get; set; }
		public DbSet<TaskTag> TaskTag { get; set; }
		public DbSet<RoleUser> RoleUser { get; set; }
        public DbSet<Stock> Stock { get; set; }
        public DbSet<StockExchange> StockExchange { get; set; }
		public DbSet<StockNote> StockNote { get; set; }
		public DbSet<StockPrice> StockPrice { get; set; }
		public DbSet<Currency> Currency { get; set; }
		public DbSet<Portfolio> Portfolio { get; set; }
		public DbSet<PortfolioStock> PortfolioStock { get; set; }
		public DbSet<Transaction> Transaction { get; set; }
		public DbSet<Watchlist> Watchlist { get; set; }
		public DbSet<WatchlistStock> WatchlistStock { get; set; }

		#endregion

		#region Methods
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasMany(e => e.RequestSender)
				.WithMany(e => e.RequestReceiver)
				.UsingEntity<Request>(
					l => l.HasOne(e => e.Receiver).WithMany(e => e.Receivers).HasForeignKey(e => e.ReceiverId),
					r => r.HasOne(e => e.Sender).WithMany(e => e.Senders).HasForeignKey(e => e.SenderId));
		}
		#endregion
	}
}
