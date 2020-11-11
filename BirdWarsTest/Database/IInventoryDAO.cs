using BirdWarsTest.GameObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	interface IInventoryDAO
	{
		public bool Create( Inventory inventory );
		public List< Inventory > ReadAll();
		public Inventory Read( int inventoryId );
		public bool Update( int inventoryId, int userId, int totalItems, List< Identifiers > itemCodes );
		public bool Delete( int inventoryId );
	}
}
