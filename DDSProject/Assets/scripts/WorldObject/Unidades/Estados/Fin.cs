using UnityEngine;

public class Fin : EstadoUnidad
	{
		public Fin(Unidad unit) : base(unit){}

		public override void Start()
			{
				Pintar (Color.yellow);
			}

		public override void LeftClick(GameObject gob)
			{
				Pintar (Color.yellow);
			}

		public override void RightClick()
			{
				Pintar (Color.clear);
				UserInput.instance.selectObject = null;
			}

		public override void RealizarAccion ()	{}
	}
