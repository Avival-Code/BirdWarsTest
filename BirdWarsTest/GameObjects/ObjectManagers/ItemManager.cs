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
			Items = new List<GameObject>();
			xPositionGenerator = new Random();
			yPositionGenerator = new Random();
			content = contentIn;
			maxBoxes = 15;
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

		private Vector2 GetRandomMapPosition()
		{
			return new Vector2( xPositionGenerator.Next( mapBounds.X, mapBounds.Width - 50 ),
								yPositionGenerator.Next( mapBounds.Y, mapBounds.Height - 50 ) );
		}

		public void Update( INetworkManager networkManager, GameTime gameTime )
		{
			if( networkManager.IsHost() )
			{
				HandleSpawnBox( networkManager, gameTime );
			}
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraRenderBounds, Rectangle cameraBounds )
		{
			foreach( var box in Boxes )
			{
				if( cameraRenderBounds.Intersects( box.GetRectangle() ) )
				{
					box.Render( ref batch, cameraBounds );
				}
			}

			foreach( var item in Items )
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
			Items.Clear();
		}

		public List< GameObject > Boxes { get; private set; }
		public List< GameObject > Items { get; private set; }
		private Microsoft.Xna.Framework.Content.ContentManager content;
		private Random xPositionGenerator;
		private Random yPositionGenerator;
		private Rectangle mapBounds;
		private int maxBoxes;
		private int maxBoxHealth;
		private float spawnBoxTimer;
	}
}