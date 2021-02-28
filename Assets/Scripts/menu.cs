using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menu : MonoBehaviour {

	public GameObject titleImg;

	public GameObject CreditImg;

	// Use this for initialization
	void Start () {
	

		titleImg.GetComponent<Image>().CrossFadeAlpha(0,0,false);

		titleImg.GetComponent<Image>().CrossFadeAlpha(1,2,false);

		StartCoroutine("fadeoutTitleDelay", 2);

		CreditImg.GetComponent<Image>().CrossFadeAlpha(0,0,false);

		Time.timeScale =1;


	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator fadeoutTitleDelay(float delay) {
		
		
		yield return new WaitForSeconds(delay);
		
		StartCoroutine("fadeoutTitle", 2);
		
		StopCoroutine("fadeoutTitleDelay");
		
		
		
	}


	IEnumerator fadeoutTitle(float delay) {
		
		
		yield return new WaitForSeconds(delay);
		
		titleImg.GetComponent<Image>().CrossFadeAlpha(0,2,false);
		
		StopCoroutine("fadeoutTitle");
		
		
		
	}

	public void ResetGame() {

		Application.LoadLevel("scene6");

	}

	public void ResumeGame() {
		
		Time.timeScale = 1;

		CreditImg.GetComponent<Image>().CrossFadeAlpha(0,1,false);
		
	}

	public void ExitGame() {
		
		Application.Quit();
		
	}


	public void ShowCredits() {
		
		StartCoroutine("PauseDelayed", 1);


		CreditImg.GetComponent<Image>().CrossFadeAlpha(1,1,false);


	}

	IEnumerator PauseDelayed(float delay) {
		
		
		yield return new WaitForSeconds(delay);
		
		Time.timeScale = 0;
		
		StopCoroutine("PauseDelayed");
		
		
		
	}

}
