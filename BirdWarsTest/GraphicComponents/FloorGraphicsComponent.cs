using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	public class FloorGraphicsComponent : GraphicsComponent
	{
		public FloorGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
									   string textureName )
			:
			base( content.Load< Texture2D >( textureName ) )
		{}

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) 
		{
			batch.Draw( texture, new Vector2( gameObject.Position.X - cameraBounds.Left, gameObject.Position.Y - cameraBounds.Top ), 
				        Color.White );
		}

		private Rectangle GetTextureRect( GameObject gameObject )
		{
			return new Rectangle( ( int )gameObject.Position.X, ( int )gameObject.Position.Y, 
								  ( int )gameObject.Position.X + 64, ( int )gameObject.Position.Y + 64 );
		}
	}
}