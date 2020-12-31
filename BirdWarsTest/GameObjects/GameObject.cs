using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.AttackComponents;
using BirdWarsTest.States;

namespace BirdWarsTest.GameObjects
{
	public class GameObject
	{
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In,
						   Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = null;
			Attack = null;
			Identifier = id_In;
			Position = position_in;
		}

		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, 
						   Identifiers id_In, float screenWidth, float screenHeight )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = null;
			Attack = null;
			Identifier = id_In;
			if( Graphics != null )
				Position = new Vector2( CenterXWidth( screenWidth, Graphics.GetTextureSize().X ),
										screenHeight );
			else { Position = new Vector2( 0.0f, 0.0f ); }
		}

		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, HealthComponent health_In,
						   Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = health_In;
			Attack = null;
			Identifier = id_In;
			Position = position_in;
		}

		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, HealthComponent health_In,
						   AttackComponent attackIn, Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = health_In;
			Attack = attackIn;
			Identifier = id_In;
			Position = position_in;
		}

		public void Update( GameTime gameTime )
		{
			Input?.HandleInput( this, gameTime );
			Health?.UpdateCoolDownTimer();
			Attack?.UpdateAttackTimer();
		}

		public void Update( KeyboardState state )
		{
			Input?.HandleInput( this, state );
			Health?.UpdateCoolDownTimer();
			Attack?.UpdateAttackTimer();
		}

		public void Update( KeyboardState state, GameState gameState )
		{
			Input?.HandleInput( this, state, gameState );
			Health?.UpdateCoolDownTimer();
			Attack?.UpdateAttackTimer();
		}

		public void Update( KeyboardState state, GameState gameState, GameTime gameTime )
		{
			var elapsedGameTime = ( float )gameTime.ElapsedGameTime.TotalSeconds;
			Input?.HandleInput( this, state, gameState );
			Health?.UpdateCoolDownTimer();
			Attack?.UpdateAttackTimer();
			Position += Input.GetVelocity() * elapsedGameTime;
		}

		public void Render( ref SpriteBatch batch )
		{
			Graphics?.Render( this, ref batch );
		}

		public void Render( ref SpriteBatch batch, HealthComponent health )
		{
			( ( LifeBarGraphicsComponent )Graphics )?.Render( this, health, ref batch );
		}

		public void Render( ref SpriteBatch batch, Rectangle cameraBounds )
		{
			Graphics?.Render( this, ref batch, cameraBounds );
		}

		public float CenterXWidth( float screenWidth, float textureWidth )
		{
			return ( screenWidth / 2 ) - ( textureWidth / 2 );
		}

		public Rectangle GetRectangle()
		{
			return new Rectangle( ( int )Position.X, ( int )Position.Y, 
								  ( int )Graphics.GetTextureSize().X, ( int )Graphics.GetTextureSize().Y );
		}


		public GraphicsComponent Graphics { get; set; }
		public InputComponent Input { get; set; }
		public HealthComponent Health { get; set; }
		public AttackComponent Attack { get; set; }
		public Identifiers Identifier { get; private set; }
		public Vector2 Position { get; set; }
	}
}