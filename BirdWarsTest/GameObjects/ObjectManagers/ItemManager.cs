using BirdWarsTest.GraphicComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.EffectComponents;
using BirdWarsTest.Network;
using BirdWarsTest.Network.Messages;
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
						ConsumableItems.Add( new GameObject( new CoinGraphicsComponent( content ), null, new HealthComponent( 1 ),
															 new CoinEffectComponent( 1 ), Identifiers.Coin,
															 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						break;

					case ( int )Identifiers.PigeonMilk:
						ConsumableItems.Add( new GameObject( new PigeonMilkGraphicsComponent( content ), null, new HealthComponent( 1 ),
															 new RecoveryEffectComponent( 2 ), Identifiers.PigeonMilk, 
															 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						break;

					case ( int )Identifiers.EggGrenade:
						ConsumableItems.Add( new GameObject( new EggGrenadeGraphicsComponent( content ), null, new HealthComponent( 1 ),
														     new GrenadePickupEffectComponent(), Identifiers.EggGrenade, 
															 new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
						break;
				}
			}
		}

		public void HandleBoxDamageMessage( BoxDamageMessage boxDamageMessage )
		{
			Boxes[ boxDamageMessage.BoxIndex ].Health.TakeDamage( boxDamageMessage.Damage );
		}

		public void HandlePickedUpItemMessage( int itemIndex )
		{
			ConsumableItems[ itemIndex ].Health.TakeFullDamage();
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

		private void HandleConsumableItemPickup( INetworkManager networkManager, PlayerManager playerManager )
		{
			for( int i = 0; i < ConsumableItems.Count; i++ )
			{
				if( !ConsumableItems[ i ].Health.IsDead() && 
					ConsumableItems[ i ].GetRectangle().Intersects( playerManager.GetLocalPlayer().GetRectangle() ) )
				{
					ConsumableItems[ i ].Health.TakeFullDamage();
					ConsumableItems[ i ].Effect.DoEffect( networkManager, playerManager );
					networkManager.SendPickedUpItemMessage( i );
				}
			}
		}

		public void Update( INetworkManager networkManager, PlayerManager playerManager, GameTime gameTime )
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

			HandleBoxDamage( networkManager, playerManager.GetLocalPlayer() );
			HandleConsumableItemPickup( networkManager, playerManager );
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
				if( !item.Health.IsDead() && cameraRenderBounds.Intersects( item.GetRectangle() ) )
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
											   new CoinEffectComponent( 1 ), Identifiers.Coin, GetRandomLocalBoxPosition( Boxes[ boxIndex ] ) );
						ConsumableItems.Add( temp );
						objectList.Add( temp );
						break;

					case ( int )Identifiers.PigeonMilk:
						temp = new GameObject( new PigeonMilkGraphicsComponent( content ), null, new HealthComponent( 1 ),
											   new RecoveryEffectComponent( 2 ), Identifiers.PigeonMilk, GetRandomLocalBoxPosition( Boxes[ boxIndex ] ) );
						ConsumableItems.Add( temp );
						objectList.Add( temp );
						break;

					case ( int )Identifiers.EggGrenade:
						temp = new GameObject( new EggGrenadeGraphicsComponent( content ), null, new HealthComponent( 1 ),
											   new GrenadePickupEffectComponent(), Identifiers.EggGrenade, GetRandomLocalBoxPosition( Boxes[ boxIndex ] ) );
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
			return randomNumberGenerator.Next( ( int )Identifiers.Coin, ( int )Identifiers.EggGrenade + 1 );
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