using UnityEngine;
using System.Collections;

public class FabricaTerrestre : Fabrica
	{
		private GameObject infanteria_ligera, infanteria_pesada, tanque, artilleriat;

		void Start ()
			{
				infanteria_ligera = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Unidades/Terrestre/Infanteria/Infanteria_Ligera.prefab", typeof(GameObject)) as GameObject;
				infanteria_pesada = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Unidades/Terrestre/Infanteria/Infanteria_Pesada.prefab", typeof(GameObject)) as GameObject;

				tanque = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Unidades/Terrestre/Vehiculo/Tanque.prefab", typeof(GameObject)) as GameObject;
				artilleriat = UnityEditor.AssetDatabase.LoadAssetAtPath("Assets/prefabs/Unidades/Terrestre/Vehiculo/Tanque.prefab", typeof(GameObject)) as GameObject;
			}

		override public GameObject CrearUnidad(F_Unidades i)
			{
				if (i == F_Unidades.INFANTERIA_LIGERA & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_INFANTERIA_LIGERA))
					return infanteria_ligera;
		
				if (i == F_Unidades.INFANTERIA_PESADA & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_INFANTERIA_PESADA))
					return infanteria_pesada;

				//if (i == INFANTERIA_MECANICO)
					//return infanteria_mec;
				if (i == F_Unidades.TANQUE & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_TANQUE))
					return tanque;

				if (i == F_Unidades.ARTILLERIAT & Turno.instancia().JugadorActual().Pagar((int)costes.COSTE_ARTILLERIAT))
					return artilleriat;

				return infanteria_ligera;	//	REFACTORING -> NULLOBJECT
			}
	}