using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GameObjects.ObjectManagers;
using BirdWarsTest.Utilities;
using System.Collections.Generic;

namespace BirdWarsTest.States
{
	class WaitingRoomState : GameState
	{
		public WaitingRoomState( Microsoft.Xna.Framework.Content.ContentManager newContent,
								 GameWindow gameWindowIn, ref GraphicsDeviceManager newGraphics, 
								 ref INetworkManager networkManagerIn, int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameWindow = gameWindowIn;
			GameObjects = new List< GameObject >();
			UsernameManager = new ChatUsernameManager();
			MessageManager = null;
		}

		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			ClearContents();
			UsernameManager.AddObjects( Content );
			GameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null, 
											 Identifiers.Background, new Vector2( 0.0f, 0.0f ) ) );
			GameObjects.Add( new GameObject( new DecorationGraphicsComponent( Content, "TextAreas/ChatBoard530x480" ), 
											 null, Identifiers.TextArea, new Vector2( 250.0f, 20.0f ) ) );
			GameObjects.Add( new GameObject( new TextAreaGraphicsComponent( Content, "TextAreas/ChatTextArea530x35" ),
											 new TextAreaInputComponent( gameWindow ), 
											 Identifiers.TextArea, new Vector2( 250.0f, 520.0f ) ) );
			GameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.Players ), 
																		"Fonts/BabeFont_22" ), 
											 null, Identifiers.TextGraphics, new Vector2( 65.0f, 20.0f ) ) );
			AddStateButtons( handler, stringManager );
			MessageManager = new ChatMessageManager( Content, GameObjects[ 1 ].GetRectangle() );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void ClearContents()
		{
			UsernameManager.ClearObjects();
			GameObjects.Clear();
			MessageManager = null;
		}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			networkManager.ProcessMessages( handler );
			foreach( var objects in GameObjects )
			{
				objects.Update( state );
			}
			UpdateButtonLogic( state );
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
			MessageManager.Render( ref batch );
			UsernameManager.Render( ref batch );
		}

		private void AddStateButtons( StateHandler handler, StringManager stringManager )
		{
			if( networkManager.IsHost() )
			{
				GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2",
																			  stringManager.GetString( StringNames.StartRound ) ),
												 new StartRoundInputComponent( handler ), Identifiers.Button2,
												 new Vector2( 25.0f, 520.0f ) ) );
			}
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2",
																		  stringManager.GetString( StringNames.Leave ) ),
											 new LeaveGameInputComponent( handler, StateTypes.MainMenuState ), Identifiers.Button2,
											 new Vector2( 25.0f, 555.0f ) ) );
			GameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2",
																		  stringManager.GetString( StringNames.SendMessage  ) ), 
											 new SendChatMessageInputComponent( handler ), Identifiers.ChatMessageSender,
											 new Vector2( 645.0f, 555.0f ) ) );
		}

		private void UpdateButtonLogic( KeyboardState state )
		{
			if( networkManager.IsHost() )
			{
				GameObjects[ 5 ].Update( state, this );
				GameObjects[ 6 ].Update( state, this );
			}
			else
			{
				GameObjects[ 4 ].Update( state, this );
				GameObjects[ 5 ].Update( state, this );
			}
		}

		public INetworkManager NetworkManager
		{
			get{ return networkManager; }
		}

		public List<GameObject> GameObjects { get; set; }
		public ChatUsernameManager UsernameManager { get; set; }
		public ChatMessageManager MessageManager { get; set; }

		private GameWindow gameWindow;
	}
}