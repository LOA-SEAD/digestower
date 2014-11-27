using UnityEngine;
using System.Collections;
using System;

public class InsertTower : MonoBehaviour {
	static public GameObject towerObject = null;
	static public bool[] activeTooth = new bool[3]{false, false, false};
	public int toothPos = 0;
	public int towerType = 0;
	/*desconsiderar*/
	public float percent = 70;
	public GameObject insertedTower = null, insertedTower2 = null;
	/**/

	/* ALTERACAO
	 * fireRate, bulletSpeed, maxRatio, energyNeeded
	 */
	[HideInInspector] public GameObject towerObj = null;
	/* Frequencia do tiro: quanto menor mais rapido */
	private float[] fireRate = new float[8]{/*Torre 1*/0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 1.2f, /*Torre 8*/1.2f};
	//private float[] force = new float[8]{2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f};
	/* Velocidade do tiro: quanto maior mais rapido */
	private float[] bulletSpeed = new float[8]{/*Torre 1*/2f, 2f, 2f, 2f, 2f, 2f, 5f, /*Torre 8*/5f};
	/* Raio de alcance */
	private float[] maxRatio = new float[8]{/*Torre 1*/1f, 2f, 1f, 2f, 1f, 2f, 1f, /*Torre 8*/1f};
	/* ultima posicao do vetor energyNeeded e para o Dente */
	[HideInInspector] public static float[] energyNeeded = new float[9]{/*Torre 1*/500.0f, 1000.0f, 500.0f, 1000.0f, 500.0f, 1000.0f, /*Torre 7*/500.0f, /*Torre 8*/500.0f, /*Dente*/300.0f};
	private GameObject selectedTower;
	/**/
	
	void Start () {
		StartGame.numberOfInsertTowerObjectsAlive++;
	}

	public void RestoreTowerPos(string tag) {
		towerObject = GameObject.FindGameObjectWithTag((tag == "xDente"?"Dente":tag));
		//Debug.Log ("TOOTHPOS: " + toothPos + "...TOWERTYPE: " + towerType);
		Debug.Log ("   loading tower..." + tag);
		// towerType = (tag == "xDente"?0:((new int[9]{0, 1, 1, 2, 2, 3, 3, 2, 3})[int.Parse(tag.Substring (6, 1))]));
		LoadTower (true);
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
				insertedTower.renderer.enabled = true;
				insertedTower.renderer.sortingOrder = 4;

				///
				// towerObjTag = towerObject.tag;
				///
				Destroy(insertedTower.GetComponent("ChooseTower"));
				insertedTower.AddComponent("BasicTower");
				BasicTower towerProperties = insertedTower.GetComponent<BasicTower>();
				towerProperties.towerType = (towerType < 1?1:towerType);
				//Debug.Log ("INSERTED: " + towerProperties.towerType);
				towerProperties.place = gameObject;
				towerProperties.type = type;
				//(insertedTower.GetComponent("BoxCollider2D") as BoxCollider2D).size = new Vector2(1.78f, 2.01f);
							
				if (toothPos > 0) {
					
					activeTooth[toothPos%3] = true;
					if (toothPos > 3) {
						pos.y += 0.9f;
						pos.x -= 0.08f;
					}
					else {
						pos.x += 0.09f;
						pos.y -= 1f;
					}
					insertedTower2 = (GameObject)Instantiate (towerObj, pos, qua2);
					Destroy(insertedTower2.GetComponent("ChooseTower"));
					(insertedTower2.AddComponent("BasicTower") as BasicTower).towerType = (towerType < 1?1:towerType);
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
					target.renderer.enabled = false;
					insertedTower2.renderer.enabled = true;
					insertedTower2.renderer.sortingOrder = 4;
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
					towerProperties.fireRate = fireRate[type-1]*(percent/100);
					towerProperties.bulletSpeed = bulletSpeed[type-1]*(percent/100);
					//towerProperties.force = force[int.Parse(words[1])-1];
					towerProperties.maxRatio = maxRatio[type-1];
					
					Transform _places = GameObject.Find("TowerPosition").transform;
					for (int i=0;i<_places.childCount;i++) {
						if (_places.GetChild(i) == transform) {
							StartGame.placeTag[i] = insertedTower.tag;
							//Debug.Log ("Saving tag: " + insertedTower.tag + " in pos " + i);
						}
					}
				}

				gameObject.renderer.enabled = false;
			}
			else {
				StartGame.msg("Voce esta sem energia no momento");
				// Debug.Log ("energia acabouuu");
			}
		}
	}

	void OnMouseDown () {
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
		StartGame.numberOfInsertTowerObjectsAlive--;
	}
}
