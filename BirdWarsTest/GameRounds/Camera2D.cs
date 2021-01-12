/********************************************
Programmer: Christian Felipe de Jesus Avila Valdes
Date: January 10, 2021

File Description:
Camera used to move and see around the game world.
*********************************************/
using Microsoft.Xna.Framework;

namespace BirdWarsTest.GameRounds
{
	/// <summary>
	/// Camera used to move and see around the game world.
	/// </summary>
	public class Camera2D
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public Camera2D()
		{
			CameraPosition = new Vector2( 0.0f, 0.0f );
			moveEntityPosition = new Vector2( CameraPosition.X + 200.0f, CameraPosition.Y + 150.0f );
			IsCameraSet = false;
		}

		/// <summary>
		/// Creates the camera at the specified position.
		/// </summary>
		/// <param name="cameraPosition">The camera position.</param>
		public Camera2D( Vector2 cameraPosition )
		{
			CameraPosition = cameraPosition;
			moveEntityPosition = new Vector2( CameraPosition.X + 200.0f, CameraPosition.Y + 150.0f );
			IsCameraSet = false;
		}

		/// <summary>
		/// Updates the camera movement and keeps it within the game
		/// world limits.
		/// </summary>
		/// <param name="position">Local player position.</param>
		/// <param name="mapBoundary">The game world area rectangle.</param>
		/// <param name="objectRect">The local player area rectangle.</param>
		/// <param name="createdPlayers">Bool indicating whether players were created.</param>
		public void Update( Vector2 position, Rectangle mapBoundary, Rectangle objectRect, bool createdPlayers )
		{
			SetCameraToLocalPlayer( position, createdPlayers );
			if( CameraPosition.Y >= mapBoundary.Top && objectRect.Top < GetMoveEntityBounds().Top )
			{
				Vector2 temp = new Vector2( 0.0f, GetMoveEntityBounds().Top - objectRect.Top );
				CameraPosition -= temp;
				moveEntityPosition -= temp;
			}
			if( GetCameraBounds().Bottom <= mapBoundary.Bottom && objectRect.Bottom > GetMoveEntityBounds().Bottom )
			{
				Vector2 temp = new Vector2( 0.0f, objectRect.Bottom - GetMoveEntityBounds().Bottom );
				CameraPosition += temp;
				moveEntityPosition += temp;
			}
			if( CameraPosition.X >= mapBoundary.Left && objectRect.Left < GetMoveEntityBounds().Left )
			{
				Vector2 temp = new Vector2( GetMoveEntityBounds().Left - objectRect.Left, 0.0f );
				CameraPosition -= temp;
				moveEntityPosition -= temp;
			}
			if( GetCameraBounds().Right <= mapBoundary.Right && objectRect.Right > GetMoveEntityBounds().Right )
			{
				Vector2 temp = new Vector2( objectRect.Right - GetMoveEntityBounds().Right, 0.0f );
				CameraPosition += temp;
				moveEntityPosition += temp;
			}
		}

		private void SetCameraToLocalPlayer( Vector2 localPlayerPosition, bool createdPlayers )
		{
			if( createdPlayers && !IsCameraSet )
			{
				SetCameraPosition( localPlayerPosition );
			}
		}

		/// <summary>
		/// Set the camera position to the specified position.
		/// </summary>
		/// <param name="position"> the desired position.</param>
		public void SetCameraPosition( Vector2 position )
		{
			CameraPosition = new Vector2( position.X - 400.0f, position.Y - 300.0f );
			moveEntityPosition = new Vector2( CameraPosition.X + 200.0f, CameraPosition.Y + 150.0f );
			IsCameraSet = true;
		}

		/// <summary>
		/// Retrieves the current camera position.
		/// </summary>
		/// <returns>The current area rectangle.</returns>
		public Rectangle GetCameraBounds()
		{
			return new Rectangle( ( int )CameraPosition.X, ( int )CameraPosition.Y, CameraWidth, CameraHeight );
		}

		/// <summary>
		/// calculates and retrieves the render area rectangle.
		/// </summary>
		/// <returns></returns>
		public Rectangle GetCameraRenderBounds()
		{
			return new Rectangle( ( int )CameraPosition.X - 100, ( int )CameraPosition.Y - 100, CameraWidth + 100, CameraHeight + 100 );
		}

		private Rectangle GetMoveEntityBounds()
		{
			return new Rectangle( ( int )moveEntityPosition.X, ( int )moveEntityPosition.Y, MoveEntityWidth, MoveEntityHeight );
		}

		/// <summary>
		/// Resets the camera values.
		/// </summary>
		public void ResetCamera()
		{
			CameraPosition = new Vector2( 0.0f, 0.0f );
			moveEntityPosition = new Vector2( CameraPosition.X + 200.0f, CameraPosition.Y + 150.0f );
			IsCameraSet = false;
		}

		/// <value>The current camera position.</value>
		public Vector2 CameraPosition { get; set; }

		/// <value>bool indiciating if camera is following the player.</value>
		public bool IsCameraSet { get; private set; }

		private Vector2 moveEntityPosition;
		private const int CameraWidth = 800;
		private const int CameraHeight = 600;
		private const int MoveEntityWidth = CameraWidth / 2;
		private const int MoveEntityHeight = CameraHeight / 2;
	}
}