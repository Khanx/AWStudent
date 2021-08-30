using UnityEngine;

public class UserInput : MonoBehaviour
	{
		public WorldObject selectObject { get; set; }
		public static UserInput instance { get; private set; }

		public const int maxX=20, minX=-5, maxY=15, minY=1, maxZ=20, minZ=1;

		void Awake()
			{
				instance = this;
			}


		// Update is called once per frame
		void Update ()
			{
				MoveCameraWithMouse ();
				MoveCameraWithButtons ();
				//RotateCamera ();		DESHABILITADO: POSIBLE IMPLEMENTACION EN UN FUTURO
				LeftClick ();
				RightClick ();
			}
		
		private Vector3 NormalizarDestinoDeCamara(Vector3 destino)
			{
				if(destino.x > maxX)
					destino.x = maxX;
				else if (destino.x < minX)
					destino.x = minX;
				
				if(destino.y > maxY)
					destino.y = maxY;
				else if (destino.y < minY)
					destino.y = minY;

				if(destino.z > maxZ)
					destino.z = maxZ;
				else if (destino.z < minZ)
					destino.z = minZ;
				
				return destino;
			}

		private void MoveCameraWithButtons()
			{
				Vector3 origin = Camera.main.transform.position;
				Vector3 destination = origin;

				destination.y += Input.GetAxis("Mouse ScrollWheel");
				destination.x -= Input.GetAxis ("Horizontal");
				destination.z -= Input.GetAxis ("Vertical");
				
				destination = NormalizarDestinoDeCamara(destination);

				if(origin != destination)
					Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * 25);
			}

		private void MoveCameraWithMouse()
			{
				if (Input.GetMouseButton(2))
					return;

				float xpos = Input.mousePosition.x;
				float ypos = Input.mousePosition.y;
				Vector3 origin = Camera.main.transform.position;
				Vector3 destination = origin;

				destination.y += Input.GetAxis("Mouse ScrollWheel");

				if(destination.y < 1)
					destination.y = 1;
				
				if(destination.y > 10)
					destination.y = 10;

				if(xpos >= 0 && xpos < 15)
					destination.x += 25;
				else if(xpos <= Screen.width && xpos > Screen.width - 15)
					destination.x -= 25;

				//vertical camera movement
				if(ypos >= 0 && ypos < 15)
					destination.z += 25;
				else if(ypos <= Screen.height && ypos > Screen.height - 15)
					destination.z -= 25;

				destination = NormalizarDestinoDeCamara(destination);

				if(origin != destination)
					Camera.main.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * 25);
			}

		public static GameObject FindHitMouse()
			{
				RaycastHit hitInfo = new RaycastHit();
				bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);

				if (hit)
					return hitInfo.transform.gameObject;

				return null;
			}

		private void LeftClick ()
			{
				if (Input.GetMouseButtonDown (0))
					{
						GameObject hitObject = FindHitMouse();
						if(selectObject)
							{
								if(hitObject)
									selectObject.LeftClick(hitObject);
							}
						else
							{
								if(hitObject && hitObject.tag != "Suelo")
									{
										WorldObject worldObject = hitObject.transform.GetComponent<WorldObject>();

										if(!worldObject)
											return;

										Debug.Log("Se ha seleccionado: "+worldObject.name);

										if(worldObject)
											{
												selectObject = worldObject;
												worldObject.Seleccionar();
											}
									}
							}
					}
			}

		private void RightClick ()
			{
				if (Input.GetMouseButtonDown (1))
					{
						if(selectObject)
							selectObject.RightClick();
					}
			}
	}