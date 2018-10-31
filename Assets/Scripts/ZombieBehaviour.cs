using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieBehaviour : MonoBehaviour
{
	public bool isAlive, isWalking, isAttacking;
	[SerializeField] int state;//0 idle, 1 spawning, 2 walking, 3 attacking, 4 dieing
	public GameObject player;
	public GameObject weapon;
	public GameObject zombieSpawn;
	private Animator anim;
	public float attackRange = 2.0f;
	private NavMeshAgent agent;
	public AudioSource audioSource;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}

	void Start()
	{
		isAlive = true;
		isWalking = false;
		isAttacking = false;
		state = 1;//sets to spawn
		zombieSpawn.GetComponent<ZombieSpawning>().addCount();
		if (player == null)
		{
			player = GameObject.FindGameObjectWithTag("Player");
		}
	}


	void LateUpdate()
	{
		if (zombieSpawn.GetComponent<ZombieSpawning>().time < .05)
			isAlive = false;
		if (isAlive)//checks if alive
		{
			transform.LookAt(player.transform);
			if (state == 1)//sets to moving after spawn
			{
				state = 2;
				isWalking = true;
			}
			if (state == 2)
			{
				anim.SetBool("isWalking", isWalking);
				anim.SetBool("isAttacking", isAttacking);
				agent.destination = player.transform.position;
				agent.isStopped = false;
				if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
				{
					state = 3;//sets to attack
					isWalking = false;
					isAttacking = true;
				}
			}
			if (state == 3)
			{
				anim.SetBool("isWalking", isWalking);
				anim.SetBool("isAttacking", isAttacking);
				agent.destination = transform.position;
				agent.isStopped = false;
				if (Vector3.Distance(transform.position, player.transform.position) > attackRange)//when player leaves attack range
				{
					state = 2;//sets back to moving
					isAttacking = false;
					isWalking = true;
				}
			}
			if (Vector3.Distance(transform.position, weapon.transform.position) <= 1.5f)//triggers when weapon gets too close
			{
				state = 4;//sets to dieing
				isAlive = false;//shows as dead
				Kill();
			}
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("grabbable"))
		{
			Debug.Log("Zombie hit!");
			state = 4;//sets to dieing
			isAlive = false;//shows as dead
			Kill();
		}
	}

	public void Kill()
	{
		if (isAlive == false)
		{
			isWalking = false;
			isAttacking = false;
			anim.SetBool("isWalking", isWalking);
			anim.SetBool("isAttacking", isAttacking);
			anim.SetBool("isAlive", isAlive);
			zombieSpawn.GetComponent<ZombieSpawning>().reduceCount();
			gameObject.GetComponent<NavMeshAgent>().isStopped = true;
			gameObject.GetComponent<Rigidbody>().isKinematic = true;
			audioSource.Play();
			Destroy(gameObject, 2.0f);
		}
	}

}
