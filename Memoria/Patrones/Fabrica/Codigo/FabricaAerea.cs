using UnityEngine;
using System.Collections;

public class FabricaAerea : Fabrica
	{
		private GameObject bombardero, caza;

		void Start ()
			{
				bombardero = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Unidades/Aire/Bombardero.prefab", typeof(GameObject)) as GameObject;
				caza = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Unidades/Aire/Caza.prefab", typeof(GameObject)) as GameObject;
			}

		override public GameObject CrearUnidad(F_Unidades i)
			{
				if (i == F_Unidades.BOMBARDERO & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_BOMBARDERO))
					return bombardero;

				if (i == F_Unidades.CAZA & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_CAZA))
					return caza;

				return bombardero;	//	REFACTORING -> NULLOBJECT
			}
	}
