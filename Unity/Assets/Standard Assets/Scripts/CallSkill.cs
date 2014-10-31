using UnityEngine;
using System.Collections;

public class CallSkill : MonoBehaviour {

	public static bool creatingSaliva = false;
	public static bool creatingAcido = false;
	public static bool usingPhysicalExercise = false;
	public int type = 0;
	// Use this for initialization
	void Start () {
		StartGame.numberOfCallSkillObjectsAlive++;
	}

	void OnMouseDown() {
		MenuControl.DisableMenu (2);
		if (type == 0 && StartGame.vitamin > 100 && !creatingSaliva) {
			creatingSaliva = true;
			/* ALTERACAO
			* quanto vai custar de vitamina para utilizar o especial de saliva
			*/
			StartGame.vitamin -= 1000;
			/**/
			GameObject item = GameObject.FindGameObjectWithTag("Saliva");
			Vector3 pos = new Vector3 ( -0.858f, 3.39f, 0);
			GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
			inserted.tag = "SalivaInserida";
		}
		else if (type == 1 && StartGame.vitamin > 100 && StartGame.fase > 0 && !creatingAcido) {
			creatingAcido = true;
			/* ALTERACAO
			* quanto vai custar de vitamina para utilizar o especial de acido
			*/
			StartGame.vitamin -= 1000;
			/**/
			GameObject item = GameObject.FindGameObjectWithTag("Acido");
			Vector3 pos = new Vector3 ( 0.35f, -3.55f, 0);
			GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
			inserted.tag = "AcidoInserido";
		}
		else if (type == 2 && StartGame.vitamin > 100 && StartGame.fase > 1 && !usingPhysicalExercise) {
			usingPhysicalExercise = true;
			/* ALTERACAO
			* quanto vai custar de vitamina para utilizar o especial de acido
			*/
			StartGame.vitamin -= 1000;
			/**/
			GameObject[] target1 = GameObject.FindGameObjectsWithTag ("ComidaInserida1");
			GameObject[] target2 = GameObject.FindGameObjectsWithTag ("ComidaInserida2");
			GameObject[] target3 = GameObject.FindGameObjectsWithTag ("ComidaInserida3");
			GameObject[] target = new GameObject[target1.Length + target2.Length + target3.Length];
			target1.CopyTo (target, 0);
			target2.CopyTo (target, target1.Length);
			target3.CopyTo (target, target1.Length + target2.Length);
			target1 = null;
			target2 = null;
			target3 = null;

			for (int i = 0;i < target.Length;i++) {
				(target[i].GetComponent ("FoodProperties") as FoodProperties).shakeIt();
			}

			target1 = (GameObject[])FindObjectsOfType(typeof(GameObject));
			for(int i=0;i < target1.Length;i++)
				if(target1[i].tag.Contains("Torre ")) {
					//target1[i].tag = tag;
				    if (target1[i].GetComponent("BasicTower"))
				    	(target1[i].GetComponent("BasicTower") as BasicTower).shootFast();
				}
		}
	}

	void OnDestroy () {
		StartGame.numberOfCallSkillObjectsAlive--;
	}
}
