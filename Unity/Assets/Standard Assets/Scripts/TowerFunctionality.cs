using UnityEngine;
using System.Collections;

public class TowerFunctionality : MonoBehaviour {

	public bool type = true;
	public AudioClip clip; /* Gracas a isso que e possivel escolher um audio na tela do Unity.
							  Para ele ser tocado, va no local que ele sera ativado e use o seguinte comando:
	                          AudioSource.PlayClipAtPoint(clip, transform.position);*/
	[HideInInspector] public GameObject tower;
	[HideInInspector] public GameObject tower2;
	[HideInInspector] public GameObject place;
	[HideInInspector] public GameObject place2;
	// Use this for initialization
	void Start () {
		tower2 = null;
		place2 = null;
		//StartGame.numberOfTowerFunctionalityObjectsAlive++;
	}

	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/
	void OnMouseDown() {
		if (type) {
			BasicTower bTower = tower.GetComponent("BasicTower") as BasicTower;
			float recoveryEnergy = InsertTower.energyNeeded[bTower.type-1]*(bTower.life/bTower.maxLife);
			if ((StartGame.energy + recoveryEnergy) <= StartGame.maxEnergy){
				StartGame.energy += recoveryEnergy;
				AudioSource.PlayClipAtPoint (clip, transform.position);
			}
			else
				StartGame.energy = StartGame.maxEnergy;
			
			Transform _places = GameObject.Find("TowerPosition").transform;
			for (int i=0;i<_places.childCount;i++) {
				if (_places.GetChild(i) == place.transform)
					StartGame.placeTag[i] = "Untagged";
			}
			
			InsertTower insertPlace = place.GetComponent ("InsertTower") as InsertTower;
			insertPlace.verifyTowerProximity(bTower.type, false);
			Destroy(tower);
			// (target.GetComponent("InsertTower") as InsertTower).towerObjTag = towerObject.tag;
			insertPlace.towerObj = null;
			place.renderer.enabled = true;
			
			/*if (insertPlace.tag.Substring (0, 5) == "Dente") {
				int tag = int.Parse(insertPlace.tag.Substring (5, 1));
				Debug.Log ("ahusdhuadhu " + tag + " - " + "xDente" + (tag+(tag>3?-3:3)));
				GameObject target = GameObject.FindGameObjectWithTag ("xDente" + (tag+(tag>3?-3:3)));

				GameObject targetAux = GameObject.FindGameObjectWithTag ("Dente" + (tag+(tag>3?-3:3)));
				//BasicTower bTower2 = target.GetComponent ("BasicTower") as BasicTower;
				targetAux.renderer.enabled = true;
				(targetAux.GetComponent ("InsertTower") as InsertTower).towerObj = null;
				Destroy(target);
			}*/
			DestroyTowerMenu.DestroyT ();
			// Debug.Log ("jkdsajd");
		}
		else {
			Debug.Log ("suposto upgrade");
		}
	}

	void OnDestroy() {
		//StartGame.numberOfTowerFunctionalityObjectsAlive--;
	}
}
