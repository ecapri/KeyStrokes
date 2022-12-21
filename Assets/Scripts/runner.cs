using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class runner : MonoBehaviour
{
    // Start is called before the first frame update
	
	public float speed;
	private int score;
	private int typedScore;
	//public float leftRightSpeed;
	public Camera playerCamera;

	private Animator animation_controller;
    public float max_velocity;   
	
	public GameObject LeftTyper;
    public GameObject ForwardTyper;
    public GameObject RightTyper;
    private int movement; //1 is forward, 2 is left, 3 is right

	public bool rightSided = false;
	public bool leftSided = false;
	
	Vector3 camHome = new Vector3(0, 2.078f, -3.32f);
	Rigidbody playerBody;
	Vector3 forwardMove;
	Vector3 sideMove;
	Vector3 zeroing;
	public GameObject character1;
	public GameObject character2;
	public GameObject character3;
	private GameObject prefab;
	
	public int health = 3;
	
	public bool isShielded = false;
	public bool canCollide = true;
	
	public AudioClip wood;
	public AudioClip chest;
	public AudioClip metalThonk;
	public AudioClip sizzle;
	public AudioClip concrete;
	public AudioSource musicMan;
	public AudioClip bone_crack;
	
	public Text lifeText;
	public Text shieldText;
	public Text speedText;
	public Text gameoverText;
	public Text chestText;
	public Text scoreText;

	public Canvas menu;
	public bool start_game = false;
	public bool first_entry = true;

    void Start()
    {
		playerBody = GetComponent<Rigidbody>();
		animation_controller = GetComponent<Animator>();
		typedScore = 0;
		score = 0;
        max_velocity = 2.0f;
        speed = 0.0f;
        movement = 1;
		health = 3;

		lifeText.text = "<color=green>Health: " + health.ToString() + "</color>";
		shieldText.text = "<color=red>Shield: No</color>";
		speedText.text = "<color=green>Speed: " + speed.ToString("F2") + "</color>";
		gameoverText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
		if (!start_game) {
			return;
		}
/* 		if (first_entry) {
			first_entry = false;
 			int character = menu.GetComponent<StartMenu>().character_prefab;

			// load character
			if (character == 1) {
				prefab = character1;
			} else if (character == 2) {
				prefab = character2;
			} else {
				prefab = character3;
			}
			GameObject player = Instantiate(prefab);
			player.transform.parent = transform;
			player.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f); 
			
		} */

		score = (((int)this.gameObject.transform.position.z) * 10);
		if (ForwardTyper.GetComponent<TyperForward>().trigger == 1)
        {
            movement = 1;
			//typedScore = typedScore + 50;
        }
        else if (LeftTyper.GetComponent<TyperLeft>().trigger == 1)
        {
            movement = 2;
			//typedScore = typedScore + 50;
        }
        else if (RightTyper.GetComponent<TyperRight>().trigger == 1)
        {
            movement = 3;
			//typedScore = typedScore + 50;
        }

        //Move forward
        if(movement == 1)
        {
            speed += 0.005f;
            if(speed > max_velocity)
            {
                speed = max_velocity;
            }
            transform.eulerAngles = zeroing;
            transform.position = transform.position + Vector3.forward * Time.deltaTime * speed;
			transform.position = new Vector3(transform.position.x, 0.614f, transform.position.z);
			playerBody.velocity = zeroing;
        }
		
		//camera recenter
		if ((this.gameObject.transform.position.x < LevelBoundry.rightSideCam) && (this.gameObject.transform.position.x > LevelBoundry.leftSideCam)) {
			leftSided = false;
			rightSided = false;
			playerCamera.transform.localPosition = Vector3.Lerp(playerCamera.transform.localPosition,camHome, 0.01f);
		} 

		//offset camera so it doesnt clip into the walls.
		if (movement == 2) {
			if (this.gameObject.transform.position.x > LevelBoundry.leftSide) {
				speed += 0.005f;
            	if(speed > max_velocity)
            	{
                	speed = max_velocity;
            	}
				transform.eulerAngles = zeroing;
				transform.position = transform.position + (Vector3.left * Time.deltaTime * speed * 0.5f) + (Vector3.forward * Time.deltaTime * speed);
				transform.position = new Vector3(transform.position.x, 0.614f, transform.position.z);
				playerBody.velocity = zeroing;
				if (this.gameObject.transform.position.x < LevelBoundry.leftSideCam && !leftSided) {
					playerCamera.transform.Translate(Vector3.right * Time.deltaTime * speed);
					leftSided = true;
				}

			}
			else
			{
				LeftTyper.GetComponent<TyperLeft>().trigger = 0;
				ForwardTyper.GetComponent<TyperForward>().trigger = 1;
			}
		}
		//offset camera so it doesnt clip into the walls.
		if (movement == 3) {
			if (this.gameObject.transform.position.x < LevelBoundry.rightSide) {
				speed += 0.005f;
            	if(speed > max_velocity)
            	{
                	speed = max_velocity;
            	}
				transform.eulerAngles = zeroing;
				transform.position = transform.position + (Vector3.right * Time.deltaTime * speed * 0.5f) + (Vector3.forward * Time.deltaTime * speed);
				transform.position = new Vector3(transform.position.x, 0.614f, transform.position.z);
				playerBody.velocity = zeroing;
				if (this.gameObject.transform.position.x > LevelBoundry.rightSideCam && !rightSided) {
					playerCamera.transform.Translate(Vector3.left * Time.deltaTime * speed);
					rightSided = true;
				}				
			}
			else
			{
				RightTyper.GetComponent<TyperRight>().trigger = 0;
				ForwardTyper.GetComponent<TyperForward>().trigger = 1;
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

		lifeText.text = "<color=green>Health: " + health.ToString() + "</color>";
		if (isShielded) {
			shieldText.text = "<color=green>Shield: Yes</color>";
		} else {
			shieldText.text = "<color=red>Shield: No</color>";
		}
		speedText.text = "<color=green>Speed: " + speed.ToString("F2") + "</color>";
		scoreText.text = "Score: " + score.ToString();


		//Game Over
		if(health == 0) 
		{
			animation_controller.SetBool("death", true);
			gameoverText.text = "Game Over";
			speed = 0f;
			// write new score
			string[] lines = { score.ToString() };
            File.WriteAllLines(@"Assets\BestScore.txt", lines);
			StartCoroutine(startOver(5f));			
			return;
		}

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
				max_velocity = max_velocity + 0.5f;
				animation_controller.SetBool("speedBoost", true);
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
				animation_controller.SetBool("hit", true);
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
				animation_controller.SetBool("hit", true);
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
				animation_controller.SetBool("hit", true);
				audioData.PlayOneShot(concrete);
				health = health - 1;
			}
			canCollide = false;
			StartCoroutine(wait(0.5f));
		} else if (collision.gameObject.name == "SkeletonWarrior(Clone)" && canCollide) {
			if (isShielded) {
				Debug.Log("Shield Used!");
				audioData.PlayOneShot(metalThonk);
				isShielded = false;
			} else {
				animation_controller.SetBool("hit", true);
				audioData.PlayOneShot(bone_crack);
				health = health - 1;
			}
			canCollide = false;
			StartCoroutine(wait(0.5f));
		}
		animation_controller.SetBool("hit", false);

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

	IEnumerator startOver(float sec) {
		yield return new WaitForSeconds(sec);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	
}
