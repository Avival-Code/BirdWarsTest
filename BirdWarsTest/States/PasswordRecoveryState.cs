using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.Network;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	class PasswordRecoveryState : GameState
	{
		public PasswordRecoveryState( Microsoft.Xna.Framework.Content.ContentManager newContent,
						   GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics,
						   ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			GameObjects = new List< GameObject >();
			gameWindow = gameWindowIn;
		}

		public override void Init( StateHandler handler ) 
		{
			GameObjects.Clear();
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null,
										 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new MenuBoxGraphicsComponent( Content ), null,
										 Identifiers.PasswordBox, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Recover Password", "Fonts/MainFont_S10" ),
							                 null, Identifiers.TextGraphics, stateWidth, 50.0f ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Email", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics, stateWidth, 
											 GameObjects[ 2 ].Position.Y + 50.0f ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
										     Identifiers.TextArea, stateWidth,
										     GameObjects[ 3 ].Position.Y + 20 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", "Send Code" ),
											 new SolicitPasswordResetInputComponent( handler ), 
											 Identifiers.Button1, stateWidth, 
										     GameObjects[ 4 ].Position.Y + 40.0f ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Code", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics, stateWidth,
											 GameObjects[ 5 ].Position.Y + 60.0f ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
											 Identifiers.TextArea, stateWidth,
											 GameObjects[ 6 ].Position.Y + 20 ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "New Password", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics, stateWidth,
											 GameObjects[ 7 ].Position.Y + 35.0f ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextArea1" ),
											 new TextAreaInputComponent( gameWindow ),
											 Identifiers.TextArea, stateWidth,
											 GameObjects[ 8 ].Position.Y + 20 ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", "Reset" ),
											 new ResetPasswordInputComponent( handler ), 
											 Identifiers.Button1, 
											 new Vector2( GameObjects[ 9 ].Position.X - 30,
														  GameObjects[ 9 ].Position.Y + 50.0f ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", "Cancel" ),
											 new ButtonChangeStateInputComponent( handler, StateTypes.LoginState ),
										     Identifiers.Button1, 
											 new Vector2( GameObjects[ 10 ].Position.X + 160.0f,
												          GameObjects[ 9 ].Position.Y + 50.0f ) ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			foreach( var objects in GameObjects )
				objects.Update( state, this );
		}

		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			UpdateLogic( handler, state );
		}

		public override void Render( ref SpriteBatch sprites ) 
		{
			foreach( var objects in GameObjects )
				objects.Render( ref sprites );
		}

		public List<GameObject> GameObjects { get; set; }

		private GameWindow gameWindow;
	}
}
