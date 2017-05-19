using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System;
using System.Text.RegularExpressions;

public class SaveLoad : MonoBehaviour {
	public bool type = false;
	public int slot = 1;
	private Sprite bkp;
	public AudioClip clip;
	//public static List<StartGame> savedGames = new List<StartHG>();

	public sealed class VersionDeserializationBinder : SerializationBinder 
	{ 
		public override Type BindToType( string assemblyName, string typeName )
		{ 
			if ( !string.IsNullOrEmpty( assemblyName ) && !string.IsNullOrEmpty( typeName ) ) 
			{ 
				Type typeToDeserialize = null; 
				
				assemblyName = Assembly.GetExecutingAssembly().FullName; 
				
				// The following line of code returns the type. 
				typeToDeserialize = Type.GetType( String.Format( "{0}, {1}", typeName, assemblyName ) ); 
				
				return typeToDeserialize; 
			} 
			
			return null; 
		} 
	}

	void Start () {
		//StartGame.numberOfSaveLoadObjectsAlive++;
	}

	void OnMouseEnter()
	{
		SpriteCollection sprites = null;
		if (slot == 1 || slot == 2 || slot == 3) {
			bkp = (gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
			sprites = new SpriteCollection("Pressed");
		}
		if (slot == 1) {
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("Arquivo1Pressionado");
		}
		else if (slot == 2) {
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("Arquivo2Pressionado");
		}
		else if (slot == 3) {
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("Arquivo3Pressionado");
		}
		sprites = null;
	}
	void OnMouseExit()
	{
		if (slot == 1 || slot == 2 || slot == 3) {
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = bkp;
		}
	}

	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/
	void OnMouseDown() {
		//2 linhas para disparar o som
		GameObject disparaSom = GameObject.FindGameObjectWithTag("Save1Button");
		disparaSom.GetComponent<AudioSource>().Play();
		//AudioSource.PlayClipAtPoint (clip, transform.position);
		if (type) {
			SpriteCollection sprites = new SpriteCollection("MsgsSalvCarr");
			GameObject msg = GameObject.FindGameObjectWithTag("MsgSalvCarr");
			if (StartGame.started) {
				Debug.Log("saving");
				Save ();
				(msg.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("msgSave1");
				//StartGame.msg("Jogo salvo com sucesso!");
			}
			else {
				(msg.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("msgSave2");
				//StartGame.msg("Inicie o jogo antes de salvar!");
			}
			msg.GetComponent("SpriteRenderer").GetComponent<Renderer>().enabled = true;
			msg.GetComponent<Renderer>().sortingOrder = 15;
			StartGame.msgBuffer = true;
		}
		else {
			Debug.Log("loading");
			Load ();
			// else StartGame.msg("Carregue antes de iniciar");
		}
	}


	public void Save() {
		/*
		SaveData data = new SaveData ();
		data.wave = StartGame.wave;
		//data.waveSet = StartGame.waveSet;
		data.fase = StartGame.fase;
		data.nivel = StartGame.nivel;
		
		data.energy = StartGame.energyBkp;
		data.fat = StartGame.fatBkp;
		data.vitamin = StartGame.vitaminBkp;
		data.indigest = StartGame.indigestBkp;
		for (int i=0;i < GameObject.Find("TowerPosition").transform.childCount;i++) {
			data.places[i] = StartGame.placeTagBkp[i];
		}
		
		//savedGames.Add(Game.current);
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/savedGames.gd");
		bf.Binder = new VersionDeserializationBinder();
		bf.Serialize(file, data);

		file.Close();
		*/

		PlayerPrefs.SetInt("wave" + slot,StartGame.wave);
		PlayerPrefs.SetInt("fase" + slot,StartGame.fase);
		PlayerPrefs.SetInt("nivel" + slot,StartGame.nivel);
		PlayerPrefs.SetInt("actualSubWave" + slot,StartGame.actualSubWave);
		PlayerPrefs.SetInt("waveSet" + slot,StartGame.waveSet);
		PlayerPrefs.SetInt("comboPrimeiraVez" + slot,StartGame.comboPrimeiraVez?1:0);
		PlayerPrefs.SetInt("gorduraPrimeiraVez" + slot,StartGame.gorduraPrimeiraVez?1:0);
		PlayerPrefs.SetInt("dentePrimeiraVez" + slot,StartGame.dentePrimeiraVez?1:0);
		PlayerPrefs.SetInt("firstUsePhysical" + slot,CallSkill.firstUsePhysical?1:0);
		PlayerPrefs.SetFloat("energy" + slot,StartGame.energy);
		PlayerPrefs.SetFloat("fat" + slot,StartGame.fat);
		PlayerPrefs.SetFloat("vitamin" + slot,StartGame.vitamin);
		PlayerPrefs.SetFloat("indigest" + slot,StartGame.indigest);
		PlayerPrefs.SetInt("acelerarAtivado" + slot,StartGame.acelerarAtivado);

		Transform _places = GameObject.Find("TowerPosition").transform;
		for (int i=0;i < _places.childCount;i++) {
			PlayerPrefs.SetString(slot + "place" + i,Regex.Replace(StartGame.placeTagBkp[i], "\\s", String.Empty));
			InsertTower insertPlace = _places.GetChild(i).GetComponent ("InsertTower") as InsertTower;
			if (insertPlace.insertedTower != null) {
				BasicTower basicTower = insertPlace.insertedTower.GetComponent("BasicTower") as BasicTower;
				PlayerPrefs.SetFloat(slot + "life"  + i, basicTower != null?basicTower.life:0);
			}
			else
				PlayerPrefs.SetFloat(slot + "life"  + i, 0);
		}
	}
	
	public void Load() {
		/*if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
			SaveData data = new SaveData ();
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
			bf.Binder = new VersionDeserializationBinder();
			
			data = (SaveData)bf.Deserialize (file);
			//SaveLoad.savedGames = (List<Game>)bf.Deserialize(file);
			
			StartGame.wave = data.wave;
			//StartGame.waveSet = data.waveSet;
			StartGame.fase = data.fase;
			StartGame.nivel = data.nivel;
			
			StartGame.energy = data.energy;
			StartGame.fat = data.fat;
			StartGame.vitamin = data.vitamin;
			StartGame.indigest = data.indigest;
			*/
		if (PlayerPrefs.HasKey("wave" + slot)) {
			StartGame.wave = PlayerPrefs.GetInt("wave" + slot);
			StartGame.fase = PlayerPrefs.GetInt("fase" + slot);
			StartGame.nivel = PlayerPrefs.GetInt("nivel" + slot);
			StartGame.actualSubWave = PlayerPrefs.GetInt("actualSubWave" + slot);
			StartGame.waveSet = 0;//PlayerPrefs.GetInt("waveSet" + slot);
			StartGame.comboPrimeiraVez = PlayerPrefs.GetInt("comboPrimeiraVez" + slot)==1?true:false;
			StartGame.gorduraPrimeiraVez = PlayerPrefs.GetInt("gorduraPrimeiraVez" + slot)==1?true:false;
			StartGame.dentePrimeiraVez = PlayerPrefs.GetInt("dentePrimeiraVez" + slot)==1?true:false;
			CallSkill.firstUsePhysical = PlayerPrefs.GetInt("firstUsePhysical" + slot)==1?true:false;
			StartGame.energy = PlayerPrefs.GetFloat("energy" + slot);
			StartGame.fat = PlayerPrefs.GetFloat("fat" + slot);
			StartGame.vitamin = PlayerPrefs.GetFloat("vitamin" + slot);
			StartGame.indigest = PlayerPrefs.GetFloat("indigest" + slot);
			StartGame.acelerarAtivado = PlayerPrefs.GetInt("acelerarAtivado" + slot);
			(GameObject.FindGameObjectWithTag("BarraGordura").GetComponent ("SpriteRenderer") as SpriteRenderer).enabled = (StartGame.fase > 1?true:false);

			GameObject botao = GameObject.FindGameObjectWithTag("AceleraButton");
			if (StartGame.acelerarAtivado == 0){
				SpriteCollection sprites = new SpriteCollection("Start");
				(botao.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("acelerar");
			}
			else{
				SpriteCollection sprites = new SpriteCollection("Pressed");
				(botao.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("AcelerarPressionado");
			}

			//StartGame.started = false;
			//StartGame.paused = 1;
			//StartGame.loose = false;
			//StartGame.firstStart = true;
			//StartGame.playAfterClose = true;
			CallSkill.creatingAcido = false;
			CallSkill.creatingSaliva = false;
			CallSkill.usingPhysicalExercise = false;
			InsertTower.activeTooth = new bool[3]{false, false, false};

			(GameObject.FindGameObjectWithTag ("IniciarButton").GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			(GameObject.FindGameObjectWithTag ("AjudaButton").GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			(GameObject.FindGameObjectWithTag ("CarregarInicialButton").GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			(GameObject.FindGameObjectWithTag ("CreditosButton").GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;

			GameObject[] target_fat = GameObject.FindGameObjectsWithTag ("FatPlace");
			for (int i = 0;i < target_fat.Length;i++) {
				FatPlace fatPlace = target_fat[i].GetComponent<FatPlace>();
				GameObject.FindGameObjectWithTag((new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPlace.fatPos]).GetComponent<Renderer>().enabled = (StartGame.fat >= fatPlace.minimalFat);
			}

			//BasicTower bTower = tower.GetComponent("BasicTower") as BasicTower;
			//Destroy(tower);

			GameObject[] target1 = GameObject.FindGameObjectsWithTag ("ComidaInserida1");
			for (int i=0;i<target1.Length;i++)
				Destroy (target1[i]);
			GameObject[] target2 = GameObject.FindGameObjectsWithTag ("ComidaInserida2");
			for (int i=0;i<target2.Length;i++)
				Destroy (target2[i]);
			GameObject[] target3 = GameObject.FindGameObjectsWithTag ("ComidaInserida3");
			for (int i=0;i<target3.Length;i++)
				Destroy (target3[i]);
			GameObject[] target4 = GameObject.FindGameObjectsWithTag ("VitaminaInserida");
			for (int i=0;i<target4.Length;i++)
				Destroy (target4[i]);

			GameObject[] target5 = GameObject.FindGameObjectsWithTag ("bullet 1");
			for (int i=0;i<target5.Length;i++)
				if ((target5[i].GetComponent("BulletAway") as BulletAway) != null)
					Destroy (target5[i]);
			GameObject[] target6 = GameObject.FindGameObjectsWithTag ("bullet 2");
			for (int i=0;i<target6.Length;i++)
				if ((target6[i].GetComponent("BulletAway") as BulletAway) != null)
					Destroy (target6[i]);
			GameObject[] target7 = GameObject.FindGameObjectsWithTag ("bullet 3");
			for (int i=0;i<target7.Length;i++)
				if ((target7[i].GetComponent("BulletAway") as BulletAway) != null)
					Destroy (target7[i]);
			GameObject[] target8 = GameObject.FindGameObjectsWithTag ("bullet 4");
			for (int i=0;i<target8.Length;i++)
				if ((target8[i].GetComponent("BulletAway") as BulletAway) != null)
					Destroy (target8[i]);
			GameObject[] target9 = GameObject.FindGameObjectsWithTag ("bullet 5");
			for (int i=0;i<target9.Length;i++)
				if ((target9[i].GetComponent("BulletAway") as BulletAway) != null)
					Destroy (target9[i]);
			GameObject[] target10 = GameObject.FindGameObjectsWithTag ("bullet 6");
			for (int i=0;i<target10.Length;i++)
				if ((target10[i].GetComponent("BulletAway") as BulletAway) != null)
					Destroy (target10[i]);
			GameObject[] target11 = GameObject.FindGameObjectsWithTag ("bullet 7");
			for (int i=0;i<target11.Length;i++)
				if ((target11[i].GetComponent("BulletAway") as BulletAway) != null)
					Destroy (target11[i]);
			GameObject[] target12 = GameObject.FindGameObjectsWithTag ("bullet 8");
			for (int i=0;i<target12.Length;i++)
				if ((target12[i].GetComponent("BulletAway") as BulletAway) != null)
					Destroy (target12[i]);

			ButtonAction.carregaTowers(false, 0);

			Transform _places = GameObject.Find("TowerPosition").transform;

			for (int i=0;i<_places.childCount;i++) {
				//if (_places.GetChild(i) == place.transform)
				//	StartGame.placeTag[i] = "Untagged";
				InsertTower insertPlace = _places.GetChild(i).GetComponent ("InsertTower") as InsertTower;
				if (insertPlace.insertedTower != null)
					Destroy (insertPlace.insertedTower);
				if (insertPlace.insertedTower2 != null)
					Destroy (insertPlace.insertedTower2);
				// (target.GetComponent("InsertTower") as InsertTower).towerObjTag = towerObject.tag;
				insertPlace.towerObj = null;
				
				/*StartGame.placeTag[i] = data.places[i];
				if (data.places[i] != "Untagged") {
					(towerPlace.GetChild(i).GetComponent("InsertTower") as InsertTower).RestoreTowerPos(data.places[i]);
					//Debug.Log("restoring pos " + i + "..." + data.places[i]);
				}*/

				StartGame.placeTag[i] = PlayerPrefs.GetString(slot + "place" + i);

				if (PlayerPrefs.GetString(slot + "place" + i) == "xDente" && !InsertTower.activeTooth[(i<14?13:16)-i]) {
					//Debug.Log ("i do dente: " + i + "..." + ((i<15?14:17)-i));
					//if () {
					_places.GetChild(i).GetComponent<Renderer>().enabled = true;
					//}
				}

				if (StartGame.placeTag[i] != "Untagged") {
					(_places.GetChild(i).GetComponent("InsertTower") as InsertTower).RestoreTowerPos(StartGame.placeTag[i], PlayerPrefs.GetFloat(slot + "life" + i));
					//Debug.Log("restoring pos " + i + "..." );
				}
				else {
					(_places.GetChild(i).GetComponent("InsertTower") as InsertTower).GetComponent<Renderer>().enabled = true;
				}
			}
			StartGame.loose = false;
			Time.timeScale = 1;
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent("StartGame") as StartGame).myTimerInterWaves = 0.09f - (StartGame.acelerarAtivado * 0.06f);
			StartGame.refreshStatus ();

			StartGame.ClearAllAudio();
			if (StartGame.nivel == 0 && StartGame.wave == 0){
				StartGame.loadingGame = true;
			}
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).myTimer = 500.423f;
			if (StartGame.fase < 3) GameObject.FindGameObjectWithTag("FaixaFase" + (StartGame.fase + 1)).GetComponent<AudioSource>().Play();
			else GameObject.FindGameObjectWithTag("Hamburguer").GetComponent<AudioSource>().Play();
			if (!StartGame.started) StartGame.StopAllAudio();
			StartGame startGame = GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame;
			Debug.Log(StartGame.activatedMenuInicial?"true":"false");
			if (StartGame.activatedMenuInicial) {
				startGame.menuInicial(false);
			}
		}
		else {
			SpriteCollection sprites = new SpriteCollection("MsgsSalvCarr");
			GameObject msg = GameObject.FindGameObjectWithTag("MsgSalvCarr");
			(msg.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("msgSave3");
			msg.GetComponent("SpriteRenderer").GetComponent<Renderer>().enabled = true;
			msg.GetComponent<Renderer>().sortingOrder = 15;
			StartGame.msgBuffer = true;
			//StartGame.msg ("Nao ha jogo salvo ate o momento");
		}
			
			//file.Close();
		//}
	}

	void OnDestroy () {
		//StartGame.numberOfSaveLoadObjectsAlive++;
	}
}