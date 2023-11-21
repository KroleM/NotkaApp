using Notka.Database.Data.Notes;
using Notka.Database.Data.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Notka.Database.Data.General
{
	public class Request : BaseDatatable 
	{
        public required int SenderId { get; set; }
		//[ForeignKey(nameof(SenderId))]
		public User Sender { get; set; } = null!; //lista?!
		//public IEnumerable<User> Senders { get; set; }
		public required int ReceiverId { get; set; }
		//[ForeignKey(nameof(ReceiverId))]
		public User Receiver { get; set; } = null!;
        public int? NoteId { get; set; }
        public Note? Note { get; set; }
        public int? ListId { get; set; }
        public List? List { get; set; }
    }
}
