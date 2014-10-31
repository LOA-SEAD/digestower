using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

	// Use this for initialization

	void OnMouseDown () {
		Debug.Log ("uhdashuduh");
		StartGame.started = false;
		Application.LoadLevel (0); 
	}

	void Start () {
		StartGame.numberOfRestartGameObjectsAlive++;
	}

	void OnDestroy () {
		StartGame.numberOfRestartGameObjectsAlive--;
	}
}
