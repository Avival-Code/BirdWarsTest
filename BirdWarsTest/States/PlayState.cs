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
		}

		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
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
			if( PlayerManager.CreatedPlayers && !camera.isCameraSet )
			{
				camera.SetCamera( PlayerManager.GetLocalPlayer().Position );
			}

			camera.Update( mapManager.GetMapBounds(), PlayerManager.GetLocalPlayer().GetRectangle() );
			DisplayManager.Update( gameTime );
			PlayerManager.Update( this, state, gameTime, mapManager.GetMapBounds() );
			ItemManager.Update( networkManager, PlayerManager, gameTime );
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

		public PlayerManager PlayerManager { get; private set; }
		public ItemManager ItemManager { get; private set; }
		public HeadsUpDisplayManager DisplayManager { get; private set; }
		private MapManager mapManager;
		private Camera2D camera;
	}
}