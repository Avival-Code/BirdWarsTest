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
			PlayerManager = new PlayerManager();
			camera = new Camera2D();
		}

		public override void Init( StateHandler handler, StringManager stringManager ) {}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) {}
		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			networkManager.ProcessMessages( handler );
			if( PlayerManager.CreatedPlayers && !camera.isCameraSet )
			{
				camera.SetCamera( PlayerManager.GetLocalPlayer().Position );
			}
			//if( camera.isCameraSet )
			//{
			camera.Update( new Rectangle(0, 0, 2400, 1800), PlayerManager.GetLocalPlayer().GetRectangle() );
			//}
			PlayerManager.Update( this, state, gameTime, new Rectangle( 0, 0, 2400, 1800 ) );
		}

		public override void Render( ref SpriteBatch batch )
		{
			PlayerManager.Render( ref batch, camera.GetCameraBounds() );
		}

		private Camera2D camera;
		public PlayerManager PlayerManager { get; private set; }
	}
}