using UnityEngine;
using System.Collections;
using System;

public class FoodProperties : MonoBehaviour {
	public float maxHealth = 100.0f;
	public float fat = 0.0f;
	public float adjustmentPos = 2.3f;
	public float vitaminUp = 0.0f;
	public bool girar = true;
	private int alternateInt = 1;
	/*[HideInInspector]*/ public float health = 0.0f;
	/*[HideInInspector]*/ public float health2 = 0.0f;
	/*[HideInInspector]*/ public float health3 = 0.0f;
	public bool healthMode, healthMode2, healthMode3;
	public float percentLifeToColorChange = 0.3f;
	public float movementSpeed = 10f;
	[HideInInspector] public bool contaminated2 = false;
	[HideInInspector] public bool contaminated3 = false;
	[HideInInspector] public bool contaminated2Bkp = false;
	[HideInInspector] public bool contaminated3Bkp = false;
	private float movementSpeedBkp;
	private Texture2D barTexture;
	
	private Vector3 worldPosition = new Vector3();
	private Vector3 screenPosition = new Vector3();
	private int healthBarHeight = 3;
	private int healthBarLeft = 45;
	private int healthBarTop = 145;
	private float myTimerInt, myTimerShakingInt;
	private bool timeFat = false;
	private bool timeShaking = false;
	//private bool dicaZimiVitamina = false;
	[HideInInspector] public float old_x, old_y, pos_x, pos_y;

	/*desconsiderar*/
	private float timerContaminated2 = 5.0f;
	private float timerContaminated3 = 5.0f;
	/**/
	private SpriteCollection sprites;

	private Vector2 scale, screen;
	public AudioClip clip; /* Gracas a isso que e possivel escolher um audio na tela do Unity.
							  Para ele ser tocado, va no local que ele sera ativado e use o seguinte comando:
	                          AudioSource.PlayClipAtPoint(clip, transform.position);*/

	public void timerFatAffectMovement (float movementSpeedInc) {
		myTimerInt = 0.2f;
		movementSpeedBkp = gameObject.GetComponent<FollowWaypoints>().movementSpeed;
		gameObject.GetComponent<FollowWaypoints>().movementSpeed *= movementSpeedInc;

		timeFat = true;
	}

	public void endPoint(int point) {
		if (point == (StartGame.fase+1) || (StartGame.fase == 3 && point == 3 && maxHealth != 920.0f)) {
			float indigestDenominator = ((health > 0?1:0) + (health2 > 0?1:0) + (health3 > 0?1:0));
			float indigestPoints = (health + health2 + health3)/(indigestDenominator<1?1:indigestDenominator);
			StartGame.indigest += indigestPoints;
			AudioSource.PlayClipAtPoint (clip, transform.position);
			if (StartGame.fase > 1) {
				if ((StartGame.fat - fat) <= StartGame.maxFat)
				{
					if ((StartGame.fat - fat) >= 0)
						StartGame.fat -= fat;
					else
						StartGame.fat = 0;
				}
				else
					StartGame.fat = StartGame.maxFat;
			}
			/*else
				StartGame.energy = StartGame.maxFat;
			*/

			GameObject[] target = GameObject.FindGameObjectsWithTag ("FatPlace");
			for (int i = 0;i < target.Length;i++) {
				FatPlace fatPlace = target[i].GetComponent<FatPlace>();
				if (StartGame.fat >= fatPlace.minimalFat) {
					GameObject.FindGameObjectWithTag((new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPlace.fatPos]).renderer.enabled = true;
					//(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).dicasZimi (25,25);
					Debug.Log ("fat enabled" + (new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPlace.fatPos] + ".." + fatPlace.fatPos);
				}
			}
			GameObject.Destroy (gameObject);
		}
		else if (maxHealth == 920.0f) {
			if (point == 3) {
				//StartGame.msg ("Voce perdeu o jogo");
				(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).carregaTela (35,37);
				//StartGame.started = false;
				return;
			}
		}
	}

	Rect ResizeGUI(Rect _rect)
	{
		float FilScreenWidth = _rect.width / 1092;
		float rectWidth = FilScreenWidth * Screen.width;
		float FilScreenHeight = _rect.height / 614;
		float rectHeight = FilScreenHeight * Screen.height;
		float rectX = (_rect.x / 1092) * Screen.width;
		float rectY = (_rect.y / 614) * Screen.height;
		
		return new Rect(rectX,rectY,rectWidth,rectHeight);
	}

	void OnTriggerEnter2D(Collider2D col){
		// Debug.Log ("aaahahha " + gameObject.tag + " laalla " + col.gameObject.tag + " kkkk ");

		// string tag = gameObject.GetComponent<FollowWaypoints>().oldTag;

		bool changed/*, changed2, changed3 = changed2 = changed*/ = false;
		if (col.gameObject.tag.Length > 4 && col.gameObject.tag.Substring (0, 5) == "xDent") {
			/* ALTERACAO
			* dano que o dente causa para todos os tipo de alimento de carboidrato (azul)
			*/
			if (healthMode)
				health -= 8;
			if (healthMode2)
				health2 -= 8;
			if (healthMode3)
				health3 -= 8;
			/**/
			// changed = true;
		}
		else if (col.gameObject.tag == "SalivaInserida" && col.gameObject.GetComponent<SalivaEspecial>().saiu && healthMode && health > 0) {
			/* ALTERACAO
			* dano que o especial de saliva causa para todos os tipos de alimentos que possuem carboidrato (azul)
			*/
			Debug.Log ("Saliva");
			health -= 60;
			(GameObject.FindGameObjectWithTag("SalivaText").GetComponent ("GUIText") as GUIText).text = ++col.gameObject.GetComponent<SalivaEspecial>().nFood + "";
			/**/
			changed = true;
		}
		else if (col.gameObject.tag == "AcidoInserido" && col.gameObject.GetComponent<AcidoEspecial>().saiu && healthMode2 && health2 > 0) {
			/* ALTERACAO
			* dano que o especial de acido causa para todos os tipos de alimentos que possuem proteina (verdes)
			*/
			Debug.Log ("Acido");
			health2 -= 60;
			(GameObject.FindGameObjectWithTag("AcidoText").GetComponent ("GUIText") as GUIText).text = ++col.gameObject.GetComponent<AcidoEspecial>().nFood + "";
			/**/
			changed = true;
		}
		else {
			float healthAux = health, healthAux2 = health2, healthAux3 = health3;
			/* ALTERACAO
			 * dano de cada torre para todos os alimentos
			 */
			if (healthMode) {
				// Debug.Log (col.gameObject.tag + "..");
				if (col.gameObject.tag == "bullet 1"/*Torre 1*/)
					health -= 25;
				else if (col.gameObject.tag == "bullet 2"/*Torre 2*/)
					health -= 30;

				if (health < 0) {
					health = 0;
					//healthMode = false;
				}
			}
			if (healthMode2) {
				//Debug.Log (".." + col.gameObject.tag);
				if (col.gameObject.tag == "bullet 3"/*Torre 3*/)
					health2 -= 25;
				else if (col.gameObject.tag == "bullet 4")
					health2 -= 30;
				else if (col.gameObject.tag == "bullet 7") {
					/* ALTERACAO
					 * tempo que a comida fica contaminada pelo tiro da torre 7
					 */
					timerContaminated2 = 5.0f;
					/**/
					/*contaminated2 = true;
					if (!contaminated3)
						gameObject.GetComponent<FollowWaypoints>().movementSpeed *= 0.5f;*/
					if (!contaminated2){
						contaminated2 = true;
						gameObject.GetComponent<FollowWaypoints>().movementSpeed *= 0.5f;
					}
				}

				if (health2 < 0) {
					health2 = 0;
					//healthMode2 = false;
				}
			}
			if (healthMode3) {
				//Debug.Log (col.gameObject.tag + " kkk");
				if (col.gameObject.tag == "bullet 5")
					health3 -= 25;
				else if (col.gameObject.tag == "bullet 6")
					health3 -= 30;
				else if (col.gameObject.tag == "bullet 8") {
					/* ALTERACAO
					 * tempo que a comida fica contaminada pelo tiro da torre 8
					 */
					timerContaminated3 = 5.0f;
					/*contaminated3 = true;
					if (!contaminated2)
						gameObject.GetComponent<FollowWaypoints>().movementSpeed *= 0.5f;*/
					if (!contaminated3){
						contaminated3 = true;
						gameObject.GetComponent<FollowWaypoints>().movementSpeed *= 0.5f;
					}
				}

				if (health3 < 0) {
					health3 = 0;
					//healthMode3 = false;
				}
			}
			/**/

			if (healthAux != health || healthAux2 != health2 || healthAux3 != health3 ||
			    contaminated2 != contaminated2Bkp || contaminated3 != contaminated3Bkp) {
				changed = true;
				if (contaminated2 != contaminated2Bkp)
					contaminated2Bkp = true;
				if (contaminated3 != contaminated3Bkp)
					contaminated3Bkp = true;
			}
		}

		if (changed && col.gameObject.tag.Split (' ')[0] == "bullet") {
			GameObject.Destroy (col.gameObject);
			//col.gameObject.GetComponent<BulletAway>().bulletAway = true;
		}

		/*if (changed2 && health2 > 0)
			changed2 = false;
		if (changed3 && health3 > 0)
			changed3 = false;
		if (changed && health > 0)
			changed = false;

		//if ((changed2 && health2 == 0) || (health3 == 0 && changed3) || (health == 0 && changed)) {*/

		if (health <= 0 && health2 <= 0 && health3 <= 0) {
		/*if ((!healthMode && !healthMode3 && healthMode2 && health2 <= 0) ||
			    (!healthMode && !healthMode2 && healthMode3 && health3 <= 0) ||
			    (!healthMode2 && !healthMode3 && healthMode && health <= 0) ||
		    	(healthMode && healthMode2 && healthMode3 && health <= 0 && health2 <= 0 && health3 <= 0) ||
		    	(healthMode && healthMode2 && !healthMode3 && health <= 0 && health2 <= 0) ||
				(healthMode2 && healthMode3 && !healthMode && health2 <= 0 && health3 <= 0)) {*/
			//GameObject game = GameObject.FindGameObjectWithTag ("StartButton");
			//StartGame gameProperties = game.GetComponent<StartGame>();
			foodDied();
		}
	}

	void foodDied () {
		if ((StartGame.energy + maxHealth) <= StartGame.maxEnergy)
			StartGame.energy += maxHealth;
		else
			StartGame.energy = StartGame.maxEnergy;
		// StartGame.vitamin += vitaminUp;
		
		if (vitaminUp > 0) {
			GameObject item = GameObject.FindGameObjectWithTag(vitaminUp<2?"Vitamina":"Vitamina2");
			GameObject inserted = (GameObject)Instantiate (item, transform.position, Quaternion.identity);
			
			FollowWaypoints wayPoint = gameObject.GetComponent<FollowWaypoints>();
			FollowWaypoints wayPointNew = inserted.AddComponent<FollowWaypoints>();
			VitaminUp vita = inserted.GetComponent<VitaminUp>() as VitaminUp;
			//Chama mais uma dica da Zimi
			/*if (!dicaZimiVitamina){
				dicaZimiVitamina = true;
				(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).dicasZimi (11,11);
			}*/
			/* ALTERACAO
				 * valor de ganho da vitamina que sai de cada alimento
				 */
			vita.vitaminUp = vitaminUp;
			/**/
			wayPointNew.vita = vita;
			wayPointNew.oldTag = inserted.tag;
			inserted.tag = "VitaminaInserida";
			wayPointNew._targetWaypoint = wayPoint._targetWaypoint;
			wayPointNew.movementSpeed = 5.0f;

			GameObject item2 = GameObject.FindGameObjectWithTag("Particulas1");
			GameObject inserted2 = (GameObject)Instantiate (item2, transform.position, Quaternion.identity);

			//FollowWaypoints wayPointNew2 = inserted2.AddComponent<FollowWaypoints>();
			//Particula particula = inserted2.GetComponent<Particula>() as Particula;
			/* ALTERACAO
				 * valor de ganho da vitamina que sai de cada alimento
				 */
			/**/
			//wayPointNew2.part = particula;
			//wayPointNew2.oldTag = inserted2.tag;
			inserted2.tag = "ParticulaInserida";

			//wayPointNew2._targetWaypoint = wayPoint._targetWaypoint;
		}

		if (maxHealth == 920.0f) {
			(GameObject.FindGameObjectWithTag("StartButton").GetComponent ("StartGame") as StartGame).carregaTela(45,47);
		}
		GameObject.Destroy (gameObject);
	}
	
	void Start () {
		sprites = new SpriteCollection ("Meleca");
		barTexture = new Texture2D (1, 1);
		//StartGame.numberOfFoodPropertiesObjectsAlive++;

		// health = health2 = health3 = maxHealth;
		healthMode = healthMode2 = healthMode3 = false;
		if (health > 0)
			healthMode = true;
		if (health2 > 0)
			healthMode2 = true;
		if (health3 > 0)
			healthMode3 = true;
	}
	void OnDestroy () {
		sprites = null;
		//StartGame.numberOfFoodPropertiesObjectsAlive--;
	}
	
	void OnGUI() {
		if (StartGame.started && StartGame.paused < 2) {
			worldPosition = new Vector3(transform.position.x, transform.position.y + adjustmentPos, transform.position.z);
			screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

			Ray ray = new Ray (Camera.main.transform.position, transform.forward);
			RaycastHit hit;

			float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

			if (!Physics.Raycast(ray, out hit, distance))
			{
					//try {

					/*Vector3 healthBarWorldPosition = transform.position;
					healthBarWorldPosition.y += 20f;
					Vector3 healthBarScreenPosition = Camera.main.WorldToScreenPoint(healthBarWorldPosition);

					Debug.Log (healthBarScreenPosition.y + ".." + screenPosition.y);

					float top = Screen.height - (healthBarScreenPosition.y + (healthBarTop / 2));*/

					barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
					barTexture.Apply ();

					int posy = 1;
					screen = new Vector2 (Screen.width, Screen.height);
					scale = new Vector2(screen.x/1092, screen.y/614);

					if (healthMode) {
						barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
						barTexture.Apply ();
						GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2 - 1,
					                    		 Screen.height - screenPosition.y + (healthBarTop)*scale.y-1,
					                    		 44*scale.x, healthBarHeight*scale.y+2), barTexture);
						barTexture.SetPixel(0, 0, (health < percentLifeToColorChange*maxHealth) ? ColorX.HexToRGB("88c5f5") : ColorX.HexToRGB("88c5f5"));
						barTexture.Apply ();
						if (health > 0)
							GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2,
						                         Screen.height - screenPosition.y + healthBarTop*scale.y,
						                         (health*42*scale.x)/maxHealth-0.5f, healthBarHeight*scale.y), barTexture);
						posy += 5;
					}
					if (healthMode2) {
						barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
						barTexture.Apply ();
						GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2 - 1,
						                         Screen.height - screenPosition.y + (healthBarTop)*scale.y-posy,
						                         44*scale.x, healthBarHeight*scale.y+2), barTexture);
						barTexture.SetPixel(0, 0, (health2 < percentLifeToColorChange*maxHealth) ? ColorX.HexToRGB("a5cc2e") : ColorX.HexToRGB("a5cc2e"));
						barTexture.Apply ();
						if (health2 > 0)
						GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2,
						                         Screen.height - screenPosition.y + (healthBarTop)*scale.y-posy+1,
						                         (health2*42*scale.x)/maxHealth-0.5f, healthBarHeight*scale.y), barTexture);
						posy += 5;
					}
					if (healthMode3) {
						barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
						barTexture.Apply ();
						GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2 - 1,
						                         Screen.height - screenPosition.y + (healthBarTop)*scale.y-posy,
						                         44*scale.x, healthBarHeight*scale.y+2), barTexture);
						barTexture.SetPixel(0, 0, (health3 < percentLifeToColorChange*maxHealth) ? ColorX.HexToRGB("a966e2") : ColorX.HexToRGB("a966e2"));
						barTexture.Apply ();
						if (health3 > 0)
						GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2,
						                         Screen.height - screenPosition.y + (healthBarTop)*scale.y-posy+1,
						                         (health3*42*scale.x)/maxHealth-0.5f, healthBarHeight*scale.y), barTexture);
					}

					Sprite meleca = null;
					if (contaminated2 && contaminated3) meleca = sprites.GetSprite("MelecaRV");
					else if (contaminated2) meleca = sprites.GetSprite("MelecaV");
					else if (contaminated3) meleca = sprites.GetSprite("MelecaR");

					if (meleca != null) {
						Texture t = meleca.texture;

						Rect tr = meleca.textureRect;
						Rect r = new Rect(tr.x / t.width, tr.y / t.height, tr.width / t.width, tr.height / t.height );

						//Color guiColor = Color.white;
						//guiColor.a = 0.5f;
						//GUI.color = guiColor;
						GUI.DrawTextureWithTexCoords(new Rect(screenPosition.x - (22/*55*/*scale.x), Screen.height - screenPosition.y + (152f/*295*/*scale.y), tr.width*scale.y*1.42f, tr.height*scale.y*1.42f), t, r);
					}
				//} catch (NullReferenceException) {
				//}
				// http://forum.unity3d.com/threads/health-bar-above-ememy.81560/
			}
		}
	}

	public void shakeIt () {
		myTimerShakingInt = 2.0f;
		timeShaking = true;
		old_x = 0;
		old_y = 0;
		pos_x = transform.position.x;
		pos_y = transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		if (StartGame.paused == 0) {
			if (timeShaking) {
				if (myTimerShakingInt > 0) {
					myTimerShakingInt -= Time.deltaTime;
					alternateInt *= -1;
					transform.position = new Vector3 (pos_x+alternateInt*old_x, pos_y+alternateInt*old_y, 0);
					old_x = UnityEngine.Random.Range(0,0.035f);
					old_y = UnityEngine.Random.Range(0,0.035f);
				}
				else {
					//transform.position = new Vector3 (old_x, old_y, 0);
					timeShaking = false;
				}
			}

			if (timeFat && !CallSkill.usingPhysicalExercise) {
				if (myTimerInt > 0)
					myTimerInt -= Time.deltaTime;
				else {
					gameObject.GetComponent<FollowWaypoints>().movementSpeed = movementSpeedBkp;
					// Debug.Log ("movementSpeed after: " + gameObject.GetComponent<FollowWaypoints>().movementSpeed + "/ before: " + movementSpeedBkp + "/ tag: " + gameObject.GetComponent<FollowWaypoints>().oldTag);
					timeFat = false;
				}
			}

			if (contaminated2) {
				if (timerContaminated2 > 0) {
					timerContaminated2 -= Time.deltaTime;
					/* ALTERACAO
				 	* quantidade de vida que tira as melecas/gosmas da Torre 7
				 	*/
					if (health2 > 0) health2 -= 0.3f;
					/**/
				}
				else {
					/*contaminated2 = false;
					if (!contaminated3)
						gameObject.GetComponent<FollowWaypoints>().movementSpeed *= 2.0f;*/
					if (contaminated2){
						contaminated2 = false;
						gameObject.GetComponent<FollowWaypoints>().movementSpeed *= 2.0f;
					}
				}
			}
			if (contaminated3) {
				if (timerContaminated3 > 0) {
					timerContaminated3 -= Time.deltaTime;
					/* ALTERACAO
				 	* quantidade de vida que tira as melecas/gosmas da Torre 8
				 	*/
					if (health3 > 0) health3 -= 0.3f;
				}
				else {
					/*contaminated3 = false;
					if (!contaminated2)
						gameObject.GetComponent<FollowWaypoints>().movementSpeed *= 2.0f;*/
					if (contaminated3){
						contaminated3 = false;
						gameObject.GetComponent<FollowWaypoints>().movementSpeed *= 2.0f;
					}
				}
			}
			if (health <= 0 && health2 <= 0 && health3 <= 0) foodDied();
		}
	}
}