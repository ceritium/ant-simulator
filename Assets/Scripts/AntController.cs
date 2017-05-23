using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntController : MonoBehaviour
{

	// Use this for initialization

	public GameObject pheromoneNet;
	public GameObject pheromoneFood;
	public float rotateFactor;
	public float pheromoneFactor;
	public float speed;
	public int rotation;
	private bool withFood;

	private GameController gameController;
	private GameObject antFood;
	private Vector3 nestPosition;

	float minDist;
	Vector3 nearPosition;


	void Start ()
	{
		InvokeRepeating("RandomRotate", 2.0f, rotateFactor);
		InvokeRepeating ("Move", 0f, speed);
		withFood = false;

		// Take components through gameController to improve performance (I suppose).
		gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController> ();
		nestPosition = gameController.nest.transform.position;
		antFood = transform.Find ("AntFood").gameObject;
		antFood.SetActive (false);
	}

	void Update(){
		Vector3 fwd = transform.TransformDirection (Vector3.forward);
		transform.position += fwd * speed;

		if (Mathf.Abs(transform.position.x) > gameController.worldSize || Mathf.Abs(transform.position.z) > gameController.worldSize) {
			transform.Rotate (Vector3.up, 180);
		}
	}
	// Update is called once per frame
	void Move ()
	{

		AddPheronomone ();

		if (withFood) {
			LookForNest ();	
		} else {
			LookForFood ();
		}

	}

	void RandomRotate ()
	{
		transform.Rotate (Vector3.up, Random.Range (-rotation, rotation));
	}

	void AddPheronomone ()
	{
		if (withFood) {
			gameController.AddMatrixPF(transform.position);
		} else {
			gameController.AddMatrixPN(transform.position);
		}
	}

	void LookForNest ()
	{
		float dist = Vector3.Distance (transform.position, nestPosition);
		if (dist < 1f) {
			withFood = false;
			antFood.SetActive (false);
			transform.Rotate (Vector3.up, 180);
		} else if (dist < 50f) {
			RotateTo (nestPosition);
		
		} else {
//			LookForPheromones ("PheronomeNet");
//			LookForPN();
		}
	}

	void LookForFood ()
	{
		// TODO: Go to the nearest food source
		foreach (GameObject food in gameController.foods) {
			Vector3 foodPosition = food.transform.position;
			float dist = Vector3.Distance (transform.position, foodPosition);
			if (dist < 1f) {
				withFood = true;
				antFood.SetActive (true);
				transform.Rotate (Vector3.up, 180);
			} else if (dist < 15f) {
				RotateTo (foodPosition);
			} else {
//				LookForPheromones ("PheronomeFood");
//				LookForPN();
			}
		}
	}

	void LookForPN(){
		Debug.Log (transform.localEulerAngles.y);

		float rotation = transform.localEulerAngles.y;



		gameController.GetMatrixPN (transform.position);
	}

	void LookForPheromones (string tag)
	{
		
//		gameController.GetMatrixPN(transform.position)

//		List<GameObject> pheromones = new List<GameObject>();
//
//		foreach (GameObject pheronome in gameController.ListFromIndex(gameObject)) {
//			if (pheronome.CompareTag (tag)) {
//				pheromones.Add (pheronome);
//			}
//		}
//			
//		foreach (GameObject pheronome in pheromones) {
//			Vector3 pheronomePosition = pheronome.transform.position;
//			float dist = Vector3.Distance (transform.position, pheronomePosition);
//			if (dist < 5f) {
//
//				if (dist < minDist) {
//					minDist = dist;
//					nearPosition = pheronomePosition;
//				}
//			}
//
//			minDist = 10f;
//
//			Vector3 targetDir = nearPosition - transform.position;
//			float angle = Vector3.Angle (targetDir, transform.forward);
//
//			// TODO:
//			// Si no hay ninguna feromona por delante, sigo alguna que esté por detrás
//			if (angle < 70) {
//				RotateTo (nearPosition);
//			}
//		}

	}

	void RotateTo (Vector3 newPosition)
	{
		Vector3 direction = (newPosition - transform.position).normalized;
		Quaternion rotation = Quaternion.LookRotation (direction);
		transform.rotation = rotation;
	}
}
