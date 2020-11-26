using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	class InventoryItemsDAO : IInventoryItemsDAO
	{
		public bool Create( Inventory inventory )
		{
			bool wasCreated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "INSERT INTO InventoryItems( inventoryId, itemCode ) " +
										  "VALUES ( @inventoryId, @itemCode );";
				foreach( var itemCode in inventory.ItemCodes )
				{
					MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
					MySqlParameter[] parameters = new MySqlParameter[ 2 ];
					parameters[ 0 ] = new MySqlParameter( "@inventoryId", inventory.InventoryId );
					parameters[ 1 ] = new MySqlParameter( "@itemCode", itemCode );
					command.Parameters.Add( parameters[ 0 ] );
					command.Parameters.Add( parameters[ 1 ] );
					command.ExecuteNonQuery();
				}

				wasCreated = true;
				Console.WriteLine( "Inventory items created succesfully!" );
			}
			catch ( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasCreated;
		}

		public bool Delete( int inventoryId )
		{
			bool wasDeleted = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "DELETE FROM InventoryItems WHERE inventoryId = @inventoryId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter parameter = new MySqlParameter( "@inventoryId", inventoryId );
				command.Parameters.Add( parameter );

				command.ExecuteNonQuery();
				wasDeleted = true;
				Console.WriteLine( "InventoryItems deleted succesfully!" );
			}
			catch ( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasDeleted;
		}

		public List< int > ReadAll( int inventoryId )
		{
			List< int > itemCodes = new List< int >();
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "SELECT * FROM InventoryItems WHERE inventoryId = @inventoryId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter parameter = new MySqlParameter( "@inventoryId", inventoryId );
				command.Parameters.Add( parameter );
				MySqlDataReader reader = command.ExecuteReader();

				while ( reader.HasRows && reader.Read() )
				{
					itemCodes.Add( reader.GetInt32( 1 ) );
					reader.NextResult();
				}
				reader.Close();
			}
			catch ( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return itemCodes;
		}

		public bool Update( Inventory inventory )
		{
			bool wasUpdated = Delete( inventory.InventoryId );
			wasUpdated = Create( inventory );
			return wasUpdated;
		}
	}
}
