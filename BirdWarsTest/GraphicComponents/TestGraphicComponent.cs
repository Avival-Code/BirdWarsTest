using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class TestGraphicsComponent : GraphicsComponent
	{
		public TestGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base(content.Load<Texture2D>( "ball" ) ) {}

		public override void Render( ref SpriteBatch batch, Vector2 position )
		{
			batch.Draw( texture, position, Color.White );
		}
	}
}
