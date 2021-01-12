/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Graphics component for a life bar object.
*********************************************/
using BirdWarsTest.GameObjects;
using BirdWarsTest.HealthComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BirdWarsTest.GraphicComponents
{
	/// <summary>
	/// Graphics component for a life bar object.
	/// </summary>
	public class LifeBarGraphicsComponent : GraphicsComponent
	{
		/// <summary>
		/// Creates an isntance of the graphics component.
		/// </summary>
		/// <param name="content">Game content manager.</param>
		public LifeBarGraphicsComponent( Microsoft.Xna.Framework.Content.ContentManager content )
			:
			base( content.Load< Texture2D >( "LifeBar/LifeBarBack" ) )
		{
			topLayer = content.Load< Texture2D >( "LifeBar/LifeBarEdge" );
			middleLayer = new Texture2D[ 3 ];
			currentLayer = 2;
			middleLayer[ 0 ] = content.Load< Texture2D >( "LifeBar/LifeBarRed" );
			middleLayer[ 1 ] = content.Load< Texture2D >( "LifeBar/LifeBarYellow" );
			middleLayer[ 2 ] = content.Load< Texture2D >( "LifeBar/LifeBarGreen" );
		}

		/// <summary>
		/// Draws the texture to the screen at the object's position.
		/// </summary>
		/// <param name="gameObject">The game object</param>
		/// <param name="batch">Game Spritebatch</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}

		/// <summary>
		/// Draws the texture to the screen if it's within
		/// the cameraBounds rectangle.
		/// </summary>
		/// <param name="gameObject">GameObject.</param>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Current camera area rectangle.</param>
		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

		/// <summary>
		/// Draws and updates a life bar graphics component.
		/// </summary>
		/// <param name="gameObject">Game Object</param>
		/// <param name="health">Object Health</param>
		/// <param name="batch">Game spritebatch</param>
		public void Render( GameObject gameObject, HealthComponent health, ref SpriteBatch batch )
		{
			batch.Draw( texture, gameObject.Position, Color.White );
			HandleLifeReduction( gameObject, health, ref batch );
			batch.Draw( topLayer, gameObject.Position, Color.White );
		}

		private void HandleLifeReduction( GameObject gameObject, HealthComponent health, ref SpriteBatch batch )
		{
			SetCurrentLayer( health );
			batch.Draw( middleLayer[ currentLayer ], gameObject.Position, 
						new Rectangle( 0, 0, ( int )( ( float )middleLayer[ currentLayer ].Width * health.GetRemainingHealthPercent() ), 
									   middleLayer[ currentLayer ].Height ), Color.White );
		}

		private void SetCurrentLayer( HealthComponent health )
		{
			if( health.Health >= 7 )
			{
				currentLayer = 2;
			}
			if( health.Health >= 4 && health.Health <= 6 )
			{
				currentLayer = 1;
			}
			if( health.Health <= 3 )
			{
				currentLayer = 0;
			}
		}

		private readonly Texture2D [] middleLayer;
		private readonly Texture2D topLayer;
		private int currentLayer;
	}
}