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
	// public GameObject activeTower;
	public static bool activatedMenuInicial = false;

	public GUIText indigestText, energyText, fatText, vitaminText;
	public static float energy;
	public static float vitamin;
	public static float fat;
	public static float indigest;
	public Camera foregroundCamera;

	public static bool firstStart = true;
	public static bool loadingGame = false;

	/*public static int numberOfChooseTowerObjectsAlive = 0,
					  numberOfFollowWaypointsObjectsAlive = 0,
					  numberOfVitaminUpObjectsAlive = 0,
					  numberOfFatPlaceObjectsAlive = 0,
					  numberOfBasicTowerObjectsAlive = 0,
					  numberOfInsertTowerObjectsAlive = 0,
					  numberOfBulletAwayObjectsAlive = 0,
					  numberOfFoodPropertiesObjectsAlive = 0,
					  numberOfAcidoEspecialObjectsAlive = 0,
					  numberOfSalivaEspecialObjectsAlive = 0,
					  numberOfTowerFunctionalityObjectsAlive = 0,
					  numberOfDestroyTowerMenuObjectsAlive = 0,
					  numberOfCallSkillObjectsAlive = 0,
					  numberOfSaveLoadObjectsAlive = 0,
					  numberOfMouseMoveCameraObjectsAlive = 0,
					  numberOfRestartGameObjectsAlive = 0,
				      numberOfSpriteCollectionObjectsAlive = 0,
					  numberOfColorXObjectsAlive = 0;*/

	public static string[] placeTag;
	public static string[] placeTagBkp;

	public static float energyBkp, vitaminBkp, fatBkp, indigestBkp;
	
	/*desconsiderar*/
	public float constantSpeed = 5.5f;
	public float insertTimeInterval = 10.0f;
	public float myTimerInterWaves = 1.0f;
	/* ALTERACAO
	 * a alteracao eh feita no StartButton no componente StartGame
	 */
	/**/

	public static float msgTimeInterval = 10.0f;
	public float initialPosX = -2.95f;
	public float initialPosY = 2.59f;
	public GameObject place1, place2;
	//public float foodMovementSpeed = 10.0f;
	// public int maximumFoods = 10;

	/*desconsiderar*/
	public static int fase = 0;
	public static int nivel = 0;
	public static int wave = 0;
	public static int waveSet = 0;

	private static bool wasFullScreen = false;

	private Texture2D barTexture;
	/**/

// 	private bool towerPosState = true;
	public static bool loose = false;

	/* ALTERACAO
	 * valores maximos energia, vitamina, gordura e indigestao
	 */
	public static float maxEnergy = 7000;
	public static float maxVitamin = 3000;
	public static float maxFat = 100;
	public static float maxIndigest = 6000;

	/**/

	public static int actualSubWave = 0;

	public float myTimer, msgTimer;
	public static bool started = false;
	public static int paused = 1;
	private GameObject[] instantiatedGameObjects;
	private int myTimerInt, msgTimerInt;
	private static string msgBuffer = null;

	private int[,][] maxInserted = new int[8, 16][];
	public static int[] maxInsertedSize = new int[8];
	private string[,][] tags = new string[8, 16][];
	public static int[] numNivelEmFase = new int[4];
	
	private int barHeight = 11;
	private int barLeft = 46;
	private int barTop = 194;

	public static int infoActive = 0;
	public static int[] infoTela = new int[2]{0, 0};
	public static int[] zimi = new int[2]{0, 0};
	public static int personagemAtivo = 1;
	private bool mostrandoFaixa = false;
	public static bool almanaqueAberto = false;
	public static bool playAfterClose = true;
	public static bool stoppedAudio = false;
	//Para controlar o trigger da uma fala da Zimi
	public static bool gorduraPrimeiraVez = false;
	//Para controlar os combos realizados no jogo (ingerir 3 alimentos
	public static int killFood = 0;
	//Para controlar o trigger de outra fala da Zimi
	public static bool comboPrimeiraVez = false;
	private static AudioSource[] allAudioSources;
	private static bool[] audioPlaying;
	private Sprite bkp;
	//Timer para controlar o tempo da animacao do Capitao Banha
	private float capBanha = 0.2f;

	public AudioClip clip1; /* Gracas a isso que e possivel escolher um audio na tela do Unity.
							  Para ele ser tocado, va no local que ele sera ativado e use o seguinte comando:
	                          AudioSource.PlayClipAtPoint(clip1, transform.position);*/

	public static void StopAllAudio() {
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		audioPlaying = new bool[allAudioSources.Length];
		for (int i = 0; i < allAudioSources.Length;++i) {
			if (allAudioSources[i].isPlaying) {
				allAudioSources[i].Pause();
				audioPlaying[i] = true;
			}
			else
				audioPlaying[i] = false;
		}
		stoppedAudio = true;
	}

	public static void ClearAllAudio() {
		allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
		audioPlaying = new bool[allAudioSources.Length];
		for (int i = 0; i < allAudioSources.Length;++i) {
			if (allAudioSources[i].isPlaying) {
				allAudioSources[i].Stop();
				audioPlaying[i] = false;
			}
		}
	}

	public static void PlayAllAudio() {
		if (allAudioSources != null)
			for (int i = 0; i < allAudioSources.Length;++i)
				if (audioPlaying[i])
					if (allAudioSources[i] != null) allAudioSources[i].Play();
		stoppedAudio = false;
	}

	// Use this for initialization
	void Start () {

		Resolution[] resolutions = Screen.resolutions;
		// Print the resolutions
		foreach (Resolution res in resolutions) {
			Debug.Log(res.width + "x" + res.height);
		}

		barTexture = new Texture2D (1, 1);

		Debug.Log (Screen.width + ".." + Screen.height);
		//foregroundCamera = new Camera ();
		ButtonAction.DisableMenu (1);
		ButtonAction.DisableMenu (2);

		placeTag = new string[GameObject.Find ("TowerPosition").transform.childCount];
		placeTagBkp = new string[GameObject.Find ("TowerPosition").transform.childCount];
		for (int i=0;i<GameObject.Find ("TowerPosition").transform.childCount;i++) {
			placeTag[i] = "Untagged";
			placeTagBkp[i] = "Untagged";
		}
		
		/* ALTERACAO
		 * Energia, vitamina, gordura e indigestao inciais
		 */
		energy = 7000;
		vitamin = 0;
		fat = 0;
		indigest = 0;
		/**/

		refreshStatus ();

		/* ALTERACAO
		 * Fase,nivel,wave e waveSet inciais
		 */
		fase = 0; /*0, 1, 2*/
		nivel = 0; /*0, 1, 2*/
		wave = 0; /*0:8, 1:7, 2:5*/
		actualSubWave = 0;
		waveSet = 0;
		loose = false;
		started = false;
		(GameObject.FindGameObjectWithTag("BarraGordura").GetComponent ("SpriteRenderer") as SpriteRenderer).enabled = false;

		/**/

		maxInserted[0,0] = new int[1]{5};	
		maxInserted[0,1] = new int[1]{5};
		maxInserted[0,2] = new int[1]{5};
		maxInserted[0,3] = new int[1]{5};
		maxInserted[0,4] = new int[1]{5};
		maxInserted[0,5] = new int[1]{5};
		maxInserted[0,6] = new int[1]{5};
		maxInserted[0,7] = new int[1]{5};
		maxInsertedSize [0] = 7;

		/*
		maxInserted[1,0] = new int[2]{3, 3};
		maxInserted[1,1] = new int[2]{3, 3};
		maxInserted[1,2] = new int[2]{3, 3};
		maxInserted[1,3] = new int[2]{3, 3};
		maxInserted[1,4] = new int[3]{3, 3, 2};
		maxInserted[1,5] = new int[3]{4, 2, 2};
		maxInserted[1,6] = new int[3]{4, 2, 3};
		maxInserted[1,7] = new int[4]{3, 2, 2, 1};
		maxInsertedSize [1] = 7;
		*/

		maxInserted[1,0] = new int[4]{3, 3, 4, 4};
		maxInserted[1,1] = new int[4]{3, 4, 3, 4};
		maxInserted[1,2] = new int[4]{3, 3, 4, 2};
		maxInserted[1,3] = new int[4]{3, 4, 3, 2};
		maxInserted[1,4] = new int[5]{2, 3, 3, 2, 4};
		maxInserted[1,5] = new int[5]{2, 3, 2, 3, 3};
		maxInserted[1,6] = new int[6]{2, 2, 3, 2, 1, 3};
		maxInserted[1,7] = new int[7]{1, 3, 1, 1, 2, 2, 3};
		maxInsertedSize [1] = 7;
		numNivelEmFase[0] = 2;
		
		maxInserted[2,0] = new int[6]{2, 3, 2, 3, 4, 3};
		maxInserted[2,1] = new int[6]{3, 3, 3, 2, 4, 5};
		maxInserted[2,2] = new int[6]{4, 4, 4, 4, 5, 4};
		maxInserted[2,3] = new int[6]{3, 3, 4, 4, 3, 3};
		maxInserted[2,4] = new int[6]{4, 3, 4, 3, 3, 3};
		maxInserted[2,5] = new int[7]{3, 4, 3, 4, 3, 2, 2};
		maxInserted[2,6] = new int[9]{3, 2, 3, 3, 2, 3, 4, 3, 2};
		maxInsertedSize [2] = 6;

		/*
		maxInserted[4,0] = new int[7]{3, 4, 3, 4, 3, 3, 5};
		maxInserted[4,1] = new int[8]{4, 4, 3, 3, 3, 3, 3, 2};
		maxInserted[4,2] = new int[9]{3, 3, 3, 2, 4, 5, 2, 2, 1};
		maxInserted[4,3] = new int[10]{4, 3, 4, 3, 3, 3, 3, 3, 3, 1};
		maxInserted[4,4] = new int[10]{4, 4, 4, 4, 4, 4, 4, 3, 2, 2};
		maxInserted[4,5] = new int[9]{4, 4, 4, 4, 4, 4, 3, 3, 4};
		maxInserted[4,6] = new int[12]{5, 3, 4, 4, 5, 3, 4, 3, 4, 5, 3, 2};
		maxInsertedSize [4] = 6;
		*/

		maxInserted[3,0] = new int[12]{3, 4, 4, 4, 4, 4, 4, 3, 3, 4, 3, 3};
		maxInserted[3,1] = new int[12]{3, 4, 4, 4, 4, 4, 3, 4, 4, 3, 2, 2};
		maxInserted[3,2] = new int[12]{5, 3, 4, 4, 5, 3, 4, 3, 4, 5, 4, 3};
		maxInserted[3,3] = new int[13]{5, 3, 3, 4, 3, 4, 4, 4, 4, 4, 4, 4, 4};
		maxInserted[3,4] = new int[12]{5, 5, 5, 4, 4, 3, 4, 4, 3, 4, 4, 2};
		maxInserted[3,5] = new int[13]{5, 3, 4, 4, 4, 4, 4, 4, 4, 4, 4, 4, 3};
		maxInserted[3,6] = new int[13]{5, 4, 4, 4, 4, 4, 4, 4, 3, 4, 3, 3, 5};
		maxInsertedSize [3] = 6;
		numNivelEmFase[1] = 2;

		maxInserted[4,0] = new int[8]{4, 4, 3, 2, 4, 2, 3, 3};
		maxInserted[4,1] = new int[10]{3, 4, 4, 4, 4, 4, 4, 4, 3, 3};
		maxInserted[4,2] = new int[11]{4, 4, 4, 4, 3, 2, 3, 3, 5, 3, 3};
		maxInserted[4,3] = new int[13]{5, 5, 4, 4, 3, 4, 4, 3, 4, 5, 3, 3, 3};
		maxInserted[4,4] = new int[11]{5, 2, 3, 4, 4, 5, 3, 4, 4, 3, 3};
		maxInsertedSize [4] = 4;

		maxInserted[5,0] = new int[14]{5, 3, 4, 4, 4, 4, 4, 4, 4, 4, 3, 3, 5, 4};
		maxInserted[5,1] = new int[15]{5, 3, 4, 4, 4, 1, 4, 3, 3, 5, 1, 2, 2, 3, 4};
		maxInserted[5,2] = new int[15]{5, 4, 5, 4, 5, 2, 3, 3, 3, 3, 3, 2, 3, 5, 4};
		maxInserted[5,3] = new int[16]{5, 4, 4, 4, 5, 4, 3, 4, 3, 5, 3, 2, 3, 3, 2, 3};
		maxInserted[5,4] = new int[15]{5, 5, 5, 5, 2, 3, 3, 5, 5, 3, 4, 3, 3, 4, 5};
		maxInsertedSize [5] = 4;

		maxInserted[6,0] = new int[10]{5, 4, 4, 4, 4, 4, 5, 2, 3, 2};
		maxInserted[6,1] = new int[12]{4, 2, 4, 4, 3, 4, 5, 3, 2, 3, 3, 2};
		maxInserted[6,2] = new int[15]{4, 5, 4, 5, 2, 2, 3, 3, 3, 3, 3, 2, 3, 5, 3};
		maxInserted[6,3] = new int[16]{4, 4, 4, 4, 1, 4, 3, 3, 2, 2, 2, 3, 4, 4, 1, 3};
		maxInserted[6,4] = new int[16]{5, 5, 5, 5, 5, 2, 3, 3, 3, 3, 3, 2, 3, 5, 4, 3};
		maxInsertedSize [6] = 4;
		numNivelEmFase[2] = 3;

		maxInserted[7,0] = new int[1]{1};
		maxInsertedSize [7] = 0;
		numNivelEmFase[3] = 1;
		
		tags[0,0] = new string[1]{"Batata"};
		tags[0,1] = new string[1]{"Vagem"};
		tags[0,2] = new string[1]{"Banana"};
		tags[0,3] = new string[1]{"Arroz"};
		tags[0,4] = new string[1]{"PaoIntegral"};
		tags[0,5] = new string[1]{"Pao"};
		tags[0,6] = new string[1]{"Mel"};
		tags[0,7] = new string[1]{"Cereal"};
	
		/*
		tags[1,0] = new string[2]{"Batata", "Vagem"};
		tags[1,1] = new string[2]{"Vagem", "Banana"};
		tags[1,2] = new string[2]{"Banana", "Arroz"};
		tags[1,3] = new string[2]{"Banana", "PaoIntegral"};
		tags[1,4] = new string[3]{"Arroz", "Pao", "Batata"};
		tags[1,5] = new string[3]{"Banana", "Mel", "Vagem"};
		tags[1,6] = new string[3]{"Arroz", "Cereal", "Mel"};
		tags[1,7] = new string[4]{"Banana", "Pao", "PaoIntegral","Cereal"};
		*/
		
		tags[1,0] = new string[4]{"Banana", "Arroz", "PaoIntegral", "Vagem"};
		tags[1,1] = new string[4]{"Pao", "Mel", "Cereal", "Batata"};
		tags[1,2] = new string[4]{"Banana", "Arroz", "PaoIntegral", "Mel"};
		tags[1,3] = new string[4]{"Pao", "Mel", "Cereal", "Vagem"};
		tags[1,4] = new string[5]{"Arroz", "PaoIntegral", "Mel", "Pao", "Batata"};
		tags[1,5] = new string[5]{"Pao", "PaoIntegral", "Mel", "Cereal","Banana"};
		tags[1,6] = new string[6]{"Banana", "Arroz", "PaoIntegral", "Pao", "Mel", "Vagem"};
		tags[1,7] = new string[7]{"PaoIntegral", "Cereal", "Arroz", "Banana", "Pao", "Mel", "Batata"};

		tags[2,0] = new string[6]{"Pao", "PaoIntegral", "Mel", "Cereal", "Vagem", "Lentilha"};
		tags[2,1] = new string[6]{"Arroz", "PaoIntegral", "Mel", "Pao", "Banana", "Peixe"};
		tags[2,2] = new string[6]{"Banana","Arroz","PaoIntegral","Pao", "Batata", "Queijo"};
		tags[2,3] = new string[6]{"PaoIntegral","Cereal","Banana","Pao","Mel","Mortadela"};
		tags[2,4] = new string[6]{"Banana","Arroz","PaoIntegral","Pao","Mel","Soja"};
		tags[2,5] = new string[7]{"Pao","Mel","Cereal","Banana","Queijo","Mortadela","Peixe"};
		tags[2,6] = new string[9]{"Vagem","Arroz","PaoIntegral","Mel","Pao","Banana","Lentilha","Carne", "Soja"};

		/*
		tags[4,0] = new string[7]{"Pao","Mel","Cereal","Banana","Queijo","Mortadela","Lentilha"};
		tags[4,1] = new string[8]{"Mel","Cereal","Arroz","Banana","Pao","Queijo","Carne","Soja"};
		tags[4,2] = new string[9]{"Arroz","PaoIntegral","Mel","Pao","Banana","Peixe","Carne","Soja","Queijo"};
		tags[4,3] = new string[10]{"Banana","Arroz","PaoIntegral","Pao","Mel","Soja","Peixe","Lentilha","Mortadela","Carne"};
		tags[4,4] = new string[10]{"Cereal","Mel","Pao","Arroz","Banana","Lentilha","Mortadela","Queijo","Peixe","Carne"};
		tags[4,5] = new string[9]{"Banana","Arroz","PaoIntegral","Mel","Cereal","Queijo","Carne","Lentilha","Mortadela"};
		tags[4,6] = new string[12]{"Batata","Arroz","PaoIntegral","Mel","Cereal","Banana","Lentilha","Carne","Peixe","Soja", "Peixe", "Queijo"};
		*/

		tags[3,0] = new string[12]{"Batata", "Banana", "Arroz","PaoIntegral","Mel","Cereal","Queijo","Carne","Lentilha","Mortadela","Soja","Peixe"};
		tags[3,1] = new string[12]{"Vagem","Cereal","Mel","Pao","Arroz","Banana","PaoIntegral","Lentilha","Mortadela","Queijo","Carne","Peixe"};
		tags[3,2] = new string[12]{"Batata", "Arroz","PaoIntegral","Mel","Cereal","Banana","Lentilha","Carne","Peixe","Soja","Queijo","Mortadela"};
		tags[3,3] = new string[13]{"Vagem","Banana","PaoIntegral","Arroz","Cereal","Pao","Mel","Soja","Peixe","Carne","Mortadela","Lentilha","Queijo"};
		tags[3,4] = new string[12]{"Cereal","Mel","Pao","Arroz","Banana","PaoIntegral","Lentilha","Mortadela","Queijo","Carne","Peixe","Soja"};
		tags[3,5] = new string[13]{"Batata","Mel","Cereal","Pao","Arroz","Banana","PaoIntegral","Lentilha","Peixe","Queijo","Mortadela","Soja","Carne"};
		tags[3,6] = new string[13]{"Vagem","Banana","Arroz","PaoIntegral","Mel","Cereal","Pao","Queijo","Lentilha","Mortadela","Soja","Peixe","Carne"};
		
		tags[4,0] = new string[8]{"Arroz", "Banana", "PaoIntegral", "Lentilha", "Mortadela", "Soja", "Margarina", "Maionese"};
		tags[4,1] = new string[10]{"Mel", "Cereal", "Arroz", "Batata", "Lentilha", "Peixe", "Queijo", "Mortadela", "Coco", "Abacate"};
		tags[4,2] = new string[11]{"Vagem", "Arroz", "PaoIntegral", "Pao", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Chocolate", "Chips"};
		tags[4,3] = new string[13]{"Cereal", "Pao", "Arroz", "Batata", "Banana", "Lentilha", "Mortadela", "Queijo", "Peixe", "Soja", "Bolo", "Ovo", "Amendoim"};
		tags[4,4] = new string[11]{"Vagem","Batata", "Banana", "Mel", "Queijo", "Peixe", "Lentilha", "Soja", "Carne", "Leite", "Pastel"};
	
		tags[5,0] = new string[14]{"Vagem", "Mel", "Pao", "Banana", "PaoIntegral", "Lentilha", "Peixe", "Queijo", "Mortadela", "Soja", "Carne", "Chocolate", "Abacate", "Coco"};
		tags[5,1] = new string[15]{"Batata", "Banana", "Mel", "Cereal", "Pao", "Queijo", "Mortadela", "Soja", "Peixe", "Carne", "Soja", "Chips", "Margarina", "Coco", "Bolo"};
		tags[5,2] = new string[15]{"Vagem", "Banana", "Cereal", "Arroz", "PaoIntegral", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Abacate", "Ovo", "Leite", "Margarina", "Chips"};
		tags[5,3] = new string[16]{"Batata", "Banana", "Arroz", "PaoIntegral", "Mel", "Queijo", "Lentilha", "Mortadela", "Soja", "Carne", "Coco", "Margarina", "Leite", "Ovo", "Bolo", "Amendoim"};
		tags[5,4] = new string[15]{"Vagem", "Banana", "Batata", "Cereal", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Abacate", "Leite", "Ovo", "Bolo", "Pastel", "Amendoim"};
	
		tags[6,0] = new string[10]{"Vagem", "Banana", "Arroz", "PaoIntegral", "Queijo", "Lentilha", "Mortadela", "Abacate", "Coco", "Margarina"};
		tags[6,1] = new string[12]{"Pao", "Batata", "Mel", "Cereal", "Soja", "Peixe", "Carne", "Coco", "Margarina", "Leite", "Ovo", "Abacate"};
		tags[6,2] = new string[15]{"Vagem", "Cereal", "Batata", "PaoIntegral", "Pao", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Abacate", "Ovo", "Leite", "Maionese", "Bolo"};
		tags[6,3] = new string[16]{"Banana","Mel","Cereal", "Batata", "Queijo", "Mortadela", "Soja", "Peixe", "Soja", "Chips", "Margarina", "Coco", "Bolo", "Coco", "Pastel", "Amendoim"};
		tags[6,4] = new string[16]{"Vagem", "Batata", "Banana", "Cereal", "PaoIntegral", "Queijo", "Lentilha", "Soja", "Peixe", "Carne", "Abacate", "Ovo", "Leite", "Margarina", "Pastel", "Chocolate"};

		tags[7,0] = new string[1]{"Hamburguer"};

		energyBkp = energy;
		vitaminBkp = vitamin;
		fatBkp = fat;
		indigestBkp = indigest;

		insertTimeInterval = 1f;
		myTimerInterWaves = 0.09f;
		myTimer = 149.423f; //maximumFoods * insertTimeInterval + 1;
		msgTimer = 100.0f;
		playAfterClose = false;
	
		CallSkill.firstUsePhysical = true;

		/*place1.GetComponent("SpriteRenderer").renderer.enabled = false;
		place2.GetComponent("SpriteRenderer").renderer.enabled = false;
		(place1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
		(place2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;*/

		menuInicial (true);

		// instantiatedGameObjects = new GameObject[maximumFoods];
		Debug.Log("GAME STARTED");
	}

	public void menuInicial(bool ativ) {
		activatedMenuInicial = ativ;
		GameObject Tela = GameObject.FindGameObjectWithTag("MenuInicial");
		Tela.GetComponent("SpriteRenderer").renderer.enabled = ativ;
		if (ativ) Tela.renderer.sortingOrder = 10;
		else Tela.renderer.sortingOrder = 0;
		(Tela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = ativ;
		
		//GameObject btnAjuda = GameObject.FindGameObjectWithTag("AjudaButton");
		//btnAjuda.GetComponent("SpriteRenderer").renderer.enabled = ativ;
		//btnAjuda.renderer.sortingOrder = 11;
		//(btnAjuda.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = ativ;
		
		GameObject btnIniciar = GameObject.FindGameObjectWithTag("IniciarButton");
		(btnIniciar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = ativ;
		btnIniciar.GetComponent("SpriteRenderer").renderer.enabled = ativ;
		btnIniciar.renderer.sortingOrder = 11;

		//GameObject btnCreditos = GameObject.FindGameObjectWithTag("CreditosButton");
		//(btnCreditos.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = ativ;
		//btnCreditos.GetComponent("SpriteRenderer").renderer.enabled = ativ;
		//btnCreditos.renderer.sortingOrder = 11;

		GameObject btnCarregar = GameObject.FindGameObjectWithTag("CarregarInicialButton");
		(btnCarregar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = ativ;
		btnCarregar.GetComponent("SpriteRenderer").renderer.enabled = ativ;
		btnCarregar.renderer.sortingOrder = 11;

		GameObject.FindGameObjectWithTag("MenuInicial").audio.Play();
	}
	
	// Update is called once per frame

	public void pause (int pauseId = 1) {
		// Time.timeScale = 0;

		GUITextStatus(false);
		SpriteCollection sprites = new SpriteCollection("Start");
		
		SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer>();
		myRenderer.sprite = sprites.GetSprite("iniciar");
		
		sprites = null;
		
		paused = pauseId;
	}

	public void carregaTela(int start, int end) {
		SpriteCollection sprites = new SpriteCollection("Telas");
		infoTela = new int[2]{start,end};
		infoActive = start;
		Debug.Log ("kkkkkkkk");

		GUITextStatus(false);
		paused = 2;
		GameObject tela = GameObject.FindGameObjectWithTag("InfoTela");
		Debug.Log (GameObject.FindGameObjectWithTag ("InfoTela").name);
		tela.GetComponent("SpriteRenderer").renderer.enabled = true;
		(tela.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("Tela" + start);
		sprites = null;
		//(tela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
		tela.renderer.sortingOrder = 10;

		GameObject btnProx = GameObject.FindGameObjectWithTag("InfoProx");
		btnProx.GetComponent("SpriteRenderer").renderer.enabled = true;
		btnProx.renderer.sortingOrder = 11;
		//GameObject btnAnt = GameObject.FindGameObjectWithTag("InfoAnt");
		//btnAnt.GetComponent("SpriteRenderer").renderer.enabled = true;
		//btnAnt.renderer.sortingOrder = 11;
		GameObject btnFechar = GameObject.FindGameObjectWithTag("InfoFechar");
		btnFechar.GetComponent("SpriteRenderer").renderer.enabled = true;
		btnFechar.renderer.sortingOrder = 11;
		(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
		(btnProx.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
		//(btnAnt.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;

		GameObject telaEscura = GameObject.FindGameObjectWithTag ("TelaEscura");
		telaEscura.renderer.enabled = true;
		telaEscura.renderer.sortingOrder = 7;
		(telaEscura.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
	}

	//Funcao criada para controlar a aparicao das dicas dadas pela Zimi
	public void dicasZimi(int start, int end) {
		//Carrega a pasta
		SpriteCollection sprites = new SpriteCollection("Zimi");
		zimi = new int[2]{start,end};
		infoActive = start;
		Debug.Log ("zimi");
		
		GUITextStatus(false);
		paused = 2;
		GameObject tela = GameObject.FindGameObjectWithTag("DicasZimi");
		tela.GetComponent("SpriteRenderer").renderer.enabled = true;
		//Para carregar o que sera utilizado naquele momento
		(tela.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("Zimi" + start);
		sprites = null;
		//(tela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
		tela.renderer.sortingOrder = 10;

		if (start != end) {
			GameObject btnProx = GameObject.FindGameObjectWithTag ("InfoProxZimi");
			btnProx.GetComponent ("SpriteRenderer").renderer.enabled = true;
			//btnProx.GetComponent("SpriteRenderer").
			btnProx.renderer.sortingOrder = 11;
			(btnProx.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
		}
		//GameObject btnAnt = GameObject.FindGameObjectWithTag("InfoAnt");
		//btnAnt.GetComponent("SpriteRenderer").renderer.enabled = true;
		//btnAnt.renderer.sortingOrder = 11;
		GameObject btnFechar = GameObject.FindGameObjectWithTag("InfoFecharZimi");
		btnFechar.GetComponent("SpriteRenderer").renderer.enabled = true;
		btnFechar.renderer.sortingOrder = 11;
		(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
		//(btnAnt.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
	}

	public void play() {
		if (paused > 0 && !loose) {
			//Time.timeScale = 1;
			//Para tocar o trompete
			if (!started) {
				started = true;
				audio.Play ();
			}
			GUITextStatus(true);
			//if (paused < 2) {
			SpriteCollection sprites = new SpriteCollection("Start");
			
			SpriteRenderer myRenderer = gameObject.GetComponent<SpriteRenderer>();
			myRenderer.sprite = sprites.GetSprite("pausar");
			sprites = null;
			
			placeTagBkp = placeTag;
			
			paused = 0;
			
			if (ButtonAction.activatedMenuTorres) {
				ButtonAction.DisableMenu (1);
			}
			if (ButtonAction.activatedMenuEspeciais) {
				ButtonAction.DisableMenu (2);
			}
			//}
			//if (!firstStart) paused = 0;
			if (paused == 2) paused = 1;
		}
		else if (loose) {
			//restartGame();
		}
	}

	public static void restartGame() {
		StartGame.started = false;
		StartGame.paused = 1;
		StartGame.loose = false;
		StartGame.firstStart = true;
		StartGame.playAfterClose = true;
		(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).myTimer = 149.423f;
		CallSkill.creatingAcido = false;
		CallSkill.creatingSaliva = false;
		CallSkill.usingPhysicalExercise = false;
		InsertTower.activeTooth = new bool[3]{false, false, false};
		ButtonAction.activatedMenuEspeciais = ButtonAction.activatedMenuPause = ButtonAction.activatedMenuSaveLoad = ButtonAction.activatedMenuTorres = false;
		ButtonAction.storre3 = ButtonAction.storre4 = ButtonAction.storre5 = ButtonAction.storre6 = ButtonAction.storre7 = ButtonAction.storre8 = null;
		InsertTower.towerObject = null;
		Application.LoadLevel (0);
		Time.timeScale = 1;
	}

	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/

	void OnMouseEnter()
	{
		SpriteCollection sprites = null;
		bkp = (gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		sprites = new SpriteCollection("Pressed");
		(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("IniciarPressionado");
		sprites = null;
	}
	void OnMouseExit()
	{
		(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = bkp;
	}
	void OnMouseDown() {
		//Audio do pause nao funciona por causa do stopAllAudio()
		if (paused > 0) {
			//Audio do play
			if (started) {
				AudioSource.PlayClipAtPoint(clip1, transform.position);
			}
			play();
			if (!ButtonAction.activatedMenuPause && !ButtonAction.activatedMenuEspeciais &&
				!ButtonAction.activatedMenuSaveLoad)
				PlayAllAudio ();
		}
		else if (paused < 2 && !ButtonAction.activatedMenuPause) {
			pause ();
			StopAllAudio();
		}
	}

	public static void msg(string msg) {
		msgTimeInterval = 3.0f;
		msgBuffer = msg;
	}

	public static void refreshStatus () {
		(GameObject.FindGameObjectWithTag ("IndigestText").GetComponent ("GUIText") as GUIText).text = Math.Floor((indigest*100)/maxIndigest) + "/" + 100;
		(GameObject.FindGameObjectWithTag ("EnergyText").GetComponent ("GUIText") as GUIText).text = Math.Floor((energy*100)/maxEnergy) + "/" + 100;
		(GameObject.FindGameObjectWithTag ("VitaminText").GetComponent ("GUIText") as GUIText).text = Math.Floor(vitamin*100/maxVitamin) + "/" + 100;
		(GameObject.FindGameObjectWithTag("FatText").GetComponent ("GUIText") as GUIText).text = (fase > 1?(Math.Floor(fat*100/maxFat) + "/" + 100):"");
	}

	private Rect ResizeGUI(Rect _rect)
	{
		float FilScreenWidth = _rect.width / 1092;
		float rectWidth = FilScreenWidth * Screen.width;
		float FilScreenHeight = _rect.height / 614;
		float rectHeight = FilScreenHeight * Screen.height;
		float rectX = (_rect.x / 1092) * Screen.width;
		float rectY = (_rect.y / 614) * Screen.height;
		
		return new Rect(rectX,rectY,rectWidth,rectHeight);
	}
	void OnGUI() {
		if (msgBuffer != null) {
			GUI.color = Color.white;
			GUI.Label(new Rect(Screen.height/2, Screen.width/4, 400, 400), "<size=40>" + msgBuffer + "</size>");
		}
		if (!activatedMenuInicial && !ButtonAction.activatedMenuTorres && !ButtonAction.activatedMenuEspeciais && !ButtonAction.activatedMenuPause && infoActive == 0 && !mostrandoFaixa && !almanaqueAberto) {
			if (fat > maxFat)
				fat = maxFat;

			//if (Event.current.type == EventType.Repaint){
				//	if (foregroundCamera == null) foregroundCamera = new Camera();
			//	foregroundCamera.Render();
			//}


			//worldPosition = new Vector3(transform.position.x, transform.position.y + adjustmentPos, transform.position.z);
			//screenPosition = Camera.main.WorldToScreenPoint();
			
			//Ray ray = new Ray (Camera.main.transform.position, transform.forward);
			//RaycastHit hit;
			
			//float distance = Vector3.Distance(Camera.main.transform.position, transform.position);
			
			//if (!Physics.Raycast(ray, out hit, distance))
			//{

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


			GUI.color = Color.white;

			/*
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-0, 200, 100), "FP<size=20>" + numberOfFatPlaceObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-30, 200, 100), "VU<size=20>" + numberOfVitaminUpObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-60, 200, 100), "FW<size=20>" + numberOfFollowWaypointsObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-90, 200, 100), "CT<size=20>" + numberOfChooseTowerObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-120, 200, 100), "BT<size=20>" + numberOfBasicTowerObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-150, 200, 100), "IT<size=20>" + numberOfInsertTowerObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-180, 200, 100), "BA<size=20>" + numberOfBulletAwayObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-210, 200, 100), "FP<size=20>" + numberOfFoodPropertiesObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-240, 200, 100), "SE<size=20>" + numberOfSalivaEspecialObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-270, 200, 100), "AE<size=20>" + numberOfAcidoEspecialObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-300, 200, 100), "TF<size=20>" + numberOfTowerFunctionalityObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-330, 200, 100), "DTM<size=20>" + numberOfDestroyTowerMenuObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-360, 200, 100), "CS<size=20>" + numberOfCallSkillObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-390, 200, 100), "SL<size=20>" + numberOfSaveLoadObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-420, 200, 100), "MMC<size=20>" + numberOfMouseMoveCameraObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-450, 200, 100), "RG<size=20>" + numberOfRestartGameObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-480, 200, 100), "CX<size=20>" + numberOfColorXObjectsAlive + "</size>");
			GUI.Label(new Rect(Screen.height/2-100, Screen.width/2-510, 200, 100), "SC<size=20>" + numberOfSpriteCollectionObjectsAlive + "</size>");

	*/
			//if (!started) {
			//	GUI.Label(new Rect(220, 10, 500, 40), "Posicione suas torres e clique no botao START");
			//}

			if (loose) {
				//Como esta tudo na mesma tela, nao da para deixar duas musicas, portanto nao tem como tocar musica no final
				carregaTela (35,37);
				//GUI.Label(new Rect(10, 10, 500, 20), "Voce perdeu o jogo!!! Carregue novamente para voltar de onde havia salvo.");
			}
			gameObject.layer = 10;
			barTexture.SetPixel(0, 0, ColorX.HexToRGB("ffec19"));
			barTexture.Apply ();
			GUI.DrawTexture(ResizeGUI(new Rect(barLeft - 0.5f,
			                        barTop + 127f,
			                        (fat*60)/maxFat, barHeight - 0.5f)), barTexture);
			//GUI.Label(ResizeGUI(new Rect(barLeft+69.5f, barTop + 123, 200, 20)), "<color=#ffec19>" + Math.Floor(fat) + "/" + maxFat + "</color>");
			// 60 / 300
			
			barTexture.SetPixel(0, 0, ColorX.HexToRGB("fe7f02"));
			barTexture.Apply ();
			GUI.DrawTexture(ResizeGUI(new Rect(barLeft - 0.5f,
			                         barTop + 89.5f,
			                         vitamin*58.5f/maxVitamin, barHeight - 0.5f)), barTexture);
			//GUI.Label(ResizeGUI(new Rect(barLeft+69.5f, barTop + 85, 200, 20)), "<color=#fe7f02>" + Math.Floor(vitamin) + "/" + maxVitamin + "</color>");
			// 60 / 2000
			
			barTexture.SetPixel(0, 0, ColorX.HexToRGB("ffffff"));
			barTexture.Apply ();
			GUI.DrawTexture(ResizeGUI(new Rect(barLeft - 0.5f,
			                         barTop + 51.5f,
			                         (energy*85)/maxEnergy, barHeight - 1.5f)), barTexture);
			//GUI.Label(ResizeGUI(new Rect(barLeft+94, barTop + 47.5f, 200, 20)), "<color=#ffffff>" + Math.Floor(energy) + "/" + maxEnergy + "</color>");
			// 85 / 3000

			barTexture.SetPixel(0, 0, ColorX.HexToRGB("b52929"));
			barTexture.Apply ();
			GUI.DrawTexture(ResizeGUI(new Rect(barLeft - 0.5f,
			                         barTop + 14.5f,
			                         (indigest*120)/maxIndigest, barHeight - 0.5f)), barTexture);
			//GUI.Label(ResizeGUI(new Rect(barLeft+130, barTop + 9.8f, 200, 20)), "<color=#b52929>" + Math.Floor(indigest) + "/" + maxIndigest + "</color>");

			barTexture.SetPixel(0, 0, ColorX.HexToRGB("3c3a3c"));
			barTexture.Apply ();
			GUI.DrawTexture(ResizeGUI(new Rect(barLeft+18,
			                                   barTop + 89.6f,
			                                   1.5f, barHeight - 0.5f)), barTexture);
			GUI.DrawTexture(ResizeGUI(new Rect(barLeft+38,
			                                   barTop + 89.6f,
			                                   1.5f, barHeight - 0.5f)), barTexture);
			// 60 / 6000
			// http://forum.unity3d.com/threads/health-bar-above-ememy.81560/
			//}
		}
	}

	public void GUITextStatus(bool enable) {
		(GameObject.FindGameObjectWithTag ("IndigestText").GetComponent ("GUIText") as GUIText).enabled = enable;
		(GameObject.FindGameObjectWithTag ("EnergyText").GetComponent ("GUIText") as GUIText).enabled = enable;
		(GameObject.FindGameObjectWithTag ("VitaminText").GetComponent ("GUIText") as GUIText).enabled = enable;
		if (enable) {
			if (fase > 1)
				(GameObject.FindGameObjectWithTag("FatText").GetComponent ("GUIText") as GUIText).enabled = true;
			else
				(GameObject.FindGameObjectWithTag("FatText").GetComponent ("GUIText") as GUIText).enabled = false;
		}
		else
			(GameObject.FindGameObjectWithTag("FatText").GetComponent ("GUIText") as GUIText).enabled = enable;
	}

	private IEnumerator Pause(int p, int f)
	{
		Time.timeScale = 0.00001f;
		float pauseEndTime = Time.realtimeSinceStartup + p;
		while (Time.realtimeSinceStartup < pauseEndTime)
		{
			GUITextStatus(false);
			paused = 2;
			yield return 0;
		}
		//Time.timeScale = 0;

		if (f == 0) {
			GameObject faixaFase1 = GameObject.FindGameObjectWithTag("FaixaFase1");
			faixaFase1.GetComponent("SpriteRenderer").renderer.enabled = false;
			place1.GetComponent("SpriteRenderer").renderer.enabled = false;
			place2.GetComponent("SpriteRenderer").renderer.enabled = false;
			(place1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			(place2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			mostrandoFaixa = false;
			pause ();
			StartGame.playAfterClose = false;
			carregaTela(13,14);
		}
		else if (f == 1) {
			GameObject faixaFase2 = GameObject.FindGameObjectWithTag("FaixaFase2");
			faixaFase2.GetComponent("SpriteRenderer").renderer.enabled = false;
			place1.GetComponent("SpriteRenderer").renderer.enabled = true;
			place2.GetComponent("SpriteRenderer").renderer.enabled = true;
			(place1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			(place2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			mostrandoFaixa = false;
			pause ();
			StartGame.playAfterClose = false;
			carregaTela(18,19);
		}
		else if (f == 2) {
			GameObject faixaFase3 = GameObject.FindGameObjectWithTag("FaixaFase3");
			faixaFase3.GetComponent("SpriteRenderer").renderer.enabled = false;
			place1.GetComponent("SpriteRenderer").renderer.enabled = true;
			place2.GetComponent("SpriteRenderer").renderer.enabled = true;
			(place1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			(place2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			mostrandoFaixa = false;
			pause ();
			StartGame.playAfterClose = false;
			carregaTela(15,17);
		}
		Time.timeScale = 1;
	}

	void Update () {
		if (msgBuffer != null) {
			if (msgTimer > 100-msgTimeInterval) {
				msgTimer -= Time.deltaTime;
			} else {
				msgBuffer = null;
				msgTimer = 100;
			}
		}
		if (Time.frameCount % 30 == 0)
		{
			System.GC.Collect();
		}

		//if (wasFullScreen && !Screen.fullScreen) {
			// this gets executed once right after leaving full screen
		//}
		if (/*!wasFullScreen && */Screen.fullScreen) {
			// this gets executed once right after entering full screen
			Screen.fullScreen = false;
		}
		//wasFullScreen = Screen.fullScreen;

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
		if (paused == 0) {
			if (indigest >= maxIndigest) {
				//msg ("Voce perdeu!!!!!");
				indigest = maxIndigest;
				Time.timeScale = 0;
				started = false;
				loose = true;
				carregaTela (35,37);
			}
			refreshStatus();

			if (CallSkill.usingPhysicalExercise) {
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
						// Debug.Log (numNivelEmFase[0]*(fase > 0?1:0)+numNivelEmFase[1]*(fase > 1?1:0)+nivel);
						int arrayPos = numNivelEmFase[0]*(fase > 0?1:0)+numNivelEmFase[1]*(fase > 1?1:0)+numNivelEmFase[2]*(fase > 2?1:0)+nivel;
						waveNum = maxInserted[arrayPos,wave];

						//Debug.Log ("INSERTING OBJECTS f" + fase + "...n" + nivel + "...fn" + (fase*3+nivel) + "...w" + wave + "...a" + actualSubWave + ".. wS" + waveSet + "..waveN" + waveNum);
						if (actualSubWave < waveNum.Length) {
							if (waveSet < maxInserted[arrayPos,wave][actualSubWave]) {
								if (fase == 0 && nivel == 0 && wave == 0 && actualSubWave == 0 && waveSet == 1) {
									mostrandoFaixa = true;
									GameObject.FindGameObjectWithTag("MenuInicial").audio.Stop();
									GameObject faixaFase1 = GameObject.FindGameObjectWithTag("FaixaFase1");
									if (!loadingGame) {
										faixaFase1.audio.Play();
									}
									else loadingGame = false;
									faixaFase1.GetComponent("SpriteRenderer").renderer.enabled = true;
									StartCoroutine(Pause(3, 0));
									//(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).dicasZimi (11,11);
								}
								if (fase == 1 && nivel == 0 && wave == 0 && actualSubWave == 0 && waveSet == 1) {
									mostrandoFaixa = true;
									GameObject faixaFase2 = GameObject.FindGameObjectWithTag("FaixaFase2");
									GameObject.FindGameObjectWithTag("MenuInicial").audio.Stop();
									GameObject.FindGameObjectWithTag("FaixaFase1").audio.Stop();
									if (!loadingGame) {
										faixaFase2.audio.Play();
									}
									else loadingGame = false;
									faixaFase2.GetComponent("SpriteRenderer").renderer.enabled = true;
									StartCoroutine(Pause(3, 1));
									//(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).dicasZimi (14,16);
								}
								if (fase == 2 && nivel == 0 && wave == 0 && actualSubWave == 0 && waveSet == 1) {
									mostrandoFaixa = true;
									(GameObject.FindGameObjectWithTag("BarraGordura").GetComponent ("SpriteRenderer") as SpriteRenderer).enabled = true;
									GameObject faixaFase3 = GameObject.FindGameObjectWithTag("FaixaFase3");
									GameObject.FindGameObjectWithTag("MenuInicial").audio.Stop();
									GameObject.FindGameObjectWithTag("FaixaFase2").audio.Stop ();
									if (!loadingGame) {
										faixaFase3.audio.Play();
									}
									else loadingGame = false;
									faixaFase3.GetComponent("SpriteRenderer").renderer.enabled = true;
									StartCoroutine(Pause(3, 2));
									//(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).dicasZimi (19,22);
								}
								// if (insertTimeInterval > 0.5f) insertTimeInterval = 0.5f;
								//Debug.Log (fase*3+nivel + ", " + wave + ", " + actualSubWave);
								GameObject item = GameObject.FindGameObjectWithTag(tags[arrayPos,wave][actualSubWave]);
								FoodProperties food = item.GetComponent<FoodProperties>();
								
								//food.healthMode = fase;
								Vector3 pos = new Vector3 (initialPosX, initialPosY, 0);
								GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
								inserted.AddComponent<FollowWaypoints>().movementSpeed = food.movementSpeed * constantSpeed;

								FollowWaypoints foodWayProperties = inserted.GetComponent<FollowWaypoints>();
								foodWayProperties.food = inserted.GetComponent<FoodProperties>();
								foodWayProperties.oldTag = inserted.tag;
								inserted.tag = "ComidaInserida1"/* + (fase+1)*/;
								//Debug.Log ("fase: " + fase + ", nivel: " + nivel + ", wave: " + wave + ", actualSubWave: " + actualSubWave + ", waveSet:" + waveSet);
								//GameObject lastFood = GameObject.FindGameObjectWithTag("LastFood");
								//(lastFood.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = (inserted.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;

								if (fase < 3) {
									if (waveSet == 0) {
										int actualSubWaveTemp = actualSubWave+1;
										int waveTemp = wave;
										int nivelTemp = nivel;
										int faseTemp = fase;
										int[] waveNumTemp = maxInserted[StartGame.numNivelEmFase[0]*(StartGame.fase > 0?1:0)+StartGame.numNivelEmFase[1]*(StartGame.fase > 1?1:0)+StartGame.nivel,wave];
										if (actualSubWaveTemp >= waveNumTemp.Length) {
											waveTemp++;

											if (waveTemp > maxInsertedSize[StartGame.numNivelEmFase[0]*(StartGame.fase > 0?1:0)+StartGame.numNivelEmFase[1]*(StartGame.fase > 1?1:0)+StartGame.nivel]) {
												actualSubWaveTemp = waveTemp = 0;
												nivelTemp++;

												if (nivelTemp > StartGame.numNivelEmFase[StartGame.fase]) {
													/*if (StartGame.fase > 2) {
														StartGame.msg ("As fases acabaram");
														nivelChange = false;
														StartGame.started = false;
														StartGame.paused = 1;
													}*/
													//else {
													nivelTemp = 0;
													faseTemp++;
													//}
												}
											}
											else {
												actualSubWaveTemp = 0;
											}
										}
										//Debug.Log ("actual: " + fase + ".." + nivel + ".." + wave + ".." + actualSubWave);
										//Debug.Log ("next: " + faseTemp + ".." + nivelTemp + ".." + waveTemp + ".." + actualSubWaveTemp);

										if (faseTemp < 3) {
											GameObject nextFood = GameObject.FindGameObjectWithTag("NextFood");
											//Debug.Log ("faseTemp: " + faseTemp + ", actualSubWave: " + actualSubWave + ", next: " + (numNivelEmFase[0]*(faseTemp > 0?1:0)+numNivelEmFase[1]*(faseTemp > 1?1:0)+numNivelEmFase[2]*(faseTemp > 2?1:0)+nivelTemp));
											//Debug.Log ("tag: " + tags[numNivelEmFase[0]*(faseTemp > 0?1:0)+numNivelEmFase[1]*(faseTemp > 1?1:0)+numNivelEmFase[2]*(faseTemp > 2?1:0)+nivelTemp,waveTemp][actualSubWaveTemp]);
											GameObject nextSprite = GameObject.FindGameObjectWithTag(tags[numNivelEmFase[0]*(faseTemp > 0?1:0)+numNivelEmFase[1]*(faseTemp > 1?1:0)+numNivelEmFase[2]*(faseTemp > 2?1:0)+nivelTemp,waveTemp][actualSubWaveTemp]);

											(nextFood.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = (nextSprite.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
										}
										else ButtonAction.nivelChange = false;
									}
								}
								else {
									GameObject.FindGameObjectWithTag("MenuInicial").audio.Stop();
									GameObject.FindGameObjectWithTag("FaixaFase3").audio.Stop();
									GameObject.FindGameObjectWithTag("Hamburguer").audio.Play();
									myTimer = 0;


								}
								waveSet++;
							}
							else {
								if (myTimerInterWaves > 0) {
									myTimerInterWaves -= Time.deltaTime;
								}
								else {
									actualSubWave++;
									waveSet = 0;
									killFood = 0;
									myTimerInterWaves = 0.09f;

								}
							}
						}
						else {

							//else {
								actualSubWave = 0;
								waveSet = 0;
								killFood = 0;
								
								energyBkp = energy;
								vitaminBkp = vitamin;
								fatBkp = fat;
								indigestBkp = indigest;
								placeTagBkp = placeTag;

								wave++;
								//msg ("Wave: "+ (wave+1));
								
								myTimer = 149.423f;
							//}

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


						if (wave > maxInsertedSize[numNivelEmFase[0]*(fase > 0?1:0)+numNivelEmFase[1]*(fase > 1?1:0)+nivel]) {
							wave = 0;
							actualSubWave = 0;
							waveSet = 0;
							killFood = 0;
							nivel++;
							//msg ("Nivel: "+ (nivel+1));
							// insertTimeInterval = 12.5f;
						}

						if (nivel >= numNivelEmFase[fase]) {
							//started = false;

							//if (fase > 1) fase = 0;
							fase++;

							actualSubWave = 0;
							waveSet = 0;
							wave = 0;
							nivel = 0;
							killFood = 0;
							//energy = 1000;
							//vitamin = 0;
							//msg ("Fase: "+ (fase+1));
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
				//Debug.Log ("FINISHING INSERTION");
				// started = false;
			}
		}
	}
}
