using UnityEngine;

public abstract class Fabrica : MonoBehaviour
	{
		public abstract GameObject CrearUnidad(F_Unidades i);
	}