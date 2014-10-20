using UnityEngine;
using System.Collections;
using System;

public class SalivaEspecial : MonoBehaviour {
	private SpriteCollection sprites;
	private int reverse = 1;
	private float timer = 0f;
	// private float timer2 = 10f;
	[HideInInspector] public bool saiu = false;
	
	// Use this for initialization
	void Start () {
		//StartGame.numberOfVitaminUpObjectsAlive++;
		sprites = new SpriteCollection("Saliva");
	}
	
	
	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "SalivaInserida") {
			timer -= Time.deltaTime;
			// timer2 -= Time.deltaTime;
			if (!saiu && timer < 0) {
				try {
					gameObject.GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Saliva (frame " + reverse + ")");
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
			GameObject.Destroy (gameObject);
		}
	}
	
	void OnDestroy () {
		//StartGame.numberOfVitaminUpObjectsAlive--;
	}
}
