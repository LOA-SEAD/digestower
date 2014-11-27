using UnityEngine;
using System.Collections;

public class RestartGame : MonoBehaviour {

	// Use this for initialization

	void OnMouseDown () {
	}

	void Start () {
		StartGame.numberOfRestartGameObjectsAlive++;
	}

	void OnDestroy () {
		StartGame.numberOfRestartGameObjectsAlive--;
	}
}
