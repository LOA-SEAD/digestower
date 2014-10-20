using UnityEngine;
using System.Collections;
using System;

public class InsertTower : MonoBehaviour {
	static public GameObject towerObject = null;
	static public bool[] activeTooth = new bool[3]{false, false, false};
	public int toothPos = 0;
	public int towerType = 0;
	public float percent = 70;

	// public string towerObjTag;
	[HideInInspector] public GameObject towerObj = null;
	private float[] fireRate = new float[8]{0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.3f, 0.8f, 0.8f};
	private float[] force = new float[8]{2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f, 2.0f};
	private float[] bulletSpeed = new float[8]{2f, 2f, 2f, 2f, 2f, 2f, 5f, 5f};
	private float[] maxRatio = new float[8]{1f, 2f, 1f, 2f, 1f, 2f, 1f, 1f};
	/* ultima posicao do vetor energyNeeded e para o Dente */
	[HideInInspector] public static float[] energyNeeded = new float[9]{100.0f, 150.0f, 100.0f, 150.0f, 100.0f, 150.0f, 100.0f, 100.0f, 100.0f};
	private GameObject selectedTower;
	
	void Start () {
		StartGame.numberOfInsertTowerObjectsAlive++;
	}

	public void RestoreTowerPos(string tag) {
		towerObject = GameObject.FindGameObjectWithTag(tag);
		Debug.Log ("   loading tower.."  );
		LoadTower ();
	}

	private void LoadTower() {
		if (towerObj == null && towerObject) {
			int type = 0;
			float sufficientEnergy;
			string[] words = towerObject.tag.Split(' ');
			if (towerObject.tag == "Dente")
				sufficientEnergy = energyNeeded[8];
			else if (words[0] == "Torre" && towerType > 0) {
				type = int.Parse(towerObject.tag.Split(' ')[1]);
				// Debug.Log ("t: " + towerType);
				Debug.Log ("type: " + type + " fase: "+ StartGame.fase);
				if (towerType > 2)
					sufficientEnergy = energyNeeded[int.Parse (words[1])];
				else if ((towerType-1) > StartGame.fase) return;
				else if ((new int[3]{1, 3, 5})[towerType-1] == type ||
				         (new int[3]{2, 4, 6})[towerType-1] == type ||
				         (new int[3]{0, 7, 8})[towerType-1] == type)
					sufficientEnergy = energyNeeded[int.Parse (words[1])];
				else
					return;
			}
			else return;
			
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
				
				GameObject insertedTower = (GameObject)Instantiate (towerObj, pos, qua);
				///
				// towerObjTag = towerObject.tag;
				///
				Destroy(insertedTower.GetComponent("ChooseTower"));
				insertedTower.AddComponent("BasicTower");
				BasicTower towerProperties = insertedTower.GetComponent<BasicTower>();
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
					GameObject insertedTower2 = (GameObject)Instantiate (towerObj, pos, qua2);
					Destroy(insertedTower2.GetComponent("ChooseTower"));
					insertedTower2.AddComponent("BasicTower");
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
					insertedTower2.tag = "xDente"/* + (tag+(tag>3?-3:3))*/;
					insertedTower.tag = "xDente";
					
					Transform _places = GameObject.Find("TowerPosition").transform;
					for (int i=0;i<_places.childCount;i++) {
						if (_places.GetChild(i) == transform) {
							StartGame.placeTag[i] = insertedTower.tag;
							Debug.Log ("Saving tag: " + insertedTower.tag + " in pos " + i);
						} else if (_places.GetChild(i) == target.transform) {
							StartGame.placeTag[i] = insertedTower2.tag;
							Debug.Log ("Saving tag: " + insertedTower2.tag + " in pos " + i);
						}
					}
				}
				else {
					towerProperties.fireRate = fireRate[int.Parse(words[1])-1]*(percent/100);
					towerProperties.bulletSpeed = bulletSpeed[int.Parse(words[1])-1]*(percent/100);
					towerProperties.force = force[int.Parse(words[1])-1];
					towerProperties.maxRatio = maxRatio[int.Parse(words[1])-1];
					
					Transform _places = GameObject.Find("TowerPosition").transform;
					for (int i=0;i<_places.childCount;i++) {
						if (_places.GetChild(i) == transform) {
							StartGame.placeTag[i] = insertedTower.tag;
							Debug.Log ("Saving tag: " + insertedTower.tag + " in pos " + i);
						}
					}
				}
				
				gameObject.renderer.enabled = false;
				
				gameObject.renderer.enabled = false;
			}
			else {
				Debug.Log ("energia acabouuu");
			}
		}
	}

	void OnMouseDown () {
		LoadTower ();
	}

	void Update () {
		/*
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
		*/
	}
	void OnDestroy () {
		StartGame.numberOfInsertTowerObjectsAlive--;
	}
}
