using UnityEngine;
using System.Collections;

public class MouseMoveCamera : MonoBehaviour {
	public float speed = 0.2f;
	public float scrollfactor = 10f;
	public float minimumY = -7.42f;
	public float maximumY = 1.42f;

	// Use this for initialization
	void Start () {
		StartGame.numberOfMouseMoveCameraObjectsAlive++;
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetAxis ("Mouse ScrollWheel") < 0 || (Input.GetMouseButton(0) && Input.mousePosition.y <= (Screen.height * 0.1)))
		{
			float posY = Mathf.Clamp (Camera.main.transform.position.y - (1 * speed), minimumY, maximumY);
			//Debug.Log (posY);
			DestroyTowerMenu.DestroyT();
			if ((StartGame.fase == 0 && posY > -2.42) || (StartGame.fase == 1 && posY > -5.37) || StartGame.fase > 1)
				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, posY, Camera.main.transform.position.z);
		}
		else if(Input.GetAxis ("Mouse ScrollWheel") > 0 || (Input.GetMouseButton(0) && Input.mousePosition.y >= (Screen.height * 0.9)))
		{
			float posY = Mathf.Clamp (Camera.main.transform.position.y + (1 * speed), minimumY, maximumY);
			DestroyTowerMenu.DestroyT();
			Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, posY, Camera.main.transform.position.z);
		}
	}

	void OnDestroy () {
		StartGame.numberOfMouseMoveCameraObjectsAlive--;
	}
}