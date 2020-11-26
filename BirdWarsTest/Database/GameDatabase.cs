using System;
using System.Collections.Generic;
using System.Text;

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

		public UserDAO Users { get; set; }
		public AccountDAO Accounts { get; set; }
		public InventoryDAO Inventories { get; set; }
	}
}
