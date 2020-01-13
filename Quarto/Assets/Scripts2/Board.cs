using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

	public Stone[] stonePrefabs;
	public BoardSlot[] slots;
	Stone[] stones;

	void Start () {
		CreateStones();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateStones(){
		stones = new Stone[16];
		for (int i = 0; i < stones.Length; i++) {
			stones [i] = Instantiate(stonePrefabs[i]);
			stones [i].transform.position = new Vector3 (7f, 0f, 8f - (i * 1.1f));
			stones [i].transform.parent = this.transform.Find("Stones");
			stones [i].myLocation = Stone.StoneLocation.start;
		}
	}

	void ResetStones(){
		for (int i = 0; i < slots.Length; i++) {
			slots[i] = null; 
		}
		for (int i = 0; i < stones.Length; i++) {
			if (stones[i] != null)
			Destroy (stones [i].gameObject);
		}
		stones = new Stone[16];
		for (int i = 0; i < stones.Length; i++) {
			stones [i].transform.position = slots[i].transform.position;
		}

	}
}
