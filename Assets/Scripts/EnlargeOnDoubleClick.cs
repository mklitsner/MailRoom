using UnityEngine;
using System.Collections;

public class EnlargeOnDoubleClick : MonoBehaviour {


	bool doubleClicked;
	Vector3 scale;
	float startxpos;
	float startypos;
	float currentxpos;
	float currentypos;
	string level;
	public bool draggable;



	bool reset;

	// Use this for initialization
	void Start () {


		scale = transform.localScale;
		draggable = true;

	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<BoxCollider2D> ().enabled = true;
	
		doubleClicked = GetComponent<DoubleClickEnlarge> ().doubleClicked;

		if (doubleClicked) {


			if (!reset) {
				startxpos = transform.position.x;
				startypos = transform.position.y;
				reset = true;
			}

			transform.localScale = new Vector3(5,5,5);
			GetComponent<SpriteRenderer> ().sortingLayerName = "CloseUp";
			draggable = false;

			transform.position = new Vector3(0,0,0);



		} else{
			
			if (reset) {
				draggable = true;
				GetComponent<BoxCollider2D> ().enabled = false;
				GetComponent<SpriteRenderer> ().sortingLayerName = "Default";
				reset = false;
				transform.localScale = scale;

			}


		
		}


	}
	
}
