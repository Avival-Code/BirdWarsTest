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
			gameObjects.Clear();
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null, Identifiers.Background,
											 new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new RegisterBoxGraphicsComponent( Content ), null, 
											 Identifiers.RegisterBox, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "User Statistics", "Fonts/MainFont_S15" ),
											 null, Identifiers.TextGraphics, stateWidth, gameObjects[ 1 ].Position.Y + 50 ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Matches Played.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics, 
											 new Vector2( 70.0f, gameObjects[ 2 ].Position.Y + 50 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 2 ].Position.Y + 50 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Matches Won.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 3 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 3 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Win Rate.......", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 5 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0%", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 5 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Matches Survived.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 7 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 7 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Survival Rate.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 9 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0%", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 9 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Matches Lost.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 11 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 11 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Lose Rate.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 13 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0%", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 13 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "Shortest Match Time.....", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 15 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0:00", "Fonts/MainFont_S10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 334.0f, gameObjects[ 15 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", "Return" ), 
											 new ButtonChangeStateInputComponent( handler, StateTypes.MainMenuState ),
											 Identifiers.Button2, stateWidth, gameObjects[ 17 ].Position.Y + 40.0f ) );
			SetGameObjectStatistics();
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

		private void SetStatistic( GameObject gameObject, int statistic )
		{
			gameObject.Graphics.SetText( statistic.ToString() );
		}

		private void SetPercentStatistic( GameObject gameObject, float statistic )
		{
			string value = statistic.ToString();
			gameObject.Graphics.SetText( value + "%" );
		}

		private void SetGameObjectStatistics()
		{
			SetStatistic( gameObjects[ 4 ], networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed );
			SetStatistic( gameObjects[ 6 ], networkManager.GetLoginSession().CurrentAccount.MatchesWon );
			SetPercentStatistic( gameObjects[ 8 ], CalculateWinRate() );
			SetStatistic( gameObjects[ 10 ], networkManager.GetLoginSession().CurrentAccount.MatchesSurvived );
			SetPercentStatistic( gameObjects[ 12 ], CalculateSurvivalRate() );
			SetStatistic( gameObjects[ 14 ], networkManager.GetLoginSession().CurrentAccount.MatchesLost );
			SetPercentStatistic( gameObjects[ 16 ], CalculateLoseRate() );
		}

		private float CalculateWinRate()
		{
			if( networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed == 0 )
			{
				return 0.0f;
			}
			return networkManager.GetLoginSession().CurrentAccount.MatchesWon /
				   networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed;
		}

		private float CalculateSurvivalRate()
		{
			if( networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed == 0 )
			{
				return 0.0f;
			}
			return networkManager.GetLoginSession().CurrentAccount.MatchesSurvived /
				   networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed;
		}

		private float CalculateLoseRate()
		{
			if( networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed == 0 )
			{
				return 0.0f;
			}
			return networkManager.GetLoginSession().CurrentAccount.MatchesLost /
				   networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed;
		}

		private List< GameObject > gameObjects;
	}
}