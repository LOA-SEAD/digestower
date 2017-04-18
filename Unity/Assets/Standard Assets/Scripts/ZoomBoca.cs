using UnityEngine;
using System.Collections;
using System;

public class ZoomBoca : MonoBehaviour {
	private float timer = 0.05f;

	public static bool zoom = false;
	public static bool reset = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (zoom) {
			timer -= Time.deltaTime;
			if (timer < 0) {
				try {
					GameObject.FindGameObjectWithTag ("InfoTela").GetComponent<Transform>().localScale += new Vector3(0.05f, 0.05f, 0);
					//Debug.Log("Escala atual: " + GameObject.FindGameObjectWithTag ("InfoTela").GetComponent<Transform>().lossyScale.x);
				} catch (NullReferenceException) {
					Debug.LogError ("nao eh assim que faz escala");
				}
				timer = 0.05f;
			}
			if (GameObject.FindGameObjectWithTag ("InfoTela").GetComponent<Transform>().lossyScale.x > 4.0f){
				zoom = false;
			}
		}
		if (reset){
			GameObject.FindGameObjectWithTag ("InfoTela").GetComponent<Transform>().localScale = new Vector3(1.32f, 1.32f, 1);
			reset = false;
		}
	}
}
