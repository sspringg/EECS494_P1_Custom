using UnityEngine;
using System.Collections;

public class Intro : MonoBehaviour {

	public static Intro S;

	void Awake () {
		S = this;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		GUIText mytext;
		mytext = GameObject.Find ("IntroText").GetComponent<GUIText> ();

		mytext.text = "Hello, welcome to Pokemon Race.\nWe are still under construction.\n" +
			"You can see that we have limited\nyour movement to a 100 spaces.\n" +
			"Once we are done you and your opponent\nwill be able to take turns\n" +
			"moving a 100 spaces while having the\noption to perform actions\n" +
			"to make your opponent fall behind you in the race\n" +
			"Sorry for the inconvenience\n" +
			"Please press K to continue";

		if (Input.GetKey (KeyCode.K)) {
			gameObject.SetActive (false);
		}
	}
}
