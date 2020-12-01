using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BirdWarsTest.Utilities
{
	class PositionGenerator
	{
		public PositionGenerator()
		{
			indexGenerator = new Random();
			startPositions = new List< Vector2 >();
			ResetStartPositions();
			lowerLimit = 0;
			upperLimit = 8;
		}

		public void ResetLimit()
		{
			upperLimit = 8;
		}

		public void ResetStartPositions()
		{
			startPositions.Clear();
			startPositions.Add( new Vector2( 400, 300 ) );
			startPositions.Add( new Vector2( 1200, 300 ) );
			startPositions.Add( new Vector2( 2000, 300 ) );
			startPositions.Add( new Vector2( 400, 900 ) );
			startPositions.Add( new Vector2( 2000, 900 ) );
			startPositions.Add( new Vector2( 400, 1500 ) );
			startPositions.Add( new Vector2( 1200, 1500 ) );
			startPositions.Add( new Vector2( 2000, 1500 ) );
		}

		public Vector2 GetAPosition()
		{
			int vectorIndex = indexGenerator.Next( lowerLimit, upperLimit );
			upperLimit--;
			var vector = startPositions[ vectorIndex ];
			startPositions.RemoveAt( vectorIndex );
			ResetLimitAndPositions();
			return vector;
		}

		private void ResetLimitAndPositions()
		{
			if( upperLimit == lowerLimit )
			{
				ResetStartPositions();
				ResetLimit();
			}
		}

		private Random indexGenerator;
		private List< Vector2 > startPositions;
		private int lowerLimit;
		private int upperLimit;
	}
}