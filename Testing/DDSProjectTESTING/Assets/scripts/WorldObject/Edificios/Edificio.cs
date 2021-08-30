using UnityEngine;

public abstract class Edificio : WorldObject, IColega
	{
		private bool puedeCrear = true;

		override public void RightClick()
			{
				UserInput.instance.selectObject = null;
				ControlInterfaz.MenuFabricOff();
			}

		override public void Seleccionar()	
			{
				ControlInterfaz.MenuFabricaOn();
			}

		override public void IniciarTurno() { puedeCrear = true; }

		public bool GetPuedeCrear() { return puedeCrear; }
	
		public void SetPuedeCrear(bool cond) { puedeCrear = cond; }
	}