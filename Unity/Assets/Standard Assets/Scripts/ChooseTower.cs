using UnityEngine;
using System.Collections;

public class ChooseTower : MonoBehaviour {
	void Start () {
		StartGame.numberOfChooseTowerObjectsAlive++;
	}

	void OnMouseDown() {
		InsertTower.towerObject = gameObject;
		GameObject actualTower = GameObject.FindGameObjectWithTag("ActualTower");
		(actualTower.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = (gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		ButtonAction.DisableMenu (1);
		ButtonAction.play ();
	}

	void OnDestroy () {
		StartGame.numberOfChooseTowerObjectsAlive--;
	}
}