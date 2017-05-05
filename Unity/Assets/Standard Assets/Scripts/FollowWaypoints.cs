using UnityEngine;
using System.Collections;

public class FollowWaypoints : MonoBehaviour
{
	public string oldTag = null;

	public int _targetWaypoint = 0;
	public float movementSpeed = 10.0f;
	public string point = null;

	public FoodProperties food = null;
	public VitaminUp vita = null;
	public SalivaEspecial saliva = null;
	public AcidoEspecial acido = null;
	//public Particula part = null;
	//private BaseLevelScript _levelScript;
	private Transform _waypoints;

	// Use this for initialization
	void Start ()
	{
		//_levelScript = GameObject.Find("LevelScript").GetComponent<BaseLevelScript>();
		_waypoints = GameObject.Find("Waypoints").transform;
		//StartGame.numberOfFollowWaypointsObjectsAlive++;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	// Fixed update
	void FixedUpdate()
	{
		if (StartGame.paused == 0)
			handleWalkWaypoints();
	}
	
	// Handle walking the waypoints
	private void handleWalkWaypoints()
	{
		Transform targetWaypoint = _waypoints.GetChild(_targetWaypoint);
		Vector3 relative = targetWaypoint.position - transform.position;

		Vector3 movementNormal = Vector3.Normalize(relative);
		float distanceToWaypoint = relative.magnitude;

		if (distanceToWaypoint < 0.1)
		{
			if (_targetWaypoint + 1 < _waypoints.childCount)
			{
				point = targetWaypoint.tag;
				if (point.Substring(0, 8) == "EndPoint") {

					if (vita) {
						vita.endPoint(int.Parse (point.Substring(8, 1)));
					}
					else if (food) {
						int tag = int.Parse (point.Substring(8, 1));
						// Debug.Log (oldTag);
						food.endPoint(tag);
						if (tag < 3) food.tag = "ComidaInserida" + (tag+1);
					}
					else if (saliva) {
						saliva.endPoint(int.Parse (point.Substring(8, 1)));
					}
					else if (acido) {
						acido.endPoint(int.Parse (point.Substring(8, 1)));
					}
					//else if (part) {
					//	part.endPoint (int.Parse (point.Substring(8, 1)));
					//}
				}
				// Set new waypoint as target
				_targetWaypoint++;
			}
			else
			{
				// Inform level script that a unit has reached the last waypoint
				//_levelScript.reduceHearts(1);
				Destroy(gameObject);
				return;
			}
		}
		else
		{
			if (StartGame.acelerarAtivado == 1){
				movementSpeed = 20;
			}
			else{
				movementSpeed = 10;
			}
			// Walk towards waypoint
			GetComponent<Rigidbody2D>().AddForce(new Vector2(movementNormal.x, movementNormal.y) * movementSpeed);
		}

		float targetAngle;
		// Face walk direction
		if ((food != null && !food.girar) || vita != null)
			targetAngle = 0;
			/*oldTag == "Vitamina" ||
		    oldTag == "Amendoim" ||
		    oldTag == "Mel" ||
		    oldTag == "Cereal" ||
		    oldTag == "Chips" ||
		    oldTag == "Coco" ||
		    oldTag == "Leite" ||
		    oldTag == "Lentilha" ||
		    oldTag == "Maionese" ||
		    oldTag == "Margarina" ||
		    oldTag == "Queijo" ||
		    oldTag == "Arroz" ||
		    oldTag == "Soja")*/

		else
			targetAngle = Mathf.Atan2(relative.y, relative.x) * Mathf.Rad2Deg - 270;

		transform.rotation = Quaternion.Euler(0, 0, targetAngle);
	}

	void OnDestroy () {
		//StartGame.numberOfFollowWaypointsObjectsAlive--;
	}
}