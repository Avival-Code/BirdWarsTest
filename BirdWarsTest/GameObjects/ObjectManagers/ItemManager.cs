using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.Network;
using BirdWarsTest.Network.Messages;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	public class ItemManager
	{
		public ItemManager( Microsoft.Xna.Framework.Content.ContentManager contentIn )
		{
			Boxes = new List<GameObject>();
			ConsumableItems = new List<GameObject>();
			boxPositionGenerator = new Random();
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
			}
		}

		private void SpawnBox( INetworkManager networkManager )
		{
			if( networkManager.IsHost() )
			{
				GameObject newBox = new GameObject( new ItemBoxGraphicsComponent( content ), null, 
													new HealthComponent( maxBoxHealth ), Identifiers.ItemBox, GetRandomMapPosition() );
				Boxes.Add( newBox );
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
			}
		}

		public void HandleBoxDamageMessage( NetIncomingMessage incomingMessage )
		{
			int index = incomingMessage.ReadInt32();
			Boxes[ index ].Health.TakeDamage( incomingMessage.ReadInt32() );
		}

		private Vector2 GetRandomMapPosition()
		{
			return new Vector2( boxPositionGenerator.Next( mapBounds.X, mapBounds.Width - 50 ),
								boxPositionGenerator.Next( mapBounds.Y, mapBounds.Height - 50 ) );
		}

		public void Update( INetworkManager networkManager, GameObject localPlayer, GameTime gameTime )
		{
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

		private void HandleSpawnBox( INetworkManager networkManager, GameTime gameTime )
		{
			UpdateTimer( gameTime );
			if ( ( int )spawnBoxTimer >= 30 && Boxes.Count < maxBoxes )
			{
				SpawnBox( networkManager );
				ResetTimer();
			}
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
		private Microsoft.Xna.Framework.Content.ContentManager content;
		private Random boxPositionGenerator;
		private Rectangle mapBounds;
		private int maxBoxes;
		private int maxBoxHealth;
		private float spawnBoxTimer;
	}
}