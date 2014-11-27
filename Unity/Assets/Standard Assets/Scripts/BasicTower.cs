using UnityEngine;
using System.Collections;
using System;

public class BasicTower : MonoBehaviour {
	private int reverse = -1;
	/*desconsiderar*/
	public float fireRate = 0.5f;
	public float bulletSpeed = 5.0f;
	public float maxRatio = 2.5f;
	/**/
	//public float force = 0.9f;
	public int type = 0;
	public int towerType = 1;
	private float timer = 1.5f;
	private bool shootingFast = false;
	private float myShootingTimer;
	private GameObject btnDestruir = null;
	[HideInInspector] public GameObject place;

	public GameObject bullet;

	public void shootFast() {
		shootingFast = true;
		myShootingTimer = 2.0f;
		bulletSpeed *= 2;
	}
	void Start () {
		StartGame.numberOfBasicTowerObjectsAlive++;
		InvokeRepeating ("SpawnBullet", 0.0f, fireRate);
	}

	/*private static bool ProductGT10(GameObject p)
	{
		if (p.X * p.Y > 100000)
		{
			return true;
		}
		else
		{
			return false;
		}
	}*/
	
	void SpawnBullet() {
		if (StartGame.paused == 0) {
			GameObject[] target = GameObject.FindGameObjectsWithTag ("ComidaInserida" + towerType);
			/*GameObject[] target2 = GameObject.FindGameObjectsWithTag ("ComidaInserida2");
			GameObject[] target3 = GameObject.FindGameObjectsWithTag ("ComidaInserida3");
			GameObject[] target = new GameObject[target1.Length + target2.Length + target3.Length];
			target1.CopyTo (target, 0);
			target2.CopyTo (target, target1.Length);
			target3.CopyTo (target, target1.Length + target2.Length);*/

			if (target.Length > 0) {
				float closerDistance = maxRatio;
				int closerTarget = 0;
				float health = 1000;
				bool haveNear = false;
				bool type0, type1, type2, type3 = type2 = type1 = type0 = false;
				if (type > 4 && type < 7)
					type3 = true;
				else if (type > 2 && type <= 4)
					type2 = true;
				else if (type > 0 && type <= 2)
					type1 = true;
				else if (type < 1)
					type0 = true;

				for (int i = 0; i < target.Length; i++) {
					float distance = Vector3.Distance(target[i].transform.position, transform.position);
					FoodProperties foodProp = target[i].GetComponent<FoodProperties>();
					FollowWaypoints followProp = target[i].GetComponent<FollowWaypoints>();
					//Debug.Log (type + "..." + !foodProp.contaminated2 + "..." + !foodProp.contaminated3);

					if (distance < closerDistance /* && target[i].tag.Equals ("ComidaInserida")*/ &&
					 (type0 || ((type1 && foodProp.healthMode && foodProp.health > 0 && foodProp.health < health) ||
					 (((type == 7 && !foodProp.contaminated2) || (type2)) && foodProp.healthMode2 && foodProp.health2 > 0 && foodProp.health2 < health && followProp._targetWaypoint > 16) ||
					 ((type == 8 && !foodProp.contaminated3) || (type3)) && foodProp.healthMode3 && foodProp.health3 > 0 && foodProp.health3 < health && followProp._targetWaypoint > 36))) {
						// closerDistance = distance;
						if (type3) health = foodProp.health3;
						else if (type2) health = foodProp.health2;
						else health = foodProp.health;
						closerTarget = i;
						haveNear = true;

						//Debug.Log ("type0:" + type0 + " type1:" + type1 + " type2:" + type2 + "  type3:" + type3
						//           + " distance:" + distance + " closerDistance:" + closerDistance +
						//           " healthMode:" + foodProp.healthMode + " healthMode2:" + foodProp.healthMode2
						//           + " healthMode3:" + foodProp.healthMode3);
					}
				}
				if (haveNear) {
					if (gameObject.tag.Substring(0, 5) == "xDent") {
						if (target[closerTarget].GetComponent<FoodProperties>().healthMode) {
							transform.Translate (0, 0.05f*reverse, 0);
							reverse *= -1;
						}
					}
					else {
						Vector3 dir = (target[closerTarget].transform.position - transform.position).normalized;

						float angle = 270+ Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
						// Debug.Log ("objjj: " + gameObject.tag);
						if (bullet == null) {
							string[] words = gameObject.tag.Split(' ');

							/* temporary */
							int bullet_num = int.Parse(words[1]);
							//if (bullet_num == 7) bullet_num = 3;
							//if (bullet_num == 8) bullet_num = 5;
							/* temporary */

							bullet = GameObject.FindGameObjectWithTag ("bullet " + bullet_num);
						}
						GameObject newBullet = (GameObject)Instantiate(bullet.gameObject, transform.renderer.bounds.center, Quaternion.AngleAxis(angle, Vector3.forward));
						newBullet.AddComponent<BulletAway>().towerShooter = gameObject;

						//Debug.Log ("shooting.." + Vector3.Distance(target[closerTarget].transform.position, transform.position));
						newBullet.rigidbody2D.AddForce (dir * bulletSpeed, ForceMode2D.Impulse);
						//Destroy (newBullet, 2.0f);
						//newBullet.rigidbody.rotation = Quaternion.LookRotation((target[closerTarget].transform.position - transform.position) * 0.01f);

						//Vector3 targetDelta = target[closerTarget].transform.position - newBullet.transform.position;
						//get the angle between transform.forward and target delta
						//float angleDiff = Vector3.Angle(newBullet.transform.forward, targetDelta);
						
						// get its cross product, which is the axis of rotation to
						// get from one vector to the other
						//Vector3 cross = Vector3.Cross(newBullet.transform.forward, targetDelta);
						
						// apply torque along that axis according to the magnitude of the angle.
						//newBullet.rigidbody.AddTorque(cross * angleDiff * force);
					}
				}
			}
		}
	}

	void OnMouseDown() {
		GameObject destruirObj = GameObject.FindGameObjectWithTag ("Destruir");
		// GameObject upgradeObj = GameObject.FindGameObjectWithTag ("Upgrade");
		//Debug.Log ((gameObject.GetComponent("BoxCollider2D") as BoxCollider2D).size);

		Vector3 pos = gameObject.transform.position;

		GameObject[] destruirObjArray = GameObject.FindGameObjectsWithTag ("DestruirInserido");
		GameObject[] upgradeObjArray = GameObject.FindGameObjectsWithTag ("UpgradeInserido");
		for (int i = 0;i < destruirObjArray.Length;i++)
			Destroy (destruirObjArray[i]);
		for (int i = 0;i < upgradeObjArray.Length;i++)
			Destroy (upgradeObjArray[i]);

		pos.y += 0.55f;
		if (type == 0)
			pos.y -= 0.2f;
		Quaternion qua = Quaternion.identity;

		if (gameObject.tag != "xDente") {
			btnDestruir = (GameObject)Instantiate (destruirObj, pos, qua);
			//btnDestruir.AddComponent("TowerFunctionality");
			btnDestruir.tag = "DestruirInserido";
			TowerFunctionality towerProp = btnDestruir.GetComponent("TowerFunctionality") as TowerFunctionality;
			towerProp.place = place;
			towerProp.tower = gameObject;
		}
		//RectTransform rectTrans = btnDestruir.GetComponent<RectTransform> ();
		//rectTrans.position = new Vector3 (pos.x, pos.y, 0);

		//pos.y += 0.2f;
		//if (type < StartGame.fase*2/* || StartGame.metadeUltimaFase*/){
		//	GameObject btnUpgrade = (GameObject)Instantiate (upgradeObj, pos, qua); 
			//btnUpgrade.AddComponent("TowerFunctionality");
		//	btnUpgrade.tag = "UpgradeInserido";
		//}

		//RectTransform rectTrans2 = btnUpgrade.GetComponent<RectTransform> ();
		//rectTrans2.position = new Vector3 (pos.x, pos.y, 0);
	}

	
	// Update is called once per frame
	void Update () {
		if (btnDestruir != null) {
			timer -= Time.deltaTime;
			// Debug.Log (btnDestruir + "..");
			//timer2 -= Time.deltaTime;
			if (timer < 0) {
				Destroy (btnDestruir);
				timer = 1.5f;
			}
		}
		if (shootingFast) {
			if (myShootingTimer > 0) {
				myShootingTimer -= Time.deltaTime;
			}
			else {
				shootingFast = false;
				bulletSpeed /= 2;
				if (CallSkill.usingPhysicalExercise)
					CallSkill.usingPhysicalExercise = false;
			}
		}
	}

	void OnDestroy() {
		StartGame.numberOfBasicTowerObjectsAlive--;
	}
}
