using UnityEngine;
using System.Collections;

public class autodestroy : MonoBehaviour {

	public float timeout;


	// Use this for initialization
	void Start () {
	
		StartCoroutine("autoDestroy", timeout);

	}
	
	// Update is called once per frame
	void Update () {
	
	}


	IEnumerator autoDestroy(float delay) {
		
		
		yield return new WaitForSeconds(delay);
		
		Destroy (this.gameObject);
		
		StopCoroutine("autoDestroy");
		
		
		
	}
}
