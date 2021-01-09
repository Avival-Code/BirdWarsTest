namespace BirdWarsTest.Database
{
	public class GameDatabase
	{
		public GameDatabase()
		{
			Users = new UserDAO();
			Accounts = new AccountDAO();
			Inventories = new InventoryDAO();
		}

		public void UpdateUserPassword( string email, string newPassword )
		{
			var user = Users.Read( email );
			if( user != null )
			{
				Users.Update( user.UserId, user.Names, user.LastName, user.Username, user.Email, newPassword );
			}
		}

		public void UpdateUserInformation( User userIn, Account accountIn )
		{
			Users.Update( userIn.UserId, userIn.Names, userIn.LastName, userIn.Username, userIn.Email, userIn.Password );
			Accounts.Update( userIn.UserId, accountIn.TotalMatchesPlayed, accountIn.MatchesWon, accountIn.MatchesLost,
							 accountIn.MatchesSurvived, accountIn.Money, accountIn.Seconds );
		}

		public UserDAO Users { get; set; }
		public AccountDAO Accounts { get; set; }
		public InventoryDAO Inventories { get; set; }
	}
}