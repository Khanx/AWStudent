using System.Collections;

public class Turno
	{
		private static Turno ins;
		private ArrayList jugadores = new ArrayList();
		private int turnoAcual = 1;

		private Turno()
			{
				Mapa.Instancia().IniciarTurno();
			}

		public static Turno instancia()
			{
				if(ins == null)
					ins = new Turno();
				
				return ins;
			}

		public int GetTurno()
			{
				return turnoAcual;
			}

		public void AddPlayer(Player jugador)
			{
				jugadores.Add(jugador);
			}
		public void SiguienteTurno()
			{
				turnoAcual++;
				Mapa.Instancia().IniciarTurno();
			}

		public Player JugadorActual()
			{
				return (Player)jugadores[turnoAcual % jugadores.Count];
			}
	}