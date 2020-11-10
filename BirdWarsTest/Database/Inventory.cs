using System;
using System.Collections.Generic;
using System.Text;
using BirdWarsTest.GameObjects;

namespace BirdWarsTest.Database
{
	class Inventory
	{
		public Inventory( int inventoryId_In, int userId_In, int totalItems_In,
					      List< Identifiers > itemCodes_In )
		{
			inventoryId = inventoryId_In;
			userId = userId_In;
			totalItems = totalItems_In;
			itemCodes = itemCodes_In;
		}

		public int inventoryId;
		public int userId;
		public int totalItems;
		public List< Identifiers > itemCodes;
	}
}
