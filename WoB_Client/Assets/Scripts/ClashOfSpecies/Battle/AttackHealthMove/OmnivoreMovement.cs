﻿using UnityEngine;
using System.Collections;

public class OmnivoreMovement : MonoBehaviour {

	Animator anim;    
	Transform player;               // Reference to the player's position.
	Health playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	
	
	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Prey").transform;
		playerHealth = player.GetComponent <Health> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}
	
	
	void Update ()
	{
		// If the enemy and the player have health left...
		if(/*enemyHealth.currentHealth > 0 &&*/ playerHealth.currentHealth > 0)
		{
			// ... set the destination of the nav mesh agent to the player.
			nav.SetDestination (player.position);
			anim.SetTrigger("PredatorWalking");
		}
		// Otherwise...
		else
		{
			// ... disable the nav mesh agent.
			nav.enabled = false;
		}
	}
}