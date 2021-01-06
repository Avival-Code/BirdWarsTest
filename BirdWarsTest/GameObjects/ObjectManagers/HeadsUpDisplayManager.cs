using BirdWarsTest.GraphicComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	public class HeadsUpDisplayManager
	{
		public HeadsUpDisplayManager()
		{
			gameObjects = new List< GameObject >();
		}

		public void InitializeInterfaceComponents( Microsoft.Xna.Framework.Content.ContentManager content,
												   StateHandler handler )
		{
			gameObjects.Add( new GameObject( new LifeBarGraphicsComponent( content ), null, 
											 Identifiers.LifeBar, new Vector2( 15.0f, 15.0f ) ) );
			gameObjects.Add( new GameObject( new RoundTimerGraphicsComponent( content ), new RoundTimeInputComponent( handler ),
											 Identifiers.Timer, new Vector2( 375.0f, 15.0f ) ) );
		}

		public void Update( GameTime gameTime )
		{
			gameObjects[ 1 ].Update( gameTime );
		}

		public void Render( ref SpriteBatch batch, HealthComponent localPlayerHealth )
		{
			gameObjects[ 0 ].Render( ref batch, localPlayerHealth );
			gameObjects[ 1 ].Render( ref batch );
		}

		public void HandleUpdateRoundTimeMessage( float remainingTime )
		{
			( ( RoundTimeInputComponent )gameObjects[ 1 ].Input ).SetRemainingRoundTime( remainingTime );
		}

		public bool IsRoundTimeOver()
		{
			return ( ( RoundTimeInputComponent )gameObjects[ 1 ].Input ).IsRoundTimeOver();
		}

		public void ClearAllDisplayObjects()
		{
			gameObjects.Clear();
		}

		public int GetRemainingRoundTime()
		{
			return ( int )( ( RoundTimeInputComponent )gameObjects[1].Input ).RemainingRoundTime;
		}

		private List< GameObject > gameObjects;
	}
}