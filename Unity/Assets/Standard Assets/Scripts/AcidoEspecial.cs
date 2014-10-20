using UnityEngine;
using System.Collections;
using System;

public class AcidoEspecial : MonoBehaviour {
	private SpriteCollection sprites;
	private int reverse = 1;
	private float timer = 0f;
	// private float timer2 = 10f;
	[HideInInspector] public bool saiu = false;

	// Use this for initialization
	void Start () {
		//StartGame.numberOfVitaminUpObjectsAlive++;
		sprites = new SpriteCollection("Acido");
	}


	
	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "AcidoInserido") {
			timer -= Time.deltaTime;
			// timer2 -= Time.deltaTime;
			if (!saiu && timer < 0) {
				try {
					gameObject.GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Acido (frame " + reverse + ")");
				} catch (NullReferenceException) {
					Debug.LogError ("acido sumiu");
				}

				if (reverse == 6) {
					FollowWaypoints wayPoint = gameObject.AddComponent<FollowWaypoints>();
					wayPoint.acido = this;
					wayPoint._targetWaypoint = 17;
					wayPoint.movementSpeed = 40f;
					wayPoint.oldTag = "Acido";

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
		if (point == 2) {
			GameObject.Destroy (gameObject);
		}
	}

	void OnDestroy () {
		//StartGame.numberOfVitaminUpObjectsAlive--;
	}
}
