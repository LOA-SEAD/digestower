using UnityEngine;
using System.Collections;

public class MouseMoveCamera : MonoBehaviour {
	public float speed = 0.2f;
	public float scrollfactor = 10f;
	public float minimumY = -7.42f;
	public float maximumY = 1.42f;
	//public int bottomMargin = 80; // if you have some icons at the bottom (like an RPG game) this will help preventing the drag action at the bottom
	private Vector3 dragOrigin;
	private float dragSpeed = -.5f;

	// Use this for initialization
	void Start () {
		//StartGame.numberOfMouseMoveCameraObjectsAlive++;
	}

	// Update is called once per frame
	void Update () {
		if (StartGame.paused != 2) {
			if(Input.GetAxis ("Mouse ScrollWheel") < 0/* || (Input.GetMouseButton(0) && Input.mousePosition.y <= (Screen.height * 0.1))*/)
			{
				float posY = Mathf.Clamp (Camera.main.transform.position.y - (1 * speed), minimumY, maximumY);
				//Debug.Log (posY);
				DestroyTowerMenu.DestroyT();
				if ((StartGame.fase == 0 && posY > -2.42) || (StartGame.fase == 1 && posY > -5.37) || StartGame.fase > 1)
					Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, posY, Camera.main.transform.position.z);
			}
			else if(Input.GetAxis ("Mouse ScrollWheel") > 0/* || (Input.GetMouseButton(0) && Input.mousePosition.y >= (Screen.height * 0.9))*/)
			{
				float posY = Mathf.Clamp (Camera.main.transform.position.y + (1 * speed), minimumY, maximumY);
				DestroyTowerMenu.DestroyT();
				Camera.main.transform.position = new Vector3(Camera.main.transform.position.x, posY, Camera.main.transform.position.z);
			}
			else
				moveCamera ();
		}
	}

	void moveCamera()
	{
		if (Input.GetMouseButtonDown(0))
		{    
			dragOrigin = Input.mousePosition;
			return;
		}
		
		if (!Input.GetMouseButton(0)) return;

		//if(dragOrigin.y <= bottomMargin) return;
		
		Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - dragOrigin);
		Vector3 move = new Vector3(0, pos.y * dragSpeed, 0);
		//Debug.Log (move.x + "..." + move.y + "..." + move.z);
		
		if(move.y > 0)
		{
			if(!isWithinTopBorder())
				move.y=0;
		}
		else
		{
			if(!isWithinBottomBorder())
				move.y=0;
		}
		
		transform.Translate(move, Space.World);
	}

	bool isWithinTopBorder()
	{
		Vector3 currentTopLeftGlobal = Camera.main.ScreenToWorldPoint(new Vector3(0,Screen.height,0));
		if(currentTopLeftGlobal.y < maximumY+3f)
			return true;
		else
			return false;
	}
	
	bool isWithinBottomBorder()
	{
		Vector3 currentBottomRightGlobal = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,0,0));
		if(currentBottomRightGlobal.y > minimumY+(StartGame.fase == 0?6:(StartGame.fase == 1?3.1:-3.5f)))
			return true;
		else
			return false;
	}

	void OnDestroy () {
		//StartGame.numberOfMouseMoveCameraObjectsAlive--;
	}
}