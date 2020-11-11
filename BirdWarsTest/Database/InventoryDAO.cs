using BirdWarsTest.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	class InventoryDAO : IInventoryDAO
	{
		public bool Create( Inventory inventory )
		{
			throw new NotImplementedException();
		}

		public bool Delete( int inventoryId )
		{
			throw new NotImplementedException();
		}

		public Inventory Read( int inventoryId )
		{
			throw new NotImplementedException();
		}

		public List<Inventory> ReadAll()
		{
			throw new NotImplementedException();
		}

		public bool Update( int inventoryId, int userId, int totalItems, List<Identifiers> itemCodes )
		{
			throw new NotImplementedException();
		}
	}
}
