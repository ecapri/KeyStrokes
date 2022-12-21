using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    public GameObject player_obj;
    public float radius_of_search_for_player;
    public float npc_speed;
    public Animator animation_controller;

	void Start ()
    {
        npc_speed = 0.005f;
        radius_of_search_for_player = 15f;
        player_obj = GameObject.FindGameObjectWithTag("Player");
        animation_controller = GetComponent<Animator>();
    }

    void Update()
    {
        transform.localScale = new Vector3(
                               0.9f + 0.2f * Mathf.Abs(Mathf.Sin(4.0f * Time.time)), 
                               0.9f + 0.2f * Mathf.Abs(Mathf.Sin(4.0f * Time.time)), 
                               0.9f + 0.2f * Mathf.Abs(Mathf.Sin(4.0f * Time.time))
                               );

        if ((player_obj.transform.position - transform.position).magnitude < radius_of_search_for_player){
            animation_controller.SetBool("inRange", true);
            Vector3 direction = (player_obj.transform.position - transform.position).normalized;
            transform.position += direction * npc_speed * Time.deltaTime;
            transform.position = new Vector3(transform.position.x, 0.0f, transform.position.z);
        }
    }

    void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.name == "Player")
        {
            animation_controller.SetBool("collision", true);
        }
    }
}

