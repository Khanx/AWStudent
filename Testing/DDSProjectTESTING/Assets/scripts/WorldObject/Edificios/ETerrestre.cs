using UnityEngine;

public class ETerrestre : Edificio
{
	private FabricaTerrestre FabricaTerrestre;

	public void Start ()
		{
			base.Start ();
			GameObject FabricaTerrestreG = GameObject.Find("FabricaTierra");

			FabricaTerrestre = FabricaTerrestreG.GetComponent<FabricaTerrestre>();
		}

	override public void LeftClick(GameObject gob)
		{
			if (!gob || Turno.instancia().JugadorActual() != Duenyo)
				return;
			
			if(!GetPuedeCrear())
				return;
			
			Vector3 nuevaPosicion = gob.transform.position;
			nuevaPosicion.y=1;

			GameObject newGO = Instantiate(FabricaTerrestre.CrearUnidad(F_Unidades.INFANTERIA_LIGERA), nuevaPosicion, Quaternion.identity) as GameObject;
			Unidad unit = newGO.GetComponent<Unidad>();
			unit.transform.parent = this.transform.parent;

			SetPuedeCrear (false);
		}
}

