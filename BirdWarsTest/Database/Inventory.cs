using System.Collections.Generic;
using BirdWarsTest.GameObjects;

namespace BirdWarsTest.Database
{
	public class Inventory
	{
		public Inventory( int inventoryId_In, int userId_In, int totalItems_In,
					      List< int > itemCodes_In )
		{
			InventoryId = inventoryId_In;
			UserId = userId_In;
			TotalItems = totalItems_In;
			ItemCodes = itemCodes_In;
		}

		public int InventoryId { get; set; }
		public int UserId { get; set; }
		public int TotalItems { get; set; }
		public List< int > ItemCodes { get; set; }
	}
}