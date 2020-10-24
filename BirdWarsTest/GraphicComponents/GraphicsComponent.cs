using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	abstract class GraphicsComponent
	{
		public GraphicsComponent( Texture2D texture_In )
		{
			texture = texture_In;
		}

		abstract public void Render( ref SpriteBatch batch, Vector2 position );

		public Vector2 getTextureSize()
		{
			return new Vector2( texture.Width, texture.Height );
		}

		protected Texture2D texture;
	}
}
