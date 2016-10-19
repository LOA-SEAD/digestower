﻿using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour {
	public int type = 0;
	private float myTimerInt = 4.0f;
	public static Sprite storre3 = null, storre4 = null, storre5 = null, storre6 = null, storre7 = null, storre8 = null, sacido = null, sexercicio = null;
	public static bool activated = false, activatedMenuEspeciais = false,
						activatedMenuPause = false, activatedMenuSaveLoad = false, activatedMenuTorres = false;
	public static bool nivelChange = true;
	private Sprite bkp;
	public AudioClip clip; /* Gracas a isso que e possivel escolher um audio na tela do Unity.
							  Para ele ser tocado, va no local que ele sera ativado e use o seguinte comando:
	                          AudioSource.PlayClipAtPoint(clip, transform.position);*/
	//public AudioClip clip2;
	// Use this for initialization
	void Start () {

	}

	public static void play() {
		StartGame startGame = GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame;
		if (StartGame.stoppedAudio) StartGame.PlayAllAudio ();
		startGame.play();
	}

	public static void pause() {
		StartGame startGame = GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame;
		startGame.pause(2);
	}

	void OnMouseEnter()
	{
		SpriteCollection sprites = null;
		if ((type > 0 && type < 10) || (type > 10 && type < 15) || type == 22 || type == 21 || type == 20 || (type >= 24 && type <= 30)) {
			bkp = (gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
			sprites = new SpriteCollection("Pressed");
		}
		if (type == 25)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("AlmanaquePressionado");
		else if (type == 1)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("TorrePressionado");
		else if (type == 2)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("EspeciaisPressionado");
		else if (type == 3)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("FecharPressionado");
		else if (type == 11)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("OpcoesPressionado");
		else if (type == 4 || type == 9)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("ContinuarPressionado");
		else if (type == 5)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("SalvarPressionado");
		else if (type == 6)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("CarregarPressionado");
		else if (type == 7)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("MusicaPressionado");
		else if (type == 8)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("SomPressionado");
		else if (type == 12)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("DireitaPressionado");
		else if (type == 13)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("EsquerdaPressionado");
		else if (type == 14 || type == 22 || type == 26 ||  type == 21)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("Fechar2Pressionado");
		else if (type == 24)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("TorresPressionado");
		else if (type == 20)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("AlimentosPressionado");
		else if (type == 27)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("IniciarPressed");
		else if (type == 28)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("AjudaPressionado");
		else if (type == 29)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("CarregarInicialPressionado");
		else if (type == 30)
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("CreditosPressionado");
		sprites = null;
	}
	void OnMouseExit()
	{
		if ((type > 0 && type < 10) || (type > 10 && type < 15) || type == 22 || type == 21 || type == 20 || (type >= 24 && type <= 30)) {
			(gameObject.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = bkp;
		}
	}

	public static void carregaTowers(bool enabled, int sortingOrder) {
		GameObject torre3 = GameObject.FindGameObjectWithTag ("Torre3");
		GameObject torre4 = GameObject.FindGameObjectWithTag ("Torre4");
		GameObject torre5 = GameObject.FindGameObjectWithTag ("Torre5");
		GameObject torre6 = GameObject.FindGameObjectWithTag ("Torre6");
		GameObject torre7 = GameObject.FindGameObjectWithTag ("Torre7");
		GameObject torre8 = GameObject.FindGameObjectWithTag ("Torre8");
		if (storre3 == null) {
			storre3 = (torre3.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
			storre4 = (torre4.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
			storre5 = (torre5.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
			storre6 = (torre6.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
			storre7 = (torre7.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
			storre8 = (torre8.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		}

		torre3.renderer.enabled = enabled;
		torre3.renderer.sortingOrder = sortingOrder;
		if (StartGame.fase < 1)
			(torre3.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
				(GameObject.FindGameObjectWithTag("Torre3Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		else {
			(torre3.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = enabled;
			(torre3.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = storre3;
		}
		torre4.renderer.enabled = enabled;
		torre4.renderer.sortingOrder = sortingOrder;
		if (StartGame.fase < 1)
			(torre4.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
				(GameObject.FindGameObjectWithTag("Torre4Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		else {
			(torre4.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = enabled;
			(torre4.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = storre4;
		}
		torre5.renderer.enabled = enabled;
		torre5.renderer.sortingOrder = sortingOrder;
		if (StartGame.fase < 2)
			(torre5.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
				(GameObject.FindGameObjectWithTag("Torre5Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		else {
			(torre5.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = enabled;
			(torre5.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = storre5;
		}
		torre6.renderer.enabled = enabled;
		torre6.renderer.sortingOrder = sortingOrder;
		if (StartGame.fase < 2)
			(torre6.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
				(GameObject.FindGameObjectWithTag("Torre6Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		else {
			(torre6.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = enabled;
			(torre6.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = storre6;
		}
		torre7.renderer.enabled = enabled;
		torre7.renderer.sortingOrder = sortingOrder;
		if (StartGame.fase < 1)
			(torre7.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
				(GameObject.FindGameObjectWithTag("Torre7Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		else {
			(torre7.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = enabled;
			(torre7.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = storre7;
		}
		torre8.renderer.enabled = enabled;
		torre8.renderer.sortingOrder = sortingOrder;
		if (StartGame.fase < 2)
			(torre8.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
				(GameObject.FindGameObjectWithTag("Torre8Desabilitada").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
		else {
			(torre8.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = enabled;
			(torre8.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = storre8;
		}
	}
	/*#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
	void OnMouseUpAsButton () { OnPointerUpAsButton(); }
	#endif
	void OnPointerUpAsButton() {*/
	void OnMouseDown() {
		if (type == 1 && !activatedMenuPause) {
			AudioSource.PlayClipAtPoint (clip, transform.position);
			if (activatedMenuTorres) {
				DisableMenu (1);
				if (StartGame.started) {
					play ();
				} else {
					StartGame.paused = 1;
					(GameObject.FindGameObjectWithTag ("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus (true);
				}
			} else {
				DisableMenu (2);
				pause ();
				GameObject menuTorre = GameObject.FindGameObjectWithTag ("MenuTorres");
				//(menuTorre.GetComponent("MenuControl") as MenuControl).EnableMenu(1);

				GameObject torre1 = GameObject.FindGameObjectWithTag ("Torre1");
				GameObject torre2 = GameObject.FindGameObjectWithTag ("Torre2");
				GameObject dente = GameObject.FindGameObjectWithTag ("Dente");
				
				menuTorre.renderer.enabled = true;
				menuTorre.renderer.sortingOrder = 5;
				(menuTorre.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre1.renderer.enabled = true;
				torre1.renderer.sortingOrder = 6;
				(torre1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				torre2.renderer.enabled = true;
				torre2.renderer.sortingOrder = 6;
				(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				carregaTowers (true, 6);

				dente.renderer.enabled = true;
				dente.renderer.sortingOrder = 6;
				(dente.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;

				myTimerInt = 4.0f;
				activatedMenuTorres = true;
			}
		} else if (type == 2 && StartGame.started && !activatedMenuPause && !activatedMenuSaveLoad) {
			AudioSource.PlayClipAtPoint (clip, transform.position);
			//StartGame.indigest = 0;
			//StartGame.energy = StartGame.maxEnergy;
			if (activatedMenuEspeciais) {
				DisableMenu (2);
				play ();
			} else {
				DisableMenu (1);
				pause ();
				GameObject menuEspecial = GameObject.FindGameObjectWithTag ("MenuEspeciais");
				//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
				GameObject saliva = GameObject.FindGameObjectWithTag ("SalivaCaller");
				GameObject acido = GameObject.FindGameObjectWithTag ("AcidoCaller");
				GameObject exercicio = GameObject.FindGameObjectWithTag ("ExerciseCaller");
				if (sacido == null) {
					sacido = (acido.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
					sexercicio = (exercicio.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				}

				menuEspecial.renderer.enabled = true;
				menuEspecial.renderer.sortingOrder = 5;
				(menuEspecial.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				saliva.renderer.enabled = true;
				saliva.renderer.sortingOrder = 6;
				(saliva.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
				acido.renderer.enabled = true;
				acido.renderer.sortingOrder = 6;
				Debug.Log ("actual: " + StartGame.fase);
				if (StartGame.fase < 1)
					(acido.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag ("AcidoDesabilitado").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else {
					Debug.Log ("Entrou!");
					(acido.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
					(acido.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sacido;
				}
				exercicio.renderer.enabled = true;
				exercicio.renderer.sortingOrder = 6;
				if (StartGame.fase < 2)
					(exercicio.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = 
						(GameObject.FindGameObjectWithTag ("AtividadeFisicaDesabilitado").GetComponent ("SpriteRenderer") as SpriteRenderer).sprite;
				else {
					(exercicio.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
					(exercicio.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sexercicio;
				}
				myTimerInt = 4.0f;
				activatedMenuEspeciais = true;
			}
		} else if (type == 3) {
			StartGame.restartGame ();
		} else if (type == 4) {
			AudioSource.PlayClipAtPoint (clip, transform.position);
			DisableMenu (3);
			if (StartGame.started)
				play ();
			if (StartGame.paused == 2) {
				StartGame.paused = 1;
				(GameObject.FindGameObjectWithTag ("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus (true);
			}
		} else if (type == 5 || type == 6) {
			AudioSource.PlayClipAtPoint (clip, transform.position);
			activatedMenuSaveLoad = true;
			DisableMenu (3);
			// GameObject menuPause = GameObject.FindGameObjectWithTag ("MenuPause");
			//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
			GameObject save1 = GameObject.FindGameObjectWithTag ("Save1Button");
			SaveLoad saveload1 = save1.GetComponent ("SaveLoad") as SaveLoad;
			saveload1.type = (type == 5 ? true : false);
			saveload1.slot = 1;
			GameObject save2 = GameObject.FindGameObjectWithTag ("Save2Button");
			SaveLoad saveload2 = save2.GetComponent ("SaveLoad") as SaveLoad;
			saveload2.type = (type == 5 ? true : false);
			saveload2.slot = 2;
			GameObject save3 = GameObject.FindGameObjectWithTag ("Save3Button");
			SaveLoad saveload3 = save3.GetComponent ("SaveLoad") as SaveLoad;
			saveload3.type = (type == 5 ? true : false);
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
		else if (type == 7) {
			AudioSource.PlayClipAtPoint (clip, transform.position);
		} 
		else if (type == 8) {
			AudioSource.PlayClipAtPoint (clip, transform.position);
		}
		else if (type == 9) {
			AudioSource.PlayClipAtPoint (clip, transform.position);
			DisableMenu(4);
			if (StartGame.started) play ();
			if (StartGame.paused == 2) {
				StartGame.paused = 1;
				(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(true);
			}
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
				Debug.Log ("fase: " + StartGame.fase + " nivel: " + StartGame.nivel + " wave: " + StartGame.wave + " actualSubWave: " + StartGame.actualSubWave + " waveSet: " + StartGame.waveSet);
			}
		}
		else if (type == 11) {
			if (StartGame.paused < 2 && !ButtonAction.activatedMenuPause) {
				AudioSource.PlayClipAtPoint(clip, transform.position);
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

				GameObject telaEscura = GameObject.FindGameObjectWithTag ("TelaEscura");
				telaEscura.renderer.enabled = true;
				telaEscura.renderer.sortingOrder = 7;
				(telaEscura.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
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
		else if (type == 12) {
			AudioSource.PlayClipAtPoint(clip, transform.position);
			if (StartGame.infoActive < StartGame.infoTela[1]) {
				//Debug.Log (StartGame.infoActive + "..." + StartGame.infoTela[1]);
				SpriteCollection sprites = new SpriteCollection("Telas");
				GameObject tela = GameObject.FindGameObjectWithTag("InfoTela");
				(tela.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("Tela" + (++StartGame.infoActive));
				sprites = null;
			}
			GameObject btnProx = GameObject.FindGameObjectWithTag ("InfoProx");
			GameObject btnAnt = GameObject.FindGameObjectWithTag ("InfoAnt");
			if (StartGame.infoActive == StartGame.infoTela[1]) {
				btnProx.renderer.enabled = false;
				btnProx.renderer.sortingOrder = 0;
				(btnProx.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			}
			else {
				btnProx.renderer.enabled = true;
				btnProx.renderer.sortingOrder = 11;
				(btnProx.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			}
			if (StartGame.infoActive == StartGame.infoTela[0]) {
				btnAnt.renderer.enabled = false;
				btnAnt.renderer.sortingOrder = 0;
				(btnAnt.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			}
			else {
				btnAnt.renderer.enabled = true;
				btnAnt.renderer.sortingOrder = 11;
				(btnAnt.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			}
		}
		else if (type == 13) {
			AudioSource.PlayClipAtPoint(clip, transform.position);
			if (StartGame.infoActive > StartGame.infoTela[0]) {
				SpriteCollection sprites = new SpriteCollection("Telas");
				GameObject tela = GameObject.FindGameObjectWithTag("InfoTela");
				(tela.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("Tela" + (--StartGame.infoActive));
				sprites = null;
			}
			GameObject btnProx = GameObject.FindGameObjectWithTag ("InfoProx");
			GameObject btnAnt = GameObject.FindGameObjectWithTag ("InfoAnt");
			if (StartGame.infoActive == StartGame.infoTela[1]) {
				btnProx.renderer.enabled = false;
				btnProx.renderer.sortingOrder = 0;
				(btnProx.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			}
			else {
				btnProx.renderer.enabled = true;
				btnProx.renderer.sortingOrder = 11;
				(btnProx.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			}
			if (StartGame.infoActive == StartGame.infoTela[0]) {
				btnAnt.renderer.enabled = false;
				btnAnt.renderer.sortingOrder = 0;
				(btnAnt.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			}
			else {
				btnAnt.renderer.enabled = true;
				btnAnt.renderer.sortingOrder = 11;
				(btnAnt.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			}
		}
		else if (type == 14) {
			GameObject tela = GameObject.FindGameObjectWithTag ("InfoTela");
			tela.renderer.enabled = false;
			tela.renderer.sortingOrder = 0;
			GameObject btnFechar = GameObject.FindGameObjectWithTag ("InfoFechar");
			GameObject btnProx = GameObject.FindGameObjectWithTag ("InfoProx");
			GameObject btnAnt = GameObject.FindGameObjectWithTag ("InfoAnt");
			//GameObject personagem = GameObject.FindGameObjectWithTag ("Personagem" + StartGame.personagemAtivo);
			btnFechar.renderer.enabled = false;
			btnFechar.renderer.sortingOrder = 0;
			btnProx.renderer.enabled = false;
			btnProx.renderer.sortingOrder = 0;
			btnAnt.renderer.enabled = false;
			btnAnt.renderer.sortingOrder = 0;
			//personagem.renderer.enabled = false;
			//personagem.renderer.sortingOrder = 0;

			(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			(btnProx.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			(btnAnt.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			//(personagem.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;

			GameObject telaEscura = GameObject.FindGameObjectWithTag ("TelaEscura");
			telaEscura.renderer.enabled = false;
			telaEscura.renderer.sortingOrder = 0;
			(telaEscura.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			if (StartGame.infoActive == 47 || StartGame.infoActive == 46 || StartGame.infoActive == 45 ||
			         StartGame.infoActive == 37 || StartGame.infoActive == 36 || StartGame.infoActive == 35) {
				StartGame.restartGame();
				StartGame.infoActive = 0;
				(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(true);
				return;
			}
			StartGame.infoActive = 0;
			Time.timeScale = 1;
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(true);
			if (StartGame.playAfterClose) {
				play ();
			}
			else {
				StartGame.paused = 1;
				if (StartGame.infoTela[0] == 1 && StartGame.infoTela[1] == 4)
					(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).carregaTela (5,10);
				else if (StartGame.infoTela[0] == 20 && StartGame.infoTela[1] == 24)
					(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).carregaTela (25,28);
				else if (StartGame.infoTela[0] == 25 && StartGame.infoTela[1] == 28)
					(GameObject.FindGameObjectWithTag("InfoFechar").GetComponent ("StartGame") as StartGame).dicasZimi (1,3);
				else if (StartGame.infoTela[0] == 48 && StartGame.infoTela[1] == 52) {
					CallSkill.firstUsePhysical = false;
					//StartGame.playAfterClose = true;
					play ();
				}
				else
					StartGame.playAfterClose = true;
			}
		}
		else if (type == 15) {
			GameObject personagem = GameObject.FindGameObjectWithTag ("Personagem" + StartGame.personagemAtivo);
			personagem.renderer.enabled = false;
			personagem.renderer.sortingOrder = 0;
			(personagem.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			GameObject personagem2 = GameObject.FindGameObjectWithTag ("Personagem" + (StartGame.personagemAtivo==1?2:1));
			personagem2.renderer.enabled = true;
			personagem2.renderer.sortingOrder = 11;
			(personagem2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			StartGame.personagemAtivo = (StartGame.personagemAtivo==1?2:1);
		}
		else if (type == 16) {

		}
		else if (type == 17) {
		}
		else if (type == 18) {
		}
		else if (type == 19) {
			GameObject foodInfo = GameObject.FindGameObjectWithTag ("FoodInfo");
			Transform _places = GameObject.Find("ComidasAlmanaque").transform;
			for (int i=0;i<_places.childCount;i++) {
				if (gameObject == _places.GetChild(i).gameObject) {
					Debug.Log ("pos: " + i + " name: "+ gameObject.tag.Substring (0, gameObject.tag.Length-4));
					SpriteCollection sprites = new SpriteCollection("Almanach");
					(foodInfo.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite ("almanaque - " + gameObject.tag.Substring (0, gameObject.tag.Length-4));
				}
			}
		}
		else if (type == 20) {
			GameObject btnAbrirAlimento = GameObject.FindGameObjectWithTag("AbrirAlimentos");
			(btnAbrirAlimento.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			btnAbrirAlimento.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnAbrirAlimento.renderer.sortingOrder = 0;
			
			GameObject btnAbrirTorre = GameObject.FindGameObjectWithTag("AbrirTorres");
			(btnAbrirTorre.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			btnAbrirTorre.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnAbrirTorre.renderer.sortingOrder = 0;

			GameObject menuTela = GameObject.FindGameObjectWithTag("MenuAlmanaque");
			menuTela.GetComponent("SpriteRenderer").renderer.enabled = false;
			menuTela.renderer.sortingOrder = 0;
			(menuTela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			
			GameObject btnFecharA = GameObject.FindGameObjectWithTag("MenuAlmanaqueFechar");
			btnFecharA.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnFecharA.renderer.sortingOrder = 0;
			(btnFecharA.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;

			GameObject infoTela = GameObject.FindGameObjectWithTag("FundoAlmanaque");
			infoTela.GetComponent("SpriteRenderer").renderer.enabled = true;
			infoTela.renderer.sortingOrder = 10;
			(infoTela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;

			GameObject foodInfo = GameObject.FindGameObjectWithTag ("FoodInfo");
			foodInfo.GetComponent("SpriteRenderer").renderer.enabled = true;
			foodInfo.renderer.sortingOrder = 11;

			GameObject btnFechar = GameObject.FindGameObjectWithTag("AlmanaqueFechar");
			btnFechar.GetComponent("SpriteRenderer").renderer.enabled = true;
			btnFechar.renderer.sortingOrder = 11;
			(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;

			Transform _places = GameObject.Find("ComidasAlmanaque").transform;
			for (int i=0;i<_places.childCount;i++) {
				(_places.GetChild(i).GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			}

			Time.timeScale = 0;
			StartGame.almanaqueAberto = true;
			StartGame.paused = 2;
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(false);
		}
		else if (type == 21) {
			GameObject infoTela = GameObject.FindGameObjectWithTag("FundoAlmanaque");
			infoTela.GetComponent("SpriteRenderer").renderer.enabled = false;
			infoTela.renderer.sortingOrder = 0;
			(infoTela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;

			GameObject foodInfo = GameObject.FindGameObjectWithTag("FoodInfo");
			foodInfo.GetComponent("SpriteRenderer").renderer.enabled = false;
			foodInfo.renderer.sortingOrder = 0;
			
			GameObject btnFechar = GameObject.FindGameObjectWithTag("AlmanaqueFechar");
			btnFechar.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnFechar.renderer.sortingOrder = 0;
			(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			
			Transform _places = GameObject.Find("ComidasAlmanaque").transform;
			for (int i=0;i<_places.childCount;i++) {
				(_places.GetChild(i).GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			}

			StartGame.almanaqueAberto = false;
			Time.timeScale = 1;
			if (StartGame.started) play ();
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(true);
		}
		else if (type == 22) {
			GameObject infoTela = GameObject.FindGameObjectWithTag("FundoAlmanaqueTorres");
			infoTela.GetComponent("SpriteRenderer").renderer.enabled = false;
			infoTela.renderer.sortingOrder = 0;
			(infoTela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			
			GameObject foodInfo = GameObject.FindGameObjectWithTag("TowerInfo");
			foodInfo.GetComponent("SpriteRenderer").renderer.enabled = false;
			foodInfo.renderer.sortingOrder = 0;
			
			GameObject btnFechar = GameObject.FindGameObjectWithTag("AlmanaqueTorresFechar");
			btnFechar.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnFechar.renderer.sortingOrder = 0;
			(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			
			Transform _places = GameObject.Find("TorresAlmanaque").transform;
			for (int i=0;i<_places.childCount;i++) {
				(_places.GetChild(i).GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			}
			
			StartGame.almanaqueAberto = false;
			Time.timeScale = 1;
			if (StartGame.started) play ();
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(true);
		}
		else if (type == 23) {
			GameObject foodInfo = GameObject.FindGameObjectWithTag ("TowerInfo");
			Transform _places = GameObject.Find("TorresAlmanaque").transform;
			for (int i=0;i<_places.childCount;i++) {
				if (gameObject == _places.GetChild(i).gameObject) {
					Debug.Log ("pos: " + i + " name: "+ gameObject.tag.Substring (0, gameObject.tag.Length-4));
					SpriteCollection sprites = new SpriteCollection("AlmanachTower");
					(foodInfo.GetComponent ("SpriteRenderer") as SpriteRenderer).sprite = sprites.GetSprite (gameObject.tag.Substring (0, gameObject.tag.Length-4));
				}
			}
		}
		else if (type == 24) {
			GameObject btnAbrirAlimento = GameObject.FindGameObjectWithTag("AbrirAlimentos");
			(btnAbrirAlimento.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			btnAbrirAlimento.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnAbrirAlimento.renderer.sortingOrder = 0;

			GameObject btnAbrirTorre = GameObject.FindGameObjectWithTag("AbrirTorres");
			(btnAbrirTorre.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			btnAbrirTorre.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnAbrirTorre.renderer.sortingOrder = 0;

			GameObject infoTelaA = GameObject.FindGameObjectWithTag("MenuAlmanaque");
			infoTelaA.GetComponent("SpriteRenderer").renderer.enabled = false;
			infoTelaA.renderer.sortingOrder = 0;
			(infoTelaA.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			
			GameObject btnFecharA = GameObject.FindGameObjectWithTag("MenuAlmanaqueFechar");
			btnFecharA.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnFecharA.renderer.sortingOrder = 0;
			(btnFecharA.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;

			GameObject infoTela = GameObject.FindGameObjectWithTag("FundoAlmanaqueTorres");
			infoTela.GetComponent("SpriteRenderer").renderer.enabled = true;
			infoTela.renderer.sortingOrder = 10;
			(infoTela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			
			GameObject foodInfo = GameObject.FindGameObjectWithTag ("TowerInfo");
			foodInfo.GetComponent("SpriteRenderer").renderer.enabled = true;
			foodInfo.renderer.sortingOrder = 11;
			
			GameObject btnFechar = GameObject.FindGameObjectWithTag("AlmanaqueTorresFechar");
			btnFechar.GetComponent("SpriteRenderer").renderer.enabled = true;
			btnFechar.renderer.sortingOrder = 11;
			(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			
			Transform _places = GameObject.Find("TorresAlmanaque").transform;
			for (int i=0;i<_places.childCount;i++) {
				(_places.GetChild(i).GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			}
			
			Time.timeScale = 0;
			StartGame.almanaqueAberto = true;
			StartGame.paused = 2;
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(false);
		}
		else if (type == 25) {
			AudioSource.PlayClipAtPoint(clip, transform.position);
			GameObject infoTela = GameObject.FindGameObjectWithTag("MenuAlmanaque");
			infoTela.GetComponent("SpriteRenderer").renderer.enabled = true;
			infoTela.renderer.sortingOrder = 10;
			(infoTela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			
			GameObject btnAbrirAlimento = GameObject.FindGameObjectWithTag("AbrirAlimentos");
			(btnAbrirAlimento.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			btnAbrirAlimento.GetComponent("SpriteRenderer").renderer.enabled = true;
			btnAbrirAlimento.renderer.sortingOrder = 11;

			GameObject btnAbrirTorre = GameObject.FindGameObjectWithTag("AbrirTorres");
			(btnAbrirTorre.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			btnAbrirTorre.GetComponent("SpriteRenderer").renderer.enabled = true;
			btnAbrirTorre.renderer.sortingOrder = 11;

			GameObject btnFechar = GameObject.FindGameObjectWithTag("MenuAlmanaqueFechar");
			btnFechar.GetComponent("SpriteRenderer").renderer.enabled = true;
			(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			btnFechar.renderer.sortingOrder = 11;
			
			Time.timeScale = 0;
			StartGame.almanaqueAberto = true;
			StartGame.paused = 2;
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(false);
		}
		else if (type == 26) {
			GameObject infoTela = GameObject.FindGameObjectWithTag("MenuAlmanaque");
			infoTela.GetComponent("SpriteRenderer").renderer.enabled = false;
			infoTela.renderer.sortingOrder = 0;
			(infoTela.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			
			GameObject btnFechar = GameObject.FindGameObjectWithTag("MenuAlmanaqueFechar");
			btnFechar.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnFechar.renderer.sortingOrder = 0;
			(btnFechar.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;

			GameObject btnAbrirAlimento = GameObject.FindGameObjectWithTag("AbrirAlimentos");
			(btnAbrirAlimento.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			btnAbrirAlimento.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnAbrirAlimento.renderer.sortingOrder = 11;
			
			GameObject btnAbrirTorre = GameObject.FindGameObjectWithTag("AbrirTorres");
			(btnAbrirTorre.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			btnAbrirTorre.GetComponent("SpriteRenderer").renderer.enabled = false;
			btnAbrirTorre.renderer.sortingOrder = 11;

			StartGame.almanaqueAberto = false;
			Time.timeScale = 1;
			if (StartGame.started) play ();
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(true);
		}
		else if (type == 27) {// Iniciar
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).menuInicial(false);

			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).GUITextStatus(true);

			StartGame.almanaqueAberto = false;
			Time.timeScale = 1;
			if (StartGame.started) play ();
			
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).carregaTela (20, 24);
		}
		else if (type == 28) {//Ajuda

		}
		else if (type == 29) {//Carregar
			activatedMenuSaveLoad = true;
			//DisableMenu (3);
			// GameObject menuPause = GameObject.FindGameObjectWithTag ("MenuPause");
			//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
			GameObject save1 = GameObject.FindGameObjectWithTag ("Save1Button");
			SaveLoad saveload1 = save1.GetComponent("SaveLoad") as SaveLoad;
			saveload1.type = false;
			saveload1.slot = 1;
			GameObject save2 = GameObject.FindGameObjectWithTag ("Save2Button");
			SaveLoad saveload2 = save2.GetComponent("SaveLoad") as SaveLoad;
			saveload2.type = false;
			saveload2.slot = 2;
			GameObject save3 = GameObject.FindGameObjectWithTag ("Save3Button");
			SaveLoad saveload3 = save3.GetComponent("SaveLoad") as SaveLoad;
			saveload3.type = false;
			saveload3.slot = 3;
			GameObject continu = GameObject.FindGameObjectWithTag ("Continue2Button");

			GameObject menuPause = GameObject.FindGameObjectWithTag ("MenuPause");
			menuPause.renderer.enabled = true;
			menuPause.renderer.sortingOrder = 13;
			GameObject telaEscura = GameObject.FindGameObjectWithTag ("TelaEscura");
			telaEscura.renderer.enabled = true;
			telaEscura.renderer.sortingOrder = 12;
			(telaEscura.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;

			//menuPause.renderer.enabled = true;
			//menuPause.renderer.sortingOrder = 8;
			//(menuEspecial.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			save1.renderer.enabled = true;
			save1.renderer.sortingOrder = 14;
			(save1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			save2.renderer.enabled = true;
			save2.renderer.sortingOrder = 14;
			(save2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			save3.renderer.enabled = true;
			save3.renderer.sortingOrder = 14;
			(save3.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
			continu.renderer.enabled = true;
			continu.renderer.sortingOrder = 14;
			(continu.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = true;
		}
		else if (type == 30) {//Creditos

		}
	}

	public static void DisableMenu (int type) {
		if (type == 1 && activatedMenuTorres && !activatedMenuPause && !activatedMenuSaveLoad) {
			//Time.timeScale = 0;
			GameObject menuTorre = GameObject.FindGameObjectWithTag ("MenuTorres");
			GameObject torre1 = GameObject.FindGameObjectWithTag ("Torre1");
			GameObject torre2 = GameObject.FindGameObjectWithTag ("Torre2");
			/*GameObject torre3 = GameObject.FindGameObjectWithTag ("Torre3");
			GameObject torre4 = GameObject.FindGameObjectWithTag ("Torre4");
			GameObject torre5 = GameObject.FindGameObjectWithTag ("Torre5");
			GameObject torre6 = GameObject.FindGameObjectWithTag ("Torre6");
			GameObject torre7 = GameObject.FindGameObjectWithTag ("Torre7");
			GameObject torre8 = GameObject.FindGameObjectWithTag ("Torre8");*/
			GameObject dente = GameObject.FindGameObjectWithTag ("Dente");
			
			menuTorre.renderer.enabled = false;
			menuTorre.renderer.sortingOrder = 0;
			(menuTorre.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre1.renderer.enabled = false;
			torre1.renderer.sortingOrder = 0;
			(torre1.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			torre2.renderer.enabled = false;
			torre2.renderer.sortingOrder = 0;
			(torre2.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
			/*torre3.renderer.enabled = false;
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
			*/
			carregaTowers (false, 0);
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
			(menuEspeciais.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
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
			//(menuEspecial.GetComponent("MenuControl") as MenuControl).EnableMenu(2);
			GameObject save = GameObject.FindGameObjectWithTag ("SaveButton");
			GameObject load = GameObject.FindGameObjectWithTag ("LoadButton");
			GameObject continu = GameObject.FindGameObjectWithTag ("ContinueButton");
			GameObject music = GameObject.FindGameObjectWithTag ("MusicButton");
			GameObject sound = GameObject.FindGameObjectWithTag ("SoundButton");
			
			if (!activatedMenuSaveLoad) {
				GameObject menuPause = GameObject.FindGameObjectWithTag ("MenuPause");
				menuPause.renderer.enabled = false;
				menuPause.renderer.sortingOrder = 0;
				GameObject telaEscura = GameObject.FindGameObjectWithTag ("TelaEscura");
				telaEscura.renderer.enabled = false;
				telaEscura.renderer.sortingOrder = 0;
				(telaEscura.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
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

			GameObject telaEscura = GameObject.FindGameObjectWithTag ("TelaEscura");
			telaEscura.renderer.enabled = false;
			telaEscura.renderer.sortingOrder = 0;
			(telaEscura.GetComponent ("BoxCollider2D") as BoxCollider2D).enabled = false;
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
		if (StartGame.paused == 0 && (activatedMenuTorres || activatedMenuEspeciais)) {
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
