using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBoundry : MonoBehaviour
{
	
	public static float leftSide = -2.25f;
	public static float rightSide = 2.25f;
	public float left;
	public float right;
	
	public static float leftSideCam = -2f;
	public static float rightSideCam = 2f;
	
    // Start is called before the first frame update
    void Start()
    {
        left = leftSide;
		right = rightSide;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
