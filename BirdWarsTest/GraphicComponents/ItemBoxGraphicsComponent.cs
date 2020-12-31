using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class ItemBoxGraphicsComponent : GraphicsComponent
	{
		public ItemBoxGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			: 
			base( content.Load< Texture2D >( "Items/ItemBox" ) )
		{}

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}	

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) 
		{
			if( !gameObject.Health.IsDead() )
			{
				batch.Draw( texture, new Vector2( gameObject.Position.X - cameraBounds.X, gameObject.Position.Y - cameraBounds.Top ),
						    Color.White );
			}
		}
	}
}