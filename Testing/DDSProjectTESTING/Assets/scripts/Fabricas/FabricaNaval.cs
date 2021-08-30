using UnityEngine;
using System.Collections;

public class FabricaNaval : Fabrica
	{
		public GameObject Destructor, ArtilleriaA;

		override public GameObject CrearUnidad(F_Unidades i)
			{
				if (i == F_Unidades.DESTRUCTOR & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_DESTRUCTOR))
					return Destructor;
				
				if (i == F_Unidades.ARTILLERIAA & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_ARTILLERIAA))
					return ArtilleriaA;
				
				return Destructor;	//	REFACTORING -> NULLOBJECT
			}
	}
