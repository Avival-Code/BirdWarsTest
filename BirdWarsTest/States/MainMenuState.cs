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
	class MainMenuState : GameState
	{
		public MainMenuState(Microsoft.Xna.Framework.Content.ContentManager newContent,
							  ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
							  int width_in,
							  int height_in)
			:
			base(newContent, ref newGraphics, ref networkManagerIn, width_in, height_in)
		{
			gameObjects = new List<GameObject>();
		}

		public override void Init( StateHandler handler, StringManager stringManager )
		{
			ClearContents();
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null, Identifiers.Background,
											 new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new DecorationGraphicsComponent( Content, "Decorations/MainMenuBar800x110" ),
											 null, Identifiers.Decoration, new Vector2( 0.0f, 60.0f ) ) );
			gameObjects.Add( new GameObject( new DecorationGraphicsComponent( Content, "Logos/BirdWarsLogo_440x246" ),
											 null, Identifiers.Decoration, new Vector2( 0.0f, 20.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.FindGame ) ), 
										     new FindGameInputComponent(), Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 2 ].Position.Y + 270.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.CreateGame ) ), 
											 new CreateLobbyInputComponent(), Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 3 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.Statistics ) ), 
										     new SelectorChangeStateInputComponent( handler, StateTypes.StatisticsState ), 
											 Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 4 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.Settings ) ), 
											 new SelectorChangeStateInputComponent( handler, StateTypes.OptionsState ), 
											 Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 5 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( new MenuOptionGraphicsComponent( Content, stringManager.GetString( StringNames.Logout ) ), 
											 new LogoutInputComponent( handler ), Identifiers.MenuOption,
											 new Vector2( 0.0f, gameObjects[ 6 ].Position.Y + 60.0f ) ) );
			gameObjects.Add( new GameObject( null, new SelectorInputComponent( GetMenuOptions() ), Identifiers.Selector,
											 new Vector2( 0.0f, 0.0f ) ) );
		}

		public override void Pause() { }

		public override void Resume() { }

		public override void ClearContents()
		{
			gameObjects.Clear();
		}

		public override void HandleInput(KeyboardState state) { }

		public override void UpdateLogic( StateHandler handler, KeyboardState state )
		{
			networkManager.ProcessMessages( handler );
			gameObjects[ 8 ].Update( state, this );
		}

		public override void UpdateLogic( StateHandler handler, KeyboardState state, GameTime gameTime )
		{
			UpdateLogic( handler, state );
		}

		public override void Render(ref SpriteBatch sprites)
		{
			foreach( var objects in gameObjects )
				objects.Render(ref sprites);
		}

		private List<GameObject> GetMenuOptions()
		{
			List<GameObject> menuOptions = new List<GameObject>();
			foreach( var objects in gameObjects )
			{
				if( objects.Identifier == Identifiers.MenuOption )
				{
					menuOptions.Add( objects );
				}
			}
			return menuOptions;
		}

		public INetworkManager NetworkManager
		{
			get{ return networkManager; }
		}

		private List< GameObject > gameObjects;
	}
}