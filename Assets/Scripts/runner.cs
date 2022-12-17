using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runner : MonoBehaviour
{
    // Start is called before the first frame update
	
	public float speed;
	public float leftRightSpeed;
	public Camera playerCamera;
	
	public bool rightSided = false;
	public bool leftSided = false;
	
	Vector3 camHome = new Vector3(0, 1.29f, -3.32f);
	Rigidbody playerBody;
	Vector3 forwardMove;
	Vector3 sideMove;
	Vector3 zeroing;
	
	public int health = 3;
	
	public bool isShielded = false;
	public bool canCollide = true;
	
	public AudioClip wood;
	public AudioClip chest;
	public AudioClip metalThonk;
	public AudioClip sizzle;
	public AudioClip concrete;
	public AudioSource musicMan;
	
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
		//forward movement with corrections
		transform.eulerAngles = zeroing;
        transform.position = transform.position + Vector3.forward * Time.deltaTime * speed;
		transform.position = new Vector3(transform.position.x, 0.614f, transform.position.z);
		playerBody.velocity = zeroing;
		
		//camera recenter
		if ((this.gameObject.transform.position.x < LevelBoundry.rightSideCam) && (this.gameObject.transform.position.x > LevelBoundry.leftSideCam)) {
			leftSided = false;
			rightSided = false;
			playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition,camHome, 0.01f);
		} 

		//offset camera so it doesnt clip into the walls.
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
			if (this.gameObject.transform.position.x > LevelBoundry.leftSide) {
				transform.position = transform.position + (Vector3.left * Time.deltaTime * leftRightSpeed);
				if (this.gameObject.transform.position.x < LevelBoundry.leftSideCam) {
					playerCamera.transform.Translate(Vector3.right * Time.deltaTime * leftRightSpeed);
					leftSided = true;
				}

			}
		}
		//offset camera so it doesnt clip into the walls.
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
			if (this.gameObject.transform.position.x < LevelBoundry.rightSide) {
				transform.position = transform.position + (Vector3.right * Time.deltaTime * leftRightSpeed);
				if (this.gameObject.transform.position.x > LevelBoundry.rightSideCam) {
					playerCamera.transform.Translate(Vector3.left * Time.deltaTime * leftRightSpeed);
					rightSided = true;
				}
				
			}
		}		
			
			/*
			if (!needStay) {
				needStay = true;
				lastX = playerCamera.gameObject.transform.position.x;
				Debug.Log("NEEDSTAY " + lastX);
			}
			Debug.Log(lastX);
			playerCamera.transform.position = new Vector3(lastX, playerCamera.gameObject.transform.position.y, playerCamera.gameObject.transform.position.z);
		} else {
			needStay = false;
			playerCamera.transform.position = new Vector3(0, 1.29f, -3.32f);
		}
		*/
    }
	
	void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
		AudioSource audioData = collision.gameObject.GetComponent<AudioSource>();
        if (collision.gameObject.name == "Chest(Clone)" && canCollide)
        {
            int bepis = Random.Range(1, 4);
			audioData.PlayOneShot(chest);
			if (bepis == 1) {
				Debug.Log("Speed Boost");
				speed = speed + 0.5f;
			} else if (bepis == 2) {
				Debug.Log("Health Potion");
				if (health < 3) {
					health = health + 1;
				}
			} else if (bepis == 3) {
				Debug.Log("Shield");
				isShielded = true;
			}
			canCollide = false;
			StartCoroutine(wait(0.5f));
        } else if (collision.gameObject.name == "Barrel(Clone)" && canCollide) { 
			if (isShielded) {
				Debug.Log("Shield Used!");
				audioData.PlayOneShot(metalThonk);
				isShielded = false;
			} else {
				audioData.PlayOneShot(wood);
				health = health - 1;
			}
			canCollide = false;
			StartCoroutine(wait(0.5f));
		} else if (collision.gameObject.name == "Column_1(Clone)" && canCollide) {
			Debug.Log("Helmet thonk");
			if (isShielded) {
				Debug.Log("Shield Used!");
				audioData.PlayOneShot(metalThonk);
				isShielded = false;
			} else {
				audioData.PlayOneShot(concrete);
				health = health - 1;
			}
			canCollide = false;
			StartCoroutine(wait(0.5f));
		} else if (collision.gameObject.name == "Column_2(Clone)" && canCollide) {
			Debug.Log("Helmet thonk");
			if (isShielded) {
				Debug.Log("Shield Used!");
				audioData.PlayOneShot(metalThonk);
				isShielded = false;
			} else {
				audioData.PlayOneShot(concrete);
				health = health - 1;
			}
			canCollide = false;
			StartCoroutine(wait(0.5f));
		}

        /* copy this for more interactions
        if (collision.gameObject.tag == "MyGameObjectTag")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
		*/
    }
	
	private void OnTriggerEnter(Collider other)
    {
		AudioSource audioData = other.gameObject.GetComponent<AudioSource>();
        if (other.gameObject.name == "CampFire(Clone)" && canCollide) {
			Debug.Log("Burning Sizzle");
			if (isShielded) {
				Debug.Log("Shield Used!");
				audioData.PlayOneShot(metalThonk);
				isShielded = false;
			} else {
				audioData.PlayOneShot(sizzle);
				health = health - 1;
			}
			canCollide = false;
			StartCoroutine(wait(0.5f));
		}
    }
	
	IEnumerator wait(float sec)	{
        yield return new WaitForSeconds(sec);
        canCollide = true;
    }
	
}
