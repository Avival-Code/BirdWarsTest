using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.GameRounds;
using BirdWarsTest.Utilities;

namespace BirdWarsTest.States
{
	class PlayState : GameState
	{
		public PlayState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						  ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn, 
						  int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			DisplayManager = new HeadsUpDisplayManager();
			PlayerManager = new PlayerManager();
			mapManager = new MapManager();
			ItemManager = new ItemManager( Content );
			camera = new Camera2D();
			sentEndRoundMessage = false;
		}

		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			ClearAllGameObjects();
			DisplayManager.InitializeInterfaceComponents( Content, handler );
			mapManager.InitializeMapTiles( Content );
			ItemManager.SetMapBounds( mapManager.GetMapBounds() );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void ClearContents() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) {}
		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			networkManager.ProcessMessages( handler );
			camera.Update( PlayerManager.GetLocalPlayer().Position, mapManager.GetMapBounds(), 
						   PlayerManager.GetLocalPlayer().GetRectangle(), PlayerManager.CreatedPlayers );
			DisplayManager.Update( gameTime );
			PlayerManager.Update( this, gameTime, state, mapManager.GetMapBounds(), networkManager );
			ItemManager.Update( networkManager, PlayerManager, gameTime, mapManager.GetMapBounds() );
			CheckEndRound();
		}

		public override void Render( ref SpriteBatch batch )
		{
			mapManager.Render( ref batch, camera.GetCameraRenderBounds(), camera.GetCameraBounds() );
			PlayerManager.Render( ref batch, camera.GetCameraRenderBounds(), camera.GetCameraBounds() );
			ItemManager.Render( ref batch, camera.GetCameraRenderBounds(), camera.GetCameraBounds() );
			DisplayManager.Render( ref batch, PlayerManager.GetLocalPlayer().Health );
		}

		public INetworkManager NetWorkManager 
		{
			get { return networkManager; }
		}

		private void CheckEndRound()
		{
			if( networkManager.IsHost() )
			{
				if( ( !sentEndRoundMessage && DisplayManager.IsRoundTimeOver() ) ||
					( !sentEndRoundMessage && PlayerManager.GetNumberOfPlayersStillAlive() == 1 ) )
				{
					sentEndRoundMessage = true;
					networkManager.SendRoundFinishedMessage( DisplayManager.GetRemainingRoundTime() );
				}
			}
		}

		private void ClearAllGameObjects()
		{
			mapManager.ClearAllTiles();
			PlayerManager.ClearAllPlayers();
			DisplayManager.ClearAllDisplayObjects();
			ItemManager.ClearAllItems();
			camera.ResetCamera();
			sentEndRoundMessage = false;
		}

		public PlayerManager PlayerManager { get; private set; }
		public ItemManager ItemManager { get; private set; }
		public HeadsUpDisplayManager DisplayManager { get; private set; }
		private MapManager mapManager;
		private Camera2D camera;
		private bool sentEndRoundMessage;
	}
}