
public class Caza : Aereo
	{
		public void Start()
			{
				base.Start ();
				PenetracionDeArmadura = 20;
				Danyo = 50;
				Armadura = 40;
			}
	}