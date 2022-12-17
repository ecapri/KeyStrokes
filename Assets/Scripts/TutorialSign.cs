using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialSign : MonoBehaviour
{
	GameObject player;
	public Text text;   
	public ParticleSystem particles;
	
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
		particles.enableEmission = false;
		text.enabled = false;
		StartCoroutine(wait(1.5f));
    }

    // Update is called once per frame
    void Update()
    {
    }
	
	IEnumerator wait(float sec)	{
        yield return new WaitForSeconds(sec);
        particles.enableEmission = true;
		yield return new WaitForSeconds(2.5f*(sec/3));
		text.enabled = true;
		yield return new WaitForSeconds(sec/3);
		particles.enableEmission = false;
    }
}
