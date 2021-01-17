/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Input component used to change the MainMenu menu options
state.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BirdWarsTest.InputComponents
{
	/// <summary>
	/// Input component used to change the MainMenu menu options
	/// state.
	/// </summary>
	public class SelectorInputComponent : InputComponent
	{
		/// <summary>
		/// Default constructor, creates the possible menu options
		/// from a list of possible selections.
		/// </summary>
		/// <param name="selectionsIn"></param>
		public SelectorInputComponent( List< GameObject > selectionsIn )
		{
			selections = selectionsIn;
			minSelectorValue = 0;
			maxSelectorValue = selections.Count - 1;
			currentSelection = 0;
			timer = 0;
			changedSelection = false;

			( ( MenuOptionGraphicsComponent )selections[ currentSelection ].Graphics ).ToggleSelect();
		}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and game time.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="gameTime">Current game time.</param>
		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		/// <summary>
		/// Handles the input recieved based on the current game object state
		/// and keyboard state.
		/// </summary>
		/// <param name="gameObject">Current game object.</param>
		/// <param name="state">Current keyboard state.</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		/// <summary>
		/// Checks user input for keyboard keys and changed the selector state
		/// based on that input.
		/// </summary>
		/// <param name="gameObject">The Game object</param>
		/// <param name="state">current keyboard state</param>
		/// <param name="gameState">current game state</param>
		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			ChangeSelection( state, gameObject );
			InvokeSelectionInput( state, gameState );
			UpdateTimer();
		}

		private void InvokeSelectionInput( KeyboardState state, GameState gameState )
		{
			if( state.IsKeyDown( Keys.Enter ) )
			{
				HandleEnterKeyInput( state, gameState );
			}
		}

		private void ChangeSelection( KeyboardState state, GameObject gameObject )
		{
			if( state.IsKeyDown( Keys.Up ) )
			{
				HandleUpKeyInput( gameObject );
			}
			if( state.IsKeyDown( Keys.Down ) )
			{
				HandleDownKeyInput( gameObject );
			}
		}

		private void HandleUpKeyInput( GameObject gameObject )
		{
			if( !changedSelection && currentSelection > minSelectorValue )
			{
				changedSelection = !changedSelection;
				( ( MenuOptionGraphicsComponent )selections[ currentSelection ].Graphics ).ToggleSelect();
				currentSelection -= 1;
				( ( MenuOptionGraphicsComponent )selections[ currentSelection ].Graphics ).ToggleSelect();
				gameObject.Audio.Play();
			}
		}

		private void HandleDownKeyInput( GameObject gameObject )
		{
			if( !changedSelection && currentSelection < maxSelectorValue )
			{
				changedSelection = !changedSelection;
				( ( MenuOptionGraphicsComponent)selections[ currentSelection ].Graphics ).ToggleSelect();
				currentSelection += 1;
				( ( MenuOptionGraphicsComponent)selections[ currentSelection ].Graphics ).ToggleSelect();
				gameObject.Audio.Play();
			}
		}

		private void HandleEnterKeyInput( KeyboardState state, GameState gameState )
		{
			if( !changedSelection )
			{
				changedSelection = !changedSelection;
				selections[ currentSelection ].Update( state, gameState );
			}
		}

		private void UpdateTimer()
		{
			if( changedSelection )
			{
				timer += 1;
				if( timer >= 10 )
				{
					timer = 0;
					changedSelection = !changedSelection;
				}
			}
		}

		private List< GameObject > selections;
		private int minSelectorValue;
		private int maxSelectorValue;
		private int currentSelection;
		private int timer;
		private bool changedSelection;
	}
}