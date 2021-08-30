using UnityEngine;

public class Inicio : EstadoUnidad
	{
		public Inicio(Unidad unit) : base(unit)	{}

		public override void Start()
			{
				Pintar (Color.yellow);
			}

		public override void LeftClick(GameObject gob)
			{
				if (Turno.instancia().JugadorActual() != unit.GetDuenyo())
						return;

				if (!gob || (gob.tag != "Suelo" && gob != unit.gameObject))
					return;

				Vector3 origen = unit.transform.position;
				Vector3 destino = gob.transform.position;
					destino.y = origen.y;

				Camino c = CalcularCosteCamino(origen, destino);

				if(c.GetCoste()<=unit.GetDistanciaDeMovimiento())
					unit.SetEstado (new Movimiento(unit, c.GetCamino()));
			}

		public override void RightClick()
			{
				Pintar (Color.clear);
				UserInput.instance.selectObject=null;
			}

		public override void RealizarAccion ()	{}
	}
