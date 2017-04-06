 using UnityEngine;
using System.Collections;
using System;

public class BasicTower : MonoBehaviour {
	private int reverse = -1;
	/*desconsiderar*/
	public float fireRate = 0.5f;
	private float fireRateBkp = 0f;
	public float bulletSpeed = 5.0f;
	private float bulletSpeedBkp = 0f;
	public float maxRatio = 2.5f;
	/**/
	//public float force = 0.9f;
	public int type = 0;
	public int towerType = 1;
	public int proximityTowers = 0;
	private float timer = 1.5f;
	private bool shootingFast = false;
	private float myShootingTimer;
	private GameObject btnDestruir = null;
	//private GameObject blueStar = null;
	//private GameObject yellowStar = null;
	[HideInInspector] public GameObject place;
	private float percentLifeToColorChange = 0.35f;
	private int healthBarHeight = 3;
	private int healthBarLeft = 17;
	private int healthBarTop = 145;
	public float maxLife = 100.0f;
	public float life = 0.0f;
	public float adjustmentPos = 2.3f;
	private LineRenderer lineRenderer;
	//private bool activateBlueStar = false;
	//private bool activateYellowStar = false;

	public GameObject bullet;

	public void shootFast() {
		shootingFast = true;
		myShootingTimer = 2.0f;
		bulletSpeed *= 2;
	}

	private Vector3 worldPosition = new Vector3();
	private Vector3 screenPosition = new Vector3();
	private Texture2D barTexture;
	private Vector2 scale, screen;

	public void adjustWithPercent(float p, float increment, int proximityCount) {
		if ((proximityCount == 0 && proximityTowers == 1) || (proximityCount == 1 && proximityTowers == 0)) {
			if (bulletSpeedBkp != 0f)
				bulletSpeed = bulletSpeedBkp;
			else
				bulletSpeedBkp = bulletSpeed;
			if (fireRateBkp != 0f)
				fireRate = fireRateBkp;
			else
				fireRateBkp = fireRate;

			float pinc = p+increment;
			fireRate *= (pinc / 100);
			bulletSpeed *= (pinc / 100);

			//if (pinc == 120 || pinc == 70 || p == 70) activateBlueStar = true;
			//if ((pinc == 100 && gameObject.tag != "xDente") || pinc == 80) activateYellowStar = true;
			//if (pinc == 50 || (pinc == 70 && proximityCount > 0)) activateYellowStar = false;
			//Debug.Log (gameObject.tag + "..."+ proximityCount + "..." + p + "..." + proximityTowers);
		}
	}

	public void invokeBullets() {
		if (IsInvoking("SpawnBullet"))
			CancelInvoke ("SpawnBullet");
		InvokeRepeating ("SpawnBullet", 0.0f, fireRate);
	}
	void Start () {
		if (life == 0) life = 100f;
		barTexture = new Texture2D (1, 1);
		if (type == 0) InvokeRepeating ("SpawnBullet", 0.0f, fireRate);
		// StartGame.numberOfBasicTowerObjectsAlive++;
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
						//if (target[closerTarget].GetComponent<FoodProperties>().healthMode) {
							transform.Translate (0, 0.05f*reverse, 0);
							reverse *= -1;
						//}
					}
					else {
						Vector3 dir = (target[closerTarget].transform.position - transform.position).normalized;

						float angle = 270+ Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
						// Debug.Log ("objjj: " + gameObject.tag);
						string[] words = gameObject.tag.Split(' ');
						int bullet_num = int.Parse(words[1]);
						if (bullet == null) {
 							/* temporary */
							//if (bullet_num == 7) bullet_num = 3;
							//if (bullet_num == 8) bullet_num = 5;
							/* temporary */

							bullet = GameObject.FindGameObjectWithTag ("bullet " + bullet_num);
						}
						GameObject newBullet = (GameObject)Instantiate(bullet.gameObject, transform.GetComponent<Renderer>().bounds.center, Quaternion.AngleAxis(angle, Vector3.forward));
						newBullet.AddComponent<BulletAway>().towerShooter = gameObject;
						life -= 1.5f;

						//Debug.Log ("shooting.." + Vector3.Distance(target[closerTarget].transform.position, transform.position));
						newBullet.GetComponent<Rigidbody2D>().AddForce (dir * bulletSpeed, ForceMode2D.Impulse);

						if (bullet_num > 0 && bullet_num < 3){
							GameObject disparaSom = GameObject.FindGameObjectWithTag("bullet 1");
							disparaSom.GetComponent<AudioSource>().Play();
						}
						if ((bullet_num > 2 && bullet_num < 5) || bullet_num == 7){
							GameObject disparaSom = GameObject.FindGameObjectWithTag("bullet 3");
							disparaSom.GetComponent<AudioSource>().Play();
						}
						if ((bullet_num > 4 && bullet_num < 7) || bullet_num == 8){
							GameObject disparaSom = GameObject.FindGameObjectWithTag("bullet 5");
							disparaSom.GetComponent<AudioSource>().Play();
						}
						if (life <= 0) {
							Transform _places = GameObject.Find("TowerPosition").transform;
							for (int i=0;i<_places.childCount;i++) {
								if (_places.GetChild(i) == place.transform)
									StartGame.placeTag[i] = "Untagged";
							}
							
							InsertTower insertPlace = place.GetComponent ("InsertTower") as InsertTower;
							insertPlace.verifyTowerProximity(type, false);
							// (target.GetComponent("InsertTower") as InsertTower).towerObjTag = towerObject.tag;
							insertPlace.towerObj = null;
							place.GetComponent<Renderer>().enabled = true;
							Destroy(gameObject);
						}
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

	void OnGUI() {
		if (StartGame.started && StartGame.paused < 2) {
			if (gameObject.tag != "xDente" /*(||
			  (gameObject.tag == "xDente" &&
			  (place.GetComponent ("InsertTower") as InsertTower).toothPos > 0 &&
			  (place.GetComponent ("InsertTower") as InsertTower).toothPos < 4)*/) {
				worldPosition = new Vector3(transform.position.x, transform.position.y + adjustmentPos, transform.position.z);
				screenPosition = Camera.main.WorldToScreenPoint(worldPosition);
				
				Ray ray = new Ray (Camera.main.transform.position, transform.forward);
				RaycastHit hit;
				
				float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
				
				if (!Physics.Raycast(ray, out hit, distance))
				{
					//try {
					
					/*Vector3 healthBarWorldPosition = transform.position;
						healthBarWorldPosition.y += 20f;
						Vector3 healthBarScreenPosition = Camera.main.WorldToScreenPoint(healthBarWorldPosition);

						Debug.Log (healthBarScreenPosition.y + ".." + screenPosition.y);

						float top = Screen.height - (healthBarScreenPosition.y + (healthBarTop / 2));*/

					barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
					barTexture.Apply ();

					screen = new Vector2 (Screen.width, Screen.height);
					scale = new Vector2(screen.x/1092, screen.y/614);

					barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
					barTexture.Apply ();
					GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2 - 1,
					                         Screen.height - screenPosition.y + (healthBarTop*scale.y)-1,
					                         20*scale.x, healthBarHeight*scale.y+2), barTexture);
					barTexture.SetPixel(0, 0, (life < percentLifeToColorChange*maxLife) ? ColorX.HexToRGB("ff0000") : ColorX.HexToRGB("00ff00"));
					barTexture.Apply ();
					GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2,
					                         Screen.height - screenPosition.y + healthBarTop*scale.y,
					                         (life*20*scale.x)/maxLife-2, healthBarHeight*scale.y), barTexture);
				}
			}
		}
	}

	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/
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

		pos.y += 0.65f;
		if (type == 0)
			pos.y -= 0.3f;
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

	void OnMouseEnter()
	{
		if (gameObject.tag != "xDente") {
			float theta_scale = 128f;             //Set lower to add more points
			float size = (2.0f * Mathf.PI) / theta_scale; //Total number of points in circle.

			if (lineRenderer == null && Shader.Find ("Particles/Additive") != null) {
				lineRenderer = gameObject.AddComponent<LineRenderer> ();
				lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
				lineRenderer.SetColors (Color.white, Color.white);
				lineRenderer.SetWidth (0.03F, 0.03F);
				lineRenderer.SetVertexCount ((int)theta_scale + 1);
				
				int i = 0;
				for (float theta = 0; i < theta_scale + 1; theta += size,i++) {
					float x = gameObject.transform.position.x + maxRatio * Mathf.Cos (theta);
					float y = gameObject.transform.position.y + maxRatio * Mathf.Sin (theta);
					
					Vector3 pos = new Vector3 (x, y, -9);
					lineRenderer.SetPosition (i, pos);
				}
			}
		}
	}
	void OnMouseExit()
	{
		if (gameObject.tag != "xDente") {
			Destroy(lineRenderer);
			lineRenderer = null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (StartGame.paused == 0) {
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
			/*if (blueStar == null && activateBlueStar) {
				GameObject estrelaObj = GameObject.FindGameObjectWithTag ("EstrelaAzul");
				// GameObject upgradeObj = GameObject.FindGameObjectWithTag ("Upgrade");
				//Debug.Log ((gameObject.GetComponent("BoxCollider2D") as BoxCollider2D).size);
				
				Vector3 pos = gameObject.transform.position;
				
				pos.y += 0.55f;
				pos.x += 0.25f;
				Quaternion qua = Quaternion.identity;

				blueStar = (GameObject)Instantiate (estrelaObj, pos, qua);
				//btnDestruir.AddComponent("TowerFunctionality");
				//blueStar.tag = "DestruirInserido";
				//TowerFunctionality towerProp = btnDestruir.GetComponent("TowerFunctionality") as TowerFunctionality;
				//towerProp.place = place;
				//towerProp.tower = gameObject;
			}
			if (yellowStar == null && activateYellowStar) {
				if (proximityTowers > 0) {
				GameObject estrelaObj = GameObject.FindGameObjectWithTag ("EstrelaAmarela");
				// GameObject upgradeObj = GameObject.FindGameObjectWithTag ("Upgrade");
				//Debug.Log ((gameObject.GetComponent("BoxCollider2D") as BoxCollider2D).size);
				
				Vector3 pos = gameObject.transform.position;
				
				pos.y += 0.55f;
				pos.x += 0.45f;
				Quaternion qua = Quaternion.identity;
				
				yellowStar = (GameObject)Instantiate (estrelaObj, pos, qua);
				//btnDestruir.AddComponent("TowerFunctionality");
				//yellowStar.tag = "DestruirInserido";
				//TowerFunctionality towerProp = btnDestruir.GetComponent("TowerFunctionality") as TowerFunctionality;
				//towerProp.place = place;
				//towerProp.tower = gameObject;
				}
			}
			else if (yellowStar != null && !activateYellowStar) {
				Destroy (yellowStar);
			}*/
		}
	}

	/*void OnDestroy() {
		if (yellowStar != null)
			Destroy (yellowStar);
		if (blueStar != null)
			Destroy (blueStar);
		//StartGame.numberOfBasicTowerObjectsAlive--;
	}*/
}
