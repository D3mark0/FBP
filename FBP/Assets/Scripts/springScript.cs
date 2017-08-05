using UnityEngine;
using System.Collections;

public class springScript : MonoBehaviour {

    //public bool activated;
    Animator animator;

    // Use this for initialization
    void Start() {

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        animator.SetTrigger("Inactive");
    }

    void OnTriggerEnter2D(Collider2D col) {

        if (col.gameObject.name == "player") {

            animator.SetTrigger("Active");
        }
    }
}