using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using System.Runtime.Serialization.Formatters.Binary; 
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Reflection;

[Serializable ()]
public class SaveData : ISerializable {
	public int wave, fase, nivel, waveSet;
	public float energy, vitamin, fat, indigest;
	public string[] places = new string[37];

	public SaveData () {}

	public SaveData (SerializationInfo info, StreamingContext ctxt)
	{
		wave = (int)info.GetValue ("wave", typeof(int));
		//waveSet = (int)info.GetValue ("waveSet", typeof(int));
		fase = (int)info.GetValue ("fase", typeof(int));
		nivel = (int)info.GetValue ("nivel", typeof(int));

		energy = (float)info.GetValue ("energy", typeof(float));
		fat = (float)info.GetValue ("fat", typeof(float));
		vitamin = (float)info.GetValue ("vitamin", typeof(float));
		indigest = (float)info.GetValue ("indigest", typeof(float));

		for (int i=0;i < GameObject.Find("TowerPosition").transform.childCount;i++) {
			places[i] = info.GetString ("place" + i);
		}
	}

	public void GetObjectData (SerializationInfo info, StreamingContext ctxt)
	{
		info.AddValue("wave", wave);
		//info.AddValue("waveSet", waveSet);
		info.AddValue("fase", fase);
		info.AddValue("nivel", nivel);

		info.AddValue("energy", energy);
		info.AddValue("fat", fat);
		info.AddValue("vitamin", vitamin);
		info.AddValue("indigest", indigest);

		for (int i=0;i < GameObject.Find("TowerPosition").transform.childCount;i++) {
			info.AddValue("place" + i, places[i]);
		}
	}
}