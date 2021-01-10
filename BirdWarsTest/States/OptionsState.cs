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
		public OptionsState( Microsoft.Xna.Framework.Content.ContentManager newContent, GameWindow gameWindowIn,
							 ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
							 int width_in, int height_in)
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			GameObjects = new List< GameObject >();
			gameWindow = gameWindowIn;
		}

		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			ClearContents();
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null, Identifiers.Background,
											 new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new DecorationBoxGraphicsComponent( Content, "Decorations/ConfigurationBox450x400" ),
											 null, Identifiers.ConfigurationBox, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Settings ), 
																		"Fonts/BabeFont_17" ),
											 null, Identifiers.TextGraphics, stateWidth, 50.0f ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.IpAddress ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics, stateWidth, GameObjects[ 2 ].Position.Y + 50.0f ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ), 
											 new TextAreaInputComponent( gameWindow ), Identifiers.TextArea,
											 stateWidth, GameObjects[ 3 ].Position.Y + 20.0f ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Port ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics, stateWidth, GameObjects[ 3 ].Position.Y + 50.0f ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ), Identifiers.TextArea, stateWidth, 
											 GameObjects[ 5 ].Position.Y + 20.0f ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
											 stringManager.GetString( StringNames.Connect ) ), new ConnectToServerInputComponent( handler ), 
											 Identifiers.Button2, stateWidth, GameObjects[ 6 ].Position.Y + 35.0f ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Language ), 
																		"Fonts/BabeFont_17" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, GameObjects[ 6 ].Position.Y + 80.0f ) ) );
			GameObjects.Add( new GameObject( new LanguageSelectorGraphicsComponent( Content, stringManager, 
											 stringManager.CurrentLanguage, new Vector2( 230.0f, GameObjects[ 6 ].Position.Y + 80.0f ) ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 230.0f, GameObjects[ 6 ].Position.Y + 80.0f ) ) );
			GameObjects.Add( new GameObject( null, new LanguageSelectorInputComponent( GameObjects[ 9 ], stringManager ),
											Identifiers.LanguageSelector, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.Accept ) ), 
											 new ButtonChangeConfigurationInputComponent( handler, GameObjects[ 10 ], stringManager ), 
											 Identifiers.Button2, stateWidth, GameObjects[ 8 ].Position.Y + 40.0f ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, Color.Red, "", "Fonts/BabeFont_8" ), null,
											 Identifiers.TextGraphics, stateWidth, GameObjects[ 11 ].Position.Y + 30 ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, Color.Blue, "", "Fonts/BabeFont_8" ), null,
											 Identifiers.TextGraphics, stateWidth, GameObjects[ 11 ].Position.Y + 30 ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void ClearContents()
		{
			GameObjects.Clear();
		}


		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			foreach( var objects in GameObjects )
			{
				objects.Update( state );
			}
			GameObjects[ 7 ].Update( state, this );
		}

		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			UpdateLogic( handler, state );
		}

		public override void Render( ref SpriteBatch batch ) 
		{
			foreach( var objects in GameObjects )
			{
				objects.Render( ref batch );
			}
		}

		public override void SetErrorMessage( string errorMessage )
		{
			GameObjects[ 13 ].Graphics.ClearText();
			( ( TextGraphicsComponent )GameObjects[ 12 ].Graphics ).SetText( errorMessage );
			GameObjects[ 12 ].RecenterXWidth( stateWidth );
		}

		public override void SetMessage( string message )
		{
			GameObjects[ 12 ].Graphics.ClearText();
			( ( TextGraphicsComponent )GameObjects[ 13 ].Graphics ).SetText( message );
			GameObjects[ 13 ].RecenterXWidth( stateWidth );
		}

		public List< GameObject > GameObjects { get; private set; }
		private GameWindow gameWindow;
	}
}