using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;

namespace BirdWarsTest.InputComponents
{
	public abstract class Command
	{
		abstract public void Execute( GameObject gameObject );

		protected const float regularMoveSpeed = 5.0f;
	}

	public class MoveUpCommand : Command
	{
		public override void Execute( GameObject gameObject )
		{
			gameObject.Position += new Vector2( 0.0f, -regularMoveSpeed );
		}
	}

	public class MoveDownCommand : Command
	{
		public override void Execute(GameObject gameObject)
		{
			gameObject.Position += new Vector2( 0.0f, regularMoveSpeed );
		}
	}

	public class MoveLeftCommand : Command
	{
		public override void Execute(GameObject gameObject)
		{
			gameObject.Position += new Vector2( -regularMoveSpeed, 0.0f );
		}
	}

	public class MoveRightCommand : Command
	{
		public override void Execute(GameObject gameObject)
		{
			gameObject.Position += new Vector2( regularMoveSpeed, 0.0f );
		}
	}
}
