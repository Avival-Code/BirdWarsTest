using BirdWarsTest.GameObjects;
using BirdWarsTest.States;
using BirdWarsTest.GraphicComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BirdWarsTest.InputComponents
{
	class SelectorInputComponent : InputComponent
	{
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

		public override void HandleInput( GameObject gameObject, GameTime gameTime ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state ) {}

		public override void HandleInput( GameObject gameObject, KeyboardState state, GameState gameState )
		{
			ChangeSelection( state );
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

		private void ChangeSelection( KeyboardState state )
		{
			if( state.IsKeyDown( Keys.Up ) )
			{
				HandleUpKeyInput();
			}
			if( state.IsKeyDown( Keys.Down ) )
			{
				HandleDownKeyInput();
			}
		}

		public void HandleUpKeyInput()
		{
			if( !changedSelection && currentSelection > minSelectorValue )
			{
				changedSelection = !changedSelection;
				( ( MenuOptionGraphicsComponent )selections[ currentSelection ].Graphics ).ToggleSelect();
				currentSelection -= 1;
				( ( MenuOptionGraphicsComponent )selections[ currentSelection ].Graphics ).ToggleSelect();
			}
		}

		private void HandleDownKeyInput()
		{
			if( !changedSelection && currentSelection < maxSelectorValue )
			{
				changedSelection = !changedSelection;
				( ( MenuOptionGraphicsComponent)selections[ currentSelection ].Graphics ).ToggleSelect();
				currentSelection += 1;
				( ( MenuOptionGraphicsComponent)selections[ currentSelection ].Graphics ).ToggleSelect();
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