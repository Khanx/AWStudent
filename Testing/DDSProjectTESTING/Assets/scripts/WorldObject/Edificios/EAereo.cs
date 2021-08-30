using UnityEngine;

public class EAereo : Edificio
{
	private FabricaAerea FabricaAerea;

	public void Start ()
	{
		base.Start ();
		GameObject FabricaAereaG = GameObject.Find("FabricaAire");
		
		FabricaAerea = FabricaAereaG.GetComponent<FabricaAerea>();
	}

	override public void LeftClick(GameObject gob)
	{
		if (!gob || Turno.instancia().JugadorActual() != Duenyo)
			return;

		if(!GetPuedeCrear())
			return;
		
		Vector3 nuevaPosicion = gob.transform.position;
		nuevaPosicion.y=1;

		GameObject gob2 = Instantiate(FabricaAerea.CrearUnidad(F_Unidades.CAZA), nuevaPosicion, Quaternion.identity) as GameObject;

		Unidad unit = gob2.GetComponent<Unidad>();
		unit.transform.parent = this.transform.parent;

		SetPuedeCrear (false);
	}
}
