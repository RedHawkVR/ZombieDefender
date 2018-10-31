using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour0 : MonoBehaviour
{

	public GameObject player;

	private bool isWalking = false;
	private bool isAttacking = false;
	private bool isAlive = true;

	private Animator anim;
	private NavMeshAgent navAgent;

	private enum State { IDLE, WALKING, ATTACKING, KILLED };
	private State currentState;

	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
		currentState = State.IDLE;
	}

	void LateUpdate()
	{
		transform.LookAt(player.transform);
		switch (currentState)
		{
			case State.IDLE:
				idle();
				break;
			case State.WALKING:
				walk();
				break;
			case State.ATTACKING:
				attack();
				break;
			case State.KILLED:
				killed();
				break;
			default:
				idle();
				break;
		}

	}

	void idle()
	{
		isWalking = false;
		isAttacking = false;
		anim.SetBool("isWalking", isWalking);
		anim.SetBool("isAttacking", isAttacking);
		navAgent.isStopped = true;
	}

	void walk()
	{
		isWalking = true;
		isAttacking = false;
		anim.SetBool("isWalking", isWalking);
		anim.SetBool("isAttacking", isAttacking);
		navAgent.destination = player.transform.position;
		navAgent.isStopped = false;
	}

	void attack()
	{
		isWalking = false;
		isAttacking = true;
		anim.SetBool("isWalking", isWalking);
		anim.SetBool("isAttacking", isAttacking);
		navAgent.isStopped = false;
	}

	void killed()
	{
		isWalking = false;
		isAttacking = false;
		isAlive = false;
		anim.SetBool("isWalking", isWalking);
		anim.SetBool("isAttacking", isAttacking);
		anim.SetBool("isAlive", isAlive);
		navAgent.isStopped = true;
	}
}
