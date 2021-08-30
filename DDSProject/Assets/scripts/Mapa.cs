using UnityEngine;

//MEDIADOR
public class Mapa
	{
		public static int tamanyox = 16, tamanyoz = 12;	//DEPENDE DEL MAPA REFACTORING
		public static Mapa ins;
		private IColega[,,] colegas = new IColega[tamanyox, 3, tamanyoz];
		private Mapa()	{}

		public static Mapa Instancia()
			{
				if(ins == null)
					ins = new Mapa();

				return ins;
			}

		public void AddEdificio(Edificio edificio) 
			{
				colegas[(int)edificio.transform.position.x,2,(int)edificio.transform.position.z] = edificio;
			}
	
		public void DeleteEdificio(Vector3 posicion) 
			{
				colegas[(int)posicion.x,2,(int)posicion.z] = null;
			}
	
		public void AddTerreno(Terreno terrain)
			{
				colegas[(int)terrain.transform.position.x, 0, (int)terrain.transform.position.z] = terrain;
			}

		public void PintarTerreno(Vector3 posicion, Color color)
			{
				int x = (int)posicion.x, z = (int)posicion.z;

				if (x < tamanyox && z < tamanyoz && x >= 0 && z >= 0)
					((Terreno)colegas [x, 0, z]).Pintar (color);
			}

		public void AddUnidad(Unidad unit)
			{
				colegas[(int)unit.transform.position.x, 1, (int)unit.transform.position.z] = unit;
			}

		public void SetUnidad(Vector3 origen, Vector3 destino)
			{
				int xo = (int)origen.x, zo = (int)origen.z;
				int xd = (int)destino.x, zd = (int)destino.z;

				if(xo == xd && zo == zd)
					return;

				//NO HAY COMPROBACION DE XY PORQUE HAY RESTRICCIONES INTERMEDIAS
				colegas [xd, 1, zd] = colegas [xo, 1, zo];
				colegas [xo, 1, zo] = null;
			}

		public void DelUnidad(Vector3 posicion)
			{
				//NO HAY COMPROBACION DE XZ PORQUE HAY RESTRICCIONES INTERMEDIAS
				colegas [(int)posicion.x, 1, (int)posicion.z] = null;
			}

		public bool PuedeAndar(Vector3 posicion, PosiblesTerrenos p)
			{
				int x = (int)posicion.x, z = (int)posicion.z;

				if ((int)posicion.x >= tamanyox || z >= tamanyoz || x<0 || z<0)
					return false;

				bool PosicionNoOcupada = colegas[x, 1, z] == null;
				bool PuedeAndarPorTerreno = colegas [x, 0, z] != null && ((Terreno)colegas [x,0, z]).PuedeAndar (p);	

				return  PosicionNoOcupada && PuedeAndarPorTerreno;
			}

		public void IniciarTurno()
			{
				for (int i=0; i<tamanyox; i++) {
					for (int j=0; j<tamanyoz-1; j++) {
						if (colegas [i, 1, j] != null) { ((Unidad)colegas [i, 1, j]).IniciarTurno (); }
						if (colegas [i, 2, j] != null) { ((Edificio)colegas [i, 2, j]).IniciarTurno (); }	
					}
				}
			}
	
		public bool UnidadesEnemigasEnRango(Vector3 posicion, int distancia)
			{
				int x = (int)posicion.x, z=(int)posicion.z;

				for(int i=x-distancia;i <= x+distancia;i++)
					for(int j=z-distancia;j <= z+distancia;j++)
						{
							bool PosicionFueraDelMapa = (i >= tamanyox || j >= tamanyoz || i < 0 || j < 0 || (i==x && j==z));
							
							if (PosicionFueraDelMapa)
								continue;

								bool DetectaUnidadEnemiga = (colegas[i,1,j] != null && ((Unidad)colegas[i,1,j]).GetDuenyo() != Turno.instancia().JugadorActual());
								if(DetectaUnidadEnemiga)
									if(Vector3.Distance(posicion, new Vector3(i,1,j))<=distancia)
										return true;
						}

				return false;
			}
	}