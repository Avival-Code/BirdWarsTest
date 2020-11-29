using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.States;

namespace BirdWarsTest.GameObjects
{
	public class GameObject
	{
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In,
						   Identifiers id_In, Vector2 position_in )
		{
			graphics = graphics_In;
			input = input_In;
			identifier = id_In;
			position = position_in;
		}

		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, 
						   Identifiers id_In, float screenWidth, float screenHeight )
		{
			graphics = graphics_In;
			input = input_In;
			identifier = id_In;
			if( graphics != null )
				position = new Vector2( CenterXWidth( screenWidth, graphics.GetTextureSize().X ),
										screenHeight );
			else { position = new Vector2( 0.0f, 0.0f ); }
		}

		public void Update( KeyboardState state )
		{
			input?.HandleInput( this, state );
		}

		public void Update( KeyboardState state, GameState gameState )
		{
			input?.HandleInput( this, state, gameState );
		}

		public void Render( ref SpriteBatch batch )
		{
			graphics?.Render( this, ref batch );
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraBounds )
		{

		}

		public float CenterXWidth( float screenWidth, float textureWidth )
		{
			return ( screenWidth / 2 ) - ( textureWidth / 2 );
		}

		public Rectangle GetRectangle()
		{
			return new Rectangle( ( int )position.X, ( int )position.Y, 
								( int )graphics.GetTextureSize().X, ( int )graphics.GetTextureSize().Y );
		}

		public Vector2 Position 
		{ 
			get{ return position; } 
			set{ position = value; }
		}

		public GraphicsComponent graphics = null;
		public InputComponent input = null;
		public Identifiers identifier;
		private Vector2 position;
	}
}