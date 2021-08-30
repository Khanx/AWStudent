using UnityEngine;

public abstract class WorldObject : MonoBehaviour
	{
		protected Player Duenyo;

		public void Start ()
			{
				Duenyo = this.transform.root.GetComponent<Player> ();
				Pintar();
			}

		public Player GetDuenyo()				{	return Duenyo;					}
		public void SetDuenyo(Player Duenyo)	{	this.Duenyo = Duenyo;			}

		public abstract void LeftClick(GameObject gob);
		public abstract void RightClick();
		public abstract void Seleccionar ();
		public abstract void IniciarTurno ();

	//CODIGO DUPLICADO -> SOLUCIONAR
	private void Pintar()
		{
			Color color = GetDuenyo().ColorEquipo;
			
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

	}