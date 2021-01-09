using BirdWarsTest.GameObjects;
using BirdWarsTest.AttackComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	public class ActiveEggGrenadeGraphicsComponent : GraphicsComponent
	{
		public ActiveEggGrenadeGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load<Texture2D>( "Items/ActiveEggGrenade" ) )
		{ 
			explosionBegin = content.Load< Texture2D >( "Effects/ExplosionBegin" );
			explosionEnd = content.Load< Texture2D >( "Effects/ExplosionEnd" );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}
	
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

		private Texture2D explosionBegin;
		private Texture2D explosionEnd;
	}
}