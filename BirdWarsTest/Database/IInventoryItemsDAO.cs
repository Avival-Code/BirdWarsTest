using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	interface IInventoryItemsDAO
	{
		public bool Create( Inventory inventory );
		public List< int > ReadAll( int inventoryId );
		public bool Update( Inventory inventory );
		public bool Delete( int inventoryId );
	}
}
