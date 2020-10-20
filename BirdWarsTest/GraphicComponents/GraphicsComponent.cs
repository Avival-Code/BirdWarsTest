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

		protected Texture2D texture;
	}
}
