using Microsoft.Xna.Framework;

namespace BirdWarsTest.GameRounds
{
	class Camera2D
	{
		public Camera2D()
		{
			CameraPosition = new Vector2( 0.0f, 0.0f );
			moveEntityPosition = new Vector2( CameraPosition.X + 200.0f, CameraPosition.Y + 150.0f );
		}
		public Camera2D( Vector2 cameraPosition )
		{
			CameraPosition = cameraPosition;
			moveEntityPosition = new Vector2( CameraPosition.X + 200.0f, CameraPosition.Y + 150.0f );
		}

		public void Update( Rectangle mapBoundary, Rectangle objectRect )
		{
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

		public Rectangle GetCameraBounds()
		{
			return new Rectangle( ( int )CameraPosition.X, ( int )CameraPosition.Y, CameraWidth, CameraHeight );
		}

		private Rectangle GetMoveEntityBounds()
		{
			return new Rectangle( ( int )moveEntityPosition.X, ( int )moveEntityPosition.Y, MoveEntityWidth, MoveEntityHeight );
		}

		public Vector2 CameraPosition { get; set; }
		private Vector2 moveEntityPosition;
		private const int CameraWidth = 800;
		private const int CameraHeight = 600;
		private const int MoveEntityWidth = CameraWidth / 2;
		private const int MoveEntityHeight = CameraHeight / 2;
	}
}