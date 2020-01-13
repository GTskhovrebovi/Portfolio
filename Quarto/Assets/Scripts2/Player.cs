using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public int playerNum;
	public enum TurnPhase {place_stone, select_stone, disabled};
	public TurnPhase myTurnPhase;

	Stone selectedStone = null;
	Stone stoneInHand = null;

	Board _board;
	CursorGui _cursorGui;
	Player otherPlayer;

	void Start () {
		Setup ();
	}

	void Update () {

		// SELECTING PHASE
		if (myTurnPhase == TurnPhase.select_stone) {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, 300f)) {
					//selecting stone
					if (hit.collider.gameObject.GetComponent<Stone> () != null) {
						if (hit.collider.gameObject.GetComponent<Stone> ().myLocation == Stone.StoneLocation.start) {
							if (selectedStone != null)
								selectedStone.Deselect ();
							selectedStone = hit.collider.gameObject.GetComponent<Stone> ();
							selectedStone.Select (playerNum);
							}
						}

					//giving to other player
					if (hit.collider.gameObject.tag == "stone_changer" && hit.collider.transform.parent != this.transform && selectedStone != null) {
						selectedStone.SetDestination (hit.collider.transform.position);
						otherPlayer.TakeStone (selectedStone);
						selectedStone.Deselect ();
						EndTurn ();
					}
				}


			}
		}
		// PLACING PHASE
		if (myTurnPhase == TurnPhase.place_stone) {
			if (Input.GetMouseButtonDown (0)) {
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hit = new RaycastHit ();
				if (Physics.Raycast (ray, out hit, 300f)) {
					//placing stone
					if (hit.collider.gameObject.GetComponent<BoardSlot> () != null) {
						BoardSlot _boardSlot = hit.collider.gameObject.GetComponent<BoardSlot> ();
						if (stoneInHand != null && _boardSlot.stone == null) {
							stoneInHand.SetDestination (_boardSlot.transform.position);
							stoneInHand.myLocation = Stone.StoneLocation.board;
							_boardSlot.stone = stoneInHand;
							Rules.CheckWinLose (_board, playerNum);
							stoneInHand = null;
							myTurnPhase = TurnPhase.select_stone;
						}

					}
				}
			}

		}

	}

	void Setup(){
		_board = GameObject.Find ("Board").GetComponent<Board>();
		_cursorGui = GameObject.Find ("Camera_Point").GetComponent<CursorGui>();
		if (playerNum == 1) otherPlayer = GameObject.Find ("Player2").GetComponent<Player>();
		else otherPlayer = GameObject.Find ("Player1").GetComponent<Player>();
	}
	void EndTurn(){
		myTurnPhase = TurnPhase.disabled;
		otherPlayer.myTurnPhase = TurnPhase.place_stone;
		_cursorGui.ChangeCursor (otherPlayer.playerNum);
	}
	void TakeStone(Stone _stone){
		stoneInHand = _stone;
		_stone.myLocation = Stone.StoneLocation.hand;
	}
/*
		if(Input.GetMouseButtonDown(0))
		{	
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if(Physics.Raycast (ray, out hit, 300f))
			{
				//if hit to stones when mode is CHANGING
				if(hit.collider.tag == objTagName &&
					hit.collider.gameObject.GetComponent<Stone>().movable &&
					_conditions.modeChanging && GetComponent<Rules>().win == false &&
					_conditions.gamePaused == false)
				{
					SelectGameObject();
				}
				//if hit to collider
				if(hit.collider.tag == collTagName && _conditions.modePlaying && _conditions.gamePaused == false)
				{
					if(selectedObject != null)
					{
						CellCollider _cellCollider= hit.transform.GetComponent<CellCollider>();
						if(_cellCollider.status == false)
						{
							if(_conditions.playerTurn == 1){
								_conditions.p1Step ++;
								_conditions.SwitchPlayer();
							}
							else if(_conditions.playerTurn == 2){
								_conditions.p2Step ++;
								_conditions.SwitchPlayer();
							}											
							_cellCollider.status = true;
							selectedObject.GetComponent<Stone>().SetDestination(hit.collider.gameObject.transform.position);

							GetComponent<Rules> ().CheckWinLose ();									
							_conditions.modeChanging = true;
							_conditions.modePlaying = false;  	
							ChangeRingColor ();			    			 				
							DeselectGameObjectIfSelected();					    							    							    			
						}			
					}
				}
				//if hit to stone changer;
				if(hit.collider.tag == stoneChanger && _conditions.modeChanging && _conditions.gamePaused == false)
				{
					if(selectedObject != null)
					{
						StoneChanger _stoneChanger = hit.transform.GetComponent<StoneChanger>();
						if(_stoneChanger.stoneChange == false)
						{
							if(_conditions.playerTurn == 1){
								_conditions.p1Step ++;
								_conditions.SwitchPlayer ();
							}
							else if(_conditions.playerTurn == 2){ 
								_conditions.p2Step ++; 
								_conditions.SwitchPlayer (); 
							}
							selectedObject.GetComponent<Stone> ().SetDestination (hit.collider.gameObject.transform.position);		
							_conditions.modeChanging = false; 
							_conditions.modePlaying = true; 
							ChangeRingColor ();
							selectedObject.GetComponent<Stone> ().movable = false;
						}				
					}
				}
			}
		}
	}

	//select stones
	void SelectGameObject(){
		if(selectedObject == null)
		{
			selectedObject = hit.collider.gameObject;
			tempPosition = selectedObject.transform.position;
			selectedObject.transform.FindChild("ring1").gameObject.SetActive(true);
			selectedObject.transform.FindChild("light").gameObject.SetActive(true);
			ChangeRingColor ();			    			 					
		}
		else
		{
			if(hit.collider.gameObject.transform.position == selectedObject.transform.position)
			{
				DeselectGameObjectIfSelected();
				selectedObject = hit.collider.gameObject;
				selectedObject.transform.FindChild("ring1").gameObject.SetActive(true);
				selectedObject.transform.FindChild("light").gameObject.SetActive(true);
				ChangeRingColor ();			    			 						
			}
			else
			{
				selectedObject.GetComponent<Stone> ().SetDestination (tempPosition);
				DeselectGameObjectIfSelected(); 
				selectedObject = hit.collider.gameObject;
				tempPosition = selectedObject.transform.position;
				selectedObject.transform.FindChild("ring1").gameObject.SetActive(true);
				selectedObject.transform.FindChild("light").gameObject.SetActive(true);
				ChangeRingColor ();			    			 						
			}
		}
	}

	//deselect stones
	void DeselectGameObjectIfSelected(){ 
		if(selectedObject != null)
		{ 
			selectedObject.transform.FindChild("ring1").gameObject.SetActive(false);
			selectedObject.transform.FindChild("light").gameObject.SetActive(false);
			selectedObject = null;
		}
	}

	//change stone's ring color
	void ChangeRingColor (){

		selectedObject.transform.FindChild("light").transform.GetComponent<Light>().intensity = 0.3f;
		if(_conditions.modePlaying){
			if(_conditions.playerTurn == 1){
				//yield WaitForSeconds(0.5);
				if(selectedObject){
					selectedObject.transform.FindChild("ring1").transform.GetComponent<Renderer>().material = material1;
					selectedObject.transform.FindChild("light").transform.GetComponent<Light>().color = new Color(0,004,005);
				}
			}
			else{
				//yield WaitForSeconds(0.5);
				if(selectedObject){
					selectedObject.transform.FindChild("ring1").transform.GetComponent<Renderer>().material = material2;
					selectedObject.transform.FindChild("light").transform.GetComponent<Light>().color = new Color(004,004,0);
				}
			}
		}else{
			if(_conditions.playerTurn == 1){
				selectedObject.transform.FindChild("ring1").transform.GetComponent<Renderer>().material = material1;
				selectedObject.transform.FindChild("light").transform.GetComponent<Light>().color = new Color(0,004,005);
			}else{
				selectedObject.transform.FindChild("ring1").transform.GetComponent<Renderer>().material = material2;
				selectedObject.transform.FindChild("light").transform.GetComponent<Light>().color = new Color(004,004,0);
			}
		}
	}

	//check win/lose
//	void Check (){
//		//yield WaitForSeconds(0.6f);
//		GetComponent(script_rules).CheckWinLose ();
//	}

*/
}
