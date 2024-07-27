using NotkaAPI.Models.Notes;
using NotkaAPI.Models.Users;

namespace NotkaAPI.Models.General
{
	public class Request : BaseDatatable 
	{
        public required int SenderId { get; set; }
		//[ForeignKey(nameof(SenderId))]
		public User Sender { get; set; } = null!;
		//public IEnumerable<User> Senders { get; set; }
		public required int ReceiverId { get; set; }
		//[ForeignKey(nameof(ReceiverId))]
		public User Receiver { get; set; } = null!;
        public int? NoteId { get; set; }
        public Note? Note { get; set; }
        public int? ListId { get; set; }
        public List? List { get; set; }
		public int? TaskId { get; set; }
	}
}
