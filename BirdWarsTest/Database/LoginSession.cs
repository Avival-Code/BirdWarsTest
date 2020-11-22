using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	public class LoginSession
	{
		public LoginSession()
		{
			currentUser = null;
			currentAccount = null;
			currentInventory = null;
			isLoggedIn = false;
		}

		public LoginSession( User userIn, GameDatabase database )
		{
			currentUser = userIn;
			currentAccount = database.accounts.Read( currentUser.userId );
			currentInventory = null;
			isLoggedIn = true;
		}

		public void Login( User userIn, Account accountIn )
		{
			currentUser = userIn;
			currentAccount = accountIn;
			currentInventory = null;
			isLoggedIn = true;
		}

		public void Logout()
		{
			isLoggedIn = false;
			currentUser = null;
			currentAccount = null;
			currentInventory = null;
		}

		public bool isLoggedIn;
		public User currentUser;
		public Account currentAccount;
		public Inventory currentInventory;
	}
}
