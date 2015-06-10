using UnityEngine;
using System.Collections;
using System;

public class SalivaEspecial : MonoBehaviour {
	// private SpriteCollection sprites;
	private int reverse = 1;
	private float timer = 0f;
	public int nFood = 0;
	// private float timer2 = 10f;
	[HideInInspector] public bool saiu = false;
	
	// Use this for initialization
	void Start () {
		//StartGame.numberOfSalivaEspecialObjectsAlive++;
		//sprites = new SpriteCollection("Saliva");
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if (StartGame.paused == 0 && gameObject.tag == "SalivaInserida") {
			timer -= Time.deltaTime;
			// timer2 -= Time.deltaTime;
			if (!saiu && timer < 0) {
				try {
					SpriteCollection sprites = new SpriteCollection("Saliva");
					gameObject.GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Saliva" + reverse);
					sprites = null;
				} catch (NullReferenceException) {
					Debug.LogError ("saliva sumiu");
				}
				
				if (reverse == 6) {
					FollowWaypoints wayPoint = gameObject.AddComponent<FollowWaypoints>();
					wayPoint.saliva = this;
					wayPoint._targetWaypoint = 3;
					wayPoint.movementSpeed = 40f;
					wayPoint.oldTag = "Saliva";
					
					saiu = true;
					audio.Play ();
					nFood = 0;
					CallSkill.creatingSaliva = false;
				}
				else {
					reverse++;
					timer = 0.5f;
				}
			}
			//if (timer2 < 0) {
			//	GameObject.Destroy (gameObject);
			//}
		}
	}
	
	public void endPoint(int point) {
		if (point == 1) {
			if (nFood > 3) {
				if (StartGame.indigest < 501)
					StartGame.indigest = 0;
				else
					StartGame.indigest -= 500;
			}
			(GameObject.FindGameObjectWithTag("SalivaText").GetComponent ("GUIText") as GUIText).text = "";
			GameObject.Destroy (gameObject);
		}
	}
	
	void OnDestroy () {
		// sprites = null;
		//StartGame.numberOfSalivaEspecialObjectsAlive--;
	}
}
