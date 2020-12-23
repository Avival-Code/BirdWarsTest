using BirdWarsTest.GameObjects;
using BirdWarsTest.HealthComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BirdWarsTest.GraphicComponents
{
	class LifeBarGraphicsComponent : GraphicsComponent
	{
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

		public override void Render( GameObject gameObject, ref SpriteBatch batch ) {}

		public override void Render( GameObject gameObject, ref SpriteBatch batch, Rectangle cameraBounds ) {}

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

		private Texture2D [] middleLayer;
		private Texture2D topLayer;
		private int currentLayer;
	}
}
