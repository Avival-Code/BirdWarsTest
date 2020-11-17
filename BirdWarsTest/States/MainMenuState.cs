using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using System;

namespace BirdWarsTest.States
{
	class MainMenuState : GameState
	{
		public MainMenuState( Microsoft.Xna.Framework.Content.ContentManager newContent,
							  ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
							  int width_in,
							  int height_in)
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{}

		public override void Init( StateHandler handler ) {}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state )
		{
			HandleInput( state );
			Console.WriteLine( networkManager.GetConnectionState() );
		}

		public override void Render( ref SpriteBatch sprites ) {}
	}
}