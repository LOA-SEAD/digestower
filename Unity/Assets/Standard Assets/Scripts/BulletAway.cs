using UnityEngine;
using System.Collections;

public class BulletAway : MonoBehaviour {
	public GameObject towerShooter = null;

	public bool bulletAway = false;
	private float myTimerInt;

	void Start () {
		//StartGame.numberOfBulletAwayObjectsAlive++;
		myTimerInt = 1f;
	}

	void Update () {
		if (StartGame.paused == 0) {
			if (!bulletAway && towerShooter) {
				BasicTower towerProperties = towerShooter.GetComponent<BasicTower>();
				if (Vector3.Distance (transform.position, towerShooter.transform.position) > towerProperties.maxRatio) {
					gameObject.tag = "Untagged";
					bulletAway = true;
				}
			}
			else if (bulletAway && myTimerInt > 0) {
				myTimerInt -= Time.deltaTime;
			}
			else GameObject.Destroy(gameObject);
		}
	}
	void OnDestroy () {
		//StartGame.numberOfBulletAwayObjectsAlive--;
	}

}
