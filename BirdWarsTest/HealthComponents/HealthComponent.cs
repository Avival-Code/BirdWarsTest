namespace BirdWarsTest.HealthComponents
{
	public class HealthComponent
	{
		public HealthComponent()
		{
			Health = 5.0f;
		}

		public void TakeDamage( float damage )
		{
			Health -= damage;
		}

		public void Heal( float health )
		{
			Health += health;
		}

		public bool IsDead()
		{
			return Health > 0.0f;
		}

		public float Health { get; set; }
	}
}