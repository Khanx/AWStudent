using UnityEngine;
using System.Collections.Generic;

public abstract class EstadoUnidad
	{
		protected Unidad unit;
		protected int x, y, z,mov;
		protected PosiblesTerrenos AndaPor;

		public EstadoUnidad(Unidad unit)
			{
				this.unit = unit;
				x = (int)unit.transform.position.x;
				y = (int)unit.transform.position.y;
				z = (int)unit.transform.position.z;
				mov = unit.getDistanciaDeMovimiento();
				AndaPor = unit.getAndaPor();
			}

		public EstadoUnidad(Unidad unit, int x, int z, int mov)
			{
				this.unit = unit;
				this.x = x;
				this.z = z;
				this.mov = mov;
			}

		public abstract void Start();	//Seleccionar Unidad
		public abstract void leftClick(GameObject gob);
		public abstract void End();	//Cancelar | Deseleccionar

		protected void pintar(Color color)
		{
			Vector3 origen = new Vector3 (x,y, z);

			for (int i=x-mov; i<=x+mov; i++)
				for (int j=z-mov; j<=z+mov; j++)
					{
						Vector3 pos = new Vector3(i,y, j);
						Camino c = CalcularCosteCamino(origen,pos, new List<Vector3>());
						if(c!=null && c.getCoste()<=mov)
								Mapa.instancia.pintarTerreno(i,j,color);
					}
		}

		protected Camino CalcularCosteCamino(Vector3 origen, Vector3 destino, List<Vector3> list, int cost = 0)
		{
			list.Add (origen);

			//Si no es posible ir al destino no calculamos
			if (!Mapa.instancia.puedeAndar ((int)destino.x, (int)destino.z, AndaPor))
				return new Camino(int.MaxValue, list);

			//Usamos 0.5 porque hay una cota de error
			if (Vector3.Distance(origen,destino) <= 0.5)
				return new Camino (cost, list);



			float[,] mod = new float[4,2]{{1,0},{-1,0},{0,1},{0,-1}};
			Vector3 BNext = origen;
			float Bcost = float.MaxValue;

			for (int i=0; i<4; i++)
			{
				Vector3 ndexdest = origen;
				ndexdest.x += mod[i,0];
				ndexdest.z += mod[i,1];

				bool found=false;
				foreach (Vector3 vec in list)
					if (Vector3.Distance(ndexdest,vec) <= 0.5) //Usamos 0.5 porque hay una cota de error
				{
					found = true;
					break;
				}

				if(found)	//No pasar por un sitio por donde ya has pasado
					continue;

				if(!Mapa.instancia.puedeAndar((int)ndexdest.x,(int)ndexdest.z, AndaPor))
					continue;

				float hcost = ((Vector3)(destino - ndexdest)).magnitude+cost;

				if(hcost<Bcost)
				{
					BNext=ndexdest;
					Bcost=hcost;
				}
			}

			if (Bcost == float.MaxValue)	//Camino imposible
				return new Camino(int.MaxValue, list);

			return CalcularCosteCamino(BNext, destino, list, cost+1);
		}

	}