using UnityEngine;
using System.Collections;


//MEDIADOR
public class Mapa : MonoBehaviour
	{

		public static int tamanyo = 22;	//DEPENDE DEL MAPA REFACTORING
		public static Mapa instancia { get; private set; }
		private Terreno[,] terrenos = new Terreno[tamanyo,tamanyo];		//EL TAMAÑO HAY QUE DEFINIRLO FUERA
		private Unidad[,] unidades = new Unidad[tamanyo, tamanyo];
		private Mapa()	{}

		void Awake() //SINGLETON
			{
				instancia = this;
			}

		public void addTerreno(Terreno terrain)
			{
				terrenos[(int)terrain.transform.position.x, (int)terrain.transform.position.z] = terrain;
			}

		public Terreno getTerreno(int x, int z)
		{
			if (x >= tamanyo || z >= tamanyo || x<0 || z<0)
				return null;
			
			return terrenos [x,z];
		}

		public void addUnidad(Unidad unit)
			{
				unidades[(int)unit.transform.position.x, (int)unit.transform.position.z] = unit;
			}

		public Unidad getUnidad(int x, int z)
			{
				if (x >= tamanyo || z >= tamanyo || x<0 || z<0)
					return null;

				return unidades [x, z];
			}

		public void setUnidad(int oldx, int oldz, int x, int z)
			{
				//NO HAY COMPROBACION DE XY PORQUE HAY RESTRICCIONES INTERMEDIAS
				unidades [x, z] = unidades [oldx, oldz];
				unidades [oldx, oldz] = null;
			}

		public void delUnidad(int x, int z)
			{
				//NO HAY COMPROBACION DE XY PORQUE HAY RESTRICCIONES INTERMEDIAS
				unidades [x, z] = null;
			}

		public bool puedeAndar(int x, int z, PosiblesTerrenos p)
			{
				if (x >= tamanyo || z >= tamanyo || x<0 || z<0)
					return false;
				
				return !unidades[x,z] && terrenos [x,z] && terrenos [x,z].puedeAndar (p);		//REFACTORING -> NULLOBJECT
			}
	}