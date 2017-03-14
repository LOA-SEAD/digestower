using UnityEngine;
using System.Collections;
using System;

public class InsertTower : MonoBehaviour {
	static public GameObject towerObject = null;
	static public bool[] activeTooth = new bool[3]{false, false, false};
	public int toothPos = 0;
	public int towerType = 0;
	public int localFase = 0;
	/*desconsiderar*/
	public float percent = 70;
	private const float distancePromixity = 3f;
	public GameObject insertedTower = null, insertedTower2 = null;
	private LineRenderer lineRenderer;
	/**/

	/* ALTERACAO
	 * fireRate, bulletSpeed, maxRatio, energyNeeded
	 */
	[HideInInspector] public GameObject towerObj = null;
	/* Frequencia do tiro: quanto menor mais rapido */
	private float[] fireRate = new float[8]{/*Torre 1*/0.9f, 0.5f, 1f, 0.5f, 1f, 0.5f, 0.8f, /*Torre 8*/0.8f};
	//private float[] force = new float[8]{2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f};
	/* Velocidade do tiro: quanto maior mais rapido */
	private float[] bulletSpeed = new float[8]{/*Torre 1*/4f, 4f, 4f, 4f, 4f, 4f, 5f, /*Torre 8*/5f};
	/* Raio de alcance */
	private float[] maxRatio = new float[8]{/*Torre 1*/1f, 1.5f, 1f, 1.2f, 1f, 1.2f, 1f, /*Torre 8*/1f};
	/* ultima posicao do vetor energyNeeded e para o Dente */
	[HideInInspector] public static float[] energyNeeded = new float[9]{/*Torre 1*/700.0f, 1400.0f, 700.0f, 1400.0f, 700.0f, 1400.0f, /*Torre 7*/700.0f, /*Torre 8*/700.0f, /*Dente*/350.0f};
	private GameObject selectedTower;
	/**/
	
	void Start () {
		//StartGame.numberOfInsertTowerObjectsAlive++;
	}

	public void verifyTowerProximity(int type, bool percentUp) {
		if (type == 7 || type == 8) {
			GameObject[] target1 = GameObject.FindGameObjectsWithTag ("Torre " + (type==7?3:5));
			GameObject[] target2 = GameObject.FindGameObjectsWithTag ("Torre " + (type==7?4:6));
			GameObject[] target = new GameObject[target1.Length + target2.Length];
			target1.CopyTo (target, 0);
			target2.CopyTo (target, target1.Length);
			target1 = null;
			target2 = null;

			for (int i = 0;i < target.Length;i++) {
				BasicTower tower = (target[i].GetComponent ("BasicTower") as BasicTower);
				if (tower != null && target[i].gameObject != gameObject && (tower.place.GetComponent ("InsertTower") as InsertTower).localFase == localFase) {
					float distance = Vector3.Distance(target[i].transform.position, transform.position);
					if (distance <= distancePromixity) {
						if (tower.type != 7 && tower.type != 8) {
							if (percentUp)
								tower.adjustWithPercent((tower.place.GetComponent ("InsertTower") as InsertTower).percent, 30, tower.proximityTowers++);
							else
								tower.adjustWithPercent((tower.place.GetComponent ("InsertTower") as InsertTower).percent, 0, tower.proximityTowers--);
							tower.invokeBullets();
						}
					}
				}
			}
		}
		else if (type > 2 && type < 7) {
			GameObject[] target = GameObject.FindGameObjectsWithTag ("Torre " + (type > 4?8:7));
			for (int i = 0;i < target.Length;i++) {
				BasicTower tower = (target[i].GetComponent ("BasicTower") as BasicTower);
				if (tower != null && target[i].gameObject != gameObject && (tower.place.GetComponent ("InsertTower") as InsertTower).localFase == localFase) {
					float distance = Vector3.Distance(target[i].transform.position, transform.position);
					if (distance <= distancePromixity) {
						if (tower.type != 7 && tower.type != 8) {
							if (percentUp)
								tower.adjustWithPercent((tower.place.GetComponent ("InsertTower") as InsertTower).percent, 30, tower.proximityTowers++);
							else
								tower.adjustWithPercent((tower.place.GetComponent ("InsertTower") as InsertTower).percent, 0, tower.proximityTowers--);
							tower.invokeBullets();
						}
					}
				}
			}
		}
	}

	public void RestoreTowerPos(string tag, float new_life = 0) {
		towerObject = GameObject.FindGameObjectWithTag((tag == "xDente"?"Dente":tag));
		//Debug.Log ("TOOTHPOS: " + toothPos + "...TOWERTYPE: " + towerType);
		// towerType = (tag == "xDente"?0:((new int[9]{0, 1, 1, 2, 2, 3, 3, 2, 3})[int.Parse(tag.Substring (6, 1))]));
		LoadTower (true);
		if (insertedTower != null) {
			(insertedTower.GetComponent ("BasicTower") as BasicTower).life = new_life;
		}
	}

	void OnMouseEnter()
	{
		if (towerObject != null && towerObject.tag != "Dente") {
			int type = int.Parse (towerObject.tag.Substring(5,1));
			if (towerType > 0 && ((new int[3]{1, 3, 5})[towerType-1] == type ||
			    (new int[3]{2, 4, 6})[towerType-1] == type ||
			    (new int[3]{0, 7, 8})[towerType-1] == type)) {
			
				float theta_scale = 128f;             //Set lower to add more points
				float size = (2.0f * Mathf.PI) / theta_scale; //Total number of points in circle.

				if (lineRenderer == null && insertedTower == null) {
					lineRenderer = gameObject.AddComponent<LineRenderer> ();
					lineRenderer.material = new Material (Shader.Find ("Particles/Additive"));
					lineRenderer.SetColors (Color.white, Color.white);
					lineRenderer.SetWidth (0.03F, 0.03F);
					lineRenderer.SetVertexCount ((int)theta_scale + 1);
					
					int i = 0;
					for (float theta = 0; i < theta_scale + 1; theta += size,i++) {
						float x = gameObject.transform.position.x + maxRatio[type-1] * Mathf.Cos (theta);
						float y = gameObject.transform.position.y + maxRatio[type-1] * Mathf.Sin (theta);
						
						Vector3 pos = new Vector3 (x, y, -9);
						lineRenderer.SetPosition (i, pos);
					}
				}
			}
		}
	}
	void OnMouseExit()
	{
		if (towerObject != null && towerObject.tag != "Dente") {
			Destroy (lineRenderer);
			lineRenderer = null;
		}
	}

	private void LoadTower(Boolean loadingGame) {
		if (towerObj == null && towerObject) {
			int type = 0;
			float sufficientEnergy = 0;
			if (!loadingGame) {
				if (towerObject.tag == "Dente")
					sufficientEnergy = energyNeeded[8];
				else if (towerObject.tag.Substring(0,5) == "Torre" && towerType > 0) {
					type = int.Parse (towerObject.tag.Substring(5,1));
					// Debug.Log ("t: " + towerType);
					// Debug.Log ("type: " + type + " fase: "+ StartGame.fase);
					if (towerType > 2)
						sufficientEnergy = energyNeeded[type-1];
					else if ((towerType-1) > StartGame.fase) return;
					else if ((new int[3]{1, 3, 5})[towerType-1] == type ||
					         (new int[3]{2, 4, 6})[towerType-1] == type ||
					         (new int[3]{0, 7, 8})[towerType-1] == type)
						sufficientEnergy = energyNeeded[type-1];
					else
						return;
				}
				else return;
			}
			else if (towerObject.tag != "Dente")
				type = int.Parse (towerObject.tag.Substring(5,1));

			//if (type == 7 || type == 8)
			verifyTowerProximity(type, true);
			
			if (sufficientEnergy <= StartGame.energy) {
				if (towerObject.tag == "Dente" && toothPos < 1)
					return;
				if (towerObject.tag != "Dente" && toothPos > 0)
					return;
				if (toothPos > 0 && activeTooth[toothPos%3])
					return;
				StartGame.energy -= sufficientEnergy;
				towerObj = towerObject;
				Vector3 pos = transform.position;
				pos.y += 0.26f;
				
				Quaternion qua = Quaternion.identity,
				qua2 = Quaternion.AngleAxis(180, Vector3.forward);
				
				if (toothPos > 0) {
					pos.y -= 0.21f;
					if (toothPos > 3) {
						qua = qua2;
						qua2 = Quaternion.identity;
					}
				}
				
				insertedTower = (GameObject)Instantiate (towerObj, pos, qua);

				if (towerObject.tag.Substring(0,5) == "Torre") {
					insertedTower.tag = "Torre " + type;
				}

				(insertedTower.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				insertedTower.GetComponent<Renderer>().enabled = true;
				insertedTower.GetComponent<Renderer>().sortingOrder = 4;

				///
				// towerObjTag = towerObject.tag;
				///
				Destroy(insertedTower.GetComponent("ChooseTower"));
				insertedTower.AddComponent<BasicTower>();
				BasicTower towerProperties = insertedTower.GetComponent<BasicTower>();
				towerProperties.towerType = (towerType < 1?1:towerType);
				//Debug.Log ("INSERTED: " + towerProperties.towerType);
				towerProperties.place = gameObject;
				towerProperties.type = type;
				//(insertedTower.GetComponent("BoxCollider2D") as BoxCollider2D).size = new Vector2(1.78f, 2.01f);

				if (!loadingGame) {
					AudioSource aud = gameObject.AddComponent <AudioSource>() as AudioSource;
					aud.playOnAwake = false;
					aud.clip = Resources.Load("Audio/PosicionarTorres") as AudioClip;
					aud.Play();
					aud = null;
				}

				if (toothPos > 0) {
					//Chama mais uma dica da Zimi
					if (activeTooth[0] == false && activeTooth[1] == false && activeTooth[2] == false && StartGame.started == false){
						StartGame.playAfterClose = false;
						(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).dicasZimi (4,8);
					}
					activeTooth[toothPos%3] = true;
					if (toothPos > 3) {
						pos.y += 0.9f;
						pos.x -= 0.08f;
					}
					else {
						Debug.Log ("Iativa");
						pos.x += 0.09f;
						pos.y -= 1f;
					}
					insertedTower2 = (GameObject)Instantiate (towerObj, pos, qua2);
					Destroy(insertedTower2.GetComponent("ChooseTower"));
					BasicTower towerProperties2 = insertedTower2.AddComponent<BasicTower>() as BasicTower;
					towerProperties2.towerType = (towerType < 1?1:towerType);
					//
					//BasicTower towerProperties2 = insertedTower.GetComponent<BasicTower>();
					//towerProperties2.place = insertedTower2;
					//towerProperties2.type = type;
					//
					
					int tag = int.Parse(gameObject.tag.Substring (5, 1));
					GameObject target = GameObject.FindGameObjectWithTag ("Dente" + (tag+(tag>3?-3:3)));
					//
					//(target.GetComponent("InsertTower") as InsertTower).towerObjTag = towerObject.tag;
					//
					target.GetComponent<Renderer>().enabled = false;
					towerProperties2.place = target;
					insertedTower2.GetComponent<Renderer>().enabled = true;
					insertedTower2.GetComponent<Renderer>().sortingOrder = 4;
					(insertedTower2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
					insertedTower2.tag = "xDente"/* + (tag+(tag>3?-3:3))*/;
					insertedTower.tag = "xDente";
					
					Transform _places = GameObject.Find("TowerPosition").transform;
					for (int i=0;i<_places.childCount;i++) {
						if (_places.GetChild(i) == transform) {
							StartGame.placeTag[i] = insertedTower.tag;
							//Debug.Log ("Saving tag: " + insertedTower.tag + " in pos " + i);
						} else if (_places.GetChild(i) == target.transform) {
							StartGame.placeTag[i] = insertedTower2.tag;
							//Debug.Log ("Saving tag: " + insertedTower2.tag + " in pos " + i);
						}
					}
				}
				else {
					towerProperties.fireRate = fireRate[type-1];
					towerProperties.bulletSpeed = bulletSpeed[type-1];
					//towerProperties.force = force[int.Parse(words[1])-1];
					towerProperties.maxRatio = maxRatio[type-1];

					bool changed = false;
					if (type != 7 && type != 8) {
						GameObject[] target = GameObject.FindGameObjectsWithTag ("Torre " + (type > 4?8:7));
						for (int i = 0;i < target.Length;i++) {
							float distance = Vector3.Distance(target[i].transform.position, transform.position);
							if (distance <= distancePromixity && ((target[i].GetComponent ("BasicTower") as BasicTower).place.GetComponent ("InsertTower") as InsertTower).localFase == localFase) {
								towerProperties.adjustWithPercent(percent, 30, towerProperties.proximityTowers++);
								if (!changed) towerProperties.invokeBullets();
								changed = true;
							}
						}
					}
					if (!changed) {
						towerProperties.adjustWithPercent(percent, 0, towerProperties.proximityTowers++);
						towerProperties.proximityTowers--;
						towerProperties.invokeBullets();
					}
					
					Transform _places = GameObject.Find("TowerPosition").transform;
					for (int i=0;i<_places.childCount;i++) {
						if (_places.GetChild(i) == transform) {
							StartGame.placeTag[i] = insertedTower.tag;
							//Debug.Log ("Saving tag: " + insertedTower.tag + " in pos " + i);
						}
					}
				}

				gameObject.GetComponent<Renderer>().enabled = false;
			}
			else {
				EnergyBar.piscar = true;
				//StartGame.msg("Voce esta sem energia no momento");

				// Debug.Log ("energia acabouuu");
			}
		}
	}

	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/
	void OnMouseDown() {
		LoadTower (false);
	}

	/*void Update () {

		Ray ray;
		RaycastHit hit;
		
		if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
		{
			foreach(Touch touch in Input.touches)
			{
				if(touch.phase == TouchPhase.Began)
				{
					ray = Camera.main.ScreenPointToRay(touch.position);
					
					if(Physics.Raycast(ray, out hit, Mathf.Infinity))
					{
						//if(debug)
						//{
							Debug.Log("You touched " + hit.collider.gameObject.name,hit.collider.gameObject);
						//}
						hit.transform.gameObject.SendMessage ("Clicked", hit.point, SendMessageOptions.DontRequireReceiver);
					}
				}		
			}
		}
	}*/
	void OnDestroy () {
		//StartGame.numberOfInsertTowerObjectsAlive--;
	}
}
