using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BirdWarsTest.GameObjects;

namespace BirdWarsTest.GraphicComponents
{
	public abstract class GraphicsComponent
	{
		public GraphicsComponent() {}

		public GraphicsComponent( Texture2D texture_In )
		{
			texture = texture_In;
		}

		abstract public void Render( GameObject gameObject, ref SpriteBatch batch  );

		abstract public void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds );

		virtual public Vector2 GetTextureSize()
		{
			return new Vector2( texture.Width, texture.Height );
		}

		virtual public void SetText( string newText ) {}

		protected Texture2D texture;
	}
}
