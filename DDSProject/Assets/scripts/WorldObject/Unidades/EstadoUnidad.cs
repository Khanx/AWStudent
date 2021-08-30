using UnityEngine;

public abstract class EstadoUnidad
	{

		protected Unidad unit;
		protected Acciones Action = Acciones.INDEFINIDO;

		public EstadoUnidad(Unidad unit)
			{
				this.unit = unit;
			}

		//1- Esperar
		//2- Atacar
		//3- Capturar ¿?
		public void SetAction(Acciones Action)
			{
				this.Action=Action;

				if(Action == Acciones.ESPERAR)
					RealizarAccion();
			}

		public abstract void Start();	//Seleccionar Unidad
		public abstract void RealizarAccion();
		public abstract void LeftClick(GameObject gob);
		public abstract void RightClick();	//Cancelar | Deseleccionar

		protected void Pintar(Color color)
			{
				Vector3 origen = unit.transform.position;
				int x = (int)origen.x, y = 1, z = (int)origen.z;	//NOTESE EL 1 en la Y

				int DistanciaDeMovimiento = unit.GetDistanciaDeMovimiento ();

				for (int i=x-DistanciaDeMovimiento; i<=x+DistanciaDeMovimiento; i++)
					for (int j=z-DistanciaDeMovimiento; j<=z+DistanciaDeMovimiento; j++)
						{
							Vector3 pos = new Vector3(i,y, j);
							Camino c = CalcularCosteCamino(origen, pos);

							if(c.GetCoste()<=DistanciaDeMovimiento)
							Mapa.Instancia().PintarTerreno(pos, color);
						}
			}

		//REFACTORIZAR: Replace Method with Method Object
		protected Camino CalcularCosteCamino(Vector3 origen, Vector3 destino)
			{
				return new Camino(origen, destino, unit.GetAndaPor()).CalcularCosteCamino();
			}

	}
