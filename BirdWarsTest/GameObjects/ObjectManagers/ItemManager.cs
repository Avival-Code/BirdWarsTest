using BirdWarsTest.GraphicComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.Network;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	public class ItemManager
	{
		public ItemManager( Microsoft.Xna.Framework.Content.ContentManager contentIn )
		{
			Boxes = new List< GameObject >();
			ConsumableItems = new List< GameObject >();
			spawnedItems = new List< bool >();
			randomNumberGenerator = new Random();
			content = contentIn;
			maxBoxes = 25;
			maxBoxHealth = 3;
			spawnBoxTimer = 0.0f;
		}

		public void SetMapBounds( Rectangle mapBoundsIn )
		{
			mapBounds = mapBoundsIn;
		}

		public void SpawnBoxes()
		{
			for( int i = 0; i < 10; i++ )
			{
				Boxes.Add( new GameObject( new ItemBoxGraphicsComponent( content ), null, new HealthComponent( maxBoxHealth ),
										   Identifiers.ItemBox, GetRandomMapPosition() ) );
				spawnedItems.Add( false );
			}
		}

		public void SpawnConsumableItems( INetworkManager networkManager )
		{
			if( networkManager.IsHost() )
			{
				for( int i = 0; i < Boxes.Count; i++ )
				{
					if( Boxes[ i ].Health.IsDead() && !spawnedItems[ i ] )
					{
						List< GameObject > newObjects = new List< GameObject >();
						CreateRandomItems( newObjects, i );
						networkManager.SendSpawnConsumablesMessage( newObjects );
					}
				}
			}
		}

		private void SpawnBox( INetworkManager networkManager )
		{
			if( networkManager.IsHost() )
			{
				GameObject newBox = new GameObject( new ItemBoxGraphicsComponent( content ), null, 
													new HealthComponent( maxBoxHealth ), Identifiers.ItemBox, GetRandomMapPosition() );
				Boxes.Add( newBox );
				spawnedItems.Add( false );
				List< GameObject > boxes = new List< GameObject >();
				boxes.Add( newBox );
				networkManager.SendSpawnBoxMessage( boxes );
			}
		}

		public void HandleSpawnBoxMessage( NetIncomingMessage incomingMessage )
		{
			int boxCount = incomingMessage.ReadInt32();
			for( int i = 0; i < boxCount; i++ )
			{
				Boxes.Add( new GameObject( new ItemBoxGraphicsComponent( content ), null, new HealthComponent( maxBoxHealth ),
										   Identifiers.ItemBox, new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
				spawnedItems.Add( false );
			}
		}

		public void HandleSpawnConsumablesMessage( NetIncomingMessage incomingMessage )
		{
			for( int i = 0; i < 4; i++ )
			{
				int identifier = incomingMessage.ReadInt32();
				switch( identifier )
				{
					case ( int )Identifiers.Coin:
						ConsumableItems.Add( new GameObject( new CoinGraphicsComponent( content ), null, ( Identifiers )identifier, 
															 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						break;

					case ( int )Identifiers.PigeonMilk:
						ConsumableItems.Add( new GameObject( new PigeonMilkGraphicsComponent( content ), null, ( Identifiers )identifier,
															 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						break;

					case ( int )Identifiers.EggGrenade:
						ConsumableItems.Add( new GameObject( new EggGrenadeGraphicsComponent( content ), null, ( Identifiers )identifier,
															 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						break;
				}
			}
		}

		public void HandleBoxDamageMessage( NetIncomingMessage incomingMessage )
		{
			int index = incomingMessage.ReadInt32();
			Boxes[ index ].Health.TakeDamage( incomingMessage.ReadInt32() );
		}

		private void HandleSpawnBox( INetworkManager networkManager, GameTime gameTime )
		{
			UpdateTimer( gameTime );
			if( ( int )spawnBoxTimer >= 30 && Boxes.Count < maxBoxes )
			{
				SpawnBox( networkManager );
				ResetTimer();
			}
		}

		private void HandleBoxDamage( INetworkManager networkManager, GameObject localPlayer )
		{
			for( int i = 0; i < Boxes.Count; i++ )
			{
				if( localPlayer.Attack.IsAttacking && !Boxes[ i ].Health.IsDead() && !Boxes[ i ].Health.TookDamage &&
					localPlayer.Attack.GetAttackRectangle( localPlayer ).Intersects( Boxes[ i ].GetRectangle() ) )
				{
					Boxes[ i ].Health.TakeDamage( localPlayer.Attack.Damage );
					networkManager.SendBoxDamageMessage( i, localPlayer.Attack.Damage );
				}
			}
		}

		public void Update( INetworkManager networkManager, GameObject localPlayer, GameTime gameTime )
		{
			SpawnConsumableItems( networkManager );

			if( networkManager.IsHost() )
			{
				HandleSpawnBox( networkManager, gameTime );
			}

			foreach( var box in Boxes )
			{
				box.Update( gameTime );
			}

			HandleBoxDamage( networkManager, localPlayer );
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			foreach( var box in Boxes )
			{
				if( !box.Health.IsDead() && cameraRenderBounds.Intersects( box.GetRectangle() ) )
				{
					box.Render( ref batch, cameraBounds );
				}
			}

			foreach( var item in ConsumableItems )
			{
				if( cameraRenderBounds.Intersects( item.GetRectangle() ) )
				{
					item.Render( ref batch, cameraBounds );
				}
			}
		}

		private void CreateRandomItems( List< GameObject > objectList, int boxIndex )
		{
			for( int i = 0; i < 4; i++ )
			{
				GameObject temp;
				switch( GetRandomItemType() )
				{
					case ( int )Identifiers.Coin:
						temp = new GameObject( new CoinGraphicsComponent( content ), null, new HealthComponent( 1 ),
											   Identifiers.Coin, GetRandomLocalBoxPosition( Boxes[ boxIndex ] ) );
						ConsumableItems.Add( temp );
						objectList.Add( temp );
						break;

					case ( int )Identifiers.PigeonMilk:
						temp = new GameObject( new PigeonMilkGraphicsComponent( content ), null, new HealthComponent( 1 ),
											   Identifiers.Coin, GetRandomLocalBoxPosition( Boxes[ boxIndex ] ) );
						ConsumableItems.Add( temp );
						objectList.Add( temp );
						break;

					case ( int )Identifiers.EggGrenade:
						temp = new GameObject( new EggGrenadeGraphicsComponent( content ), null, new HealthComponent( 1 ),
											   Identifiers.Coin, GetRandomLocalBoxPosition( Boxes[ boxIndex ] ) );
						ConsumableItems.Add( temp );
						objectList.Add( temp );
						break;
				}
			}
			spawnedItems[ boxIndex ] = true;
		}

		private Vector2 GetRandomMapPosition()
		{
			return new Vector2( randomNumberGenerator.Next( mapBounds.X, mapBounds.Width - 50 ),
								randomNumberGenerator.Next( mapBounds.Y, mapBounds.Height - 50 ) );
		}

		private Vector2 GetRandomLocalBoxPosition( GameObject box )
		{
			return new Vector2( randomNumberGenerator.Next( ( int )box.Position.X, ( int )box.Position.X + box.GetRectangle().Width ),
								randomNumberGenerator.Next( ( int )box.Position.Y, ( int )box.Position.Y + box.GetRectangle().Height ) );
		}

		private int GetRandomItemType()
		{
			return randomNumberGenerator.Next( ( int )Identifiers.Coin, ( int )Identifiers.EggGrenade );
		}

		private void UpdateTimer( GameTime gameTime )
		{
			spawnBoxTimer += ( float )gameTime.ElapsedGameTime.TotalSeconds;
		}

		private void ResetTimer()
		{
			spawnBoxTimer = 0.0f;
		}

		public void ClearAllObjects()
		{
			Boxes.Clear();
			ConsumableItems.Clear();
		}

		public List< GameObject > Boxes { get; private set; }
		public List< GameObject > ConsumableItems { get; private set; }
		private List< bool > spawnedItems;
		private Microsoft.Xna.Framework.Content.ContentManager content;
		private Random randomNumberGenerator;
		private Rectangle mapBounds;
		private int maxBoxes;
		private int maxBoxHealth;
		private float spawnBoxTimer;
	}
}