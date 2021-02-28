using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class aimer : MonoBehaviour {

	public GameObject cannon;

	public GameObject shootanim;

	public GameObject cannontip;

	public GameObject chair;

	public GameObject decal;

	private Vector3 wordPos;

	private Quaternion hitRotation;

	private Quaternion targetRotation;
	private Quaternion targetRotation2;

	public float speed = 1.0f;

	private bool triggered0;

	private bool triggered;

	private bool fired;

	public GameObject explosion;

	public Light cannonLight1;
	public Light cannonLight2;

	public float Light1Intensity = 8f;
	public float Light2Intensity = 5.4f;

	static public int playerlife = 10;
	static public int playerdamage = 1;

	//private ArrayList decalList;

	//public int decalLimit = 10;

	public GameObject shakeOb;

	public AudioClip laserfire;

	public AudioClip enemyhit;

	public AudioClip turretrotateClip;

	public AudioClip turrestopClip;

	private bool turretrotateplaying;

	private bool turretstopplayed;

	private bool firsttimeCalibrate;

	// Use this for initialization
	void Start () {
	
		wordPos = Vector3.zero ;

		shootanim.GetComponent<Animation>().wrapMode = WrapMode.Once;

		shootanim.GetComponent<Animation>().Play();
		shootanim.GetComponent<Animation>().Stop();

		triggered = false;


		cannonLight1.intensity = 0;
		cannonLight2.intensity = 0;

		//decalList = new ArrayList();


		cannontip.GetComponent<LineRenderer>().enabled = false;

		turretrotateplaying = false;

		turretstopplayed = false;

		firsttimeCalibrate = true;

	}
	
	// Update is called once per frame
	void Update () {


		if (Input.mousePosition.y < 40) return;

		Quaternion pos = cannon.transform.rotation;
		Quaternion pos2 = chair.transform.rotation;


						
		Quaternion temp0 = new Quaternion();


		temp0 = Quaternion.Slerp (pos , targetRotation, Time.deltaTime * speed);

			
		if (temp0.eulerAngles.x < 25 || temp0.eulerAngles.x > 90) cannon.transform.rotation = temp0; 
		else fired = false;
		//Debug.Log(temp0.eulerAngles.x );


		Quaternion original = new Quaternion();
		Quaternion temp = new Quaternion();

		original.x = chair.transform.rotation.x;

		original.z = chair.transform.rotation.z;


		temp = Quaternion.Slerp (pos2 , targetRotation2, Time.deltaTime * speed);


		if ((temp.eulerAngles.x < 25 || temp.eulerAngles.x > 90) && (temp0.eulerAngles.x < 25 || temp0.eulerAngles.x > 90) ) chair.transform.rotation = temp;
		else fired = false;

		original.y = chair.transform.rotation.y;
		original.w = chair.transform.rotation.w;

		chair.transform.rotation = original;

		if (Quaternion.Angle(cannon.transform.rotation, targetRotation) < 5.0f && fired == true) { 

			triggered = true; 

		
		} 


		if (Quaternion.Angle(cannon.transform.rotation, targetRotation) < 5.0f ) { 
			



			if (turretstopplayed == false) {
				
				if (firsttimeCalibrate != true) AudioSource.PlayClipAtPoint(turrestopClip, Camera.main.transform.position, 0.2f);


				turretstopplayed = true;

				turretrotateplaying = false;

				firsttimeCalibrate = false;
				
			}
			
			
		} 

		else {

			if (turretrotateplaying == false) {

				if (firsttimeCalibrate != true) AudioSource.PlayClipAtPoint(turretrotateClip, Camera.main.transform.position, 0.2f);

				turretrotateplaying = true;


			}


			turretstopplayed = false;



		}





		RaycastHit hit;

			
			
		Vector3 mousePos= new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f);
		
		

		
		
		Ray ray=Camera.main.ScreenPointToRay(mousePos);
		
		if(Physics.Raycast(ray,out hit,1000f)) {
			
			wordPos = hit.point;
			hitRotation = Quaternion.FromToRotation(Vector3.forward, hit.normal);



			
		} else {
			
			wordPos = Camera.main.ScreenToWorldPoint(mousePos);
			
		}
		
		targetRotation = Quaternion.LookRotation(wordPos - cannon.transform.position);

		targetRotation2 = Quaternion.LookRotation(wordPos - chair.transform.position);





		if (Input.GetMouseButtonDown(0) && triggered0 == false)	{

			if (wordPos == Vector3.zero) return;


			if (hit.transform == null || hit.collider.gameObject.name == "HitPlane") return;


			triggered0 = true;

			fired = true;
		}


		if (triggered) {


			
			GameObject boom;
			
			boom = (GameObject) Instantiate(explosion);
			
			boom.transform.position = wordPos;



			if (hit.collider.name.StartsWith("Env_"))
			{
				GameObject decals = (GameObject) Instantiate(decal, hit.point, hitRotation);

				//decalList.Add(decals);

				//if (decalList.Count > decalLimit) {Destroy((GameObject) decalList[0]); decalList.RemoveAt(0); }
			}

			//else if (hit.collider.name.StartsWith("Character"))
			else if (hit.collider != null && hit.collider.gameObject.transform.parent.name.StartsWith("Bot"))
			{
				hit.collider.gameObject.transform.parent.GetComponent<botclass>().state = botclass.states.hit;


				AudioSource.PlayClipAtPoint(enemyhit, Camera.main.transform.position);
				//Debug.Log("botHIT");

			}
			
			shootanim.GetComponent<Animation>().wrapMode = WrapMode.Once;
			shootanim.GetComponent<Animation>().Play();
			
			shakeOb.GetComponent<Animation>().Play();

			cannonLight1.intensity = Light1Intensity;
			cannonLight2.intensity = Light2Intensity;
			
			cannontip.GetComponent<LineRenderer>().SetPosition(0,cannontip.transform.position);
			cannontip.GetComponent<LineRenderer>().SetPosition(1,wordPos);
			
			cannontip.GetComponent<LineRenderer>().enabled = true;
			StartCoroutine("dimLaser", 0.1f);

			triggered = false;

			fired = false;

			AudioSource.PlayClipAtPoint(laserfire, Camera.main.transform.position);


		}



		if (cannonLight1.intensity > 0 || cannonLight2.intensity > 0 ) StartCoroutine("dimLights", 0.05f);


		if (Input.GetMouseButtonUp(0))	{

			triggered0 = false;
		}

	
	}



	IEnumerator dimLights(float delay) {
		
		
		yield return new WaitForSeconds(delay);


		cannonLight1.intensity -= cannonLight1.intensity * 0.3f;
		cannonLight2.intensity -= cannonLight2.intensity * 0.3f;

		if (cannonLight1.intensity < 0.1f) cannonLight1.intensity = 0;
		if (cannonLight2.intensity < 0.1f) cannonLight2.intensity = 0;
		
		StopCoroutine("dimLights");
		
		
		
	}


	IEnumerator dimLaser(float delay) {
		
		
		yield return new WaitForSeconds(delay);

	
		cannontip.GetComponent<LineRenderer>().enabled = false;

		
		StopCoroutine("dimLaser");
		
		
		
	}




}

