	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;

	public class ZombieSpawning : MonoBehaviour {

	public GameObject zombie;
	public GameObject spawn1,spawn2, spawn3, spawn4;
	public static int zombieCount = 0;
	public static int zombieMax = 1;
	public float time = 0;
	private float timeBeforeStart = 10.0f;
	bool zombiealive;
	private Vector3 range;

	void Start () {
		zombiealive = zombie.GetComponent<ZombieBehaviour>().isAlive;
	}

	void Update()
	{
		time += Time.deltaTime;
		if (timeBeforeStart < time)
		{
			if (time < .03)
				IncreaseZombieMax();
			if(time>10&&time<60*5)
			{
				if (time % 10 > 0 && time % 10 < .015)
					IncreaseZombieMax();
			}
			if(time>=60*5)
			{
				if(time%10>0 && time % 10 <.01)
				{
					IncreaseZombieMax();
				}
			}
			if (time % 10 < 1 && time % 10 > .6)
			{

				if (zombieCount < zombieMax)
				{
					float ranNum = Random.Range(0, 4);
                
					if (ranNum % 4 == 0)
					{
						range = spawnLocation(spawn1);
						Instantiate(zombie, range, Quaternion.identity);
					}
					else if (ranNum % 4 == 1)
					{
						range = spawnLocation(spawn2);
						Instantiate(zombie, range, Quaternion.identity);
					}
					else if (ranNum % 4 == 2)
					{
						range = spawnLocation(spawn3);
						Instantiate(zombie, range, Quaternion.identity);
					}
					else if (ranNum % 4 == 3)
					{
						range = spawnLocation(spawn4);
						Instantiate(zombie, range, Quaternion.identity);
					}
				}
			}
		}
	}

	Vector3 spawnLocation(GameObject spawn)
	{
		float variance = 1.0f;
		return new Vector3(Random.Range(spawn.transform.position.x, spawn.transform.position.x + variance), 1, 
			Random.Range(spawn.transform.position.z, spawn.transform.position.z + variance));
	}

	void IncreaseZombieMax()
	{
		if(zombieMax < 5)
		{
			zombieMax++;
		}
		else if (time > 300.0f)
		{
			zombieMax = 8;
		}
	}

	public void addCount()
	{
		zombieCount+=1;
	}

	public void reduceCount()
	{
		zombieCount-=1;
	}

}
