using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour {
	public int type = 0;
	private float myTimerInt = 4.0f;
	public static bool activatedMenuTorres = false, activatedMenuEspeciais = false,
						activatedMenuPause = false, activatedMenuSaveLoad = false;
	public static bool nivelChange = true;
	// Use this for initialization
	void Start () {
	}

	public static void play() {
		StartGame startGame = GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame;
		startGame.play();
	}

	public static void pause() {
		StartGame startGame = GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame;
		startGame.pause(2);
	}

	void OnMouseDown () {
		if (type == 1 && !activatedMenuPause) {
			if (activatedMenuTorres) {
				DisableMenu (1);
				play ();
			}
			else {
				DisableMenu (2);
				pause ();
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
				menuTorre.renderer.sortingOrder = 5;
				//(menuTorre.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre1.renderer.enabled = true;
				torre1.renderer.sortingOrder = 6;
				(torre1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre2.renderer.enabled = true;
				torre2.renderer.sortingOrder = 6;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre3.renderer.enabled = true;
				torre3.renderer.sortingOrder = 6;
				if (StartGame.fase < 1)
					(torre3.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag("Torre3Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else
					(torre3.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre4.renderer.enabled = true;
				torre4.renderer.sortingOrder = 6;
				if (StartGame.fase < 1)
					(torre4.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag("Torre4Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else
					(torre4.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre5.renderer.enabled = true;
				torre5.renderer.sortingOrder = 6;
				if (StartGame.fase < 2)
					(torre5.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag("Torre5Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else
					(torre5.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre6.renderer.enabled = true;
				torre6.renderer.sortingOrder = 6;
				if (StartGame.fase < 2)
					(torre6.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag("Torre6Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else
					(torre6.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre7.renderer.enabled = true;
				torre7.renderer.sortingOrder = 6;
				if (StartGame.fase < 1)
					(torre7.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag("Torre7Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else
					(torre7.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre8.renderer.enabled = true;
				torre8.renderer.sortingOrder = 6;
				if (StartGame.fase < 2)
					(torre8.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag("Torre8Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else
				(torre8.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				dente.renderer.enabled = true;
				dente.renderer.sortingOrder = 6;
				(dente.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;

				myTimerInt = 4.0f;
				activatedMenuTorres = true;
			}
		}
		else if (type == 2 && StartGame.started && !activatedMenuPause && !activatedMenuSaveLoad) {
			if (activatedMenuEspeciais) {
				DisableMenu (2);
				play ();
			}
			else {
				DisableMenu (1);
				pause();
				GameObject menuEspecial = GameObject.FindGameObjectWithTag ("MenuEspeciais");
				//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
				GameObject saliva = GameObject.FindGameObjectWithTag ("SalivaCaller");
				GameObject acido = GameObject.FindGameObjectWithTag ("AcidoCaller");
				GameObject exercicio = GameObject.FindGameObjectWithTag ("ExerciseCaller");

				menuEspecial.renderer.enabled = true;
				menuEspecial.renderer.sortingOrder = 5;
				//(menuEspecial.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				saliva.renderer.enabled = true;
				saliva.renderer.sortingOrder = 6;
				(saliva.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				acido.renderer.enabled = true;
				acido.renderer.sortingOrder = 6;
				Debug.Log ("fase: " + StartGame.fase);
				if (StartGame.fase < 1)
					(acido.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag("AcidoDesabilitado").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else
					(acido.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				exercicio.renderer.enabled = true;
				exercicio.renderer.sortingOrder = 6;
				if (StartGame.fase < 2)
					(exercicio.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag("AtividadeFisicaDesabilitado").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else
					(exercicio.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				myTimerInt = 4.0f;
				activatedMenuEspeciais = true;
			}
		}
		else if (type == 3) {
			StartGame.started = false;
			StartGame.paused = 1;
			StartGame.firstStart = true;
			Application.LoadLevel (0);
		}
		else if (type == 4) {
			DisableMenu(3);
			play ();
		}
		else if (type == 5 || type == 6) {
			activatedMenuSaveLoad = true;
			Debug.Log ("lal: " + StartGame.paused);
			DisableMenu (3);
			Debug.Log ("l4l: " + StartGame.paused);
			// GameObject menuPause = GameObject.FindGameObjectWithTag ("MenuPause");
			//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
			GameObject save1 = GameObject.FindGameObjectWithTag ("Save1Button");
			SaveLoad saveload1 = save1.GetComponent("SaveLoad") as SaveLoad;
			saveload1.type = (type == 5?true:false);
			saveload1.slot = 1;
			GameObject save2 = GameObject.FindGameObjectWithTag ("Save2Button");
			SaveLoad saveload2 = save2.GetComponent("SaveLoad") as SaveLoad;
			saveload2.type = (type == 5?true:false);
			saveload2.slot = 2;
			GameObject save3 = GameObject.FindGameObjectWithTag ("Save3Button");
			SaveLoad saveload3 = save3.GetComponent("SaveLoad") as SaveLoad;
			saveload3.type = (type == 5?true:false);
			saveload3.slot = 3;
			GameObject continu = GameObject.FindGameObjectWithTag ("Continue2Button");

			//menuPause.renderer.enabled = true;
			//menuPause.renderer.sortingOrder = 8;
			//(menuEspecial.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			save1.renderer.enabled = true;
			save1.renderer.sortingOrder = 9;
			(save1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			save2.renderer.enabled = true;
			save2.renderer.sortingOrder = 9;
			(save2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			save3.renderer.enabled = true;
			save3.renderer.sortingOrder = 9;
			(save3.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			continu.renderer.enabled = true;
			continu.renderer.sortingOrder = 9;
			(continu.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;

		}
		else if (type == 9) {
			DisableMenu(4);
			play ();
		}
		else if (type == 10) {
			if (nivelChange) {
				StartGame.actualSubWave = 0;
				if (StartGame.wave < StartGame.maxInsertedSize[StartGame.numNivelEmFase[0]*(StartGame.fase > 0?1:0)+StartGame.numNivelEmFase[1]*(StartGame.fase > 1?1:0)+StartGame.numNivelEmFase[2]*(StartGame.fase > 2?1:0)+StartGame.nivel])
					StartGame.wave++;
				else {
					StartGame.wave = 0;
					StartGame.nivel++;
					if (StartGame.nivel > StartGame.numNivelEmFase[StartGame.fase]) {
						/*if (StartGame.fase > 2) {
							StartGame.msg ("As fases acabaram");
							nivelChange = false;
							StartGame.started = false;
							StartGame.paused = 1;
						}*/
						//else {
							StartGame.nivel = 0;
							StartGame.fase++;
						//}
					}
				}
				//StartGame.nivel++;
				Debug.Log ("fase: " + StartGame.fase + " nivel: " + StartGame.nivel + " wave: " + StartGame.wave + " actualSubWave: " + StartGame.actualSubWave);
			}
		}
		else if (type == 11) {
			if (StartGame.paused < 2 && !ButtonAction.activatedMenuPause) {
				pause ();
				
				ButtonAction.DisableMenu (1);
				ButtonAction.DisableMenu (2);
				
				GameObject menuPause = GameObject.FindGameObjectWithTag ("MenuPause");
				//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
				GameObject save = GameObject.FindGameObjectWithTag ("SaveButton");
				GameObject load = GameObject.FindGameObjectWithTag ("LoadButton");
				GameObject continu = GameObject.FindGameObjectWithTag ("ContinueButton");
				GameObject music = GameObject.FindGameObjectWithTag ("MusicButton");
				GameObject sound = GameObject.FindGameObjectWithTag ("SoundButton");
				
				menuPause.renderer.enabled = true;
				menuPause.renderer.sortingOrder = 8;
				//(menuEspecial.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				save.renderer.enabled = true;
				save.renderer.sortingOrder = 9;
				(save.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				load.renderer.enabled = true;
				load.renderer.sortingOrder = 9;
				(load.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				continu.renderer.enabled = true;
				continu.renderer.sortingOrder = 9;
				(continu.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				music.renderer.enabled = true;
				music.renderer.sortingOrder = 9;
				(music.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				sound.renderer.enabled = true;
				sound.renderer.sortingOrder = 9;
				(sound.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				
				ButtonAction.activatedMenuPause = true;
			}
		}
	}

	public static void DisableMenu (int type) {
		if (type == 1 && activatedMenuTorres && !activatedMenuPause && !activatedMenuSaveLoad) {
			//Time.timeScale = 0;
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
			//(menuTorre.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
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
			ButtonAction.activatedMenuTorres = false;
		}
		else if (type == 2 && activatedMenuEspeciais && !activatedMenuPause && !activatedMenuSaveLoad) {
			GameObject menuEspeciais = GameObject.FindGameObjectWithTag ("MenuEspeciais");
			GameObject saliva = GameObject.FindGameObjectWithTag ("SalivaCaller");
			GameObject acido = GameObject.FindGameObjectWithTag ("AcidoCaller");
			GameObject exercicio = GameObject.FindGameObjectWithTag ("ExerciseCaller");
			
			menuEspeciais.renderer.enabled = false;
			menuEspeciais.renderer.sortingOrder = 0;
			//(menuEspeciais.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			saliva.renderer.enabled = false;
			saliva.renderer.sortingOrder = 0;
			(saliva.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			acido.renderer.enabled = false;
			acido.renderer.sortingOrder = 0;
			(acido.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			exercicio.renderer.enabled = false;
			exercicio.renderer.sortingOrder = 0;
			(exercicio.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			ButtonAction.activatedMenuEspeciais = false;
		}
		else if (type == 3 && activatedMenuPause) {
			GameObject menuPause = GameObject.FindGameObjectWithTag ("MenuPause");
			//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
			GameObject save = GameObject.FindGameObjectWithTag ("SaveButton");
			GameObject load = GameObject.FindGameObjectWithTag ("LoadButton");
			GameObject continu = GameObject.FindGameObjectWithTag ("ContinueButton");
			GameObject music = GameObject.FindGameObjectWithTag ("MusicButton");
			GameObject sound = GameObject.FindGameObjectWithTag ("SoundButton");
			
			if (!activatedMenuSaveLoad) {
				menuPause.renderer.enabled = false;
				menuPause.renderer.sortingOrder = 0;
			}
			//(menuEspecial.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			save.renderer.enabled = false;
			save.renderer.sortingOrder = 0;
			(save.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			load.renderer.enabled = false;
			load.renderer.sortingOrder = 0;
			(load.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			continu.renderer.enabled = false;
			continu.renderer.sortingOrder = 0;
			(continu.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			music.renderer.enabled = false;
			music.renderer.sortingOrder = 0;
			(music.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			sound.renderer.enabled = false;
			sound.renderer.sortingOrder = 0;
			(sound.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;

			activatedMenuPause = false;
		}
		else if (type == 4 && activatedMenuSaveLoad) {
			GameObject menuPause = GameObject.FindGameObjectWithTag ("MenuPause");
			//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
			GameObject save1 = GameObject.FindGameObjectWithTag ("Save1Button");
			GameObject save2 = GameObject.FindGameObjectWithTag ("Save2Button");
			GameObject save3 = GameObject.FindGameObjectWithTag ("Save3Button");
			GameObject continu = GameObject.FindGameObjectWithTag ("Continue2Button");
			
			menuPause.renderer.enabled = false;
			menuPause.renderer.sortingOrder = 0;
			//(menuEspecial.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			save1.renderer.enabled = false;
			save1.renderer.sortingOrder = 0;
			(save1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			save2.renderer.enabled = false;
			save2.renderer.sortingOrder = 0;
			(save2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			save3.renderer.enabled = false;
			save3.renderer.sortingOrder = 0;
			(save3.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			continu.renderer.enabled = false;
			continu.renderer.sortingOrder = 0;
			(continu.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			activatedMenuSaveLoad = false;
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
					DisableMenu (1);
					activatedMenuTorres = false;
				}
				if (activatedMenuEspeciais) {
					DisableMenu (2);
					activatedMenuEspeciais = false;
				}
			}
		}
	}
}
