
public class Bombardero : Aereo
	{
		public void Start()
			{
				base.Start ();
				PenetracionDeArmadura = 30;
				Danyo = 40;
				Armadura = 20;
			}
	}