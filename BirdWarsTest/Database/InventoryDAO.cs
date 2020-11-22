using BirdWarsTest.GameObjects;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.Database
{
	public class InventoryDAO : IInventoryDAO
	{
		public bool Create( Inventory inventory )
		{
			bool wasCreated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "INSERT INTO Inventory( userId, totalItems ) " +
										  "VALUES ( @userId, @totalItems );";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter[] parameters = new MySqlParameter[ 2 ];
				parameters[ 0 ] = new MySqlParameter( "@userId", inventory.userId );
				parameters[ 1 ] = new MySqlParameter( "@totalItems", inventory.totalItems );
				command.Parameters.Add( parameters[ 0 ] );
				command.Parameters.Add( parameters[ 1 ] );

				InventoryItemsDAO tempSql = new InventoryItemsDAO();
				tempSql.Create( inventory );

				command.ExecuteNonQuery();
				wasCreated = true;
				Console.WriteLine( "Inventory created succesfully!" );
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasCreated;
		}

		public bool Delete( int userId )
		{
			bool wasDeleted = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "DELETE FROM Inventory WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter parameter = new MySqlParameter( "@userId", userId );
				command.Parameters.Add( parameter );

				command.ExecuteNonQuery();
				wasDeleted = true;
				Console.WriteLine( "Inventory deleted succesfully!" );
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasDeleted;
		}

		public Inventory Read( int userId )
		{
			Inventory temp = null;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string MySqlCommandText = "SELECT * FROM Inventory WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( MySqlCommandText, connection.connection );
				MySqlParameter parameter = new MySqlParameter( "@userId", userId );
				command.Parameters.Add( parameter );
				MySqlDataReader reader = command.ExecuteReader();

				if( reader.HasRows && reader.Read() )
				{
					int inventoryId = reader.GetInt32( 0 );
					InventoryItemsDAO items = new InventoryItemsDAO();
					temp = new Inventory( inventoryId, reader.GetInt32( 1 ), reader.GetInt32( 2 ), 
										  items.ReadAll( inventoryId ) );
				}
				else
				{
					Console.WriteLine( "No inventory matches found." );
				}
				reader.Close();
			}
			catch ( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return temp;
		}

		public List< Inventory > ReadAll()
		{
			List< Inventory > inventories = new List< Inventory >();
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string MySqlCommandText = "SELECT * FROM Inventory";
				MySqlCommand command = new MySqlCommand( MySqlCommandText, connection.connection );
				MySqlDataReader reader = command.ExecuteReader();

				while( reader.HasRows && reader.Read() )
				{
					int inventoryId = reader.GetInt32( 0 );
					InventoryItemsDAO items = new InventoryItemsDAO();
					inventories.Add( new Inventory( inventoryId, reader.GetInt32( 1 ),
										  reader.GetInt32( 2 ), items.ReadAll( inventoryId ) ) );
				}
				reader.Close();
			}
			catch ( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return inventories;
		}

		public bool Update( Inventory inventory )
		{
			bool wasUpdated = false;
			Connection connection = new Connection();
			connection.StartConnection();

			try
			{
				string mySqlCommandText = "UPDATE Inventory SET totalItems = @totalItems " +
										  "WHERE userId = @userId";
				MySqlCommand command = new MySqlCommand( mySqlCommandText, connection.connection );
				MySqlParameter[] parameters = new MySqlParameter[ 5 ];
				parameters[ 0 ] = new MySqlParameter( "@totalItems", inventory.totalItems );
				parameters[ 1 ] = new MySqlParameter( "@userId", inventory.userId );
				command.Parameters.Add( parameters[ 0 ] );
				command.Parameters.Add( parameters[ 1 ] );

				command.ExecuteNonQuery();

				InventoryItemsDAO tempItems = new InventoryItemsDAO();
				tempItems.Update( inventory );

				wasUpdated = true;
				Console.WriteLine( "Inventory Updated Successfully!" );
			}
			catch( Exception exception )
			{
				Console.WriteLine( exception.Message );
			}

			connection.StopConnection();
			return wasUpdated;
		}
	}
}