using NotkaMobile.Service.Reference;
using NotkaMobile.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotkaMobile.Services
{
    public class LoginDataStore : ADataStore
    {
		private User _user = new();
		public async Task<User> LoginUser(string email, string passwordHash)
		{
			_user = await _service.UserGET2Async(email, passwordHash);

			return _user;
		}

	}
}
