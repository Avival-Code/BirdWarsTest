/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Handles the item game objects creationg, destruction,
update, and display.
*********************************************/
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.AttackComponents;
using BirdWarsTest.AudioComponents;
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
	/// <summary>
	/// Handles the item game objects creationg, destruction,
    /// update, and display.
	/// </summary>
	public class ItemManager
	{
		/// <summary>
		/// Initializes a new instance of itemManager.
		/// </summary>
		/// <param name="contentIn">Game contentManager.</param>
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

		/// <summary>
		/// Sets the area limit of the game world.
		/// </summary>
		/// <param name="mapBoundsIn">Game world area rectangle.</param>
		public void SetMapBounds( Rectangle mapBoundsIn )
		{
			mapBounds = mapBoundsIn;
		}

		/// <summary>
		/// Spawns the first 10 boxes of the game round at random positions.
		/// </summary>
		public void SpawnBoxes()
		{
			for( int i = 0; i < 10; i++ )
			{
				Boxes.Add( new GameObject( new ItemBoxGraphicsComponent( content ), null, new HealthComponent( maxBoxHealth ),
										   null, null, new AudioComponent( content, "SoundEffects/HittingWoodSound" ),
										   Identifiers.ItemBox, GetRandomMapPosition() ) );
				spawnedItems.Add( false );
			}
		}

		/// <summary>
		/// Sends a SpawnConsumablesMessage to server.
		/// </summary>
		/// <param name="networkManager">Game networkmanager.</param>
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

		/// <summary>
		/// If the networkManager is the server, spawns a box at 
		/// a random position then sends a SpawnBoxMessage to all
		/// clients.
		/// </summary>
		/// <param name="networkManager">game networkManager.</param>
		private void SpawnBox( INetworkManager networkManager )
		{
			if( networkManager.IsHost() )
			{
				GameObject newBox = new GameObject( new ItemBoxGraphicsComponent( content ), null, new HealthComponent( maxBoxHealth ), 
													null, null, new AudioComponent( content, "SoundEffects/HittingWoodSound"), 
													Identifiers.ItemBox, GetRandomMapPosition() );
				Boxes.Add( newBox );
				spawnedItems.Add( false );
				List< GameObject > boxes = new List< GameObject >();
				boxes.Add( newBox );
				networkManager.SendSpawnBoxMessage( boxes );
			}
		}

		/// <summary>
		/// Sends a spawnGrenadeMessage to server.
		/// </summary>
		/// <param name="networkManager">Game networkManager.</param>
		/// <param name="playerManager">Player manager.</param>
		public void SpawnGrenade( INetworkManager networkManager, PlayerManager playerManager )
		{
			if( !threwGrenade && playerManager.GrenadeAmount > 0 )
			{
				GameObject temp = new GameObject( new ActiveEggGrenadeGraphicsComponent( content ), 
												  new GrenadeInputComponent( playerManager.GetLocalPlayer().Input.GetLastActiveVelocity(),
																			 playerManager.GetLocalPlayer().Input.GetObjectSpeed() ), 
												  new HealthComponent( 1 ), new GrenadeAttackComponent(),
												  new AudioComponent( content, "SoundEffects/ExplosionSound" ), Identifiers.EggGrenade,
												  GetGrenadePosition( playerManager.GetLocalPlayer() ) );
				EggGrenades.Add( temp );
				networkManager.SendSpawnGrenadeMessage( playerManager.GetLocalPlayer().Identifier, temp );
				threwGrenade = true;
				playerManager.GrenadeAmount -= 1;
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
											   new CoinEffectComponent( 1 ), new AudioComponent( content, "SoundEffects/CoinSound" ),
											   Identifiers.Coin, GetRandomLocalBoxPosition( Boxes[ boxIndex ] ) );
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

		/// <summary>
		/// Creates a box at the location written in the message.
		/// </summary>
		/// <param name="incomingMessage">The incoming spawn box message.</param>
		public void HandleSpawnBoxMessage( NetIncomingMessage incomingMessage )
		{
			int boxCount = incomingMessage.ReadInt32();
			for( int i = 0; i < boxCount; i++ )
			{
				Boxes.Add( new GameObject( new ItemBoxGraphicsComponent( content ), null, new HealthComponent( maxBoxHealth ),
										   null, null, new AudioComponent( content, "SoundEffects/HittingWoodSound" ),
										   Identifiers.ItemBox, new Vector2( incomingMessage.ReadFloat(), incomingMessage.ReadFloat() ) ) );
				spawnedItems.Add( false );
			}
		}

		/// <summary>
		/// Spawns the amount adn type of consumable items written 
		/// in the message.
		/// </summary>
		/// <param name="incomingMessage">The incoming SpawnConsumablesMessage.</param>
		public void HandleSpawnConsumablesMessage( NetIncomingMessage incomingMessage )
		{
			SpawnConsumablesMessage message = new SpawnConsumablesMessage( incomingMessage );
			for( int i = 0; i < message.Identifiers.Length; i++ )
			{
				switch( message.Identifiers[ i ] )
				{
					case ( int )Identifiers.Coin:
						ConsumableItems.Add( new GameObject( new CoinGraphicsComponent( content ), null, new HealthComponent( 1 ),
															 new CoinEffectComponent( 1 ), new AudioComponent( content, "SoundEffects/CoinSound" ), 
															 Identifiers.Coin, message.ObjectPositions[ i ] ) );
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

		/// <summary>
		/// Uses the box index contained in the incoming message and damages
		/// the box that has the same index. Used to keep item box health
		/// syncronized on all applications.
		/// </summary>
		/// <param name="boxDamageMessage">The incoming BoxDamageMessage.</param>
		/// <param name="playerManager">The game player manager.</param>
		public void HandleBoxDamageMessage( BoxDamageMessage boxDamageMessage, PlayerManager playerManager )
		{
			if( playerManager.GetLocalPlayer().Identifier != boxDamageMessage.PlayerWhoHitBoxID )
			{
				Boxes[ boxDamageMessage.BoxIndex ].Health.TakeDamageNoInvincibility( boxDamageMessage.Damage );
			}
		}
		
		/// <summary>
		/// Removes any items from the game that have already been picked up
		/// by other players.
		/// </summary>
		/// <param name="itemIndex">The incoming PickedUpItemmessage.</param>
		public void HandlePickedUpItemMessage( int itemIndex )
		{
			ConsumableItems[ itemIndex ].Health.TakeFullDamage();
		}

		/// <summary>
		/// Spawns a grenade at the position and with the velocity and speed
		/// written in the message.
		/// </summary>
		/// <param name="playerManager">The game player manager.</param>
		/// <param name="grenadeMessage">The spawn grenade message.</param>
		public void HandleSpawnGrenadeMessage( PlayerManager playerManager, SpawnGrenadeMessage grenadeMessage )
		{
			if( playerManager.GetLocalPlayer().Identifier != grenadeMessage.LocalPlayerId )
			{
				EggGrenades.Add( new GameObject( new ActiveEggGrenadeGraphicsComponent( content ),
												 new GrenadeInputComponent( grenadeMessage.Direction, grenadeMessage.GrenadeSpeed ),
												 new HealthComponent( 1 ), new GrenadeAttackComponent(),
												 new AudioComponent( content, "SoundEffects/ExplosionSound" ),
												 Identifiers.EggGrenade, grenadeMessage.Position ) );
			}
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

		private void HandleObjectDamage( INetworkManager networkManager, GameObject localPlayer,
										 Rectangle cameraRenderBounds )
		{
			HandlePlayerToBoxDamage( networkManager, localPlayer, cameraRenderBounds );
			HandleGrenadeToPlayerDamage( localPlayer );
			HandleGrenadeToBoxDamage( networkManager, cameraRenderBounds );
		}

		private void HandlePlayerToBoxDamage( INetworkManager networkManager, GameObject localPlayer,
											  Rectangle cameraRenderBounds )
		{
			for( int i = 0; i < Boxes.Count; i++ )
			{
				if( localPlayer.Attack.IsAttacking && !Boxes[ i ].Health.IsDead() && !Boxes[ i ].Health.TookDamage &&
					localPlayer.Attack.GetAttackRectangle( localPlayer ).Intersects( Boxes[ i ].GetRectangle() ) )
				{
					Boxes[ i ].Health.TakeDamage( localPlayer.Attack.Damage );
					if( cameraRenderBounds.Intersects( Boxes[ i ].GetRectangle() ) )
					{
						Boxes[ i ].Audio.Play();
					}
					networkManager.SendBoxDamageMessage( localPlayer.Identifier, i, localPlayer.Attack.Damage );
				}
			}
		}

		private void HandleGrenadeToPlayerDamage( GameObject localPlayer )
		{
			for( int i = 0; i < EggGrenades.Count; i++ )
			{
				if( EggGrenades[ i ].Attack.IsAttacking &&
					EggGrenades[ i ].Attack.GetAttackRectangle( EggGrenades[ i ] ).Intersects( 
																localPlayer.Health.GetPlayerHitBox( localPlayer ) ) )
				{
					localPlayer.Health.TakeDamage( EggGrenades[ i ].Attack.Damage );
				}
			}
		}

		private void HandleGrenadeToBoxDamage( INetworkManager networkManager, Rectangle cameraRenderBounds )
		{
			for( int i = 0; i < Boxes.Count; i++ )
			{
				for( int j = 0; j < EggGrenades.Count; j++ )
				{
					if( EggGrenades[ j ].Attack.IsAttacking &&
						EggGrenades[ j ].Attack.GetAttackRectangle( EggGrenades[ j ] ).Intersects( Boxes[ i ].GetRectangle() ) )
					{
						Boxes[ i ].Health.TakeDamage( EggGrenades[ j ].Attack.Damage );
						if( cameraRenderBounds.Intersects( Boxes[ i ].GetRectangle() ) )
						{
							Boxes[ i ].Audio.Play();
						}
						networkManager.SendBoxDamageMessage( EggGrenades[ j ].Identifier, i, EggGrenades[ j ].Attack.Damage );
					}
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
					ConsumableItems[ i ].Audio?.Play();
					networkManager.SendPickedUpItemMessage( i );
				}
			}
		}

		/// <summary>
		/// Updates all gameObjects, imposes map boundaries, and handles
		/// damage.
		/// </summary>
		/// <param name="networkManager">Game network manager.</param>
		/// <param name="playerManager">Player manager.</param>
		/// <param name="gameTime">The amount of elapsed game time.</param>
		/// <param name="mapBounds">Game world area rectangle.</param>
		/// <param name="cameraRenderBounds">Camera render area rectangle.</param>
		public void Update( INetworkManager networkManager, PlayerManager playerManager, GameTime gameTime,
							Rectangle mapBounds, Rectangle cameraRenderBounds )
		{
			SpawnConsumableItems( networkManager );
			if( networkManager.IsHost() )
			{
				HandleSpawnBox( networkManager, gameTime );
			}
			UpdateBoxes( gameTime );
			UpdateGrenades( gameTime, cameraRenderBounds );
			UpdateGrenadeTimer( gameTime );
			ImposeMapBoundaryLimits( mapBounds );
			HandleObjectDamage( networkManager, playerManager.GetLocalPlayer(), cameraRenderBounds );
			HandleConsumableItemPickup( networkManager, playerManager );
		}

		private void UpdateBoxes( GameTime gameTime )
		{
			foreach( var box in Boxes )
			{
				box.Update( gameTime );
			}
		}

		private void UpdateGrenades( GameTime gameTime, Rectangle cameraRenderBounds )
		{
			foreach( var grenade in EggGrenades )
			{
				grenade.Update( gameTime, cameraRenderBounds );
			}
		}

		private void ImposeMapBoundaryLimits( Rectangle MapBounds )
		{
			ImposeMapBoundaryGrenades( mapBounds );
		}

		private void ImposeMapBoundaryGrenades( Rectangle mapBounds )
		{
			foreach( var grenade in EggGrenades )
			{
				if( grenade.Position.X < mapBounds.Left )
				{
					grenade.Position = new Vector2( mapBounds.Left, grenade.Position.Y );
				}
				if( grenade.Position.Y < mapBounds.Top )
				{
					grenade.Position = new Vector2( grenade.Position.X, mapBounds.Top );
				}
				if( grenade.GetRectangle().Right > mapBounds.Right )
				{
					grenade.Position = new Vector2( mapBounds.Right - grenade.Graphics.GetTextureSize().X, grenade.Position.Y );
				}
				if( grenade.GetRectangle().Bottom > mapBounds.Bottom )
				{
					grenade.Position = new Vector2( grenade.Position.X, mapBounds.Bottom - grenade.Graphics.GetTextureSize().Y );
				}
			}
		}

		/// <summary>
		/// Draws all objects that are within the camera render 
		/// bounds on the screen.
		/// </summary>
		/// <param name="batch">Game spritebatch.</param>
		/// <param name="cameraRenderBounds">The current camera render area rectangle.</param>
		/// <param name="cameraBounds">The current area that is seen by the user.</param>
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

			if( ( int )localPlayer.Input.GetLastActiveVelocity().X < 0 &&
				( int )localPlayer.Input.GetLastActiveVelocity().Y < 0 )
			{
				position = localPlayer.Position;
			}

			if( ( int )localPlayer.Input.GetLastActiveVelocity().X > 0 &&
				( int )localPlayer.Input.GetLastActiveVelocity().Y < 0 )
			{
				position = new Vector2( localPlayer.Position.X + localPlayer.GetRectangle().Width, localPlayer.Position.Y );
			}

			if( ( int )localPlayer.Input.GetLastActiveVelocity().X < 0 &&
				( int )localPlayer.Input.GetLastActiveVelocity().Y > 0 )
			{
				position = new Vector2( localPlayer.Position.X, localPlayer.Position.Y + localPlayer.GetRectangle().Height );
			}

			if( ( int )localPlayer.Input.GetLastActiveVelocity().X > 0 &&
				( int )localPlayer.Input.GetLastActiveVelocity().Y > 0 )
			{
				position = new Vector2( localPlayer.Position.X + localPlayer.GetRectangle().Width, 
									    localPlayer.Position.Y + localPlayer.GetRectangle().Height );
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
				if( localGrenadeTimer > 2.25 )
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

		/// <summary>
		/// Removes all gameObjects from the manager and
		/// reset values.
		/// </summary>
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

		/// <value>The list game boxes.</value>
		public List< GameObject > Boxes { get; private set; }

		/// <value>The list of game consumable items.</value>
		public List< GameObject > ConsumableItems { get; private set; }

		/// <value>The list game egg grenades.</value>
		public List< GameObject > EggGrenades { get; private set; }
		private List< bool > spawnedItems;
		private Microsoft.Xna.Framework.Content.ContentManager content;
		private Random randomNumberGenerator;
		private Rectangle mapBounds;
		private readonly int maxBoxes;
		private readonly int maxBoxHealth;
		private float spawnBoxTimer;
		private float localGrenadeTimer;
		private bool threwGrenade;
	}
}