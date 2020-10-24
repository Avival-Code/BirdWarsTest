using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GraphicComponents;

namespace BirdWarsTest.States
{
	class MainMenuState : GameState
	{
		public MainMenuState() {}

		public override void Init( Microsoft.Xna.Framework.Content.ContentManager newContent, Vector2 windowSize )
		{
			content = newContent;
		}

		public override void Pause() {}

		public override void Resume() {}

		public override void Enter() {}

		public override void HandleInput() {}

		public override void UpdateLogic( KeyboardState state )
		{
			HandleInput();
		}

		public override void Render( ref SpriteBatch sprites ) {}
	}
}