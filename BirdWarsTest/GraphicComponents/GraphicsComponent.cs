using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BirdWarsTest.GameObjects;

namespace BirdWarsTest.GraphicComponents
{
	abstract class GraphicsComponent
	{
		public GraphicsComponent( Texture2D texture_In )
		{
			texture = texture_In;
		}

		abstract public void Render( GameObject gameObject, ref SpriteBatch batch  );

		virtual public Vector2 GetTextureSize()
		{
			return new Vector2( texture.Width, texture.Height );
		}

		protected Texture2D texture;
	}
}
