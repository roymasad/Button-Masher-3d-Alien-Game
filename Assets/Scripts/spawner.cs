using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {

	public GameObject[] bots;

	public GameObject source;
	public GameObject destination;
	public GameObject target;

	public bool canspawn;

	public float spawnMinTime = 1f;
	public float spawnMaxTime = 3f;

	public float botspeed = 0f;
	public int botlife = 3;

	public int botdamage = 1;
	public float fireinterval = 2f;
	public float firewait = 3f;

	public bool fallonDeath;


	// Use this for initialization
	void Start () {
	
		canspawn = false;


		float delay = Random.Range(spawnMinTime, spawnMaxTime);

		StartCoroutine("spawnBot", delay);


	}
	
	// Update is called once per frame
	void Update () {


		if (canspawn) {

			canspawn = false;

			int i = Random.Range(0,bots.Length);


			GameObject bot = (GameObject) Instantiate(bots[i]);

			bot.transform.position = source.transform.position;


			bot.GetComponent<botclass>().speed = botspeed;
			bot.GetComponent<botclass>().state = botclass.states.walking;
			bot.GetComponent<botclass>().life = botlife;
			bot.GetComponent<botclass>().firedamage = botdamage;
			bot.GetComponent<botclass>().fireinterval = fireinterval;
			bot.GetComponent<botclass>().firewait = firewait;

			bot.GetComponent<botclass>().parentSpawner = this.gameObject;


			bot.GetComponent<botclass>().destination = destination;

			bot.GetComponent<botclass>().target = target;

			bot.GetComponent<botclass>().fallonDeath = fallonDeath;


		}




	
	}

	void OnDrawGizmos () {

		Gizmos.color = Color.red;

		Gizmos.DrawSphere (source.transform.position, 0.6f);

		Gizmos.DrawSphere (destination.transform.position, 0.3f);

		Gizmos.DrawLine (source.transform.position, destination.transform.position);


	}



	IEnumerator spawnBot(float delay) {
		
		
		yield return new WaitForSeconds(delay);
		
		canspawn = true;
		
		
		StopCoroutine("spawnBot");
		
		
		
	}

}
