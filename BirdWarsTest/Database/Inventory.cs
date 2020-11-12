using System.Collections.Generic;
using BirdWarsTest.GameObjects;

namespace BirdWarsTest.Database
{
	class Inventory
	{
		public Inventory( int inventoryId_In, int userId_In, int totalItems_In,
					      List< int > itemCodes_In )
		{
			inventoryId = inventoryId_In;
			userId = userId_In;
			totalItems = totalItems_In;
			itemCodes = itemCodes_In;
		}

		public int inventoryId;
		public int userId;
		public int totalItems;
		public List< int > itemCodes;
	}
}
