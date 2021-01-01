using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class PlayerTestGraphicsComponent : GraphicsComponent
	{
		public PlayerTestGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "WhiteBox" ) )
		{
			attackTexture = content.Load< Texture2D >( "Player/RedBox" );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds )
		{
			batch.Draw( texture, new Vector2( gameObject.Position.X - cameraBounds.Left, gameObject.Position.Y - cameraBounds.Top ), 
					    Color.White );
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

		private Texture2D attackTexture;
	}
}