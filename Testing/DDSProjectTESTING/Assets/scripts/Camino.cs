using UnityEngine;
using System.Collections.Generic;

public class Camino
	{
		private Queue<Vector3> cola;
		private int coste;
		private PosiblesTerrenos AndaPor;
		private Vector3 origen, destino;

		public Camino(Vector3 origen, Vector3 destino, PosiblesTerrenos AndaPor)
			{
				cola = new Queue<Vector3>();
				this.AndaPor = AndaPor;
				this.origen = origen;
				this.destino = destino;
			}

		public Camino(int coste, Queue<Vector3> cola)
			{
				this.coste = coste;
				this.cola = cola;
			}

		public Queue<Vector3> GetCamino()	{return cola;}
		public int GetCoste()	{return coste;}

		public Camino CalcularCosteCamino()
			{
				return CalcularCosteCamino(origen, destino);
			}

		private Camino CalcularCosteCamino(Vector3 origen, Vector3 destino)
			{
				cola.Enqueue (origen);
				
				//Usamos 0.5 porque hay una cota de error
				if (Vector3.Distance(origen,destino) <= 0.5)
					return new Camino (coste, cola);
				
				//Si no es posible ir al destino no calculamos
				if (!Mapa.Instancia().PuedeAndar (destino, AndaPor))
					return new Camino(int.MaxValue, cola);
				
				float[,] mod = new float[4,2]{{1,0},{-1,0},{0,1},{0,-1}};	//MOVIMIENTO NO DIAGONAL
				Vector3 BNext = origen;
				float Bcost = float.MaxValue;
				
				for (int i=0; i<4; i++)
					{
						Vector3 ndexdest = origen;
						ndexdest.x += mod[i,0];
						ndexdest.z += mod[i,1];
						
						if(cola.Contains(ndexdest))	//No pasar por un sitio por donde ya has pasado
							continue;
						
						if(!Mapa.Instancia().PuedeAndar(ndexdest, AndaPor))
							continue;
						
						float hcost = ((Vector3)(destino - ndexdest)).magnitude+coste;
						
						if(hcost<Bcost)
						{
							BNext=ndexdest;
							Bcost=hcost;
						}
					}
				
				if (Bcost == float.MaxValue)	//Camino imposible
				return new Camino(int.MaxValue, cola);
			
			coste++;	//Aumentamos el coste del camino
			return CalcularCosteCamino(BNext, destino);
		}
	}