using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using System.Collections.Generic;

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
		{
			gameObjects = new List< GameObject >();
		}

		public override void Init( StateHandler handler ) 
		{
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null, Identifiers.Background,
											 new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new DecorationGraphicsComponent( content, "Decorations/MainMenuBar800x110" ),
				                             null, Identifiers.Decoration, new Vector2( 0.0f, 60.0f ) ) );
			gameObjects.Add( new GameObject( new DecorationGraphicsComponent( content, "Logos/BirdWarsLogo_440x246" ),
											 null, Identifiers.Decoration, new Vector2( 0.0f, 20.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( content, "Find Game" ), null, Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 2 ].position.Y + 270.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( content, "Create Game" ), null, Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 3 ].position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( content, "Statistics" ), null, Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 4 ].position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( content, "Settings" ), null, Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 5 ].position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( content, "Logout" ), null, Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 6 ].position.Y + 60.0f ) ) );
			List< GameObject > temp = new List< GameObject >();
			temp.Add( gameObjects[ 3 ] );
			temp.Add( gameObjects[ 4 ] );
			temp.Add( gameObjects[ 5 ] );
			temp.Add( gameObjects[ 6 ] );
			temp.Add( gameObjects[ 7 ] );
			gameObjects.Add( new GameObject( null, new SelectorInputComponent( temp ), Identifiers.Selector, 
										     new Vector2( 0.0f, 0.0f ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state )
		{
			HandleInput( state );
			gameObjects[ 8 ].Update( state );
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			foreach (var objects in gameObjects)
				objects.Render( ref sprites );
		}

		private List< GameObject > gameObjects;
	}
}