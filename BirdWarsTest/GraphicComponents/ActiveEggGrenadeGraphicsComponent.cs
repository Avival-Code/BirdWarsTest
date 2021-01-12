/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for an active egg grenade object.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for an active egg grenade object.
	/// </summary>
	public class ActiveEggGrenadeGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		public ActiveEggGrenadeGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load<Texture2D>( "Items/ActiveEggGrenade" ) )
		{ 
			explosionBegin = content.Load< Texture2D >( "Effects/ExplosionBegin" );
			explosionEnd = content.Load< Texture2D >( "Effects/ExplosionEnd" );
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
			if( !gameObject.Attack.IsAttacking )
			{
				batch.Draw( texture, new Vector2( gameObject.Position.X - cameraBounds.Left, gameObject.Position.Y - cameraBounds.Top ),
							Color.White );
			}
	
			if( gameObject.Attack.IsAttacking && gameObject.Attack.AttackTimer > 8 )
			{
				RenderExplosion( gameObject, explosionBegin, ref batch, cameraBounds );
			}
	
			if( gameObject.Attack.IsAttacking && gameObject.Attack.AttackTimer <= 8 &&
				gameObject.Attack.AttackTimer > 1 )
			{
				RenderExplosion( gameObject, explosionEnd, ref batch, cameraBounds );
			}
		}

		private void RenderExplosion( GameObject gameObject, Texture2D targetTexture, ref SpriteBatch batch, Rectangle cameraBounds )
		{
			Rectangle grenadeAttackBox = gameObject.Attack.GetAttackRectangle( gameObject );
			Vector2 position = new Vector2( grenadeAttackBox.X - cameraBounds.X, grenadeAttackBox.Y - cameraBounds.Y);
			batch.Draw( targetTexture, position, Color.White );
		}

		private readonly Texture2D explosionBegin;
		private readonly Texture2D explosionEnd;
	}
}