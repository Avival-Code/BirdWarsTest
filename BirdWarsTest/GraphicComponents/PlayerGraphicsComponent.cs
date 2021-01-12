/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphcis component for a player object.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphcis component for a player object.
	/// </summary>
	public class PlayerGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		public PlayerGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "Player/Bird2" ) )
		{
			attackTexture = content.Load< Texture2D >( "Player/RedBox" );
			backTexture = content.Load< Texture2D >( "Player/Bird2_Back" );
			rightTexture = content.Load< Texture2D >( "Player/Bird2_Right" );
			leftTexture = content.Load< Texture2D >( "Player/Bird2_Left" );
		}

		/// <summary>
		/// Draws the texture to the screen at the object's position.
		/// </summary>
		/// <param name="gameObject">The game object</param>
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds )
		{
			RenderDependingOnDirection( gameObject, ref batch, cameraBounds );
			if( gameObject.Attack.IsAttacking )
			{
				Rectangle AttackPosition = gameObject.Attack.GetAttackRectangle( gameObject );
				batch.Draw( attackTexture, new Vector2( AttackPosition.X - cameraBounds.Left, 
														AttackPosition.Y - cameraBounds.Top ), Color.White );
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

			if( ( int )gameObject.Input.GetLastActiveVelocity().X < 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y < 0 )
			{
				batch.Draw( leftTexture, new Vector2( gameObject.Position.X - cameraBounds.Left,
													  gameObject.Position.Y - cameraBounds.Top ), Color.White );
			}

			if( ( int )gameObject.Input.GetLastActiveVelocity().X > 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y < 0 )
			{
				batch.Draw( rightTexture, new Vector2( gameObject.Position.X - cameraBounds.Left,
													   gameObject.Position.Y - cameraBounds.Top ), Color.White );
			}

			if( ( int )gameObject.Input.GetLastActiveVelocity().X < 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y > 0 )
			{
				batch.Draw( leftTexture, new Vector2( gameObject.Position.X - cameraBounds.Left,
													  gameObject.Position.Y - cameraBounds.Top ), Color.White );
			}

			if( ( int )gameObject.Input.GetLastActiveVelocity().X > 0 &&
				( int )gameObject.Input.GetLastActiveVelocity().Y > 0 )
			{
				batch.Draw( rightTexture, new Vector2( gameObject.Position.X - cameraBounds.Left,
													   gameObject.Position.Y - cameraBounds.Top ), Color.White );
			}
		}

		private readonly Texture2D attackTexture;
		private readonly Texture2D backTexture;
		private readonly Texture2D rightTexture;
		private readonly Texture2D leftTexture;
	}
}