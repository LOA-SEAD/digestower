using UnityEngine;
using System.Collections;

public class CallSkill : MonoBehaviour {

	public int type = 0;
	// Use this for initialization
	void Start () {
	
	}

	void OnMouseDown() {
		if (type == 0 && StartGame.vitamin > 100) {
			StartGame.vitamin -= 100;
			GameObject item = GameObject.FindGameObjectWithTag("Saliva");
			Vector3 pos = new Vector3 ( -0.858f, 3.39f, 0);
			GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
			inserted.tag = "SalivaInserida";
		}
		if (type == 1 && StartGame.vitamin > 100 && StartGame.fase > 0) {
			StartGame.vitamin -= 100;
			GameObject item = GameObject.FindGameObjectWithTag("Acido");
			Vector3 pos = new Vector3 ( 0.35f, -3.55f, 0);
			GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
			inserted.tag = "AcidoInserido";
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
