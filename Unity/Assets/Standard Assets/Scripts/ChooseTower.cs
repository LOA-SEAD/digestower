using UnityEngine;
using System.Collections;

public class ChooseTower : MonoBehaviour {
	void Start () {
		//StartGame.numberOfChooseTowerObjectsAlive++;
	}

	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/
	void OnMouseDown() {
		InsertTower.towerObject = gameObject;
		//Antiga referencia a ultima torre usada
		/*GameObject actualTower = GameObject.FindGameObjectWithTag("ActualTower");
		(actualTower.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = (gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;*/
		ButtonAction.DisableMenu (1);
		if (!StartGame.started) StartGame.paused = 1;
		if (StartGame.started) ButtonAction.play ();
	}

	void OnDestroy () {
		//StartGame.numberOfChooseTowerObjectsAlive--;
	}
}