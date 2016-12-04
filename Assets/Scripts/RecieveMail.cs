using UnityEngine;
using System.Collections;

public class RecieveMail : MonoBehaviour {

	public float Diplomacy;


	public float Dominance;

	int state;
	int recieveAnim;

	private Animator animator;

	AudioSource maildropAudio;

	bool peacetime;

	// Use this for initialization
	void Start () {
		animator =GetComponent<Animator> ();
		Diplomacy = -.25f;
			
		peacetime= transform.parent.GetComponent<DiplomacyStatus>().peacetime;
		animator =GetComponent<Animator> ();

		maildropAudio = GetComponent<AudioSource> ();
	
	}
	
	// Update is called once per frame
	void Update () {
		

		if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !animator.IsInTransition (0)) {
			animator.SetInteger ("AnimState", state);
			state = 0;
		}

		peacetime= transform.parent.GetComponent<DiplomacyStatus>().peacetime;
	
	}



	void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.tag == "document" ||collision.gameObject.tag == "mail" ) {

			float diplomacy = collision.GetComponent<DocumentStats> ().diplomacy;
			float secrecy = collision.GetComponent<DocumentStats> ().secrecy;
			string origin= collision.GetComponent<DocumentStats> ().origin;
			string destination =collision.GetComponent<DocumentStats> ().destination;



			if(!collision.GetComponent<Drag2D>().grabbed && collision.GetComponent<EnlargeOnDoubleClick>().draggable ){


				maildropAudio.Play ();







				Debug.Log (gameObject.name +" recieved "+ collision.name);



		//if you put in the right box
				if (destination == gameObject.tag) {
					Diplomacy = diplomacy;
					if (!peacetime) {
						Dominance = secrecy;
					}

		//if you send it back to the country it came from
				} else if (origin == gameObject.tag) {

					if (diplomacy > 0) {
						Diplomacy = -1*diplomacy;
					} else {
						Diplomacy = diplomacy * .5f;
					}
				
				}



				if (gameObject.tag == "au") {
					transform.parent.GetComponent<DiplomacyStatus> ().AU_Diplomacy += Diplomacy;
					transform.parent.GetComponent<DiplomacyStatus> ().Dominance += -1*Dominance;
					state = 2;
				} else if(gameObject.tag == "jov"){
					transform.parent.GetComponent<DiplomacyStatus> ().Jov_Diplomacy +=Diplomacy;
					transform.parent.GetComponent<DiplomacyStatus>().Dominance +=Dominance;
					state = 1;

				}

				animator.SetInteger ("AnimState", recieveAnim);


				Destroy (collision.gameObject);

		}
			

	}
}
}
