using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	class LoginLogoGraphicsComponent : GraphicsComponent
	{
		public LoginLogoGraphicsComponent(Microsoft.Xna.Framework.Content.ContentManager content)
			:
			base( content.Load< Texture2D >( "BirdWarsLogo_V1" ) )
		{}

		public override void Render( ref SpriteBatch batch, Vector2 position )
		{
			batch.Draw( texture, position, Color.White );
			//batch.Draw( texture, position, null, Color.White, 0f, position,
			//			new Vector2( 0.75f, 0.75f ), SpriteEffects.None, 0f );
		}
	}
}
