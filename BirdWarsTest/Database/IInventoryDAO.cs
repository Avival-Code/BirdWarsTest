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
		public Inventory Read( int userId );
		public bool Update( Inventory inventory );
		public bool Delete( int userId );
	}
}
