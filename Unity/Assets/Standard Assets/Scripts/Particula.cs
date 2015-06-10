using UnityEngine;
using System.Collections;

public class Particula : MonoBehaviour {

	private bool fase = false;
	private float myTimerInt;
	void Start () {
		myTimerInt = 0.5f;
	}
	
	void Update () {
		if (gameObject.tag == "ParticulaInserida") {
			if (myTimerInt > 0) {
				myTimerInt -= Time.deltaTime;
			}
			else if (!fase) {
				(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
					(GameObject.FindGameObjectWithTag("Particulas2").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				fase = true;
				myTimerInt = 0.5f;
			}
			else {
				GameObject.Destroy (gameObject);
			}
		}
	}

	/*public void endPoint(int point) {
		if (point == (StartGame.fase+1)) {
			GameObject.Destroy (gameObject);
		}
	}*/
}
