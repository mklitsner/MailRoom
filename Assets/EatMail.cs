using UnityEngine;
using System.Collections;

public class EatMail : MonoBehaviour {

	// Use this for initialization
	private Animator animator;

	int state;
	// 0 is idle
	// 1 is eating


	void Start(){
		animator =GetComponent<Animator> ();
	}

	void Update(){


		
		if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !animator.IsInTransition (0)) {
			animator.SetInteger ("AnimState", state);
			state = 0;
		}


	}






	void OnTriggerStay2D(Collider2D collision){
		if (collision.gameObject.tag == "document" ||collision.gameObject.tag == "mail" || collision.gameObject.tag == "news") {




			if(!collision.GetComponent<Drag2D>().grabbed && collision.GetComponent<EnlargeOnDoubleClick>().draggable){

				Debug.Log (gameObject.name +" eats "+ collision.name);
	
				Destroy (collision.gameObject);

				animator.SetInteger ("AnimState", 1);
				state = 1;

			}


		}
	}
}

