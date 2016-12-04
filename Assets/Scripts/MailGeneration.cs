using UnityEngine;
using System.Collections;

public class MailGeneration : MonoBehaviour {


	public float docInitialWaitTime;

	public float mailSpawnInterval;
	public float mailSpawnCountDown;

	public float docSpawnInterval;
	public float docSpawnCountDown;

	public float docWieght;

	public Sprite[] newspaperSprite;
	public GameObject newspaper;

	public Sprite[] documentSprite;
	public GameObject document;

	int currentDocSprite;

	float secrecy;
	float diplomacy;
	string docDestination;
	string docOrigin;

	public Sprite[] mailSprite;
	public GameObject mail;

	int currentMailSprite;

	string mailDestination;
	string mailOrigin;



	float AU_Diplomacy;
	float Jov_Diplomacy;

	float Dominance;
	float startDominance=10;

	float AU_Dominance;
	float Jov_Dominance;

	bool peacetime;

	int currentnewsNUM;
	int AUnewsNUM;
	int JovnewsNUM;
	int lastnewsNUM;

	bool treatySigned;







	// Use this for initialization
	void Start () {
		AU_Diplomacy=	transform.parent.GetComponent<DiplomacyStatus> ().AU_Diplomacy;
		Jov_Diplomacy=	transform.parent.GetComponent<DiplomacyStatus> ().Jov_Diplomacy;
		AU_Dominance= transform.parent.GetComponent<DiplomacyStatus> ().AU_Dominance;
		Jov_Dominance= transform.parent.GetComponent<DiplomacyStatus>().Jov_Dominance;
		Dominance= transform.parent.GetComponent<DiplomacyStatus>().Dominance;

		peacetime= transform.parent.GetComponent<DiplomacyStatus>().peacetime;

		lastnewsNUM = currentnewsNUM;

		float tempdocSpawnInterval;
		tempdocSpawnInterval = docSpawnInterval;

		float tempmailSpawnInterval;
		tempmailSpawnInterval = mailSpawnInterval;


		docSpawnInterval = docInitialWaitTime;
		mailSpawnInterval =5;

		docSpawnCountDown = docSpawnInterval;

		docSpawnInterval = tempdocSpawnInterval;
		mailSpawnInterval = tempmailSpawnInterval;

		mailSpawnCountDown = mailSpawnInterval;
	}
	
	// Update is called once per frame
	void Update () {
		docSpawnCountDown -= Time.deltaTime;
		mailSpawnCountDown -= Time.deltaTime;

		AU_Diplomacy=	transform.parent.GetComponent<DiplomacyStatus> ().AU_Diplomacy;
		Jov_Diplomacy=	transform.parent.GetComponent<DiplomacyStatus> ().Jov_Diplomacy;
		AU_Dominance= transform.parent.GetComponent<DiplomacyStatus> ().AU_Dominance;
		Jov_Dominance= transform.parent.GetComponent<DiplomacyStatus>().Jov_Dominance;
		Dominance= transform.parent.GetComponent<DiplomacyStatus>().Dominance;

		treatySigned= transform.parent.GetComponent<DiplomacyStatus>().treatySigned;



		//change pace of documents being sent during wartime




		if (docSpawnCountDown<0){

			//make classified docs come less often

			if (peacetime) {
				if (Random.Range (0, 10) < 3) {
					if (Random.Range (0, 2) == 0) {
						currentDocSprite = Random.Range (2, 4);
					} else {
						currentDocSprite = Random.Range (6, 8);
					}
				} else {
				
					if (Random.Range (0, 2) == 0) {
						currentDocSprite = Random.Range (0, 2);
					} else {
						currentDocSprite = Random.Range (4, 6);
					}
				}
			} else {
				float neg = Random.Range (0, 4);
				if (neg == 0) {
					currentDocSprite = 1;
				} else if (neg == 1) {
					currentDocSprite = 5;
				}else if (neg == 2) {
					currentDocSprite = 3;
				}else if (neg == 3) {
				currentDocSprite = 7;
			}

			}

			if (currentDocSprite > 3) {
				docOrigin = "jov";
				docDestination = "au";
			}

			if (currentDocSprite <4) {
				docOrigin = "au";
				docDestination = "jov";
			}


			//positive docs (only sent during peace time);


				if (currentDocSprite == 0 ||currentDocSprite == 4) {
				diplomacy = 0.2f*docWieght;
				}

				if (currentDocSprite == 2 || currentDocSprite == 6) {
				diplomacy = 0.4f*docWieght;
				}
			

			//negative docs
			if (currentDocSprite == 1 || currentDocSprite == 5) {
				diplomacy = -0.3f*docWieght;
			}

			if (currentDocSprite == 3 || currentDocSprite == 7) {
				diplomacy = -0.5f*docWieght;
				docOrigin = "trash";

				if (peacetime) {
					secrecy = 0.1f*docWieght;
				} else {
					secrecy = 0.3f*docWieght;
				}
			}


			GameObject doc = Instantiate (document) as GameObject;
			doc.GetComponent<SpriteRenderer> ().sprite = documentSprite [currentDocSprite];
			doc.transform.position = new Vector3 (transform.position.x + Random.Range (-.5f, .5f), transform.position.y + Random.Range (-.5f, .5f), 0);

			doc.GetComponent<DocumentStats> ().secrecy = secrecy;
			doc.GetComponent<DocumentStats> ().diplomacy = diplomacy;
			doc.GetComponent<DocumentStats> ().origin = docOrigin;
			doc.GetComponent<DocumentStats> ().destination = docDestination;




			//change pace of documents being sent during wartime

			if (!peacetime) {
				docSpawnCountDown = Random.Range (docSpawnInterval*.1f, docSpawnInterval*.5f);
			} else {
				docSpawnCountDown = Random.Range (docSpawnInterval-3, docSpawnInterval+3);


			}
		}







		//MAIL
	
		if (mailSpawnCountDown < 0) {

			currentMailSprite = Random.Range (0, 8);

			secrecy = 0;
			diplomacy = .01f;

			if (currentMailSprite < 4) {
				mailOrigin = "au";
				mailDestination = "jov";
			} else {
				mailOrigin = "jov";
				mailDestination = "au";
			}




			GameObject envelope = Instantiate (mail) as GameObject;
			envelope.GetComponent<SpriteRenderer> ().sprite = mailSprite [currentMailSprite];
			envelope.transform.position = new Vector3( transform.position.x + Random.Range(-.5f,.5f),transform.position.y + Random.Range(-.5f,.5f),0);

			envelope.GetComponent<DocumentStats> ().secrecy = secrecy;
			envelope.GetComponent<DocumentStats> ().diplomacy = diplomacy;
			envelope.GetComponent<DocumentStats> ().origin = mailOrigin;
			envelope.GetComponent<DocumentStats> ().destination = mailDestination;


			mailSpawnCountDown = Random.Range(mailSpawnInterval-5,mailSpawnInterval);

		}






		//NEWSPAPER



		if (peacetime) {






			//comense peace meetings
			if (AU_Diplomacy >= .25 && Jov_Diplomacy >= .25) {
				if (currentnewsNUM > 3) {
					currentnewsNUM = 3;
				} else if (AU_Diplomacy >= .75 && Jov_Diplomacy >= .75) {
					//continue peace meetings
					currentnewsNUM = 2;
				} else if (AU_Diplomacy >= 1 && Jov_Diplomacy >= 1) {
						currentnewsNUM = 1;
						//reconciliation
					}
			}


			//meetings are cancelled if either side goes below .25

			if (AU_Diplomacy < .25) {
				if (currentnewsNUM < 4) {
					currentnewsNUM = 4;
					AUnewsNUM = 4;
				}
			}

			if (Jov_Diplomacy < .25) {
				if (currentnewsNUM < 4) {
					currentnewsNUM = 5;
					JovnewsNUM = 5;
				}
			}

			// warn the other side only if things have shifted negatively
			if (AU_Diplomacy < 0) {
				if (AUnewsNUM < 8) {
					currentnewsNUM = 6;
					AUnewsNUM = 6;
				}
			}

			if (Jov_Diplomacy < 0) {
				if (JovnewsNUM < 8) {
					currentnewsNUM = 7;
					JovnewsNUM = 7;

				}
			}



			// denounce the other side only if things have shifted negatively
			if (AU_Diplomacy < -0.5f) {
				if (AUnewsNUM < 10) {
					currentnewsNUM = 8;

					AUnewsNUM = 8;
				}
			}

			if (Jov_Diplomacy < -0.5f) {
				if (JovnewsNUM < 10) {
					currentnewsNUM = 9;
					JovnewsNUM = 9;

				}
			}

			// threaten the other side only if things have shifted negatively
			if (AU_Diplomacy < -0.75f) {
				if (AUnewsNUM < 12) {
					currentnewsNUM = 10;

					AUnewsNUM = 10;
				}
			}

			if (Jov_Diplomacy < -0.75f) {
				if (JovnewsNUM < 12) {
					currentnewsNUM = 11;
					JovnewsNUM = 11;

				}
			}




			// declare war on the other country
			if (AU_Diplomacy <= -1f) {
			
				currentnewsNUM = 12;

				if (Dominance < startDominance) {
					startDominance = Dominance;
					peacetime = false;
				}

				AUnewsNUM = 12;

			}

			if (Jov_Diplomacy <= -1f) {

				currentnewsNUM = 13;

				if (Dominance < startDominance) {
					startDominance = Dominance;
					peacetime = false;
				}

				JovnewsNUM = 13;


		
			}

		} else




		//DOMINANCE (in state of war)


		if (peacetime==false) {

			if (Dominance <= -0.5) {
				if (currentnewsNUM > 15) {
					currentnewsNUM = 15;

				}
			}

			if (Dominance >= 0.5) {
				if (currentnewsNUM < 18) {
					currentnewsNUM = 18;
				}

			}


			if (Dominance < -0.1) {
					if (currentnewsNUM > 16 ) {
					currentnewsNUM = 16;
				}
			}

			if (Dominance > 0.1) {
					if (currentnewsNUM < 17) {
					currentnewsNUM = 17;
				}

			}


			if (Dominance <= -1) {
				currentnewsNUM = 14;
					peacetime = true;
			}

			if (Dominance >= 1) {
				currentnewsNUM = 19;
					peacetime = true;
			}

		}

		if (treatySigned && Dominance<= -1) {
			currentnewsNUM = 20;
		}
		if (treatySigned && Dominance>= 1) {
			currentnewsNUM = 21;
		}










		if (currentnewsNUM != lastnewsNUM){
			GameObject news = Instantiate (newspaper) as GameObject;
			news.GetComponent<SpriteRenderer> ().sprite = newspaperSprite [currentnewsNUM];
			news.transform.position = new Vector3( transform.position.x + Random.Range(-.2f,.2f),transform.position.y + Random.Range(-.2f,.2f),0);

		
			lastnewsNUM = currentnewsNUM;
		}






	
	}
}
