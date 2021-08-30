using UnityEngine;
using System.Collections.Generic;

public class Inicio : EstadoUnidad
	{
		public Inicio(Unidad unit) : base(unit)	{}

		public override void Start()
			{
				pintar (Color.yellow);
			}

		public override void leftClick(GameObject gob)
			{
				if (!gob || gob.tag != "Suelo")
						return;

					Vector3 origen = new Vector3(x,y,z);
					Vector3 pos = gob.transform.position;
					pos.y = origen.y;
					Camino c = CalcularCosteCamino(origen, pos, new List<Vector3>());

					if(c.getCoste()<=mov)
						unit.setEstado (new Movimiento(unit,c, pos));
			}

		public override void End()
			{
				pintar (Color.white);
			}
	}