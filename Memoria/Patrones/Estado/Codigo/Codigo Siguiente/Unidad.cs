using UnityEngine;
using System.Collections.Generic;

public class Unidad : WorldObject
	{
		private NavMeshAgent agent;
		protected int DistanciaDeMovimiento;
		protected int Vida, Armadura, PenetracionDeArmadura, Danyo, RangoDeAtaque;
		protected bool AccionDisponible;
		protected PosiblesTerrenos AndaPor;
		protected EstadoUnidad estado;

		public int getVida()						{	return Vida;					}
		public int getArmadura()					{	return Armadura;				}
		public int getPenetracionDeArmadura()		{	return PenetracionDeArmadura;	}
		public int getDanyo()						{	return Danyo;					}

		public NavMeshAgent getAgent()				{	return agent;					}
		public int getDistanciaDeMovimiento()		{	return DistanciaDeMovimiento;	}
		public PosiblesTerrenos getAndaPor() 		{	return AndaPor;					}
		
		public void iniciarTurno()					{	estado = new Inicio (this); 	}
		override public void seleccionar()			{	estado.Start ();				}
		public void setEstado(EstadoUnidad estado)	{	this.estado = estado; 		}
	
		// Use this for initialization
		public void Start ()
			{
				base.Start ();
				agent = GetComponent<NavMeshAgent> ();
				agent.avoidancePriority = 99;
				agent.updateRotation = false;
				Mapa.instancia.addUnidad (this);
				/*
				NO ROTAR:
				Interfaz: Angular Speed=0
				Codigo:  agent.updateRotation = false;
				*/

				//Declaramos valores por defecto
				Vida = 100;
				RangoDeAtaque = 1;
				//NO SE PUEDE USAR AQUI PORQUE FALTA DECLARAR ATRIBUTOS
				//estado = new Inicio(this);	
			}


		override public void rightClick(GameObject gob)
			{
				estado.leftClick (gob);
				if (!gob)
					return;
				/*
				if (gob.tag == "Suelo" || gob.name == "Selector")
					Move (gob);
				else
					Attack (gob);
				*/
			}

		private void Move(GameObject gob)
			{
				if (!gob)
					return;

				Vector3 destination = gob.transform.position;
				Vector3 myPosition = transform.position;

				destination.y = myPosition.y;


				List<Vector3> list = new List<Vector3> ();
				Camino recorrido = CalcularCosteCamino (myPosition, destination, list);

				Debug.Log (recorrido.getCoste());
				Debug.Log (DistanciaDeMovimiento);

				if (recorrido.getCoste () <= DistanciaDeMovimiento)
					{
						agent.SetDestination (destination);
						
						//DEBUG
						foreach(Vector3 v in list)
							Mapa.instancia.getTerreno((int)v.x,(int)v.z).Pintar(Color.yellow);
						
						Mapa.instancia.setUnidad((int)myPosition.x,(int)myPosition.z,(int)destination.x, (int)destination.z);
					}
			}

		/**
		** Algorigmo A* de busqueda de caminos
		** Devuelve un Camino compuesto por el coste del camino Y los nodos recorridos
		**/
		/**
		 * REFACTORING: cambiar lista por hashmap
		**/
		private Camino CalcularCosteCamino(Vector3 origen, Vector3 destino, List<Vector3> list, int cost = 0)
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

		private void Attack(GameObject gob)
			{
				Unidad unidad = gob.transform.root.GetComponent<Unidad> ();

				//REFACTORIZAR NO HAY DUENYO ASI QUE HE PUSETO LA CONDICION
				if (!unidad || this == unidad || (this.Duenyo && this.Duenyo == unidad.Duenyo))
					return;

				if (Vector3.Distance (this.transform.position, unidad.transform.position) > (double)(RangoDeAtaque+0.5))
					{
						Debug.Log ("No se puede atacar a la unidad, esta demasiado lejos");
						return;
					}

				unidad.QuitarVida (PenetracionDeArmadura, Danyo);
				Debug.Log ("La: "+unidad.name+" ahora tiene "+unidad.getVida()+" de vida.");
			}

		public void QuitarVida(int PenetracionDeArmadura, int Danyo)
			{
				//Porcentaje de modificacion de danyo, depende de la armadura y de la penetracion
				double ModificadorDanyo = 1 - (this.Armadura - PenetracionDeArmadura)/100;

					if (ModificadorDanyo < 0)
						ModificadorDanyo = 1;

				this.Vida -= (int)(Danyo*ModificadorDanyo);

				if (this.Vida <= 0)
					{
						Debug.Log(this.name + " ha muerto.");
						Destroy (this.gameObject);
						Mapa.instancia.delUnidad((int)(this.transform.position.x), (int)(this.transform.position.z));
					}
			}
	}