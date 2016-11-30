using UnityEngine;
using System.Collections;

public class DiplomacyStatus : MonoBehaviour {

	public float AU_Diplomacy;
	public float Jov_Diplomacy;

	public float Dominance;

	public float AU_Dominance;
	public float Jov_Dominance;

	public float treatyCountdown;
	public bool treatySigned;

	bool Jov_threatened;
	bool AU_threatened;

	public bool peacetime;

	bool armstice;

	public bool reconciled;



	// Use this for initialization
	void Start () {

		peacetime = true;
	
		AU_Diplomacy = -.25f;
		Jov_Diplomacy= -.25f;

	}
	
	// Update is called once per frame
	void Update () {

	






		Jov_Dominance = Dominance;

		AU_Dominance = -1 * Dominance;


		if (peacetime) {

			//if the diplomacy of either country goes all the way to -1, they declare war on their the other

			if (AU_Diplomacy <= -1 || Jov_Diplomacy <= -1) {
				peacetime = false;
			}

			//if both countries come to 1 diplomacy, they sign a treaty

			if (AU_Diplomacy >= 1 && Jov_Diplomacy >= 1) {
				reconciled = true;
			}


			// dominance is null in peacetime



		} else if(!peacetime){

			AU_Diplomacy = 0;
			Jov_Diplomacy = 0;


			//in war, the side that gets complete dominance over the other wins.

			if (Dominance >= 1 || Dominance <= -1) {

				armstice = true;

				peacetime = true;

			}


		}


		if (armstice) {
			treatyCountdown -= Time.deltaTime;
			if (treatyCountdown < 0) {
				treatySigned = true;
			}
		
		}



		//limiting variables
		if (Dominance > 1) {
			Dominance = 1;	
		}
		if (Dominance < -1) {
			Dominance = -1;	
		}



		if (AU_Diplomacy > 1) {
			AU_Diplomacy = 1;	
		}
		if (AU_Diplomacy < -1) {
			AU_Diplomacy = -1;	
		}

		if (Jov_Diplomacy > 1) {
			Jov_Diplomacy = 1;	
		}
		if (Jov_Diplomacy < -1) {
			Jov_Diplomacy = -1;	
		}


	
	}
}
