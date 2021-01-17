/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
The main GameObject class. It is the basic structure of all
objects in application.
*********************************************/
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BirdWarsTest.InputComponents;
using BirdWarsTest.GraphicComponents;
using BirdWarsTest.HealthComponents;
using BirdWarsTest.AttackComponents;
using BirdWarsTest.AudioComponents;
using BirdWarsTest.EffectComponents;
using BirdWarsTest.States;

namespace BirdWarsTest.GameObjects
{
	/// <summary>
	/// The main GameObject class. It is the basic structure of all
    /// objects in application.
	/// </summary>
	public class GameObject
	{
		/// <summary>
		/// Creates a gameObject with null Health, Attack, Audio 
		/// and effect componentes.
		/// </summary>
		/// <param name="graphics_In">Graphics component</param>
		/// <param name="input_In">Input component</param>
		/// <param name="id_In">Object identifier.</param>
		/// <param name="position_in">Object position.</param>
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In,
						   Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = null;
			Attack = null;
			Effect = null;
			Audio = null;
			Identifier = id_In;
			Position = position_in;
		}

		/// <summary>
		/// Creates a gameobject at the center of the gamewindow horizontal axis.
		/// Also has null Health, Attack, Audio and Effect components.
		/// </summary>
		/// <param name="graphics_In">Graphics component</param>
		/// <param name="input_In">Input component</param>
		/// <param name="id_In">Object identifier.</param>
		/// <param name="screenWidth">Object x position.</param>
		/// <param name="screenHeight">Object y position.</param>
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, 
						   Identifiers id_In, float screenWidth, float screenHeight )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = null;
			Attack = null;
			Effect = null;
			Audio = null;
			Identifier = id_In;
			if( Graphics != null )
				Position = new Vector2( CenterXWidth( screenWidth, Graphics.GetTextureSize().X ),
										screenHeight );
			else { Position = new Vector2( 0.0f, 0.0f ); }
		}

		/// <summary>
		/// Creates a gameObject with a null Attack, Audio and effect component.
		/// </summary>
		/// <param name="graphics_In">Graphics component</param>
		/// <param name="input_In">Input component</param>
		/// <param name="health_In">Health component</param>
		/// <param name="id_In">Object identifier</param>
		/// <param name="position_in">Object position</param>
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, HealthComponent health_In,
						   Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = health_In;
			Attack = null;
			Effect = null;
			Audio = null;
			Identifier = id_In;
			Position = position_in;
		}

		/// <summary>
		/// Creates a gameObject with a null Audio and Effect component.
		/// </summary>
		/// <param name="graphics_In">Graphics component</param>
		/// <param name="input_In">Input Component</param>
		/// <param name="health_In">Health component</param>
		/// <param name="attackIn">Attack component</param>
		/// <param name="id_In">Object identifier</param>
		/// <param name="position_in">Object position</param>
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, HealthComponent health_In,
						   AttackComponent attackIn, Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = health_In;
			Attack = attackIn;
			Effect = null;
			Audio = null;
			Identifier = id_In;
			Position = position_in;
		}

		/// <summary>
		/// Creates a gameObject with a null Effect Component;
		/// </summary>
		/// <param name="graphics_In">The game's graphics component.</param>
		/// <param name="input_In">The game's input component.</param>
		/// <param name="health_In">The game's Health component.</param>
		/// <param name="attackIn">The game's attack component.</param>
		/// <param name="audioIn">The game's audio component.</param>
		/// <param name="id_In">The game's identifier.</param>
		/// <param name="position_in">The game's position.</param>
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, HealthComponent health_In,
						   AttackComponent attackIn, AudioComponent audioIn, Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = health_In;
			Attack = attackIn;
			Effect = null;
			Audio = audioIn;
			Identifier = id_In;
			Position = position_in;
		}

		/// <summary>
		/// Creates a gameObejct with a null Attack and Audio component.
		/// </summary>
		/// <param name="graphics_In">Graphics component</param>
		/// <param name="input_In">Input component</param>
		/// <param name="health_In">Health component</param>
		/// <param name="effectIn">Effect component</param>
		/// <param name="id_In">Object identifier</param>
		/// <param name="position_in">Object position</param>
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, HealthComponent health_In,
						   EffectComponent effectIn, Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = health_In;
			Attack = null;
			Effect = effectIn;
			Audio = null;
			Identifier = id_In;
			Position = position_in;
		}

		/// <summary>
		/// Creates a gameObject with a null attack component.
		/// </summary>
		/// <param name="graphics_In">The game object's graphics component.</param>
		/// <param name="input_In">The game object's input component.</param>
		/// <param name="health_In">The game object's health component.</param>
		/// <param name="effectIn">The game object's effect component.</param>
		/// <param name="audioIn">The game object's audio component.</param>
		/// <param name="id_In">The game object's identifier.</param>
		/// <param name="position_in">The game object's position.</param>
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, HealthComponent health_In,
						   EffectComponent effectIn, AudioComponent audioIn, Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = health_In;
			Attack = null;
			Effect = effectIn;
			Audio = audioIn;
			Identifier = id_In;
			Position = position_in;
		}

		/// <summary>
		/// Creates a complete game object with the entered component values.
		/// </summary>
		/// <param name="graphics_In">The game object's graphics component.</param>
		/// <param name="input_In">The game object's input component.</param>
		/// <param name="health_In">The game object's health component.</param>
		/// <param name="effectIn">The game object's effect component.</param>
		/// <param name="attackIn">The game object's attack component.</param>
		/// <param name="audioIn">The game object's audio component.</param>
		/// <param name="id_In">The game object's identifier.</param>
		/// <param name="position_in">The game object's position.</param>
		public GameObject( GraphicsComponent graphics_In, InputComponent input_In, HealthComponent health_In,
						   EffectComponent effectIn, AttackComponent attackIn, AudioComponent audioIn, 
						   Identifiers id_In, Vector2 position_in )
		{
			Graphics = graphics_In;
			Input = input_In;
			Health = health_In;
			Attack = attackIn;
			Effect = effectIn;
			Audio = audioIn;
			Identifier = id_In;
			Position = position_in;
		}

		/// <summary>
		/// Updates game Object with game time.
		/// </summary>
		/// <param name="gameTime">game time</param>
		public void Update( GameTime gameTime )
		{
			Input?.HandleInput( this, gameTime );
			Health?.UpdateCoolDownTimer();
			Attack?.UpdateAttackTimer();
		}

		/// <summary>
		/// Updates game object with keyboard state.
		/// </summary>
		/// <param name="state">keyboard state</param>
		public void Update( KeyboardState state )
		{
			Input?.HandleInput( this, state );
			Health?.UpdateCoolDownTimer();
			Attack?.UpdateAttackTimer();
		}

		/// <summary>
		/// Updates game object with keyboard state and game state.
		/// </summary>
		/// <param name="state">keyboard state</param>
		/// <param name="gameState">Game state</param>
		public void Update( KeyboardState state, GameState gameState )
		{
			Input?.HandleInput( this, state, gameState );
			Health?.UpdateCoolDownTimer();
			Attack?.UpdateAttackTimer();
		}

		/// <summary>
		/// Updates game object with keyboard state, game state, and game time.
		/// </summary>
		/// <param name="state">Keyboard state</param>
		/// <param name="gameState">Hame state</param>
		/// <param name="gameTime">Game time</param>
		public void Update( KeyboardState state, GameState gameState, GameTime gameTime )
		{
			var elapsedGameTime = ( float )gameTime.ElapsedGameTime.TotalSeconds;
			Input?.HandleInput( this, state, gameState );
			Health?.UpdateCoolDownTimer();
			Attack?.UpdateAttackTimer();
			Position += Input.GetVelocity() * elapsedGameTime;
		}

		/// <summary>
		/// Checks if the graphics component is null and renders the gameObejct
		/// if it is not.
		/// </summary>
		/// <param name="batch">Game spritebatch</param>
		public void Render( ref SpriteBatch batch )
		{
			Graphics?.Render( this, ref batch );
		}

		/// <summary>
		/// Used to draw and update the lifebar graphics component.
		/// </summary>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="health">Player health</param>
		public void Render( ref SpriteBatch batch, HealthComponent health )
		{
			( ( LifeBarGraphicsComponent )Graphics )?.Render( this, health, ref batch );
		}

		/// <summary>
		/// Used to render the game obejct if it is withis the camera bounds render 
		/// area rectangle.
		/// </summary>
		/// <param name="batch">Game spritebatch</param>
		/// <param name="cameraBounds">Cmaera area rectangle.</param>
		public void Render( ref SpriteBatch batch, Rectangle cameraBounds )
		{
			Graphics?.Render( this, ref batch, cameraBounds );
		}

		/// <summary>
		/// Centers the X position of the gameObject relative
		/// to the screen size.
		/// </summary>
		/// <param name="screenWidth">Game screen width</param>
		/// <param name="textureWidth">Texture width</param>
		/// <returns></returns>
		public float CenterXWidth( float screenWidth, float textureWidth )
		{
			return ( screenWidth / 2 ) - ( textureWidth / 2 );
		}

		/// <summary>
		/// recenters the x Width of the game Object.
		/// </summary>
		/// <param name="screenWidth"> Screen width.</param>
		public void RecenterXWidth( float screenWidth )
		{
			if( Graphics != null )
			{
				Position = new Vector2( CenterXWidth( screenWidth, Graphics.GetTextureSize().X ), Position.Y );
			}
		}

		/// <summary>
		/// Returns the gameObject area rectangle.
		/// </summary>
		/// <returns></returns>
		public Rectangle GetRectangle()
		{
			return new Rectangle( ( int )Position.X, ( int )Position.Y, 
								  ( int )Graphics.GetTextureSize().X, ( int )Graphics.GetTextureSize().Y );
		}

		/// <value>The object graphics component.</value>
		public GraphicsComponent Graphics { get; set; }

		/// <value>The object input component.</value>
		public InputComponent Input { get; set; }

		/// <value>The object Health component.</value>
		public HealthComponent Health { get; set; }

		/// <value>The object attack component.</value>
		public AttackComponent Attack { get; set; }

		/// <value>The object audio component.</value>
		public AudioComponent Audio { get; set; }

		/// <value>The object effect component.</value>
		public EffectComponent Effect { get; set; }

		/// <value>The object identifier.</value>
		public Identifiers Identifier { get; private set; }

		/// <value>The current object position.</value>
		public Vector2 Position { get; set; }
	}
}