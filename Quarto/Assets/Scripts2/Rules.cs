using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Rules {
	

	public static bool win = false;

	//Material material1;
	//Material material2;
	//Material material3;
	//Material material4;


	//win/lose checking
	public static void CheckWinLose (Board board, int playerNum){

		for (int i = 0; i <= 3; i++) {
			singleCheck (board.slots[i*4 + 0], board.slots[i*4 + 1], board.slots[i*4 + 2], board.slots[i*4 + 3], playerNum );
		}

		for (int i = 0; i <= 3; i++) {
			singleCheck (board.slots[i], board.slots[i+4], board.slots[i+8], board.slots[i+12], playerNum );
		}
		singleCheck (board.slots[0], board.slots[5], board.slots[10], board.slots[15], playerNum );
		singleCheck (board.slots[3], board.slots[6], board.slots[9], board.slots[12], playerNum);

	}

	//checking on four objects
	static void singleCheck (BoardSlot boardSlot_1, BoardSlot boardSlot_2, BoardSlot boardSlot_3, BoardSlot boardSlot_4, int playerNum){

		if (boardSlot_1.stone == null || boardSlot_2.stone == null || boardSlot_3.stone == null || boardSlot_4.stone == null)
			return;

		if(compareFour(boardSlot_1.stone.myHeight.ToString(), boardSlot_2.stone.myHeight.ToString(), boardSlot_3.stone.myHeight.ToString(), boardSlot_4.stone.myHeight.ToString()))
		{
			TurnOnDetails(boardSlot_1, boardSlot_2, boardSlot_3, boardSlot_4, playerNum);

			win = true;
			//////////yield WaitForSeconds(3);
			//Application.LoadLevel ("screen_Win");
		}

		if(compareFour(boardSlot_1.stone.myColor.ToString(), boardSlot_2.stone.myColor.ToString(), boardSlot_3.stone.myColor.ToString(), boardSlot_4.stone.myColor.ToString()))
		{
			TurnOnDetails(boardSlot_1, boardSlot_2, boardSlot_3, boardSlot_4, playerNum);

			//print("COLOR!!!!");
			win = true;
			//////////yield WaitForSeconds(3);
			//Application.LoadLevel ("screen_Win");
		}
		if(compareFour(boardSlot_1.stone.myShape.ToString(), boardSlot_2.stone.myShape.ToString(), boardSlot_3.stone.myShape.ToString(), boardSlot_4.stone.myShape.ToString()))
		{
			TurnOnDetails(boardSlot_1, boardSlot_2, boardSlot_3, boardSlot_4, playerNum);

			//print("SHAPE!!!!");
			win = true;
			/////////////yield WaitForSeconds(3);
			//Application.LoadLevel ("screen_Win");
		}
		if(compareFour(boardSlot_1.stone.myConsistency.ToString(), boardSlot_2.stone.myConsistency.ToString(), boardSlot_3.stone.myConsistency.ToString(), boardSlot_4.stone.myConsistency.ToString()))
		{
			TurnOnDetails(boardSlot_1, boardSlot_2, boardSlot_3, boardSlot_4, playerNum);

			//print("CONSISTENCY!!!!");
			win = true;
			////////////////yield WaitForSeconds(3);
			//Application.LoadLevel ("screen_Win");
		}
	}


	//turn on lighting details
	static void TurnOnDetails (BoardSlot boardSlot_1, BoardSlot boardSlot_2, BoardSlot boardSlot_3, BoardSlot boardSlot_4, int playerNum){


		boardSlot_1.EnableGlow(playerNum);
		boardSlot_2.EnableGlow(playerNum);
		boardSlot_3.EnableGlow(playerNum);
		boardSlot_4.EnableGlow(playerNum);

		boardSlot_1.stone.Select(playerNum);
		boardSlot_2.stone.Select(playerNum);
		boardSlot_3.stone.Select(playerNum);
		boardSlot_4.stone.Select(playerNum);


	}


	//comparing four elements
	static bool compareFour (string a, string b, string c, string d){
		return a != null && a == b && b == c && c == d;
	}



}
