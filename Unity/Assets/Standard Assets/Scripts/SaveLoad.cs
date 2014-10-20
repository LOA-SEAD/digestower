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

	void OnMouseDown() {
		if (type) {
			Debug.Log("saving");
			Save ();
			StartGame.msg("Jogo salvo com sucesso!");
		}
		else {
			Debug.Log("loading");
			if (!StartGame.started) Load ();
			else StartGame.msg("Carregue antes de iniciar");
		}
	}
	
	public static void Save() {
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
	}
	
	public static void Load() {
		if(File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
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
			
			Transform towerPlace = GameObject.Find("TowerPosition").transform;
			for (int i=0;i < GameObject.Find("TowerPosition").transform.childCount;i++) {
				StartGame.placeTag[i] = data.places[i];
				(towerPlace.GetChild(i).GetComponent("InsertTower") as InsertTower).RestoreTowerPos(data.places[i]);
				Debug.Log("restoring pos " + i + "..." + data.places[i]);
			}
			
			file.Close();
		}
	}
}