using UnityEngine;
using System.Collections;
using System;

public class EnergyBar : MonoBehaviour {
	private float timer = 0.2f;
	private int reverse = 1;

	public static bool piscar = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (piscar) {
			timer -= Time.deltaTime;
			//timer2 -= Time.deltaTime;
			if (timer < 0) {
				try {
					SpriteCollection sprites = new SpriteCollection("Energy");
					GameObject.FindGameObjectWithTag ("EnergyBar").GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Energia" + (reverse % 3));
					sprites = null;
				} catch (NullReferenceException) {
					//Debug.LogError ("vitamina sumiu");
				}
				timer = 0.2f;
				reverse++;
				if (reverse == 11) {
					reverse = 0;
					SpriteCollection sprites = new SpriteCollection("Energy");
					GameObject.FindGameObjectWithTag ("EnergyBar").GetComponent<SpriteRenderer>().sprite = sprites.GetSprite("Energia0");
					sprites = null;
					piscar = false;
				}
			}
		}
	}
}
