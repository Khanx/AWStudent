using UnityEngine;
using System.Collections;

public class Fin : EstadoUnidad
	{
		public Fin(Unidad unit) : base(unit){}

		public override void Start()
			{
				pintar (Color.yellow);
			}

		public override void leftClick(GameObject gob){}

		public override void End()
			{
				pintar (Color.white);
			}
	}