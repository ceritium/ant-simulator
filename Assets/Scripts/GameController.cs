using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameController : MonoBehaviour {

	public int worldSize;
	public GameObject nest;
	public GameObject ant;

	public GameObject[] foods;

	private int count;
	public float[,] matrixPN;
	public float[,] matrixPF;
	int realWorldSize;

	//	public Dictionary<string, List<GameObject>> thingsIndex = new Dictionary<string, List<GameObject>>();

	// Use this for initialization
	float time;

	void Start () {
		count = 0;
		foods = GameObject.FindGameObjectsWithTag("Food");
		realWorldSize = worldSize * 2 + 2;
		matrixPF = new float[realWorldSize,realWorldSize];
		matrixPN = new float[realWorldSize,realWorldSize];
		time = Time.time;
	}

	void Update(){
		SeedAnts ();

	}

	void OnDrawGizmos() {
		for (int x = 0; x < realWorldSize; x++) {
			for (int y = 0; y < realWorldSize; y++) {
				float timeN = matrixPN [x, y];
				if (timeN > 0) {
					Gizmos.color = Color.red;
					Gizmos.DrawSphere (new Vector3 (x-worldSize, 0, y-worldSize), 0.2f);
				}

				float timeF = matrixPF [x, y];
				if (timeF > 0) {
					Gizmos.color = Color.green;
					Gizmos.DrawSphere (new Vector3 (x-worldSize, 1, y-worldSize), 0.1f);
				}

			}				
		}
	}

	void SeedAnts(){
		if (count < 100) {
			if (Time.time < time + 10f) {
				time = Time.time;

				GameObject go = Instantiate (ant, Vector3.zero, Quaternion.identity) as GameObject;
				go.transform.Rotate (Vector3.up, Random.Range (0, 360));
				Vector3 fwd = go.transform.TransformDirection (Vector3.forward);
				go.transform.position += fwd * 3;
				count++;
			}
		}
	}

	public void AddMatrixPN(Vector3 position){
		int x = (int)Mathf.Ceil(position.x);
		int y = (int)Mathf.Ceil(position.z);

		matrixPN [x+worldSize,y+worldSize] = Time.time;
	}

	public float GetMatrixPN(Vector3 position){
		int x = (int)Mathf.Ceil(position.x);
		int y = (int)Mathf.Ceil(position.z);

		return matrixPN [x + worldSize, y + worldSize];
	}

	public void AddMatrixPF(Vector3 position){
		int x = (int)Mathf.Ceil(position.x);
		int y = (int)Mathf.Ceil(position.z);

		matrixPF[x+worldSize,y+worldSize] = Time.time;
	}


//	// Smart index 

//	public Dictionary<string, List<GameObject>> thingsIndex = new Dictionary<string, List<GameObject>>();

//	public void IndexThing(GameObject thing){
//		string key = KeyIndex(thing);
//		List<GameObject> list = ListFromIndex (thing);
//		list.Add(thing);
//		thingsIndex [key] = list;
//	}
//
//	public List<GameObject> ListFromIndex(GameObject thing){
//		string key = KeyIndex(thing);
//		List<GameObject> list;
//
//		if (thingsIndex.TryGetValue (key, out list)) {
//
//		} else {
//			list = new List<GameObject> ();
//		}
//		return list;
//	}
//
//	string KeyIndex(GameObject thing){
//		Vector3 pos = thing.transform.position;
//		int x = (int)Mathf.Ceil(pos.x);
//		int y = (int)Mathf.Ceil(pos.z);
//
//		string key = x + ":" + y;
//		return key;
//	}
//	// End smart index
}