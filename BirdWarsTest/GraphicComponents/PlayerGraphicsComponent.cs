using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class PlayerGraphicsComponent : GraphicsComponent
	{
		public PlayerGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "Player/Bird2" ) )
		{
			attackTexture = content.Load< Texture2D >( "Player/RedBox" );
			backTexture = content.Load< Texture2D >( "Player/Bird2_Back" );
			rightTexture = content.Load< Texture2D >( "Player/Bird2_Right" );
			leftTexture = content.Load< Texture2D >( "Player/Bird2_Left" );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds )
		{
			RenderDependingOnDirection( gameObject, ref batch, cameraBounds );
			if( gameObject.Attack.IsAttacking )
			{
				if( ( int )gameObject.Input.GetLastActiveVelocity().X == 0 &&
					( int )gameObject.Input.GetLastActiveVelocity().Y < 0 )
				{
					batch.Draw( attackTexture, new Vector2( gameObject.Position.X - cameraBounds.Left, 
															gameObject.Position.Y - 32.0f - cameraBounds.Top ), Color.White );
				}

				if( ( int )gameObject.Input.GetLastActiveVelocity().X == 0 &&
					( int )gameObject.Input.GetLastActiveVelocity().Y > 0 )
				{
					batch.Draw( attackTexture, new Vector2( gameObject.Position.X - cameraBounds.Left, 
															gameObject.Position.Y + texture.Height - cameraBounds.Top ), Color.White );
				}

				if( ( int )gameObject.Input.GetLastActiveVelocity().X < 0 &&
					( int )gameObject.Input.GetLastActiveVelocity().Y == 0 )
				{
					batch.Draw( attackTexture, new Vector2( gameObject.Position.X - 50.0f - cameraBounds.Left, 
															gameObject.Position.Y - cameraBounds.Top ), Color.White );
				}

				if( ( int )gameObject.Input.GetLastActiveVelocity().X > 0 &&
					( int )gameObject.Input.GetLastActiveVelocity().Y == 0 )
				{
					batch.Draw( attackTexture, new Vector2( gameObject.Position.X + texture.Width - cameraBounds.Left, 
															gameObject.Position.Y - cameraBounds.Top ), Color.White );
				}
			}
		}

		private void RenderDependingOnDirection( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds )
		{
			if( ( int )gameObject.Input.GetLastActiveVelocity().X == 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y < 0 )
			{
				batch.Draw( backTexture, new Vector2( gameObject.Position.X - cameraBounds.Left,
													  gameObject.Position.Y - cameraBounds.Top ), Color.White );
			}

			if( ( int )gameObject.Input.GetLastActiveVelocity().X == 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y > 0 )
			{
				batch.Draw( texture, new Vector2( gameObject.Position.X - cameraBounds.Left,
														gameObject.Position.Y - cameraBounds.Top ), Color.White );
			}

			if( ( int )gameObject.Input.GetLastActiveVelocity().X < 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y == 0 )
			{
				batch.Draw( leftTexture, new Vector2( gameObject.Position.X - cameraBounds.Left,
														gameObject.Position.Y - cameraBounds.Top ), Color.White );
			}

			if( ( int )gameObject.Input.GetLastActiveVelocity().X > 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y == 0 )
			{
				batch.Draw( rightTexture, new Vector2( gameObject.Position.X - cameraBounds.Left,
														gameObject.Position.Y - cameraBounds.Top ), Color.White );
			}
		}

		private Texture2D attackTexture;
		private Texture2D backTexture;
		private Texture2D rightTexture;
		private Texture2D leftTexture;
	}
}