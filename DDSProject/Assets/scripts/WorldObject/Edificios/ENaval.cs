using UnityEngine;

public class ENaval : Edificio
{
	private FabricaNaval FabricaNaval;

	public void Start ()
		{
			base.Start ();
			GameObject FabricaNavalG = GameObject.Find("FabricaAgua");
			
			FabricaNaval = FabricaNavalG.GetComponent<FabricaNaval>();
		}

	override public void LeftClick(GameObject gob)
		{
			if (!gob || Turno.instancia().JugadorActual() != Duenyo)
				return;
			
			if(!GetPuedeCrear())
				return;
			
			Vector3 nuevaPosicion = gob.transform.position;
			nuevaPosicion.y=1;

			GameObject gob2 = Instantiate(FabricaNaval.CrearUnidad(F_Unidades.DESTRUCTOR), nuevaPosicion, Quaternion.identity) as GameObject;

			Unidad unit = gob2.GetComponent<Unidad>();
			unit.transform.parent = this.transform.parent;

			SetPuedeCrear (false);
		}
}
