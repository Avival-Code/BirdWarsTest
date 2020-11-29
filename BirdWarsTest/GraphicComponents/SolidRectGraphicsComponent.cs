using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class SolidRectGraphicsComponent : GraphicsComponent
	{
		public SolidRectGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D > ( "Background01" ) ) {}

		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
		}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}
	}
}