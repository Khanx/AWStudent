
public class Tierra : Terreno
	{
		public override bool PuedeAndar (PosiblesTerrenos p)
			{
				return (int)(p & PosiblesTerrenos.Tierra) >= 1;
			}
	}