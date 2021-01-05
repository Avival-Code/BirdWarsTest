namespace BirdWarsTest.Database
{
	public class LoginSession
	{
		public LoginSession()
		{
			CurrentUser = null;
			CurrentAccount = null;
			CurrentInventory = null;
			IsLoggedIn = false;
		}

		public LoginSession( User userIn, GameDatabase database )
		{
			CurrentUser = userIn;
			CurrentAccount = database.Accounts.Read( CurrentUser.UserId );
			CurrentInventory = null;
			IsLoggedIn = true;
		}

		public void Login( User userIn, Account accountIn )
		{
			CurrentUser = userIn;
			CurrentAccount = accountIn;
			CurrentInventory = null;
			IsLoggedIn = true;
		}

		public void Logout()
		{
			IsLoggedIn = false;
			CurrentUser = null;
			CurrentAccount = null;
			CurrentInventory = null;
		}

		public bool IsLoggedIn { get; set; }
		public User CurrentUser { get; set; }
		public Account CurrentAccount { get; set; }
		public Inventory CurrentInventory { get; set; }
	}
}