using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	class PasswordRecoveryState : GameState
	{
		public PasswordRecoveryState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						   GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics,
						   int width_in, int height_in )
			:
			base( newContent, ref newGraphics, width_in, height_in )
		{
			gameObjects = new List< GameObject >();
		}

		public override void Init( StateHandler handler ) 
		{
			gameObjects.Clear();
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null,
										 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new MenuBoxGraphicsComponent( content ), null,
										 Identifiers.PasswordBox, new Vector2( 0.0f, 0.0f ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( KeyboardState state ) 
		{
			foreach( var objects in gameObjects )
				objects.Update( state );
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			foreach( var objects in gameObjects )
				objects.Render( ref sprites );
		}

		private List< GameObject > gameObjects;
	}
}
