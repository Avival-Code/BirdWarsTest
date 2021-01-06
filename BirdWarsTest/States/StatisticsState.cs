using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.Network;
using BirdWarsTest.GameObjects;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.Utilities;
using System.Collections.Generic;
using System;

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

		public override void Init( StateHandler handler, StringManager stringManager ) 
		{
			ClearContents();
			gameObjects.Add( new GameObject( new SolidRectGraphicsComponent( Content ), null, Identifiers.Background,
											 new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new RegisterBoxGraphicsComponent( Content ), null, 
											 Identifiers.RegisterBox, new Vector2( 0.0f, 0.0f ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.UserStats ), 
																		"Fonts/BabeFont_17" ),
											 null, Identifiers.TextGraphics, stateWidth, gameObjects[ 1 ].Position.Y + 50 ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.MatchesPlayed ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics, 
											 new Vector2( 70.0f, gameObjects[ 2 ].Position.Y + 50 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0", "Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 324.0f, gameObjects[ 2 ].Position.Y + 50 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.MatchesWon ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 3 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0", "Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 324.0f, gameObjects[ 3 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.WinRate ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 5 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0%", "Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 324.0f, gameObjects[ 5 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.MatchesSurvived ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 7 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0", "Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 324.0f, gameObjects[ 7 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.SurvivalRate ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 9 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0%", "Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 324.0f, gameObjects[ 9 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.MatchesLost ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 11 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0", "Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 324.0f, gameObjects[ 11 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.LoseRate ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 13 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0%", "Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 324.0f, gameObjects[ 13 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, stringManager.GetString( StringNames.ShortestTime ), 
																		"Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 70.0f, gameObjects[ 15 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new TextGraphicsComponent( Content, "0:00", "Fonts/BabeFont_10" ),
											 null, Identifiers.TextGraphics,
											 new Vector2( 324.0f, gameObjects[ 15 ].Position.Y + 40 ) ) );
			gameObjects.Add( new GameObject( new ButtonGraphicsComponent( Content, "Button2", 
																		  stringManager.GetString( StringNames.Return ) ), 
											 new ButtonChangeStateInputComponent( handler, StateTypes.MainMenuState ),
											 Identifiers.Button2, stateWidth, gameObjects[ 17 ].Position.Y + 40.0f ) );
			SetGameObjectStatistics();
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void ClearContents()
		{
			gameObjects.Clear();
		}

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

		private void SetPercentStatistic( GameObject gameObject, double statistic )
		{
			int result = ( int )statistic;
			string value = result.ToString();
			gameObject.Graphics.SetText( value + "%" );
		}

		private void SetStringStatistic( GameObject gameObject, string statistic )
		{
			gameObject.Graphics.SetText( statistic );
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
			SetStringStatistic( gameObjects[ 18 ], CalculateShortestRoundTime() );
		}

		private double CalculateWinRate()
		{
			if( networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed == 0 )
			{
				return 0.0f;
			}
			return ( double )networkManager.GetLoginSession().CurrentAccount.MatchesWon /
				   ( double )networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed * 100.0;
		}

		private double CalculateSurvivalRate()
		{
			if( networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed == 0 )
			{
				return 0.0f;
			}
			return ( double )networkManager.GetLoginSession().CurrentAccount.MatchesSurvived /
				   ( double )networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed * 100.0;
		}

		private double CalculateLoseRate()
		{
			if( networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed == 0 )
			{
				return 0.0f;
			}
			return ( double )networkManager.GetLoginSession().CurrentAccount.MatchesLost /
				   ( double )networkManager.GetLoginSession().CurrentAccount.TotalMatchesPlayed * 100.0;
		}

		private string CalculateShortestRoundTime()
		{
			int seconds = networkManager.GetLoginSession().CurrentAccount.Seconds;
			if( seconds == 0 )
			{
				return "0:00";
			}
			return CalculateMinutes( seconds ) + ":" + CalculateSeconds( seconds );
		}

		private string CalculateMinutes( int totalSeconds )
		{
			return ( totalSeconds / 60 ).ToString();
		}

		private string CalculateSeconds( int totalSeconds )
		{
			int seconds = totalSeconds % 60;
			if( seconds > -1 && seconds < 10 )
			{
				return "0" + seconds.ToString();
			}
			else
			{
				return seconds.ToString();
			}
		}

		private List< GameObject > gameObjects;
	}
}