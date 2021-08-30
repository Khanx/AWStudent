
public abstract class Aereo : Unidad
	{
		public void Start()
			{
				base.Start ();
				DistanciaDeMovimiento=8;
				AndaPor = PosiblesTerrenos.Tierra | PosiblesTerrenos.Agua;
			}
	}