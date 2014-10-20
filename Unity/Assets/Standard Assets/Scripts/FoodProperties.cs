using UnityEngine;
using System.Collections;
using System;

public class FoodProperties : MonoBehaviour {
	public float maxHealth = 100.0f;
	public float fat = 0.0f;
	public float adjustmentPos = 2.3f;
	public float vitaminUp = 0.0f;
	/*[HideInInspector]*/ public float health = 0.0f;
	/*[HideInInspector]*/ public float health2 = 0.0f;
	/*[HideInInspector]*/ public float health3 = 0.0f;
	public bool healthMode, healthMode2, healthMode3;
	public float percentLifeToColorChange = 0.3f;
	public float movementSpeed = 10f;
	public bool contaminated2 = false;
	public bool contaminated3 = false;
	public bool contaminated2Bkp = false;
	public bool contaminated3Bkp = false;
	private float movementSpeedBkp;
	
	private Vector3 worldPosition = new Vector3();
	private Vector3 screenPosition = new Vector3();
	private int healthBarHeight = 3;
	private int healthBarLeft = 45;
	private int healthBarTop = 145;
	private float myTimerInt;
	private bool timeFat = false;

	private float timerContaminated2 = 5.0f;
	private float timerContaminated3 = 5.0f;

	private Vector2 scale, screen;

	public void timerFatAffectMovement (float movementSpeedInc) {
		myTimerInt = 0.2f;
		movementSpeedBkp = gameObject.GetComponent<FollowWaypoints>().movementSpeed;
		gameObject.GetComponent<FollowWaypoints>().movementSpeed *= movementSpeedInc;

		timeFat = true;
	}

	public void endPoint(int point) {
		if (point == (StartGame.fase+1)) {
			StartGame.indigest += (health + health2 + health3)/((health > 0?1:0) + (health2 > 0?1:0) + (health3 > 0?1:0));
			StartGame.fat -= fat;
			GameObject[] target = GameObject.FindGameObjectsWithTag ("FatPlace");
			for (int i = 0;i < target.Length;i++) {
				FatPlace fatPlace = target[i].GetComponent<FatPlace>();
				if (StartGame.fat >= fatPlace.minimalFat) {
					GameObject.FindGameObjectWithTag((new string[3]{"TopFat", "RightFat", "LeftFat"})[fatPlace.fatPos]).renderer.enabled = true;
				}
			}
			if (StartGame.fat < 0)
				StartGame.fat = 0;
			GameObject.Destroy (gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		// Debug.Log ("aaahahha " + gameObject.tag + " laalla " + col.gameObject.tag + " kkkk ");

		// string tag = gameObject.GetComponent<FollowWaypoints>().oldTag;

		bool changed/*, changed2, changed3 = changed2 = changed*/ = false;
		if (col.gameObject.tag.Length > 4 && col.gameObject.tag.Substring (0, 5) == "xDent") {
			health -= 3;
			changed = true;
		}
		else if (col.gameObject.tag == "SalivaInserida" && col.gameObject.GetComponent<SalivaEspecial>().saiu && healthMode && health > 0) {
			health -= 20;
			changed = true;
		}
		else if (col.gameObject.tag == "AcidoInserido" && col.gameObject.GetComponent<AcidoEspecial>().saiu && healthMode2 && health2 > 0) {
			health2 -= 25;
			changed = true;
		}
		else {
			float healthAux = health, healthAux2 = health2, healthAux3 = health3;
			if (healthMode) {
				// Debug.Log (col.gameObject.tag + "..");
				if (col.gameObject.tag == "bullet 1")
					health -= 10;
				else if (col.gameObject.tag == "bullet 2")
					health -= 20;

				if (health < 0) {
					health = 0;
					//healthMode = false;
				}
			}
			if (healthMode2) {
				//Debug.Log (".." + col.gameObject.tag);
				if (col.gameObject.tag == "bullet 3")
					health2 -= 10;
				else if (col.gameObject.tag == "bullet 4")
					health2 -= 20;
				else if (col.gameObject.tag == "bullet 7") {
					timerContaminated2 = 5.0f;
					contaminated2 = true;
				}

				if (health2 < 0) {
					health2 = 0;
					//healthMode2 = false;
				}
			}
			if (healthMode3) {
				//Debug.Log (col.gameObject.tag + " kkk");
				if (col.gameObject.tag == "bullet 5")
					health3 -= 10;
				else if (col.gameObject.tag == "bullet 6")
					health3 -= 20;
				else if (col.gameObject.tag == "bullet 8") {
					timerContaminated3 = 5.0f;
					contaminated3 = true;
				}

				if (health3 < 0) {
					health3 = 0;
					//healthMode3 = false;
				}
			}
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

		if ((!healthMode && !healthMode3 && healthMode2 && health2 <= 0) ||
			    (!healthMode && !healthMode2 && healthMode3 && health3 <= 0) ||
			    (!healthMode2 && !healthMode3 && healthMode && health <= 0) ||
		    	(healthMode && healthMode2 && healthMode3 && health <= 0 && health2 <= 0 && health3 <= 0) ||
		    	(healthMode && healthMode2 && !healthMode3 && health <= 0 && health2 <= 0) ||
				(healthMode2 && healthMode3 && !healthMode && health2 <= 0 && health3 <= 0)) {
			//GameObject game = GameObject.FindGameObjectWithTag ("StartButton");
			//StartGame gameProperties = game.GetComponent<StartGame>();
			StartGame.energy += maxHealth;
			StartGame.vitamin += vitaminUp;
			
			GameObject item = GameObject.FindGameObjectWithTag("Vitamina");
			GameObject inserted = (GameObject)Instantiate (item, transform.position, Quaternion.identity);

			FollowWaypoints wayPoint = gameObject.GetComponent<FollowWaypoints>();
			FollowWaypoints wayPointNew = inserted.AddComponent<FollowWaypoints>();
			wayPointNew.vita = inserted.GetComponent<VitaminUp>();
			wayPointNew.oldTag = inserted.tag;
			inserted.tag = "VitaminaInserida";
			wayPointNew._targetWaypoint = wayPoint._targetWaypoint;

			GameObject.Destroy (gameObject);
		}
	}
	
	void Start () {
		StartGame.numberOfFoodPropertiesObjectsAlive++;
		// health = health2 = health3 = maxHealth;
		healthMode = healthMode2 = healthMode3 = false;
		if (health > 0)
			healthMode = true;
		if (health2 > 0)
			healthMode2 = true;
		if (health3 > 0)
			healthMode3 = true;

		screen = new Vector2 (Screen.width, Screen.height);
		scale = new Vector2(screen.x/1092, screen.y/614);
	}
	void OnDestroy () {
		StartGame.numberOfFoodPropertiesObjectsAlive--;
	}
	
	void OnGUI() {
		worldPosition = new Vector3(transform.position.x, transform.position.y + adjustmentPos, transform.position.z);
		screenPosition = Camera.main.WorldToScreenPoint(worldPosition);

		Ray ray = new Ray (Camera.main.transform.position, transform.forward);
		RaycastHit hit;

		float distance = Vector3.Distance(Camera.main.transform.position, transform.position);

		if (!Physics.Raycast(ray, out hit, distance))
		{
				try {

				/*Vector3 healthBarWorldPosition = transform.position;
				healthBarWorldPosition.y += 20f;
				Vector3 healthBarScreenPosition = Camera.main.WorldToScreenPoint(healthBarWorldPosition);

				Debug.Log (healthBarScreenPosition.y + ".." + screenPosition.y);

				float top = Screen.height - (healthBarScreenPosition.y + (healthBarTop / 2));*/

				Texture2D barTexture = new Texture2D (1, 1);
				barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
				barTexture.Apply ();

				int posy = 1;

				if (healthMode) {
					barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
					barTexture.Apply ();
					GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2 - 1,
				                    		 Screen.height - screenPosition.y + (healthBarTop-1)*scale.y,
				                    		 44*scale.x, healthBarHeight*scale.y+2), barTexture);
					barTexture.SetPixel(0, 0, (health < percentLifeToColorChange*maxHealth) ? ColorX.HexToRGB("88c5f5") : ColorX.HexToRGB("88c5f5"));
					barTexture.Apply ();
					GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2,
					                         Screen.height - screenPosition.y + healthBarTop*scale.y,
					                         (health*42*scale.x)/maxHealth, healthBarHeight*scale.y), barTexture);
					posy += 5;
				}
				if (healthMode2) {
					barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
					barTexture.Apply ();
					GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2 - 1,
					                         Screen.height - screenPosition.y + (healthBarTop-posy)*scale.y,
					                         44*scale.x, healthBarHeight*scale.y+2), barTexture);
					barTexture.SetPixel(0, 0, (health2 < percentLifeToColorChange*maxHealth) ? ColorX.HexToRGB("a5cc2e") : ColorX.HexToRGB("a5cc2e"));
					barTexture.Apply ();
					GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2,
					                         Screen.height - screenPosition.y + (healthBarTop - posy+1)*scale.y,
					                         (health2*42*scale.x)/maxHealth, healthBarHeight*scale.y), barTexture);
					posy += 5;
				}
				if (healthMode3) {
					barTexture.SetPixel(0, 0, ColorX.HexToRGB("111011"));
					barTexture.Apply ();
					GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2 - 1,
					                         Screen.height - screenPosition.y + (healthBarTop - posy)*scale.y,
					                         44*scale.x, healthBarHeight*scale.y+2), barTexture);
					barTexture.SetPixel(0, 0, (health3 < percentLifeToColorChange*maxHealth) ? ColorX.HexToRGB("a966e2") : ColorX.HexToRGB("a966e2"));
					barTexture.Apply ();
					GUI.DrawTexture(new Rect(screenPosition.x - (healthBarLeft*scale.x) / 2,
					                         Screen.height - screenPosition.y + (healthBarTop - posy+1)*scale.y,
					                         (health3*42*scale.x)/maxHealth, healthBarHeight*scale.y), barTexture);
				}
				SpriteCollection sprites = new SpriteCollection("Meleca");
				Sprite meleca = null;
				if (contaminated2) meleca = sprites.GetSprite("Meleca verde (Ácido)");
				else if (contaminated3) meleca = sprites.GetSprite("Meleca roxa (Base)");

				if (meleca != null) {
					Texture t = meleca.texture;

					Rect tr = meleca.textureRect;
					Rect r = new Rect(tr.x / t.width, tr.y / t.height, tr.width / t.width, tr.height / t.height );

					//Color guiColor = Color.white;
					//guiColor.a = 0.5f;
					//GUI.color = guiColor;
					GUI.DrawTextureWithTexCoords(new Rect(screenPosition.x - (45*scale.x) / 2, Screen.height - screenPosition.y + (310f*scale.y) / 2, tr.width, tr.height), t, r);
				}
			} catch (NullReferenceException) {
			}
			// http://forum.unity3d.com/threads/health-bar-above-ememy.81560/
		}
	}

	// Update is called once per frame
	void Update () {
		if (timeFat) {
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
				if (!contaminated3) {
					movementSpeed *= 0.2f;
					//Debug.Log ("gosma 3 ativa");
				}
				if (health2 > 0) health2 -= 0.2f;
			}
			else {
				contaminated2 = false;
				if (!contaminated3)
					movementSpeed *= 5.0f;
			}
		}
		if (contaminated3) {
			if (timerContaminated3 > 0) {
				timerContaminated3 -= Time.deltaTime;
				if (!contaminated2) {
					//Debug.Log ("gosma 3 ativa");
					movementSpeed *= 0.2f;
				}
				if (health3 > 0) health3 -= 0.2f;
			}
			else {
				contaminated3 = false;
				if (!contaminated2)
					movementSpeed *= 5.0f;
			}
		}

		if (contaminated3 && contaminated2) {
			//Debug.Log ("gosmaSSS ativas");
		}

	}
}