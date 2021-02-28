using UnityEngine;
using System.Collections;

public class bullet : MonoBehaviour {

	public float speed = 8f;

	public int firedamage;

	public GameObject target;

	public GameObject parentBot;

	// Use this for initialization
	void Start () {
	
		//parentBot.GetComponent<botclass>().canFire = false;

		GetComponent<LineRenderer>().SetPosition(0,this.transform.position);
		GetComponent<LineRenderer>().SetPosition(1,this.transform.position);
				
		GetComponent<LineRenderer>().enabled = true;

	}
	
	// Update is called once per frame
	void Update () {


		float step = speed * Time.deltaTime;
				
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

		GetComponent<LineRenderer>().SetPosition(1,this.transform.position);

	
	}


	void OnTriggerEnter(Collider other) 
	{


		if(other.gameObject.name == "PlayerHitBox")
		{
			Destroy(this.gameObject);

			aimer.playerlife -= firedamage;

			//if (parentBot != null) parentBot.GetComponent<botclass>().canFire = true;

		}
	}
}
