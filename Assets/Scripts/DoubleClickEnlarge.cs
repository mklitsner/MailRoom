using UnityEngine;
using System.Collections;

public class DoubleClickEnlarge : MonoBehaviour {
	
	bool timer_running;
	float timer_for_double_click;
	float delay=.2f;
	bool one_click = false;
	public bool doubleClicked;

	bool mousedown;

	//starting location


	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {





		//this is how long in seconds to allow for a double click



		if(mousedown)
		{
			if(!one_click) // first click no previous clicks
			{
				one_click = true;

				timer_for_double_click = Time.time; // save the current time
				// do one click things;
				//Debug.Log("click");
				doubleClicked = false;

			} 
			else
			{
				one_click = false; // found a double click, now reset

				//Debug.Log("doubleclick");
				doubleClicked = true;
				}
		}
		if(one_click)
		{
			// if the time now is delay seconds more than when the first click started. 
			if((Time. time - timer_for_double_click) > delay)
				{

					//basically if thats true its been too long and we want to reset so the next click is simply a single click and not a double click.

					one_click = false;

				}



	
	}

		mousedown = false;
}


	void OnMouseDown(){
		mousedown = true;
	}
		
		
}
