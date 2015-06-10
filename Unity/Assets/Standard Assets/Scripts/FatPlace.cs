using UnityEngine;
using System.Collections;
using System;

public class FatPlace : MonoBehaviour {

	/* ALTERACAO
	 * a alteracao eh feita no painel de objetos, entrando em FatPosition (Hierarchy) e depois onde esta
	 * escrito Places
	 */
	/* desconsiderar */
	public float minimalFat = 50f;
	public float affectMovementConstant = 10f;
	/**/
	public int fatPos = 0;
	
	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false;
		GameObject.FindGameObjectWithTag ((new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPos]).renderer.enabled = false;
		//StartGame.numberOfFatPlaceObjectsAlive++;
	}

	void OnTriggerEnter2D(Collider2D col){
		//Debug.Log ("entrou: " + col.tag);
		if (GameObject.FindGameObjectWithTag((new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPos]).renderer.enabled && col.gameObject.tag.Length > 13 && col.gameObject.tag.Substring(0,14) == "ComidaInserida") {
			FoodProperties foodProp = col.gameObject.GetComponent<FoodProperties>();
			try {
				foodProp.timerFatAffectMovement(affectMovementConstant);
			} catch (System.NullReferenceException) {
			}
		}
	}

	void OnDestroy () {
		//StartGame.numberOfFatPlaceObjectsAlive--;
	}
}
