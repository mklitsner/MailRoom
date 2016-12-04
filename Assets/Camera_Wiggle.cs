using UnityEngine;
using System.Collections;

public class Camera_Wiggle : MonoBehaviour {
	private Animator animator;
	bool peacetime;
	bool audioPlaying;
	AudioSource warAudio;
	int state;
	float explosionInterval;
	float explosionCounter;
	// Use this for initialization
	void Start () {
		animator =GetComponent<Animator> ();
		explosionInterval = 5;
		warAudio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		

		peacetime = transform.parent.GetComponent<DiplomacyStatus> ().peacetime;

		if (peacetime) {
			state = 0;
			audioPlaying = false;
			warAudio.Stop ();
		} else {
			explosionCounter -= Time.deltaTime;

			if (!audioPlaying) {
				warAudio.Play ();

				audioPlaying = true;
			}
			if (explosionCounter <= 0) {
				state = 1;
				explosionInterval = Random.Range (5f, 10f);
				explosionCounter = explosionInterval;
			}

		}


		if (animator.GetCurrentAnimatorStateInfo (0).normalizedTime > 1 && !animator.IsInTransition (0)) {
			animator.SetInteger ("AnimState", state);
			state = 0;
		}
	
	}
}
