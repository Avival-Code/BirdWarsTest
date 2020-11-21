using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GraphicComponents
{
	class MenuOptionGraphicsComponent : GraphicsComponent
	{
		public MenuOptionGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										    string optionText )
			:
			base( content.Load< Texture2D >( "Buttons/MainMenuSelectionBox350x60" ) )
		{
			regularFont = content.Load< SpriteFont >( "Fonts/MainFont_S15" );
			increasedFont = content.Load< SpriteFont >( "Fonts/MainFont_S20" );
			textColor = Color.Black;
			text = optionText;
			isSelected = false;
		}
		public override void Render( GameObject gameObject, ref SpriteBatch batch )
		{
			if( isSelected )
			{
				RenderSelected( gameObject, batch );
			}
			else
			{
				RenderNotSelected( gameObject, batch );
			}
		}

		private void RenderSelected( GameObject gameObject, SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			if( !string.IsNullOrEmpty( text ) )
			{
				Vector2 temp = new Vector2( ( gameObject.Position.X + ( texture.Width - 20.0f ) ) - 
											( increasedFont.MeasureString( text ).X ),
											( gameObject.Position.Y + ( texture.Height / 2 ) ) - 
											( increasedFont.MeasureString( text ).Y / 2 ) );
				batch.DrawString( increasedFont, text, temp, textColor );
			}
		}

		private void RenderNotSelected( GameObject gameObject, SpriteBatch batch )
		{
			Vector2 newGameObjectPosition = new Vector2( gameObject.Position.X - 50.0f, gameObject.Position.Y );
			batch.Draw( texture, newGameObjectPosition, Color.White );
			if( !string.IsNullOrEmpty( text ) )
			{
				Vector2 temp = new Vector2( ( newGameObjectPosition.X + ( texture.Width - 20.0f ) ) - 
											( regularFont.MeasureString( text ).X ),
											( newGameObjectPosition.Y + ( texture.Height / 2) ) - 
											( regularFont.MeasureString( text ).Y / 2 ) );
				batch.DrawString( regularFont, text, temp, textColor );
			}
		}

		public void ToggleSelect()
		{
			isSelected = !isSelected;
		}

		private SpriteFont regularFont;
		private SpriteFont increasedFont;
		private Color textColor;
		private string text;
		private bool isSelected;
	}
}
