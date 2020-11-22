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
	class StatisticsState : GameState
	{
		public StatisticsState( Microsoft.Xna.Framework.Content.ContentManager newContent,
								ref GraphicsDeviceManager newGraphics, ref INetworkManager networkManagerIn,
								int width_in, int height_in )
			:
			base( newContent, ref newGraphics, ref networkManagerIn, width_in, height_in )
		{
			gameObjects = new List< GameObject >();
		}

		public override void Init( StateHandler handler ) 
		{
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( content ), null, Identifiers.Background,
											 new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new RegisterBoxGraphicsComponent( content ), null, 
											 Identifiers.RegisterBox, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "User Statistics", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics, stateWidth, gameObjects[ 1 ].Position.Y + 50 ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Matches Played.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics, 
											 new Vector2( 70.0f, gameObjects[ 2 ].Position.Y + 50 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "0", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 2 ].Position.Y + 50 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Matches Won.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 3 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "0", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 3 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Win Rate.......", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 5 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "0%", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 5 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Matches Survived.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 7 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "0", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 7 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Survival Rate.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 9 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "0%", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 9 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Matches Lost.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 11 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "0", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 11 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Lose Rate.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 13 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "0%", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 13 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "Shortest Match Time.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 15 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( content, "0:00", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 15 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( content, "Button2", "Return" ), 
											 new ButtonChangeStateInputComponent( handler, StateTypes.MainMenuState ),
											 Identifiers.Button2, stateWidth, gameObjects[ 17 ].Position.Y + 40.0f ) );
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void HandleInput( KeyboardState state ) {}

		public override void UpdateLogic( StateHandler handler, KeyboardState state ) 
		{
			foreach( var objects in gameObjects )
			{
				objects.Update( state, this );
			}
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