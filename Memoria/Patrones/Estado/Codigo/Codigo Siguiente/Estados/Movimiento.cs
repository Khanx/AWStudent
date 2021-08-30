using UnityEngine;
using System.Collections.Generic;

public class Movimiento : EstadoUnidad
{
	Camino c;

	public Movimiento(Unidad unit, Camino c, Vector3 destino) : base(unit)
		{
			pintar (Color.white);	//DESPINTAR
			this.c = c;
			unit.getAgent ().SetDestination(destino);
		}

	public override void Start()	{}

	public override void leftClick(GameObject gob)
		{
			Debug.Log ("La unidad espera");
			unit.setEstado (new Fin(unit));
		}

	public override void End()
		{
			unit.setEstado (new Inicio(unit));
		}
}