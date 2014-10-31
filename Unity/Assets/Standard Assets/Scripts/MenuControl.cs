using UnityEngine;
using System.Collections;

public class MenuControl : MonoBehaviour {
	public int defaultType = 0;

	public void EnableMenu(int type) {
		//if (defaultType == type) {
		//	Time.timeScale = 1;
		//}
	}
	public static void DisableMenu (int type) {
		if (type == 1) {
			Time.timeScale = 0;
			GameObject menuTorre = GameObject.FindGameObjectWithTag ("MenuTorres");
			GameObject torre1 = GameObject.FindGameObjectWithTag ("Torre1");
			GameObject torre2 = GameObject.FindGameObjectWithTag ("Torre2");
			GameObject torre3 = GameObject.FindGameObjectWithTag ("Torre3");
			GameObject torre4 = GameObject.FindGameObjectWithTag ("Torre4");
			GameObject torre5 = GameObject.FindGameObjectWithTag ("Torre5");
			GameObject torre6 = GameObject.FindGameObjectWithTag ("Torre6");
			GameObject torre7 = GameObject.FindGameObjectWithTag ("Torre7");
			GameObject torre8 = GameObject.FindGameObjectWithTag ("Torre8");
			GameObject dente = GameObject.FindGameObjectWithTag ("Dente");

			menuTorre.renderer.enabled = false;
			menuTorre.renderer.sortingOrder = 0;
			torre1.renderer.enabled = false;
			torre1.renderer.sortingOrder = 0;
			(torre1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre2.renderer.enabled = false;
			torre2.renderer.sortingOrder = 0;
			(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre3.renderer.enabled = false;
			torre3.renderer.sortingOrder = 0;
			(torre3.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre4.renderer.enabled = false;
			torre4.renderer.sortingOrder = 0;
			(torre4.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre5.renderer.enabled = false;
			torre5.renderer.sortingOrder = 0;
			(torre5.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre6.renderer.enabled = false;
			torre6.renderer.sortingOrder = 0;
			(torre6.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre7.renderer.enabled = false;
			torre7.renderer.sortingOrder = 0;
			(torre7.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre8.renderer.enabled = false;
			torre8.renderer.sortingOrder = 0;
			(torre8.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			dente.renderer.enabled = false;
			dente.renderer.sortingOrder = 0;
			(dente.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
		}
		else if (type == 2) {
			Time.timeScale = 0;
			GameObject menuEspeciais = GameObject.FindGameObjectWithTag ("MenuEspeciais");
			GameObject saliva = GameObject.FindGameObjectWithTag ("SalivaCaller");
			GameObject acido = GameObject.FindGameObjectWithTag ("AcidoCaller");
			GameObject exercicio = GameObject.FindGameObjectWithTag ("ExerciseCaller");
			
			menuEspeciais.renderer.enabled = false;
			menuEspeciais.layer = 0;
			saliva.renderer.enabled = false;
			menuEspeciais.layer = 0;
			acido.renderer.enabled = false;
			menuEspeciais.layer = 0;
			exercicio.renderer.enabled = false;
			menuEspeciais.layer = 0;
		}
	}
	// Use this for initialization
	void Start () {
		gameObject.renderer.enabled = false;
	}

	// Update is called once per frame
	void Update () {

	}
}
