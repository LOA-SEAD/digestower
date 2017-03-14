using UnityEngine;
using System.Collections;

public class CallSkill : MonoBehaviour {

	public static bool creatingSaliva = false;
	public static bool creatingAcido = false;
	public static bool usingPhysicalExercise = false;
	public static bool firstUsePhysical = true;
	public int type = 0;
	// Use this for initialization
	void Start () {
		//StartGame.numberOfCallSkillObjectsAlive++;
	}

	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/
	void OnMouseDown() {
		ButtonAction.DisableMenu (2);
		ButtonAction.play ();
		if (type == 0 && StartGame.vitamin >= 1000 && !creatingSaliva) {
			creatingSaliva = true;
			/* ALTERACAO
			* quanto vai custar de vitamina para utilizar o especial de saliva
			*/
			StartGame.vitamin -= 1000;
			/**/
			GameObject item = GameObject.FindGameObjectWithTag("Saliva");
			Vector3 pos = new Vector3 ( -0.858f, 2.75f, 0);
			GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
			inserted.tag = "SalivaInserida";
		}
		else if (type == 1 && StartGame.vitamin >= 1000 && StartGame.fase > 0 && !creatingAcido) {
			creatingAcido = true;
			/* ALTERACAO
			* quanto vai custar de vitamina para utilizar o especial de acido
			*/
			StartGame.vitamin -= 1000;
			/**/
			GameObject item = GameObject.FindGameObjectWithTag("Acido");
			Vector3 pos = new Vector3 ( 0.45f, -6.10f, 0);
			GameObject inserted = (GameObject)Instantiate (item, pos, Quaternion.identity);
			inserted.tag = "AcidoInserido";
		}
		else if (type == 2 && StartGame.vitamin >= 1000 && StartGame.fase > 1 && !usingPhysicalExercise) {
			usingPhysicalExercise = true;
			if (firstUsePhysical) {
				Debug.Log ("xxxxxxxxxx");
				(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).carregaTela (48,52);
				Debug.Log ("bbbbbbb");
			}

			/* ALTERACAO
			* quanto vai custar de vitamina para utilizar o especial de exercicio
			*/
			StartGame.vitamin -= 1000;
			if (StartGame.fat > 20) StartGame.fat -= 20;
			else StartGame.fat = 0;

			GameObject[] target_fat = GameObject.FindGameObjectsWithTag ("FatPlace");
			for (int i = 0;i < target_fat.Length;i++) {
				FatPlace fatPlace = target_fat[i].GetComponent<FatPlace>();
				if (StartGame.fat < fatPlace.minimalFat) {
					GameObject.FindGameObjectWithTag((new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPlace.fatPos]).GetComponent<Renderer>().enabled = false;
				}
			}

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
		//StartGame.numberOfCallSkillObjectsAlive--;
	}
}
