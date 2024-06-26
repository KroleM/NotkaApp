﻿using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NotkaMobile.Helpers
{
	public static class Helpers
	{
		/// <summary>
		/// Metoda typu rozszerzenie. Jako argument przyjmuje element, na którym została wywołana. 
		/// Jeżeli akcja wywoła się bez problemu, zwraca true, a jak nie to false i wypisuje błąd.
		/// </summary>
		/// <param name="serviceMethod"></param>
		/// <returns></returns>
		public async static Task<bool> HandleRequest(this Task serviceMethod)
		{
			try
			{
				await serviceMethod;
				return true;
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return false;
			}
		}
	}

	public enum UserRoles
	{
		Admin = 0,
		Basic = 1,
		Premium = 2,
	}
}
