using UnityEngine;

public class Player : MonoBehaviour
	{
		private int dinero = 1000;
		public Color ColorEquipo;

		void Start()
			{
				Turno.instancia().AddPlayer(this);
			}

		public void Aumentar(int pago)
			{
				if (pago > 0)
					dinero+=pago;
			}

		public bool Pagar(int pagar)
			{
				if (pagar > dinero)
					return false;

				dinero-=pagar;
				return true;
			}
	}