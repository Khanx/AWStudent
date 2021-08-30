using UnityEngine;

public abstract class Terreno : MonoBehaviour, IColega
	{
		private Material original;

		public abstract bool PuedeAndar(PosiblesTerrenos p);

		void Start ()
			{
				Mapa.Instancia().AddTerreno (this);
				original = GetComponent<Renderer> ().sharedMaterial;
			}

		public void Pintar(Color color)
			{
				if (color == Color.clear)
					{
						DesPintar ();
						return;
					}

				for (int i = 0; i < transform.childCount; i++)
					{
						if (transform.GetChild(i).GetComponent<Renderer>() != null)
							transform.GetChild(i).GetComponent<Renderer>().material.SetColor("_Color", color);
						else
							{
								for (int p = 0; p < transform.childCount; p++)
									transform.GetChild(i).transform.GetChild(p).GetComponent<Renderer>().material.SetColor("_Color", color);
							}
					}

				if (GetComponent<Renderer>() != null)
					GetComponent<Renderer>().material.SetColor("_Color", color);
			}

		public void DesPintar()
			{
				Renderer r = GetComponent<Renderer>();
				r.sharedMaterial = original;
			}
	}