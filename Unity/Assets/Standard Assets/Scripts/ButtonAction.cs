using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour {
	public int type = 0;
	private float myTimerInt = 4.0f;
	private bool activatedMenuTorres, activatedMenuEspeciais;
	// Use this for initialization
	void Start () {
		activatedMenuTorres = false;
		activatedMenuEspeciais = false;
	}

	void OnMouseDown () {
		myTimerInt = 4.0f;
		if (type == 1) {
			if (activatedMenuTorres) {
				MenuControl.DisableMenu (1);
				activatedMenuTorres = false;
			}
			else {
				MenuControl.DisableMenu (2);
				GameObject menuTorre = GameObject.FindGameObjectWithTag ("MenuTorres");
				//(menuTorre.GetComponent("MenuControl") as MenuControl).EnableMenu(1);
				GameObject torre1 = GameObject.FindGameObjectWithTag ("Torre1");
				GameObject torre2 = GameObject.FindGameObjectWithTag ("Torre2");
				GameObject torre3 = GameObject.FindGameObjectWithTag ("Torre3");
				GameObject torre4 = GameObject.FindGameObjectWithTag ("Torre4");
				GameObject torre5 = GameObject.FindGameObjectWithTag ("Torre5");
				GameObject torre6 = GameObject.FindGameObjectWithTag ("Torre6");
				GameObject torre7 = GameObject.FindGameObjectWithTag ("Torre7");
				GameObject torre8 = GameObject.FindGameObjectWithTag ("Torre8");
				GameObject dente = GameObject.FindGameObjectWithTag ("Dente");

				menuTorre.renderer.enabled = true;
				menuTorre.renderer.sortingOrder = 3;
				torre1.renderer.enabled = true;
				torre1.renderer.sortingOrder = 4;
				(torre1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre2.renderer.enabled = true;
				torre2.renderer.sortingOrder = 4;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre3.renderer.enabled = true;
				torre3.renderer.sortingOrder = 4;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre4.renderer.enabled = true;
				torre4.renderer.sortingOrder = 4;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre5.renderer.enabled = true;
				torre5.renderer.sortingOrder = 4;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre6.renderer.enabled = true;
				torre6.renderer.sortingOrder = 4;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre7.renderer.enabled = true;
				torre7.renderer.sortingOrder = 4;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre8.renderer.enabled = true;
				torre8.renderer.sortingOrder = 4;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				dente.renderer.enabled = true;
				dente.renderer.sortingOrder = 4;
				(dente.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				activatedMenuTorres = true;
			}
		}
		else if (type == 2) {
			if (activatedMenuEspeciais) {
				MenuControl.DisableMenu (2);
				activatedMenuEspeciais = false;
			}
			else {
				MenuControl.DisableMenu (1);
				GameObject menuEspecial = GameObject.FindGameObjectWithTag ("MenuEspeciais");
				//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
				GameObject saliva = GameObject.FindGameObjectWithTag ("SalivaCaller");
				GameObject acido = GameObject.FindGameObjectWithTag ("AcidoCaller");
				GameObject exercicio = GameObject.FindGameObjectWithTag ("ExerciseCaller");
				
				menuEspecial.renderer.enabled = true;
				menuEspecial.renderer.sortingOrder = 3;
				saliva.renderer.enabled = true;
				saliva.renderer.sortingOrder = 4;
				acido.renderer.enabled = true;
				acido.renderer.sortingOrder = 4;
				exercicio.renderer.enabled = true;
				exercicio.renderer.sortingOrder = 4;
				activatedMenuEspeciais = true;
			}
		}
	}

	
	// Update is called once per frame
	void Update () {
		if (activatedMenuTorres || activatedMenuEspeciais) {
			if (myTimerInt > 0) {
				myTimerInt -= Time.deltaTime;
			}
			else {
				if (activatedMenuTorres) {
					MenuControl.DisableMenu (1);
					activatedMenuTorres = false;
				}
				if (activatedMenuEspeciais) {
					MenuControl.DisableMenu (2);
					activatedMenuEspeciais = false;
				}
			}
		}
	}
}
