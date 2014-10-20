using UnityEngine;
using System.Collections;

public class ChooseTower : MonoBehaviour {
	void Start () {
		StartGame.numberOfChooseTowerObjectsAlive++;
	}

	void OnMouseDown() {
		InsertTower.towerObject = gameObject;
	}

	void Update () {
		
	}
	void OnDestroy () {
		StartGame.numberOfChooseTowerObjectsAlive--;
	}
}