/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for a menu option object.
*********************************************/
using BirdWarsTest.GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for a menu option object.
	/// </summary>
	public class MenuOptionGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		/// /// <param name="optionText">Game content manager.</param>
		public MenuOptionGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content,
										    string optionText )
			:
			base( content.Load< Texture2D >( "Buttons/MainMenuSelectionBox350x60" ) )
		{
			regularFont = content.Load< SpriteFont >( "Fonts/BabeFont_17" );
			increasedFont = content.Load< SpriteFont >( "Fonts/BabeFont_22" );
			textColor = Color.Black;
			text = optionText;
			isSelected = false;
		}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) { }

		/// <summary>
		/// Draws the texture to the screen at the object's position.
		/// </summary>
		/// <param name="gameObject">The game object</param>
		/// <param name="batch">Game Spritebatch</param>
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

		/// <summary>
		/// Changes isSelected to !isSelected.
		/// </summary>
		public void ToggleSelect()
		{
			isSelected = !isSelected;
		}

		private readonly SpriteFont regularFont;
		private readonly SpriteFont increasedFont;
		private Color textColor;
		private readonly string text;
		private bool isSelected;
	}
}