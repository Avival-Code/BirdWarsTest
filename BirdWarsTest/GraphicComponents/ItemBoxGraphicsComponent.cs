using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

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
			batch.Draw( texture, new Vector2( gameObject.Position.X - cameraBounds.X, gameObject.Position.Y - cameraBounds.Top ), 
						Color.White );
		}
	}
}
