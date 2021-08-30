
public abstract class Naval : Unidad
	{
		public void Start()
			{
				base.Start ();
				DistanciaDeMovimiento=7;
				AndaPor = PosiblesTerrenos.Agua;
			}
	}