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
		PlayerPrefs.SetFloat("energy" + slot,StartGame.energy);
		PlayerPrefs.SetFloat("fat" + slot,StartGame.fat);
		PlayerPrefs.SetFloat("vitamin" + slot,StartGame.vitamin);
		PlayerPrefs.SetFloat("indigest" + slot,StartGame.indigest);

		for (int i=0;i < GameObject.Find("TowerPosition").transform.childCount;i++) {
			PlayerPrefs.SetString(slot + "place" + i,Regex.Replace(StartGame.placeTagBkp[i], "\\s", String.Empty));
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
			StartGame.energy = PlayerPrefs.GetFloat("energy" + slot);
			StartGame.fat = PlayerPrefs.GetFloat("fat" + slot);
			StartGame.vitamin = PlayerPrefs.GetFloat("vitamin" + slot);
			StartGame.indigest = PlayerPrefs.GetFloat("indigest" + slot);
			InsertTower.activeTooth = new bool[3]{false, false, false};

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
					_places.GetChild(i).renderer.enabled = true;
					//}
				}

				if (StartGame.placeTag[i] != "Untagged") {
					(_places.GetChild(i).GetComponent("InsertTower") as InsertTower).RestoreTowerPos(StartGame.placeTag[i]);
					//Debug.Log("restoring pos " + i + "..." );
				}
				else {
					(_places.GetChild(i).GetComponent("InsertTower") as InsertTower).renderer.enabled = true;
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