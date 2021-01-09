using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.AttackComponents;
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
			EggGrenades = new List< GameObject >();
			spawnedItems = new List< bool >();
			randomNumberGenerator = new Random();
			content = contentIn;
			maxBoxes = 25;
			maxBoxHealth = 3;
			spawnBoxTimer = 0.0f;
			localGrenadeTimer = 0.0f;
			threwGrenade = false;
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

		public void SpawnGrenade( INetworkManager networkManager, PlayerManager playerManager )
		{
			if( !threwGrenade && playerManager.GrenadeAmount > 0 )
			{
				GameObject temp = new GameObject( new ActiveEggGrenadeGraphicsComponent( content ), 
												  new GrenadeInputComponent( playerManager.GetLocalPlayer().Input.GetLastActiveVelocity(),
																			 playerManager.GetLocalPlayer().Input.GetObjectSpeed() ), 
												  new HealthComponent( 1 ),
												  new GrenadeAttackComponent(), Identifiers.EggGrenade,
												  GetGrenadePosition( playerManager.GetLocalPlayer() ) );
				EggGrenades.Add( temp );
				networkManager.SendSpawnGrenadeMessage( temp );
				threwGrenade = true;
				playerManager.GrenadeAmount -= 1;
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
			SpawnConsumablesMessage message = new SpawnConsumablesMessage( incomingMessage );
			for( int i = 0; i < message.Identifiers.Length; i++ )
			{
				switch( message.Identifiers[ i ] )
				{
					case ( int )Identifiers.Coin:
						ConsumableItems.Add( new GameObject( new CoinGraphicsComponent( content ), null, new HealthComponent( 1 ),
															 new CoinEffectComponent( 1 ), Identifiers.Coin,
															 message.ObjectPositions[ i ] ) );
						break;

					case ( int )Identifiers.PigeonMilk:
						ConsumableItems.Add( new GameObject( new PigeonMilkGraphicsComponent( content ), null, new HealthComponent( 1 ),
															 new RecoveryEffectComponent( 2 ), Identifiers.PigeonMilk,
															 message.ObjectPositions[ i ]) );
						break;

					case ( int )Identifiers.EggGrenade:
						ConsumableItems.Add( new GameObject( new EggGrenadeGraphicsComponent( content ), null, new HealthComponent( 1 ),
														     new GrenadePickupEffectComponent(), Identifiers.EggGrenade,
															 message.ObjectPositions[ i ]) );
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

		public void HandleSpawnGrenadeMessage( Vector2 grenadePosition, Vector2 grenadeDirection, float grenadeSpeed )
		{
			EggGrenades.Add( new GameObject( new ActiveEggGrenadeGraphicsComponent( content ), 
											 new GrenadeInputComponent( grenadeDirection, grenadeSpeed ), new HealthComponent( 1 ),
											 new GrenadeAttackComponent(), Identifiers.EggGrenade, grenadePosition ) );
		}

		private void HandleSpawnBox( INetworkManager networkManager, GameTime gameTime )
		{
			UpdateBoxTimer( gameTime );
			if( ( int )spawnBoxTimer >= 30 && Boxes.Count < maxBoxes )
			{
				SpawnBox( networkManager );
				ResetBoxTimer();
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

		private void HandleGrenadeDamage( GameObject localPlayer )
		{
			for( int i = 0; i < EggGrenades.Count; i++ )
			{
				if( EggGrenades[ i ].Attack.IsAttacking &&
					EggGrenades[ i ].Attack.GetAttackRectangle( EggGrenades[ i ] ).Intersects( localPlayer.GetRectangle() ) )
				{
					localPlayer.Health.TakeDamage( EggGrenades[ i ].Attack.Damage );
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
			UpdateBoxes( gameTime );
			UpdateGrenades( gameTime );
			UpdateGrenadeTimer( gameTime );
			HandleBoxDamage( networkManager, playerManager.GetLocalPlayer() );
			HandleGrenadeDamage( playerManager.GetLocalPlayer() );
			HandleConsumableItemPickup( networkManager, playerManager );
		}

		private void UpdateBoxes( GameTime gameTime )
		{
			foreach( var box in Boxes )
			{
				box.Update( gameTime );
			}
		}

		private void UpdateGrenades( GameTime gameTime )
		{
			foreach( var grenade in EggGrenades )
			{
				grenade.Update( gameTime );
			}
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			RenderBoxes( ref batch, cameraRenderBounds, cameraBounds );
			RenderConsumableItems( ref batch, cameraRenderBounds, cameraBounds );
			RenderGrenades( ref batch, cameraRenderBounds, cameraBounds );
		}

		private void RenderBoxes( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			foreach( var box in Boxes )
			{
				if( !box.Health.IsDead() && cameraRenderBounds.Intersects( box.GetRectangle() ) )
				{
					box.Render( ref batch, cameraBounds );
				}
			}
		}

		private void RenderConsumableItems( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			foreach( var item in ConsumableItems )
			{
				if( !item.Health.IsDead() && cameraRenderBounds.Intersects( item.GetRectangle() ) )
				{
					item.Render( ref batch, cameraBounds );
				}
			}
		}

		private void RenderGrenades( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			foreach( var grenade in EggGrenades )
			{
				if( !grenade.Health.IsDead() && cameraBounds.Intersects( grenade.GetRectangle() ) )
				{
					grenade.Render( ref batch, cameraBounds );
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
			return new Vector2( randomNumberGenerator.Next( mapBounds.X, mapBounds.Width - 66 ),
								randomNumberGenerator.Next( mapBounds.Y, mapBounds.Height - 56 ) );
		}

		private Vector2 GetRandomLocalBoxPosition( GameObject box )
		{
			return new Vector2( randomNumberGenerator.Next( ( int )box.Position.X, ( int )box.Position.X + box.GetRectangle().Width ),
								randomNumberGenerator.Next( ( int )box.Position.Y, ( int )box.Position.Y + box.GetRectangle().Height ) );
		}

		private Vector2 GetGrenadePosition( GameObject localPlayer )
		{
			Vector2 position = new Vector2( 0.0f, 0.0f );
			if( ( int )localPlayer.Input.GetLastActiveVelocity().Y < 0 &&
				( int )localPlayer.Input.GetLastActiveVelocity().X == 0 )
			{
				position = new Vector2( localPlayer.Position.X + 20, localPlayer.Position.Y - 30 );
			}

			if( ( int )localPlayer.Input.GetLastActiveVelocity().Y > 0 &&
				( int )localPlayer.Input.GetLastActiveVelocity().X == 0 )
			{
				position = new Vector2( localPlayer.Position.X + 20, localPlayer.Position.Y + localPlayer.GetRectangle().Height );
			}

			if( ( int )localPlayer.Input.GetLastActiveVelocity().X < 0 &&
				( int )localPlayer.Input.GetLastActiveVelocity().Y == 0 )
			{
				position = new Vector2( localPlayer.Position.X - 30, localPlayer.Position.Y + 20 );
			}

			if( ( int )localPlayer.Input.GetLastActiveVelocity().X > 0 &&
				( int )localPlayer.Input.GetLastActiveVelocity().Y == 0 )
			{
				position = new Vector2( localPlayer.Position.X + localPlayer.GetRectangle().Width, localPlayer.Position.Y + 20 );
			}
			return position;
		}

		private int GetRandomItemType()
		{
			return randomNumberGenerator.Next( ( int )Identifiers.Coin, ( int )Identifiers.EggGrenade + 1 );
		}

		private void UpdateBoxTimer( GameTime gameTime )
		{
			spawnBoxTimer += ( float )gameTime.ElapsedGameTime.TotalSeconds;
		}

		private void UpdateGrenadeTimer( GameTime gameTime )
		{
			if( threwGrenade )
			{
				localGrenadeTimer += ( float )gameTime.ElapsedGameTime.TotalSeconds;
				if( ( int )localGrenadeTimer > 3 )
				{
					ResetGrenadeTimer();
					threwGrenade = false;
				}
			}
		}

		private void ResetBoxTimer()
		{
			spawnBoxTimer = 0.0f;
		}

		private void ResetGrenadeTimer()
		{
			localGrenadeTimer = 0.0f;
		}

		public void ClearAllItems()
		{
			Boxes.Clear();
			ConsumableItems.Clear();
			EggGrenades.Clear();
			spawnedItems.Clear();
			spawnBoxTimer = 0.0f;
			localGrenadeTimer = 0.0f;
			threwGrenade = false;
		}

		public List< GameObject > Boxes { get; private set; }
		public List< GameObject > ConsumableItems { get; private set; }
		public List< GameObject > EggGrenades { get; private set; }
		private List< bool > spawnedItems;
		private Microsoft.Xna.Framework.Content.ContentManager content;
		private Random randomNumberGenerator;
		private Rectangle mapBounds;
		private int maxBoxes;
		private int maxBoxHealth;
		private float spawnBoxTimer;
		private float localGrenadeTimer;
		private bool threwGrenade;
	}
}