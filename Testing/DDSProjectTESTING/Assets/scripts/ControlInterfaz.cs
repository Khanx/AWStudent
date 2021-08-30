using UnityEngine;

/*
 * Esta clase sera la encargada de crear todas las interfaces graficas del juego*/

public class ControlInterfaz : MonoBehaviour {
	private Unidad unidadSeleccionada;
	private static bool esPausa;
	private static bool puedeMostrarMenuUnidad;
	private static bool puedeMostrarMenuFabrica;
	private static bool objetoMoviendose;
	
	//Variables interfaz
	public static float w = Screen.width;
	public static float h = Screen.height/3;
	public static float t = 0;
	public static float l = 0;
	public GUIStyle estiloTexto;
	
	// Use this for initialization
	void Start () {
		esPausa = false;
		puedeMostrarMenuUnidad = false;
		puedeMostrarMenuFabrica = false;
		objetoMoviendose = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape)) esPausa = !esPausa;
		if(unidadSeleccionada){
			if(unidadSeleccionada.EstaMoviendose()){
				objetoMoviendose = true;
			}
			else objetoMoviendose = false;
		}
		else objetoMoviendose = false;
	}

	//Metodo principal que manejara la interfaz grafica
	void OnGUI ()
	{	
		if(esPausa && !objetoMoviendose) MenuSiguienteTurno();
		if(!objetoMoviendose && puedeMostrarMenuFabrica) MenuFabrica(); 
		if(!GetWorldObject()) return;
		unidadSeleccionada =  GetUnidad();
		if(!unidadSeleccionada) return;
		MostrarDatosUnidad();
		if(puedeMostrarMenuUnidad && !objetoMoviendose/*!unidadSeleccionada.EstaMoviendose()*/) MenuUnidad();
	}

	public static void MenuUnidadOn(){
		puedeMostrarMenuUnidad = true;
	}

	public static void MenuUnidadOff(){
		puedeMostrarMenuUnidad = false;
	}

	public static void MenuFabricaOn(){
		puedeMostrarMenuFabrica = true;
	}

	public static void MenuFabricOff(){
		puedeMostrarMenuFabrica = false;
	}
	
	//Este metodo nos evitara hacer referencias a null
	WorldObject GetWorldObject(){
		return UserInput.instance.selectObject/*.GetComponent<WorldObject>()*/;
	}

	//Este metodo se asegura de que seleccionamos una unidad y no un
	Unidad GetUnidad(){
		return UserInput.instance.selectObject.GetComponent<Unidad>();
	}
	
	public void MenuSiguienteTurno(){
		GUI.Box (new Rect(Screen.width-120,20,120,80),"Menu");
		if(GUI.Button(new Rect(Screen.width-110,45,100,20), "Finalizar Turno") && !GetWorldObject())
		{
			Turno.instancia().SiguienteTurno();
			esPausa = !esPausa;
		}
		if(GUI.Button (new Rect(Screen.width-110, 70, 100, 20), "Salir del juego"))
		{
			Application.Quit();
		}

	}

	public void MenuUnidad(){
		GUI.Box(new Rect(10,10,100,120), "Accion");
		if(GUI.Button(new Rect(20,40,80,20), "Esperar")) {
			unidadSeleccionada.GetEstado().SetAction(Acciones.ESPERAR);
			MenuUnidadOff();
		}
		if(Mapa.Instancia().UnidadesEnemigasEnRango(unidadSeleccionada.transform.position, unidadSeleccionada.GetRangoDeAtaque()) && GUI.Button(new Rect(20,70,80,20), "Atacar")) {
			unidadSeleccionada.GetEstado().SetAction(Acciones.ATACAR);
			MenuUnidadOff();
		}
	}

	public void MenuFabrica(){
		GUI.Box (new Rect(10,10,100,120), "Fabrica");
		if(GUI.Button(new Rect(20,40,80,20), "Infanteria")) {
			MenuFabricOff();
		}
	}

	public void MostrarDatosUnidad(){
		ModificaEstilo();
		GUI.Box(new Rect(l,t,w,h),DatosUnidad(),estiloTexto);
	}
	
	public void ModificaEstilo(){
		estiloTexto.padding.top = Screen.height/3;
		estiloTexto.fontSize = 15;
	}

	public string DatosUnidad(){
		string texto = "";
		texto += unidadSeleccionada.name+"\n"+
			"VIDA: "+unidadSeleccionada.GetVida()+
			"\nARMADURA: "+unidadSeleccionada.GetArmadura()+
				"\nDAÑO: "+unidadSeleccionada.GetDanyo();
		return texto;
	}
}
