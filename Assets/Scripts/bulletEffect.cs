using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEffect : MonoBehaviour {

    private Animator animator;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetTrigger("effectOn");

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
