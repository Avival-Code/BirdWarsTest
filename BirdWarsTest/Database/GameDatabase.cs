using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	class GameDatabase
	{
		public GameDatabase()
		{
			users = new UserDAO();
			accounts = new AccountDAO();
			inventories = new InventoryDAO();
		}

		public UserDAO users;
		public AccountDAO accounts;
		public InventoryDAO inventories;
	}
}
