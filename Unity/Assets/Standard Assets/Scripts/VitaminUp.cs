using UnityEngine;
using System.Collections;
using System;

public class VitaminUp : MonoBehaviour {
	// private SpriteCollection sprites;
	private int reverse = 1;
	private float timer = 0.5f;

	public float vitaminUp = 20f;
	//private float timer2 = 10f;
	// Use this for initialization
	void Start () {
		StartGame.numberOfVitaminUpObjectsAlive++;
	}

	void OnMouseDown() {
		StartGame.vitamin += vitaminUp * 50;
		GameObject.Destroy (gameObject);
		// Debug.Log ("destruiu");
	}

	public void endPoint(int point) {
		if (point == StartGame.fase+1) {
			GameObject.Destroy (gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		if (gameObject.tag == "VitaminaInserida") {
			timer -= Time.deltaTime;
			//timer2 -= Time.deltaTime;
			if (timer < 0) {
				try {
					SpriteCollection sprites = new SpriteCollection("Vitamin");
					gameObject.GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Vitamina " + (vitaminUp<2?"peq":"gra") + " (frame " + reverse + ")");
					sprites = null;
				} catch (NullReferenceException) {
					Debug.LogError ("vitamina sumiu");
				}
				timer = 0.3f;
				reverse++;
				if (reverse == 4) reverse = 1;
			}
			//if (timer2 < 0) {
			//	GameObject.Destroy (gameObject);
			//}
		}
	}

	void OnDestroy () {
		StartGame.numberOfVitaminUpObjectsAlive--;
	}
}
