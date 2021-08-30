
public abstract class Terrestre : Unidad
	{
		public void Start ()
			{
				base.Start ();
				AndaPor = PosiblesTerrenos.Tierra;// | AndaPor = PosiblesTerrenos.Bosque;
			}
	}