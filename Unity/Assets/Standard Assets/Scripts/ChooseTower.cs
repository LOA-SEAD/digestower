using UnityEngine;
using System.Collections;

public class ChooseTower : MonoBehaviour {
	void Start () {
		StartGame.numberOfChooseTowerObjectsAlive++;
	}

	void OnMouseDown() {
		InsertTower.towerObject = gameObject;
		MenuControl.DisableMenu (1);
	}

	void OnDestroy () {
		StartGame.numberOfChooseTowerObjectsAlive--;
	}
}