using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Track : MonoBehaviour {

	public GameObject[] obstacles;
	public GameObject[] junkfoods;
	public GameObject[] coins;
	public Vector2 numberOfObstacles;
	public Vector2 numberOfCoins;
	public Vector2 numberOfJunkfoods;

	public List<GameObject> newObstacles;
	public List<GameObject> newCoins;
	public List<GameObject> newJunkfoods;

	// Use this for initialization
	void Start () {

		int newNumberOfObstacles = (int)Random.Range(numberOfObstacles.x, numberOfObstacles.y);
		int newNumberOfCoins = (int)Random.Range(numberOfCoins.x, numberOfCoins.y);
		int newNumberOfJunkfoods = (int)Random.Range(numberOfJunkfoods.x, numberOfJunkfoods.y);

		for (int i = 0; i < newNumberOfObstacles; i++)
		{
			newObstacles.Add(Instantiate(obstacles[Random.Range(0, obstacles.Length)], transform));
			newObstacles[i].SetActive(false);
		}

		for (int i=0; i < newNumberOfCoins; i++)
		{
			newCoins.Add(Instantiate(coins[Random.Range(0, coins.Length)], transform));
			newCoins[i].SetActive(false);
		}

		for (int i=0; i < newNumberOfJunkfoods; i++)
		{
			newJunkfoods.Add(Instantiate(junkfoods[Random.Range(0, junkfoods.Length)], transform));
			newJunkfoods[i].SetActive(false);
		}

		PositionateObstacles();
		PositionateCoins();
		PositionateJunkfoods();

	}
	
	void PositionateJunkfoods()
	{
		for(int i = 0; i < newJunkfoods.Count; i++)
		{
			float posZMin = (297f / newJunkfoods.Count) + (297f / newJunkfoods.Count) * i;
			float posZMax = (297f / newJunkfoods.Count) + (297f / newJunkfoods.Count) * i + 1;
			newJunkfoods[i].transform.localPosition = new Vector3(0,.5f, Random.Range(posZMin, posZMax));
			newJunkfoods[i].SetActive(true);
			if(newJunkfoods[i].GetComponent<ChangeLane>() != null)
				newJunkfoods[i].GetComponent<ChangeLane>().PositionLane();
		}
	}

	void PositionateObstacles()
	{
		for(int i = 0; i < newObstacles.Count; i++)
		{
			float posZMin = (297f / newObstacles.Count) + (297f / newObstacles.Count) * i;
			float posZMax = (297f / newObstacles.Count) + (297f / newObstacles.Count) * i + 1;
			newObstacles[i].transform.localPosition = new Vector3(0,0, Random.Range(posZMin, posZMax));
			newObstacles[i].SetActive(true);
			if(newObstacles[i].GetComponent<ChangeLane>() != null)
				newObstacles[i].GetComponent<ChangeLane>().PositionLane();
		}
	}

	void PositionateCoins()
	{
		for (int i = 0; i < newCoins.Count; i++)
		{
			float posZMin = (297f / newCoins.Count) + (297f / newCoins.Count) * i;
			float posZMax = (297f / newCoins.Count) + (297f / newCoins.Count) * i + 1;
			newCoins[i].transform.localPosition = new Vector3(0,.5f, Random.Range(posZMin, posZMax));
			newCoins[i].SetActive(true);
			newCoins[i].GetComponent<ChangeLane>().PositionLane();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			other.GetComponent<Player>().IncreaseSpeed();
			transform.position = new Vector3(0,0, transform.position.z + 297 * 2);
			PositionateObstacles();
			PositionateCoins();
			PositionateJunkfoods();
		}
	} 

}
