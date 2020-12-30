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
			displayManager = new HeadsUpDisplayManager();
			PlayerManager = new PlayerManager();
			mapManager = new MapManager();
			camera = new Camera2D();
		}

		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			displayManager.InitializeInterfaceComponents( Content );
			mapManager.InitializeMapTiles( Content );
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
			displayManager.Update( gameTime );
			PlayerManager.Update( this, state, gameTime, mapManager.GetMapBounds() );
		}

		public override void Render( ref SpriteBatch batch )
		{
			mapManager.Render( ref batch, camera.GetCameraRenderBounds(), camera.GetCameraBounds() );
			PlayerManager.Render( ref batch, camera.GetCameraRenderBounds(), camera.GetCameraBounds() );
			displayManager.Render( ref batch, PlayerManager.GetLocalPlayer().Health );
		}

		private Camera2D camera;
		public PlayerManager PlayerManager { get; private set; }
		private HeadsUpDisplayManager displayManager;
		private MapManager mapManager;
	}
}