using BirdWarsTest.GameObjects;
using BirdWarsTest.InputComponents;
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
				if( ( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 0.0f, -1.0f ) )
				{
					batch.Draw( attackTexture, new Vector2( gameObject.Position.X - cameraBounds.Left, 
															gameObject.Position.Y - 32.0f - cameraBounds.Top ), Color.White );
				}

				if( ( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 0.0f, 1.0f ) )
				{
					batch.Draw( attackTexture, new Vector2( gameObject.Position.X - cameraBounds.Left, 
															gameObject.Position.Y + texture.Height - cameraBounds.Top ), Color.White );
				}

				if( ( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( -1.0f, 0.0f ) )
				{
					batch.Draw( attackTexture, new Vector2( gameObject.Position.X - 50.0f - cameraBounds.Left, 
															gameObject.Position.Y - cameraBounds.Top ), Color.White );
				}

				if( ( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 1.0f, 0.0f ) )
				{
					batch.Draw( attackTexture, new Vector2( gameObject.Position.X + texture.Width - cameraBounds.Left, 
															gameObject.Position.Y - cameraBounds.Top ), Color.White );
				}
			}
		}

		public Rectangle GetAttackTextureBounds( GameObject gameObject )
		{
			Rectangle attackRectangle = new Rectangle( -100, -100, 1, 1 );
			if( gameObject.Attack.IsAttacking && 
				( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 0.0f, -1.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y - attackTexture.Height, 
												 attackTexture.Width, attackTexture.Height );
			}

			if( gameObject.Attack.IsAttacking && 
				( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 0.0f, 1.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y + attackTexture.Height,
												 attackTexture.Width, attackTexture.Height );
			}

			if( gameObject.Attack.IsAttacking && 
				( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( -1.0f, 0.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X - attackTexture.Width, ( int )gameObject.Position.Y,
												 attackTexture.Width, attackTexture.Height);
			}

			if( gameObject.Attack.IsAttacking && 
				( ( LocalPlayerInputComponent )gameObject.Input ).LastActiveVelocity == new Vector2( 1.0f, 0.0f ) )
			{
				attackRectangle = new Rectangle( ( int )gameObject.Position.X + attackTexture.Width, ( int )gameObject.Position.Y,
												 attackTexture.Width, attackTexture.Height);
			}

			return attackRectangle;
		}

		private Texture2D attackTexture;
	}
}