using UnityEngine;
using System.Collections;

public class spawnPointScript : MonoBehaviour {

	public bool activated = false;
	Animator animator;

	// Use this for initialization
	void Start () {
	
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetBool ("Active", activated);
	}
	
}
