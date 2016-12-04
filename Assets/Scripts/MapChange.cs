using UnityEngine;
using System.Collections;

public class MapChange : MonoBehaviour {

	float Dominance;
	bool treatySigned;
	bool reconciled;
	bool peacetime;

	public Sprite[] Map;


	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = Map [0];
	}
	
	// Update is called once per frame
	void Update () {
	
		Dominance= transform.parent.GetComponent<DiplomacyStatus>().Dominance;
		treatySigned= transform.parent.GetComponent<DiplomacyStatus>().treatySigned;
		reconciled= transform.parent.GetComponent<DiplomacyStatus>().reconciled;
		peacetime= transform.parent.GetComponent<DiplomacyStatus>().peacetime;


		if (!peacetime) {
			GetComponent<SpriteRenderer> ().sprite = Map [3];
		}



		if (treatySigned) {
			if (Dominance <= -1) {
				GetComponent<SpriteRenderer> ().sprite = Map [1];
			}
			if (Dominance >= 1) {
				GetComponent<SpriteRenderer> ().sprite = Map [2];
			}
		}

		if (reconciled) {
			GetComponent<SpriteRenderer> ().sprite = Map [4];
		}


	}
}
