
public abstract class Infanteria : Terrestre
	{
		public void Start ()
			{
				base.Start ();
				Armadura = 0;
				DistanciaDeMovimiento=4;
				//AndaPor =| PosiblesTerrenos.Montanya;
			}
	}