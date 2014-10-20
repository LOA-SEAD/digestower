using UnityEngine;
using System.Collections;
using System;

public class FatPlace : MonoBehaviour {
	public float minimalFat = 100f;
	public float affectMovementConstant = 2f;
	public int fatPos = 0;
	
	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false;
		GameObject.FindGameObjectWithTag ((new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPos]).renderer.enabled = false;
		StartGame.numberOfFatPlaceObjectsAlive++;
	}

	void OnTriggerEnter2D(Collider2D col){
		// Update is called once per frame
		if (GameObject.FindGameObjectWithTag((new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPos]).renderer.enabled && col.gameObject.tag == "ComidaInserida") {
			FoodProperties foodProp = col.gameObject.GetComponent<FoodProperties>();
			try {
				foodProp.timerFatAffectMovement(affectMovementConstant);
			} catch (System.NullReferenceException) {
			}
		}
	}

	void OnDestroy () {
		StartGame.numberOfFatPlaceObjectsAlive--;
	}

	void Update () {
	
	}
}
