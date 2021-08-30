using UnityEngine;

public class FabricaAerea : Fabrica
	{
		public GameObject Bombardero, Caza;

		override public GameObject CrearUnidad(F_Unidades i)
			{
				if (i == F_Unidades.BOMBARDERO & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_BOMBARDERO))
					return Bombardero;

				if (i == F_Unidades.CAZA & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_CAZA))
					return Caza;

				return Bombardero;	//	REFACTORING -> NULLOBJECT
			}
	}
