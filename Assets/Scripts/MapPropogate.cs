using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPropogate : MonoBehaviour
{
	Vector3 nextPos;
	int number = 0;
	int numSpawns;
	GameObject MapSegment;
	GameObject player;
	bool haveCloned = false;
	MapPropogate cloneScript;
	
	GameObject clone;
	GameObject barrel;
	GameObject column;
	GameObject chest;
	GameObject bigColumn;
	GameObject flames;
	GameObject npc;
	List<Vector3> positions;
	
    // Start is called before the first frame update
    void Start()
    {
		nextPos = new Vector3(transform.position.x, transform.position.y, transform.position.z+25);
		MapSegment = GameObject.Find("MapSegment");
		player = GameObject.Find("Player");
		barrel = Resources.Load("Barrel", typeof(GameObject)) as GameObject;
		column = Resources.Load("Column_2", typeof(GameObject)) as GameObject;
		chest = Resources.Load("Chest", typeof(GameObject)) as GameObject;
		bigColumn = Resources.Load("Column_1", typeof(GameObject)) as GameObject;
		flames = Resources.Load("CampFire", typeof(GameObject)) as GameObject;
		npc = Resources.Load("SkeletonWarrior", typeof(GameObject)) as GameObject;
		
		if (this.name != "MapSegment") {
			numSpawns = (int)(Mathf.Ceil(number/5f))+1;
			for (int i = 0; i < numSpawns; i++) {
				int bepis = Random.Range(1, 7);
				Debug.Log(bepis);
				if (bepis == 1) {
					spawnBarrel();
				} else if (bepis == 2) {
					spawnColumn();
				} else if (bepis == 3) {
					spawnChest();
				} else if (bepis == 4){
					spawnBig();
				} else if (bepis == 5) {
					spawnFire();
				} else {
					Debug.Log("should spawn skeleton");
					spawnNPC();
				}
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position.z >= transform.position.z-75) && !haveCloned) {
			clone = Instantiate(MapSegment, nextPos, Quaternion.identity);
			clone.name = "Clone" + (number+1);
			cloneScript = clone.GetComponent<MapPropogate>();
			cloneScript.number = number+1;
			haveCloned = true;
		}
		if ((player.transform.position.z >= transform.position.z+35) && (this.name != "MapSegment")) {
			Destroy(gameObject);
		}
    }
	
	void spawnBarrel() {
		float x = Random.Range(LevelBoundry.leftSide+0.1f, LevelBoundry.rightSide-0.1f);
		float z = Random.Range(transform.position.z-12.5f, transform.position.z+12.5f);
		float yRot = Random.Range(-60f, 60f);
		Vector3 temp = new Vector3(x, 0.614f, z);
		Quaternion tempRot = Quaternion.Euler(180f, yRot, 0f);
		var child = Instantiate(barrel, temp, tempRot);
		child.transform.parent = gameObject.transform;
	}
	
	void spawnColumn() {
		float x = Random.Range(LevelBoundry.leftSide+0.3f, LevelBoundry.rightSide-0.3f);
		float z = Random.Range(transform.position.z-12.5f, transform.position.z+12.5f);
		float yRot = Random.Range(-180f, 180f);
		Vector3 temp = new Vector3(x, 0.234f, z);
		Quaternion tempRot = Quaternion.Euler(0f, yRot, 0f);
		var child = Instantiate(column, temp, tempRot);
		child.transform.parent = gameObject.transform;
	}
	
	void spawnChest() {
		float x = Random.Range(LevelBoundry.leftSide+0.1f, LevelBoundry.rightSide-0.1f);
		float z = Random.Range(transform.position.z-12.5f, transform.position.z+12.5f);
		float yRot = Random.Range(-60f, 60f);
		Vector3 temp = new Vector3(x, 0.428f, z);
		Quaternion tempRot = Quaternion.Euler(0f, yRot, 90f);
		var child = Instantiate(chest, temp, tempRot);
		child.transform.parent = gameObject.transform;
	}
	
	void spawnBig() {
		float x = Random.Range(LevelBoundry.leftSide+1.5f, LevelBoundry.rightSide-1.5f);
		float z = Random.Range(transform.position.z-12.5f, transform.position.z+12.5f);
		Vector3 temp = new Vector3(x, 0.614f, z);
		Quaternion tempRot = Quaternion.Euler(-90f, 0f, 45f);
		var child = Instantiate(bigColumn, temp, tempRot);
		child.transform.parent = gameObject.transform;
	}
	
	void spawnFire() {
		float x = Random.Range(LevelBoundry.leftSide+0.75f, LevelBoundry.rightSide-0.75f);
		float z = Random.Range(transform.position.z-12.5f, transform.position.z+12.5f);
		Vector3 temp = new Vector3(x, 0.5f, z);
		Quaternion tempRot = Quaternion.Euler(0f, 0f, 0f);
		var child = Instantiate(flames, temp, tempRot);
		child.transform.parent = gameObject.transform;
	}

	void spawnNPC() {
		float x = Random.Range(LevelBoundry.leftSide+1.5f, LevelBoundry.rightSide-1.5f);
		float z = Random.Range(transform.position.z-12.5f, transform.position.z+12.5f);
		Vector3 temp = new Vector3(x, 0.213f, z);
		Quaternion tempRot = Quaternion.Euler(0f, 180f, 0f);
		var child = Instantiate(npc, temp, tempRot);
		child.transform.parent = gameObject.transform;
	}
}
