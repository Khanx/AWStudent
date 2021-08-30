using UnityEngine;
using System.Collections;

public class FabricaNaval : Fabrica
	{
		private GameObject destructor, artilleriaA;

		void Start ()
			{
				destructor = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Unidades/Naval/Destructor.prefab", typeof(GameObject)) as GameObject;
				artilleriaA = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Unidades/Naval/ArtilleriaA.prefab", typeof(GameObject)) as GameObject;
			}

		override public GameObject CrearUnidad(F_Unidades i)
			{
				if (i == F_Unidades.DESTRUCTOR & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_DESTRUCTOR))
					return destructor;
				
				if (i == F_Unidades.ARTILLERIAA & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_ARTILLERIAA))
					return artilleriaA;
				
				return destructor;	//	REFACTORING -> NULLOBJECT
			}
	}
