using UnityEngine;
using System.Collections;

public class botclass : MonoBehaviour {

	public int life;
	public float speed;

	public GameObject fireprefab;
	public int firedamage;

	public float fireinterval;
	public float firewait;

	public GameObject destination;
	public GameObject target;

	public states state;

	public GameObject parentSpawner;

	public GameObject rootMesh;

	public enum states {walking, shooting, hit, dying, none};

	public GameObject pistolObject;

	public bool canFire;

	public bool fallonDeath;

	public AudioClip botfireClip;

	public AudioClip botdieClip;
	private bool playedDieClip;

	// Use this for initialization
	void Start () {
	

		canFire = true;
		playedDieClip = false;

	}
	
	// Update is called once per frame
	void Update () {
	
		Vector3 axislock = new Vector3();

		if (state == states.walking && this.transform.position == destination.transform.position && canFire) {

			state = states.shooting;


			transform.LookAt(target.transform.position);

			axislock.y = transform.eulerAngles.y;
			axislock.x = 0;
			axislock.z = 0;

			transform.eulerAngles = axislock;



		}

		if (state == states.shooting && canFire) {

			rootMesh.GetComponent<Animation>().CrossFade("BotFire");

			StartCoroutine("FireBot", fireinterval);

			StartCoroutine("ResetFireBot", firewait);

			canFire = false;

		}


		//if (state == states.dying) {

		//	StartCoroutine("removeBot", 3f);

		//	rootMesh.GetComponent<Animation>().CrossFade("BotDie");


		//}

		if (state == states.walking) {

			float step = speed * Time.deltaTime;


			transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, step);

			rootMesh.GetComponent<Animation>().CrossFade("BotWalk");

			transform.LookAt(destination.transform.position);


			axislock.y = transform.eulerAngles.y;
			axislock.x = 0;
			axislock.z = 0;
			
			transform.eulerAngles = axislock;

		}


		if (state == states.hit) {

			state = states.none;

			StartCoroutine("StunBot", 1f);

			int choice = Random.Range(0,2);

			if (choice == 0) rootMesh.GetComponent<Animation>().CrossFade("BotHit");
			if (choice == 1) rootMesh.GetComponent<Animation>().CrossFade("BotHit2");


			life -= aimer.playerdamage;

			StopCoroutine("FireBot");

			//StopCoroutine("ResetFireBot");

			//Debug.Log(life);

		}


		if (life <= 0) {

			StartCoroutine("removeBot", 4f);

			if (fallonDeath)  {

				Vector3 temp = this.transform.position;


				temp.y -= 0.5f * Time.deltaTime;

				this.gameObject.transform.position = temp;


			}

			if (playedDieClip == false) {AudioSource.PlayClipAtPoint(botdieClip, Camera.main.transform.position, 0.5f); playedDieClip = true;} 

			rootMesh.GetComponent<Animation>().CrossFade("BotDie");

			state = states.dying;

			transform.LookAt(target.transform.position);
			
			axislock.y = transform.eulerAngles.y;
			axislock.x = 0;
			axislock.z = 0;
			
			transform.eulerAngles = axislock;

		}



	}



	IEnumerator removeBot(float delay) {
		
		
		yield return new WaitForSeconds(delay);


		parentSpawner.GetComponent<spawner>().canspawn = true;

		Destroy(this.gameObject);
		
		StopCoroutine("removeBot");
		
		
		
	}

	IEnumerator FireBot(float delay) {
		
		
		yield return new WaitForSeconds(delay);

		if (state == states.hit) StopCoroutine("FireBot");
		if (state == states.dying) StopCoroutine("FireBot");

		if (state == states.shooting) {
			GameObject bulletobj = (GameObject) Instantiate(fireprefab);

			bulletobj.GetComponent<bullet>().parentBot = this.gameObject;

			bulletobj.GetComponent<bullet>().target = target;

			bulletobj.GetComponent<bullet>().firedamage = firedamage;

			bulletobj.transform.position = pistolObject.transform.position;

			AudioSource.PlayClipAtPoint(botfireClip, Camera.main.transform.position);

		}
		
		StopCoroutine("FireBot");
		
		
		
	}

	IEnumerator ResetFireBot(float delay) {
		
		
		yield return new WaitForSeconds(delay);
		
		canFire = true;
		
		StopCoroutine("ResetFireBot");
		
		
		
	}

	IEnumerator StunBot(float delay) {
		
		
		yield return new WaitForSeconds(delay);

		if (state == states.dying) StopCoroutine("StunBot");


		state = states.walking;
				

		
		StopCoroutine("StunBot");
		
		
		
	}

}
