using UnityEngine;
using System.Collections;

public class DestroyTowerMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//StartGame.numberOfDestroyTowerMenuObjectsAlive++;
	}

	public static void DestroyT () {
		GameObject[] destruirObjArray = GameObject.FindGameObjectsWithTag ("DestruirInserido");
		//GameObject[] upgradeObjArray = GameObject.FindGameObjectsWithTag ("UpgradeInserido");
		for (int i = 0;i < destruirObjArray.Length;i++)
			Destroy (destruirObjArray[i]);
		//for (int i = 0;i < upgradeObjArray.Length;i++)
		//	Destroy (upgradeObjArray[i]);
	}

	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/
	void onMouseDown() {
		this.DestroyT ();
	}

	void OnDestroy() {
		//StartGame.numberOfDestroyTowerMenuObjectsAlive--;
	}
}
