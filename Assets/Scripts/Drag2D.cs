using UnityEngine;
using System.Collections;

public class Drag2D : MonoBehaviour {

	private Vector3 offset;
	public bool grabbed;
	public bool dragging;

	//RigidbodyConstraints2D constraints;

	Rigidbody2D rb;

	void Start(){
		grabbed = false;
		rb = GetComponent<Rigidbody2D> ();
		/*constraints = GetComponent<RigidbodyConstraints2D> ();
		rb=GetComponent<Rigidbody2D>();
		rb.gravityScale = 0;
		*/

		rb.AddForce (transform.right * Random.Range(-200,-300));
	}

	void Update(){
		
	}



	void OnMouseDown()
	{
		if (GetComponent<EnlargeOnDoubleClick> ().draggable) {
			offset = gameObject.transform.position -
			Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f));

			grabbed = true;
			rb.velocity = Vector3.zero;

			dragging = true;


			//rb.gravityScale = 1;
		}
	}

	void OnMouseDrag()
	{
		if (GetComponent<EnlargeOnDoubleClick> ().draggable) {
			Vector3 newPosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, 10.0f);
			transform.position = Camera.main.ScreenToWorldPoint (newPosition) + offset;
			grabbed = true;
			dragging = true;

		}
	}

	void OnMouseUp(){
		grabbed = false;
		dragging = false;

		//constraints = RigidbodyConstraints2D.FreezeAll;
		//rb.gravityScale = 0;

	}


	void OnTriggerStay2D(Collider2D collision){
		
			//Debug.Log (collision.gameObject.name);
		if (collision.gameObject.name == "trash" && GetComponent<EnlargeOnDoubleClick>().draggable) {
			if (!grabbed) {
				//Destroy (gameObject);
			}
		}
	

			
			
			
		}
		
	}

