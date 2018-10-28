using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMenuManager : MonoBehaviour {

	public List<GameObject> objectList, objectPrefabList;
	public int currentObject = 0;
	private int listCount = 0;

	void Start(){
		foreach(Transform child in transform){
			objectList.Add(child.gameObject);
		}
		listCount = objectList.Count - 1;
	}

	public void MenuLeft(){
		// swiping menu left
		objectList[currentObject].SetActive(false); //deactivate current object
		currentObject--; //increment to next object
		if(currentObject < 0){
			currentObject = listCount;
		}
		objectList[currentObject].SetActive(true); //activate new current object
	}

	public void MenuRight(){
		// swiping menu right
		objectList[currentObject].SetActive(false); //deactivate current object
		currentObject++; //increment to next object
		if(currentObject > listCount){
			currentObject = 0;
		}
		objectList[currentObject].SetActive(true); //activate new current object
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SpawnCurrentObject(){
		/*
		foreach (GameObject prefab in objectPrefabList)
		{
			if (prefab != objectPrefabList[currentObject])
			{
				DestroyImmediate(prefab);
			}
		}
		*/
		Instantiate (objectPrefabList [currentObject], 
			objectList [currentObject].transform.position, 
			objectList [currentObject].transform.rotation);
	}
}
