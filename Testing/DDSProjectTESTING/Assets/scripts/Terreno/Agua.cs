
public class Agua : Terreno
	{
		public override bool PuedeAndar (PosiblesTerrenos p)
			{
				return (int)(p & PosiblesTerrenos.Agua) >= 1;
			}
	}