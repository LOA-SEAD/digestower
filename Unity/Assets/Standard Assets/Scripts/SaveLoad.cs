using UnityEngine;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System;

public class SaveLoad : MonoBehaviour {
	public bool type = false;
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
		StartGame.numberOfSaveLoadObjectsAlive++;
	}

	void OnMouseDown() {
		if (type) {
			if (StartGame.started) {
				Debug.Log("saving");
				Save ();
				StartGame.msg("Jogo salvo com sucesso!");
			}
			else {
				StartGame.msg("Inicie o jogo antes de salvar!");
			}
		}
		else {
			Debug.Log("loading");
			if (!StartGame.started) Load ();
			else StartGame.msg("Carregue antes de iniciar");
		}
	}
	
	public static void Save() {
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

		PlayerPrefs.SetInt("wave",StartGame.wave);
		PlayerPrefs.SetInt("fase",StartGame.fase);
		PlayerPrefs.SetInt("nivel",StartGame.nivel);
		PlayerPrefs.SetFloat("energy",StartGame.energy);
		PlayerPrefs.SetFloat("fat",StartGame.fat);
		PlayerPrefs.SetFloat("vitamin",StartGame.vitamin);
		PlayerPrefs.SetFloat("indigest",StartGame.indigest);

		for (int i=0;i < GameObject.Find("TowerPosition").transform.childCount;i++) {
			PlayerPrefs.SetString("place" + i,StartGame.placeTagBkp[i]);
		}
	}
	
	public static void Load() {
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
		if (PlayerPrefs.HasKey("wave")) {
			StartGame.wave = PlayerPrefs.GetInt("wave");
			StartGame.fase = PlayerPrefs.GetInt("fase");
			StartGame.nivel = PlayerPrefs.GetInt("nivel");
			StartGame.energy = PlayerPrefs.GetFloat("energy");
			StartGame.fat = PlayerPrefs.GetFloat("fat");
			StartGame.vitamin = PlayerPrefs.GetFloat("vitamin");
			StartGame.indigest = PlayerPrefs.GetFloat("indigest");
			InsertTower.activeTooth = new bool[3]{false, false, false};

			//BasicTower bTower = tower.GetComponent("BasicTower") as BasicTower;
			//Destroy(tower);
			
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

				StartGame.placeTag[i] = PlayerPrefs.GetString("place" + i);
				
				if (PlayerPrefs.GetString("place" + i) == "xDente" && !InsertTower.activeTooth[(i<15?14:17)-i]) {
					//Debug.Log ("i do dente: " + i + "..." + ((i<15?14:17)-i));
					//if () {
					_places.GetChild(i).renderer.enabled = true;
					//}
				}

				if (StartGame.placeTag[i] != "Untagged") {
					(_places.GetChild(i).GetComponent("InsertTower") as InsertTower).RestoreTowerPos(StartGame.placeTag[i]);
					//Debug.Log("restoring pos " + i + "..." );
				}
			}
		}
		else {
			StartGame.msg ("Nao ha jogo salvo ate o momento");
		}
			
			//file.Close();
		//}
	}

	void OnDestroy () {
		StartGame.numberOfSaveLoadObjectsAlive++;
	}
}