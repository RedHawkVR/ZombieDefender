using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour {

	public int distance = 100;
	private RaycastHit hit;
	private Ray ray;

	private Transform targetedEnemy;
	private bool enemyClicked, walking;
	private Animator anim;
	private NavMeshAgent navAgent;
	[SerializeField] private float shootDistance = 10.0f;
	private float nextFire;
	private float timeBetweenShots = 2.0f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (Input.GetButtonDown("Fire2"))
		{
			if(Physics.Raycast(ray, out hit, distance))
			{
				if (hit.collider.CompareTag("Enemy"))
				{
					targetedEnemy = hit.transform;
					enemyClicked = true;
				} else {
					walking = true;
					enemyClicked = false;
					navAgent.destination = hit.point;
					navAgent.Resume();
				}
			}
		}

		if (enemyClicked)
		{
			MoveAndShoot();
		}

		if(navAgent.remainingDistance <= navAgent.stoppingDistance)
		{
			walking = false;
		} else {
			walking = true;
		}

		anim.SetBool("isWalking", walking);
	}

	void MoveAndShoot()
	{
		if(targetedEnemy == null)
		{
			return;
		}
		navAgent.destination = targetedEnemy.position;
		if(navAgent.remainingDistance >= shootDistance)
		{
			navAgent.Resume();
			walking = true;
		}
		if(navAgent.remainingDistance <= shootDistance)
		{
			transform.LookAt(targetedEnemy);
			if(Time.time > nextFire)
			{
				nextFire = Time.time + timeBetweenShots;
				Fire();
			}
		}
	}

	void Fire()
	{
		print("Fire!");
	}

}
