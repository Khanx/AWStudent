using UnityEngine;
using System.Collections.Generic;

public class Unidad : WorldObject, IColega
	{
		protected int DistanciaDeMovimiento;
		protected int Vida, Armadura, PenetracionDeArmadura, Danyo, RangoDeAtaque;
		protected PosiblesTerrenos AndaPor;
		protected EstadoUnidad Estado;

		private Queue<Vector3> cola;
		private bool rotar, mover;
		private Vector3 objetivoMovimiento;
		private Quaternion objetivoRotacion;

		private bool sel;

		public int GetVida()						{	return Vida;					}
		public int GetArmadura()					{	return Armadura;				}
		public int GetPenetracionDeArmadura()		{	return PenetracionDeArmadura;	}
		public int GetDanyo()						{	return Danyo;					}
		public int GetRangoDeAtaque()				{	return RangoDeAtaque;			}

		public int GetDistanciaDeMovimiento()		{	return DistanciaDeMovimiento;	}
		public PosiblesTerrenos GetAndaPor() 		{	return AndaPor;					}

		override public void IniciarTurno()					{	Estado = new Inicio (this); 	}

		override public void Seleccionar()
			{
				sel=true;
				if(mover || rotar)
					return;

				sel=false;
				Estado.Start ();
			}

		public void SetEstado(EstadoUnidad estado)	{	this.Estado = estado; 		}

		public void Start ()
			{
				base.Start ();
				Mapa.Instancia().AddUnidad (this);
				Vida = 100;
				RangoDeAtaque = 1;

				Estado = new Fin(this);
				//Sistema de Movimiento
				rotar=false;
				mover=false;
				//Sistema de estados
				sel=false;
			}

		public void Update()
			{
				if(rotar)
					RotarA();
				else if(mover)
					MoverA();
				else if (sel)
					Seleccionar();
			}

		private void RotarA()
			{
				transform.rotation = Quaternion.RotateTowards(transform.rotation, objetivoRotacion, Time.deltaTime*50);

				Quaternion inverseTargetRotation = new Quaternion(-objetivoRotacion.x, -objetivoRotacion.y, -objetivoRotacion.z, -objetivoRotacion.w);

				if(transform.rotation == objetivoRotacion || transform.rotation == inverseTargetRotation)
					{
						rotar = false;
						mover = true;
					}
			}

		private void MoverA()
			{
				transform.position = Vector3.MoveTowards(transform.position, objetivoMovimiento, Time.deltaTime * 10);

				if(transform.position == objetivoMovimiento)
					{
						mover = false;
						if(cola.Count!=0)
							MoverA(cola);
					}
			}

		public void MoverA(Queue<Vector3> cola)
			{
				this.cola = cola;
				this.objetivoMovimiento = cola.Dequeue();
				this.objetivoRotacion = Quaternion.LookRotation (objetivoMovimiento - transform.position);

				rotar=true;
			}

		override public void LeftClick(GameObject gob)
			{
				if (!gob)
					return;

				if(!mover && !rotar)
					Estado.LeftClick (gob);
			}

		override public void RightClick()
			{
				if(!mover && !rotar)
					Estado.RightClick ();
			}

		public void QuitarVida(int PenetracionDeArmadura, int Danyo)
			{
				//Porcentaje de modificacion de danyo, depende de la armadura y de la penetracion
				double ModificadorDanyo = 1 - (this.Armadura - PenetracionDeArmadura)/100;

					if (ModificadorDanyo < 0)
						ModificadorDanyo = 1;

				this.Vida -= (int)(Danyo*ModificadorDanyo);
				Debug.Log(this.name+" Tiene ahora "+Vida+" de vida");
				if (this.Vida <= 0)
					{
						Debug.Log(this.name + " ha muerto.");
						Destroy (this.gameObject);
						Mapa.Instancia().DelUnidad(this.transform.position);
					}
			}
		//Metodos nuevos para acceder a atributos de unidad desde el controlador grafico
		public EstadoUnidad GetEstado(){
				return Estado;		
			}

		public bool EstaMoviendose(){
			return mover || rotar;
		}
	}