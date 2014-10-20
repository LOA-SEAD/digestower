using UnityEngine;
using System.Collections;
using System;

/*
 * fazer:
 * savegame
 * new towers proximity functionality
 * changing screens
 * special acid
 * 
 */
public class StartGame : MonoBehaviour {
	// public static StartGame current;
	public GameObject activeTower;
	public static int numberOfChooseTowerObjectsAlive = 0,
					  numberOfFollowWaypointsObjectsAlive = 0,
					  numberOfVitaminUpObjectsAlive = 0,
					  numberOfFatPlaceObjectsAlive = 0,
					  numberOfBasicTowerObjectsAlive = 0,
					  numberOfInsertTowerObjectsAlive = 0,
					  numberOfBulletAwayObjectsAlive = 0,
					  numberOfFoodPropertiesObjectsAlive = 0;

	public static float energy = 1500;
	public static float vitamin = 0;
	public static float fat = 0;
	public static float indigest = 0;
	public static string[] placeTag;
	public static string[] placeTagBkp;

	public static float energyBkp, vitaminBkp, fatBkp, indigestBkp;

	public float constantSpeed = 10.0f;
	public float insertTimeInterval = 10.0f;
	public static float msgTimeInterval = 10.0f;
	public float initialPosX = -2.818f;
	public float initialPosY = 3.19f;
	//public float foodMovementSpeed = 10.0f;
	// public int maximumFoods = 10;
	public static int fase = 0;
	public static int nivel = 0;
	public static int wave = 0;
	public static int waveSet = 0;

// 	private bool towerPosState = true;
	private bool loose = false;
	public static float maxEnergy = 3000;
	public static float maxVitamin = 2000;
	public static float maxFat = 300;
	public static float maxIndigest = 6000;

	private int actualSubWave = 0;
	private float myTimer, msgTimer;
	public static bool started = false;
	private GameObject[] instantiatedGameObjects;
	private int myTimerInt, msgTimerInt;
	private static string msgBuffer = null;

	private int[,][] maxInserted = new int[9, 16][];
	private int[] maxInsertedSize = new int[9];
	private string[,][] tags = new string[9, 16][];
	
	private Vector3 screenPosition = new Vector3();
	private int barHeight = 11;
	private int barLeft = -23;
	private int barTop = 358;

	private Vector2 scale, screen;

	// Use this for initialization
	void Start () {
		placeTag = new string[GameObject.Find ("TowerPosition").transform.childCount];
		placeTagBkp = new string[GameObject.Find ("TowerPosition").transform.childCount];
		for (int i=0;i<GameObject.Find ("TowerPosition").transform.childCount;i++) {
			placeTag[i] = "Untagged";
			placeTagBkp[i] = "Untagged";
		}

		energy = 1500;
		vitamin = 0;
		fat = 0;
		indigest = 0;

		fase = 0;
		nivel = 0;
		wave = 0;
		waveSet = 0;
		actualSubWave = 0;

		maxInserted[0,0] = new int[1]{5};
		maxInserted[0,1] = new int[1]{5};
		maxInserted[0,2] = new int[1]{5};
		maxInserted[0,3] = new int[1]{5};
		maxInserted[0,4] = new int[1]{5};
		maxInserted[0,5] = new int[1]{5};
		maxInserted[0,6] = new int[1]{5};
		maxInserted[0,7] = new int[1]{5};
		maxInsertedSize [0] = 7;
		
		maxInserted[1,0] = new int[2]{3, 3};
		maxInserted[1,1] = new int[2]{3, 3};
		maxInserted[1,2] = new int[2]{3, 3};
		maxInserted[1,3] = new int[2]{3, 3};
		maxInserted[1,4] = new int[3]{3, 3, 2};
		maxInserted[1,5] = new int[3]{4, 2, 2};
		maxInserted[1,6] = new int[3]{4, 2, 3};
		maxInserted[1,7] = new int[4]{3, 2, 2, 1};
		maxInsertedSize [1] = 7;

		maxInserted[2,0] = new int[4]{3, 3, 4, 4};
		maxInserted[2,1] = new int[4]{3, 4, 3, 4};
		maxInserted[2,2] = new int[4]{3, 3, 4, 2};
		maxInserted[2,3] = new int[4]{3, 4, 3, 2};
		maxInserted[2,4] = new int[5]{2, 3, 3, 2, 4};
		maxInserted[2,5] = new int[5]{2, 3, 2, 3, 3};
		maxInserted[2,6] = new int[6]{2, 2, 3, 2, 1, 3};
		maxInserted[2,7] = new int[7]{1, 3, 1, 1, 2, 2, 3};
		maxInsertedSize [2] = 7;
		
		maxInserted[3,0] = new int[6]{2, 3, 2, 3, 4, 3};
		maxInserted[3,1] = new int[6]{3, 3, 3, 2, 4, 5};
		maxInserted[3,2] = new int[6]{4, 4, 4, 4, 5, 4};
		maxInserted[3,3] = new int[6]{3, 3, 4, 4, 3, 3};
		maxInserted[3,4] = new int[6]{4, 3, 4, 3, 3, 3};
		maxInserted[3,5] = new int[7]{3, 4, 3, 4, 3, 2, 2};
		maxInserted[3,6] = new int[9]{3, 2, 3, 3, 2, 3, 4, 3, 2};
		maxInsertedSize [3] = 6;
		
		maxInserted[4,0] = new int[7]{3, 4, 3, 4, 3, 3, 5};
		maxInserted[4,1] = new int[8]{4, 4, 3, 3, 3, 3, 3, 2};
		maxInserted[4,2] = new int[9]{3, 3, 3, 2, 4, 5, 2, 2, 1};
		maxInserted[4,3] = new int[10]{4, 3, 4, 3, 3, 3, 3, 3, 3, 1};
		maxInserted[4,4] = new int[10]{4, 4, 4, 4, 4, 4, 4, 3, 2, 2};
		maxInserted[4,5] = new int[9]{4, 4, 4, 4, 4, 4, 3, 3, 4};
		maxInserted[4,6] = new int[12]{5, 3, 4, 4, 5, 3, 4, 3, 4, 5, 3, 2};
		maxInsertedSize [4] = 6;


		maxInserted[5,0] = new int[12]{3, 4, 4, 4, 4, 4, 4, 3, 3, 4, 3, 3};
		maxInserted[5,1] = new int[12]{3, 4, 4, 4, 4, 4, 3, 4, 4, 3, 2, 2};
		maxInserted[5,2] = new int[12]{5, 3, 4, 4, 5, 3, 4, 3, 4, 5, 4, 3};
		maxInserted[5,3] = new int[13]{5, 3, 3, 4, 3, 4, 4, 4, 4, 4, 4, 4, 4};
		maxInserted[5,4] = new int[12]{5, 5, 5, 4, 4, 3, 4, 4, 3, 4, 4, 2};
		maxInserted[5,5] = new int[13]{5, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3};
		maxInserted[5,6] = new int[13]{5, 4, 4, 4, 4, 4, 4, 4, 3, 4, 3, 3, 5};
		maxInsertedSize [5] = 6;

		maxInserted[6,0] = new int[8]{4, 4, 3, 2, 4, 2, 3, 3};
		maxInserted[6,1] = new int[10]{3, 4, 4, 4, 4, 4, 4, 4, 3, 3};
		maxInserted[6,2] = new int[11]{4, 4, 4, 4, 3, 2, 3, 3, 5, 3, 3};
		maxInserted[6,3] = new int[13]{5, 5, 4, 4, 3, 4, 4, 3, 4, 5, 3, 3, 3};
		maxInserted[6,4] = new int[11]{5, 2, 3, 4, 4, 5, 3, 4, 4, 3, 3};
		maxInsertedSize [6] = 4;

		maxInserted[7,0] = new int[14]{5, 3, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 5, 4};
		maxInserted[7,1] = new int[15]{5, 3, 4, 4, 4, 1, 4, 3, 3, 5, 1, 2, 2, 3, 4};
		maxInserted[7,2] = new int[15]{5, 4, 5, 4, 5, 2, 3, 3, 3, 3, 3, 2, 3, 5, 4};
		maxInserted[7,3] = new int[16]{5, 4, 4, 4, 5, 4, 3, 4, 3, 5, 3, 2, 3, 3, 2, 3};
		maxInserted[7,4] = new int[15]{5, 5, 5, 5, 2, 3, 3, 5, 5, 3, 4, 3, 3, 4, 5};
		maxInsertedSize [7] = 4;

		maxInserted[8,0] = new int[10]{5, 4, 4, 4, 4, 4, 5, 2, 3, 2};
		maxInserted[8,1] = new int[12]{4, 2, 4, 4, 3, 4, 5, 3, 2, 3, 3, 2};
		maxInserted[8,2] = new int[15]{4, 5, 4, 5, 2, 2, 3, 3, 3, 3, 3, 2, 3, 5, 3};
		maxInserted[8,3] = new int[16]{4, 4, 4, 4, 1, 4, 3, 3, 2, 2, 2, 3, 4, 4, 1, 3};
		maxInserted[8,4] = new int[16]{5, 5, 5, 5, 5, 2, 3, 3, 3, 3, 3, 2, 3, 5, 4, 3};
		maxInsertedSize [8] = 6;
		
		tags[0,0] = new string[1]{"Batata"};
		tags[0,1] = new string[1]{"Vagem"};
		tags[0,2] = new string[1]{"Banana"};
		tags[0,3] = new string[1]{"Arroz"};
		tags[0,4] = new string[1]{"PaoIntegral"};
		tags[0,5] = new string[1]{"Pao"};
		tags[0,6] = new string[1]{"Mel"};
		tags[0,7] = new string[1]{"Cereal"};
	
		tags[1,0] = new string[2]{"Batata", "Vagem"};
		tags[1,1] = new string[2]{"Vagem", "Banana"};
		tags[1,2] = new string[2]{"Banana", "Arroz"};
		tags[1,3] = new string[2]{"Banana", "PaoIntegral"};
		tags[1,4] = new string[3]{"Arroz", "Pao", "Batata"};
		tags[1,5] = new string[3]{"Banana", "Mel", "Vagem"};
		tags[1,6] = new string[3]{"Arroz", "Cereal", "Mel"};
		tags[1,7] = new string[4]{"Banana", "Pao", "PaoIntegral","Cereal"};
		
		tags[2,0] = new string[4]{"Banana", "Arroz", "PaoIntegral", "Vagem"};
		tags[2,1] = new string[4]{"Pao", "Mel", "Cereal", "Batata"};
		tags[2,2] = new string[4]{"Banana", "Arroz", "PaoIntegral", "Mel"};
		tags[2,3] = new string[4]{"Pao", "Mel", "Cereal", "Vagem"};
		tags[2,4] = new string[5]{"Arroz", "PaoIntegral", "Mel", "Pao", "Batata"};
		tags[2,5] = new string[5]{"Pao", "PaoIntegral", "Mel", "Cereal","Banana"};
		tags[2,6] = new string[6]{"Banana", "Arroz", "PaoIntegral", "Pao", "Mel", "Vagem"};
		tags[2,7] = new string[7]{"PaoIntegral", "Cereal", "Arroz", "Banana", "Pao", "Mel", "Batata"};

		tags[3,0] = new string[6]{"Pao", "PaoIntegral", "Mel", "Cereal", "Vagem", "Lentilha"};
		tags[3,1] = new string[6]{"Arroz", "PaoIntegral", "Mel", "Pao", "Banana", "Peixe"};
		tags[3,2] = new string[6]{"Banana","Arroz","PaoIntegral","Pao", "Batata", "Queijo"};
		tags[3,3] = new string[6]{"PaoIntegral","Cereal","Banana","Pao","Mel","Mortadela"};
		tags[3,4] = new string[6]{"Banana","Arroz","PaoIntegral","Pao","Mel","Soja"};
		tags[3,5] = new string[7]{"Pao","Mel","Cereal","Banana","Queijo","Mortadela","Peixe"};
		tags[3,6] = new string[9]{"Vagem","Arroz","PaoIntegral","Mel","Pao","Banana","Lentilha","Carne", "Soja"};
		
		tags[4,0] = new string[7]{"Pao","Mel","Cereal","Banana","Queijo","Mortadela","Lentilha"};
		tags[4,1] = new string[8]{"Mel","Cereal","Arroz","Banana","Pao","Queijo","Carne","Soja"};
		tags[4,2] = new string[9]{"Arroz","PaoIntegral","Mel","Pao","Banana","Peixe","Carne","Soja","Queijo"};
		tags[4,3] = new string[10]{"Banana","Arroz","PaoIntegral","Pao","Mel","Soja","Peixe","Lentilha","Mortadela","Carne"};
		tags[4,4] = new string[10]{"Cereal","Mel","Pao","Arroz","Banana","Lentilha","Mortadela","Queijo","Peixe","Carne"};
		tags[4,5] = new string[9]{"Banana","Arroz","PaoIntegral","Mel","Cereal","Queijo","Carne","Lentilha","Mortadela"};
		tags[4,6] = new string[12]{"Batata","Arroz","PaoIntegral","Mel","Cereal","Banana","Lentilha","Carne","Peixe","Soja", "Peixe", "Queijo"};
		
		tags[5,0] = new string[12]{"Batata", "Banana", "Arroz","PaoIntegral","Mel","Cereal","Queijo","Carne","Lentilha","Mortadela","Soja","Peixe"};
		tags[5,1] = new string[12]{"Vagem","Cereal","Mel","Pao","Arroz","Banana","PaoIntegral","Lentilha","Mortadela","Queijo","Carne","Peixe"};
		tags[5,2] = new string[12]{"Batata", "Arroz","PaoIntegral","Mel","Cereal","Banana","Lentilha","Carne","Peixe","Soja","Queijo","Mortadela"};
		tags[5,3] = new string[13]{"Vagem","Banana","PaoIntegral","Arroz","Cereal","Pao","Mel","Soja","Peixe","Carne","Mortadela","Lentilha","Queijo"};
		tags[5,4] = new string[12]{"Cereal","Mel","Pao","Arroz","Banana","PaoIntegral","Lentilha","Mortadela","Queijo","Carne","Peixe","Soja"};
		tags[5,5] = new string[13]{"Batata","Mel","Cereal","Pao","Arroz","Banana","PaoIntegral","Lentilha","Peixe","Queijo","Mortadela","Soja","Carne"};
		tags[5,6] = new string[13]{"Vagem","Banana","Arroz","PaoIntegral","Mel","Cereal","Pao","Queijo","Lentilha","Mortadela","Soja","Peixe","Carne"};

		tags[6,0] = new string[8]{"Arroz", "Banana", "PaoIntegral", "Lentilha", "Mortadela", "Soja", "Manteiga", "Maionese"};
		tags[6,1] = new string[10]{"Mel", "Cereal", "Arroz", "Batata", "Lentilha", "Peixe", "Queijo", "Mortadela", "Coco", "Abacate"};
		tags[6,2] = new string[11]{"Vagem", "Arroz", "PaoIntegral", "Pao", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Chocolate", "Chips"};
		tags[6,3] = new string[13]{"Cereal", "Pao", "Arroz", "Batata", "Banana", "Lentilha", "Mortadela", "Queijo", "Peixe", "Soja", "Bolo", "Ovo", "Amendoim"};
		tags[6,4] = new string[11]{"Vagem","Batata", "Banana", "Mel", "Queijo", "Peixe", "Lentilha", "Soja", "Carne", "Leite", "Pastel"};

		tags[7,0] = new string[14]{"Vagem", "Mel", "Pao", "Banana", "PaoIntegral", "Lentilha", "Peixe", "Queijo", "Mortadela", "Soja", "Carne", "Chocolate", "Abacate", "Coco"};
		tags[7,1] = new string[15]{"Batata", "Banana", "Mel", "Cereal", "Pao", "Queijo", "Mortadela", "Soja", "Peixe", "Carne", "Soja", "Chips", "Manteiga", "Coco", "Bolo"};
		tags[7,2] = new string[15]{"Vagem", "Banana", "Cereal", "Arroz", "PaoIntegral", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Abacate", "Ovo", "Leite", "Manteiga", "Chips"};
		tags[7,3] = new string[16]{"Batata", "Banana", "Arroz", "PaoIntegral", "Mel", "Queijo", "Lentilha", "Mortadela", "Soja", "Carne", "Coco", "Manteiga", "Leite", "Ovo", "Bolo", "Amendoim"};
		tags[7,4] = new string[15]{"Vagem", "Banana", "Batata", "Cereal", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Abacate", "Leite", "Ovo", "Bolo", "Pastel", "Amendoim"};

		tags[8,0] = new string[10]{"Vagem", "Banana", "Arroz", "PaoIntegral", "Queijo", "Lentilha", "Mortadela", "Acabate", "Coco", "Manteiga"};
		tags[8,1] = new string[12]{"Pao", "Batata", "Mel", "Cereal", "Soja", "Peixe", "Carne", "Coco", "Manteiga", "Leite", "Ovo", "Abacate"};
		tags[8,2] = new string[15]{"Vagem", "Cereal", "Batata", "PaoIntegral", "Pao", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Abacate", "Ovo", "Leite", "Maionese", "Bolo"};
		tags[8,3] = new string[16]{"Banana","Mel","Cereal", "Batata", "Queijo", "Mortadela", "Soja", "Peixe", "Soja", "Chips", "Manteiga", "Coco", "Bolo", "Coco", "Pastel", "Amendoim"};
		tags[8,4] = new string[16]{"Vagem", "Batata", "Banana", "Cereal", "PaoIntegral", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Abacate", "Ovo", "Leite", "Manteiga", "Pastel", "Chocolate"};
	
		screen = new Vector2 (Screen.width, Screen.height);
		scale = new Vector2(screen.x/1092, screen.y/614);

		energyBkp = energy;
		vitaminBkp = vitamin;
		fatBkp = fat;
		indigestBkp = indigest;

		myTimer = 100.0f; //maximumFoods * insertTimeInterval + 1;
		msgTimer = 100.0f;
		// instantiatedGameObjects = new GameObject[maximumFoods];
		Debug.Log("GAME STARTED");
	}
	
	// Update is called once per frame
	void OnMouseDown() {
		if (!started) {
			Time.timeScale = 1;
			SpriteCollection sprites = new SpriteCollection("Start");
			
			SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer>();
			myRenderer.sprite = sprites.GetSprite("Botão pause");
			
			placeTagBkp = placeTag;

			started = true;
		}
		else {
			Time.timeScale = 0;

			SpriteCollection sprites = new SpriteCollection("Start");

			SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer>();
			myRenderer.sprite = sprites.GetSprite("Botão iniciar");

			started = false;
		}

	}

	public static void msg(string msg) {
		msgTimeInterval = 3.0f;
		msgBuffer = msg;
	}

	void OnGUI() {
		if (energy > maxEnergy)
			energy = maxEnergy;
		if (vitamin > maxVitamin)
			vitamin = maxVitamin;
		if (fat > maxFat)
			fat = maxFat;
		//worldPosition = new Vector3(transform.position.x, transform.position.y + adjustmentPos, transform.position.z);
		//screenPosition = Camera.main.WorldToScreenPoint();
		
		//Ray ray = new Ray (Camera.main.transform.position, transform.forward);
		//RaycastHit hit;
		
		//float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
		
		//if (!Physics.Raycast(ray, out hit, distance))
		//{
		Texture2D barTexture = new Texture2D (1, 1);
		/*
		barTexture.SetPixel(0, 0, Color.black);
		barTexture.Apply ();
		GUI.DrawTexture(new Rect(barLeft / 2 - 1,
		                    Screen.height - barTop-1,
		                    67, barHeight+2), barTexture);
		GUI.DrawTexture(new Rect(barLeft / 2 - 1,
		                    Screen.height - barTop-21,
		                    67, barHeight+2), barTexture);
		GUI.DrawTexture(new Rect(barLeft / 2 - 1,
		                    Screen.height - barTop-41,
		                    67, barHeight+2), barTexture);
	    */

		if (msgBuffer != null) {
			if (msgTimer > 100-msgTimeInterval) {
				msgTimer -= Time.deltaTime;
				GUI.color = Color.white;
				GUI.Label(new Rect(Screen.height/2, Screen.width/4, 400, 400), "<size=40>" + msgBuffer + "</size>");
			} else {
				msgBuffer = null;
				msgTimer = 100;
			}
		}

		GUI.color = Color.white;
		/*
		GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-0, 200, 100), "FP<size=20>" + numberOfFatPlaceObjectsAlive + "</size>");
		GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-30, 200, 100), "VU<size=20>" + numberOfVitaminUpObjectsAlive + "</size>");
		GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-60, 200, 100), "FW<size=20>" + numberOfFollowWaypointsObjectsAlive + "</size>");
		GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-80, 200, 100), "CT<size=20>" + numberOfChooseTowerObjectsAlive + "</size>");
		GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-110, 200, 100), "BT<size=20>" + numberOfBasicTowerObjectsAlive + "</size>");
		GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-140, 200, 100), "IT<size=20>" + numberOfInsertTowerObjectsAlive + "</size>");
		GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-170, 200, 100), "BA<size=20>" + numberOfBulletAwayObjectsAlive + "</size>");
		GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-200, 200, 100), "FP<size=20>" + numberOfFoodPropertiesObjectsAlive + "</size>");
		*/

		if (!started) {
			GUI.Label(new Rect(10, 10, 500, 20), "Posicione suas torres e clique no botao START novamente");
		}

		if (loose) {
			GUI.Label(new Rect(10, 10, 500, 20), "Voce perdeu o jogo!!! Carregue novamente para voltar de onde havia salvo.");
		}

		barTexture.SetPixel(0, 0, ColorX.HexToRGB("ffec19"));
		barTexture.Apply ();
		GUI.DrawTexture(new Rect(screenPosition.x - barLeft*scale.x / 2,
		                        Screen.height - screenPosition.y - (barTop - 25)*scale.y,
	                            (fat*60*scale.x)/maxFat, barHeight*scale.y), barTexture);
		// 60 / 300
		
		barTexture.SetPixel(0, 0, ColorX.HexToRGB("fe7f02"));
		barTexture.Apply ();
		GUI.DrawTexture(new Rect(screenPosition.x - barLeft*scale.x / 2,
		                         Screen.height - screenPosition.y - (barTop + 12.6f)*scale.y,
		                         (vitamin*60*scale.x)/maxVitamin, barHeight*scale.y), barTexture);
		// 60 / 2000
		
		barTexture.SetPixel(0, 0, ColorX.HexToRGB("ffffff"));
		barTexture.Apply ();
		GUI.DrawTexture(new Rect(screenPosition.x - barLeft*scale.x / 2,
		                         Screen.height - screenPosition.y - (barTop + 51.5f)*scale.y,
	                            (energy*85*scale.x)/maxEnergy, barHeight*scale.y), barTexture);
		// 85 / 3000

		barTexture.SetPixel(0, 0, ColorX.HexToRGB("b52929"));
		barTexture.Apply ();
		GUI.DrawTexture(new Rect(screenPosition.x - barLeft*scale.x / 2,
		                         Screen.height - screenPosition.y - (barTop + 87.8f)*scale.y,
		                         (indigest*120*scale.x)/maxIndigest, barHeight*scale.y), barTexture);
		// 60 / 6000
		// http://forum.unity3d.com/threads/health-bar-above-ememy.81560/
		//}
	}

	void Update () {
		//if (Input.GetMouseButtonDown(0)) {
		//	started = true;
			/*RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit)) {
				if (hit.collider.gameObject.name == "Start")
				{
					started = true;
				}
			}*/
		//}
		//A simple countdown timer
		if (started) {
			if (indigest >= maxIndigest) {
				msg ("Voce perdeu!!!!!");
				indigest = maxIndigest;
				loose = true;
				started = false;
			}
			else if (myTimer > 0) {
				myTimer -= Time.deltaTime;
				if (Mathf.Round(myTimer) != myTimerInt) {
					myTimerInt = (int)Mathf.Round(myTimer);
					// int length = 0;
					// Debug.Log ("laal: " + myTimerInt + ".." + insertTimeInterval);
					if ((myTimerInt % insertTimeInterval) == 0) {
						int[] waveNum;
						//try{
							waveNum = maxInserted[fase*3+nivel,wave];

							//Debug.Log ("INSERTING OBJECTS f" + fase + "...n" + nivel + "...fn" + (fase*3+nivel) + "...w" + wave + "...a" + actualSubWave + ".. wS" + waveSet + "..waveN" + waveNum);
							if (fase > 2) {
								msg ("Chefao. Aguarde proxima versao!");
							}
							else if (actualSubWave < waveNum.Length) {
								if (waveSet < maxInserted[fase*3+nivel,wave][actualSubWave]) {
									// if (insertTimeInterval > 0.5f) insertTimeInterval = 0.5f;
									//Debug.Log (fase*3+nivel + ", " + wave + ", " + actualSubWave);
									GameObject item = GameObject.FindGameObjectWithTag(tags[fase*3+nivel,wave][actualSubWave]);
									FoodProperties food = item.GetComponent<FoodProperties>();
									
									//food.healthMode = fase;
									Vector3 pos = new Vector3 (initialPosX, initialPosY, 0);
									GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
									inserted.AddComponent<FollowWaypoints>().movementSpeed = food.movementSpeed*constantSpeed;

									FollowWaypoints foodWayProperties = inserted.GetComponent<FollowWaypoints>();
									foodWayProperties.food = inserted.GetComponent<FoodProperties>();
									foodWayProperties.oldTag = inserted.tag;
									inserted.tag = "ComidaInserida"/* + (fase+1)*/;
									waveSet++;
								}
								else {
									actualSubWave++;
									waveSet = 0;
								}
							}
							else {
								actualSubWave = waveSet = 0;

								energyBkp = energy;
								vitaminBkp = vitamin;
								fatBkp = fat;
								indigestBkp = indigest;
								placeTagBkp = placeTag;

								wave++;
								msg ("Wave: "+ (wave+1));
								
								myTimer = 100.0f;

								/*int r = Random.Range (0, 100);
								if (r < 80) {
									GameObject item = GameObject.FindGameObjectWithTag("Vitamina");
									Vector3 pos = new Vector3 (initialPosX, initialPosY, 0);
									GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
									inserted.AddComponent<FollowWaypoints>().movementSpeed = foodMovementSpeed;
									FollowWaypoints foodWayProperties = inserted.GetComponent<FollowWaypoints>();
									// foodWayProperties.oldTag = inserted.tag;
								}*/
							}
						//} catch(NullReferenceException) {
						//	Debug.Log ("err: "+ (fase*3+nivel) + ", " + wave);
						//}


						if (wave > maxInsertedSize[(fase*3)+nivel]) {
							wave = actualSubWave = waveSet = 0;
							nivel++;
							msg ("Nivel: "+ (nivel+1));
							// insertTimeInterval = 12.5f;
						}

						if (nivel > 2) {
							//started = false;

							if (fase > 1) fase = 0;
							else fase++;

							actualSubWave = waveSet = wave = nivel = 0;
							//energy = 1000;
							//vitamin = 0;
							msg ("Fase: "+ (fase+1));
							// fase++;
						}

						/*GameObject[] items = GameObject.FindGameObjectsWithTag("ComidaFase1");
						instantiatedGameObjects[insertedLast] = (GameObject)Instantiate (items[Random.Range (0, items.Length)], pos, Quaternion.identity);
						instantiatedGameObjects[insertedLast].AddComponent<FollowWaypoints>().movementSpeed = foodMovementSpeed;
						instantiatedGameObjects[insertedLast].tag = "ComidaInserida1";
						insertedLast++;*/
					}
				}

			} else {
				Debug.Log ("FINISHING INSERTION");
				started = false;
			}
		}
	}
}
