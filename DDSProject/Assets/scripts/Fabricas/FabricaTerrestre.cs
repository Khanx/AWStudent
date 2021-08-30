using UnityEngine;
using System.Collections;

public class FabricaTerrestre : Fabrica
	{
		public GameObject InfanteriaLigera, InfanteriaPesada, Tanque, ArtilleriaT;

		override public GameObject CrearUnidad(F_Unidades i)
			{
				if (i == F_Unidades.INFANTERIA_LIGERA & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_INFANTERIA_LIGERA))
					return InfanteriaLigera;
		
				if (i == F_Unidades.INFANTERIA_PESADA & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_INFANTERIA_PESADA))
					return InfanteriaPesada;

				//if (i == INFANTERIA_MECANICO)
					//return infanteria_mec;
				if (i == F_Unidades.TANQUE & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_TANQUE))
					return Tanque;

				if (i == F_Unidades.ARTILLERIAT & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_ARTILLERIAT))
					return ArtilleriaT;

				return InfanteriaLigera;	//	REFACTORING -> NULLOBJECT
			}
	}