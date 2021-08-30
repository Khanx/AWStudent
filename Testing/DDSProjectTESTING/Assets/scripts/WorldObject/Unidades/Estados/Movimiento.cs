using UnityEngine;
using System.Collections.Generic;

public class Movimiento : EstadoUnidad
	{
		Vector3 origen, destino;
		Unidad objetivo = null;

		public Movimiento(Unidad unit, Queue<Vector3> camino) : base(unit)
			{
				Pintar (Color.clear);	//DESPINTAR

				origen = camino.Peek();
				destino = camino.ToArray()[camino.Count-1];

				unit.MoverA(camino);

				Mapa.Instancia().SetUnidad (origen, destino);
				//unit.showUI();
				ControlInterfaz.MenuUnidadOn();
			}

		public override void Start()	{}


		public override void RealizarAccion ()
			{
				switch(Action)
					{
						case Acciones.INDEFINIDO:
							return;
						break;
						
						case Acciones.ESPERAR:
							Debug.Log ("La unidad espera");
						break;

						case Acciones.ATACAR:
							Debug.Log ("La unidad ataca a:"+objetivo.name);
							objetivo.QuitarVida(unit.GetPenetracionDeArmadura(), unit.GetDanyo());
						break;
					}

				unit.SetEstado(new Fin(unit));
			}

		public override void LeftClick(GameObject gob)
			{
				if(gob != null && gob != unit.gameObject)
					{
						Unidad temp = gob.GetComponent<Unidad>();
						
						bool EnRangoDeAtaque = temp != null && Vector3.Distance(temp.transform.position, destino)<=unit.GetRangoDeAtaque();
						bool EsEnemiga = temp != null && temp.GetDuenyo() != Turno.instancia().JugadorActual();
						
						if(EnRangoDeAtaque && EsEnemiga)
							{
								objetivo = temp;
								RealizarAccion();
							}
					}
			}

		public override void RightClick()
			{
				if(Action == Acciones.INDEFINIDO)
					{
						Camino c = CalcularCosteCamino(destino, origen);

						unit.MoverA(c.GetCamino());
						unit.SetEstado (new Inicio(unit));

						Mapa.Instancia().SetUnidad (destino, origen);

						//unit.hideUI();
						ControlInterfaz.MenuUnidadOff();
						unit.SetEstado(new Inicio(unit));
						unit.Seleccionar();
					}
				else
					{
						Action = Acciones.INDEFINIDO;
						//unit.showUI();
						ControlInterfaz.MenuUnidadOn();
					}
			}
	}
