/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Stores, updates and renders the game obejcts responsible
for displaying the local player lifebar and round timer.
*********************************************/
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.InputComponents;
using BirdWarsTest.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BirdWarsTest.GameObjects.ObjectManagers
{
	/// <summary>
	/// Stores, updates and renders the game obejcts responsible
	/// for displaying the local player lifebar and round timer.
	/// </summary>
	public class HeadsUpDisplayManager
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public HeadsUpDisplayManager()
		{
			gameObjects = new List< GameObject >();
		}

		/// <summary>
		/// Creates the lifebar a round time game objects.
		/// </summary>
		/// <param name="content">Game contentManager.</param>
		/// <param name="handler">Game state handler.</param>
		public void InitializeInterfaceComponents( Microsoft.Xna.Framework.Content.ContentManager content,
												   StateHandler handler )
		{
			gameObjects.Add( new GameObject( new LifeBarGraphicsComponent( content ), null, 
											 Identifiers.LifeBar, new Vector2( 15.0f, 15.0f ) ) );
			gameObjects.Add( new GameObject( new RoundTimerGraphicsComponent( content ), new RoundTimeInputComponent( handler ),
											 Identifiers.Timer, new Vector2( 375.0f, 15.0f ) ) );
		}

		/// <summary>
		/// Updates the round time gamobject.
		/// </summary>
		/// <param name="gameTime">Game time class that holds elapsed frame time.</param>
		public void Update( GameTime gameTime )
		{
			gameObjects[ 1 ].Update( gameTime );
		}

		/// <summary>
		/// Draws the gameObjects on the screen.
		/// </summary>
		/// <param name="batch">Game spritebatch.</param>
		/// <param name="localPlayerHealth">The local player's current health.</param>
		public void Render( ref SpriteBatch batch, HealthComponent localPlayerHealth )
		{
			gameObjects[ 0 ].Render( ref batch, localPlayerHealth );
			gameObjects[ 1 ].Render( ref batch );
		}

		/// <summary>
		/// Resets the round time to match the server round time.
		/// </summary>
		/// <param name="remainingTime">Server time.</param>
		public void HandleUpdateRoundTimeMessage( float remainingTime )
		{
			( ( RoundTimeInputComponent )gameObjects[ 1 ].Input ).SetRemainingRoundTime( remainingTime );
		}

		/// <summary>
		/// Checks if the round time up over.
		/// </summary>
		/// <returns>bool indicating whether round time is over.</returns>
		public bool IsRoundTimeOver()
		{
			return ( ( RoundTimeInputComponent )gameObjects[ 1 ].Input ).IsRoundTimeOver();
		}

		/// <summary>
		/// Destroys all gameObjects.
		/// </summary>
		public void ClearAllDisplayObjects()
		{
			gameObjects.Clear();
		}

		/// <summary>
		/// Retrieves the remaining round time from the round time game object.
		/// </summary>
		/// <returns>An integer value of the remaining round time.</returns>
		public int GetRemainingRoundTime()
		{
			return ( int )( ( RoundTimeInputComponent )gameObjects[1].Input ).RemainingRoundTime;
		}

		private List< GameObject > gameObjects;
	}
}