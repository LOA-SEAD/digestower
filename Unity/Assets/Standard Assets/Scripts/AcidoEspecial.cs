using UnityEngine;
using System.Collections;
using System;

public class AcidoEspecial : MonoBehaviour {
	// private SpriteCollection sprites;
	private int reverse = 1;
	private float timer = 0f;
	// private float timer2 = 10f;
	[HideInInspector] public bool saiu = false;

	// Use this for initialization
	void Start () {
		StartGame.numberOfAcidoEspecialObjectsAlive++;
	}
	
	// Update is called once per frame
	void Update () {
		if (StartGame.paused == 0 && gameObject.tag == "AcidoInserido") {
			timer -= Time.deltaTime;
			// timer2 -= Time.deltaTime;
			if (!saiu && timer < 0) {
				try {
					SpriteCollection sprites = new SpriteCollection("Acido");
					gameObject.GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Acido (frame " + reverse + ")");
					sprites = null;
				} catch (NullReferenceException) {
					Debug.LogError ("acido sumiu");
				}

				if (reverse == 6) {
					FollowWaypoints wayPoint = gameObject.AddComponent<FollowWaypoints>();
					wayPoint.acido = this;
					wayPoint._targetWaypoint = 21;
					wayPoint.movementSpeed = 40f;
					wayPoint.oldTag = "Acido";

					saiu = true;
					CallSkill.creatingAcido = false;
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
		StartGame.numberOfAcidoEspecialObjectsAlive--;
	}
}
