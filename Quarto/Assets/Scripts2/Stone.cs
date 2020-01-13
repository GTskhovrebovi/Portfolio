using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour {
	
	public enum height 	 {tall, low};
	public enum color 		 {black, white};
	public enum shape 		 {square, round};
	public enum consistency {hollow, solid};

	public height myHeight;
	public color myColor;
	public shape myShape;
	public consistency myConsistency;

	public enum StoneLocation {start, hand, board};
	public StoneLocation myLocation;

	//public bool movable = true;

	public bool moving = false;
	Vector3 start;
	Vector3 end;
	float startTime;
	public float travelTime = 4f;

	GameObject _ring;
	public Material player1_mat;
	public Material player2_mat;

	void Start(){
		_ring = transform.Find ("ring1").gameObject;

	}

	void Update () {
		moveIfNeeded ();
	}


	public void Select(int playerNum){
		
		Material _mat;
		if (playerNum == 1)
			_mat = player1_mat;
		else if (playerNum == 2)
			_mat = player2_mat;
		else
			return;
		_ring.SetActive (true);
		_ring.GetComponent<Renderer> ().material = _mat;
	}
	public void Deselect(){

		_ring.SetActive (false);
	}

	public void ActivateGlowRing( int playerNum ){
		
		Material _winmat;
		if (playerNum == 1)
			_winmat = player1_mat;
		else if (playerNum == 2)
			_winmat = player2_mat;
		else
			_winmat = null;
		_ring.SetActive (true);
		_ring.GetComponent<Renderer> ().material = _winmat;
	}

	public void SetDestination(Vector3 point){
		startTime = Time.time;
		start = transform.position;
		end = point;
		moving = true;
	}

	public void moveIfNeeded(){
	
		if(moving){
			this.transform.position = Vector3.Lerp(start, end, (Time.time - startTime)/(float)travelTime);
			if(this.transform.position == end){
				moving = false;
			}
		}

	}

}
