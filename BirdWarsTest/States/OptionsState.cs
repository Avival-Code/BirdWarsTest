using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.Utilities;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	class OptionsState : GameState
	{
		public OptionsState( Microsoft.Xna.Framework.Content.ContentManager newContent,
							 ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
							 int width_in, int height_in)
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameObjects = new List< GameObject >();
		}

		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			gameObjects.Clear();
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null, Identifiers.Background,
											 new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new DecorationBoxGraphicsComponent( Content, "Decorations/ConfigurationBox450x400" ),
											 null, Identifiers.ConfigurationBox, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Settings", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics, stateWidth, 55.0f ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Volume", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics, 
											 new Vector2( 70.0f, gameObjects[ 2 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "50%", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 305.0f, gameObjects[ 2 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Keyboard", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 4 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Standard", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 275.0f, gameObjects[ 4 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Language", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 6 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "English", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 280.0f, gameObjects[ 6 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", "Accept" ), 
											 new ButtonChangeStateInputComponent( handler, StateTypes.MainMenuState ), 
											 Identifiers.Button2, stateWidth, gameObjects[ 8 ].Position.Y + 60.0f ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			foreach( var objects in gameObjects )
			{
				objects.Update( state );
			}
		}
		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			UpdateLogic( handler, state );
		}

		public override void Render( ref SpriteBatch batch ) 
		{
			foreach( var objects in gameObjects )
			{
				objects.Render( ref batch );
			}
		}

		private List< GameObject > gameObjects;
	}
}