using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.InputComponents
{
	abstract class Command
	{
		abstract public void Execute( GameObject gameObject );

		protected const float regularMoveSpeed = 5.0f;
	}

	class MoveUpCommand : Command
	{
		public override void Execute( GameObject gameObject )
		{
			gameObject.position += new Vector2( 0.0f, -regularMoveSpeed );
		}
	}

	class MoveDownCommand : Command
	{
		public override void Execute(GameObject gameObject)
		{
			gameObject.position += new Vector2( 0.0f, regularMoveSpeed );
		}
	}

	class MoveLeftCommand : Command
	{
		public override void Execute(GameObject gameObject)
		{
			gameObject.position += new Vector2( -regularMoveSpeed, 0.0f );
		}
	}

	class MoveRightCommand : Command
	{
		public override void Execute(GameObject gameObject)
		{
			gameObject.position += new Vector2( regularMoveSpeed, 0.0f );
		}
	}
}
