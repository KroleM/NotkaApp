using CommunityToolkit.Mvvm.Messaging.Messages;
using NotkaDesktop.Service.Reference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotkaDesktop.Helpers
{
	public class ViewRequestMessage : ValueChangedMessage<MainWindowView>
	{
		public ViewRequestMessage(MainWindowView viewEnum) : base(viewEnum)
		{
		}
	}
}
