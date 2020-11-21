using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class MenuBoxGraphicsComponent : GraphicsComponent
	{
		public MenuBoxGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load<Texture2D>( "PasswordBox" ) )
		{}

		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
		}
	}
}
