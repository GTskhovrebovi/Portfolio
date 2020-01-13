using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    public int BoardX;
    public int BoardY;
    public int StartingCells;
    public float shift;
    public int points = 0;
    public Text PointsText;
    public Text HighScoreText;
    public GameObject SuicideCell;
    public GameObject CellPrefab;
    public GameObject CellBGPrefab;
    public GameObject[,] Cells;
    GameObject[,] CellBGS;
    public Swipe swipeControls;

    Vector3 StartingPos;

    void Start()
    {
        StartingPos = transform.position;
        ResetBoard();
        //print(Cells[0, 0].transform.position);
        if (!PlayerPrefs.HasKey("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", 0);
            HighScoreText.text = "0";
        }
        else
        {
            HighScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    public void ResetBoard()
    {
        ResetPoints();
        if (Cells != null)
            foreach (GameObject go in Cells)
                Destroy(go);
        if (CellBGS != null)
            foreach (GameObject go in CellBGS)
                Destroy(go);

        Cells = new GameObject[BoardX, BoardY];
        CellBGS = new GameObject[BoardX, BoardY];

        for (int i = 0; i < BoardX; i++)
        {
            for (int j = 0; j < BoardY; j++)
            {
                Cells[i, j] = Instantiate(CellPrefab, this.transform);
                CellBGS[i, j] = Instantiate(CellBGPrefab, this.transform);
                Cells[i, j].transform.position = transform.position + new Vector3(shift * i, shift * j, 0);
                CellBGS[i, j].transform.position = transform.position + new Vector3(shift * i, shift * j, 0);
                Cells[i, j].GetComponent<Cell>().SetColor(enums.CellState.Enmty, true);

            }
        }
        AvtivateRandom(StartingCells);
        transform.position = StartingPos - 1* new Vector3((BoardX - 1) * shift / 2, (BoardY - 1) * shift / 2, 0);
    }

    void AvtivateRandom()
    {
        int spaces = GetAviableSpaces();
        if (spaces > 0)
        {
            int position = Mathf.FloorToInt(Random.Range(0, spaces));

            int Cnt = 0;
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (Cells[i, j].GetComponent<Cell>().state == enums.CellState.Enmty)
                    {
                        if (Cnt == position)
                        {
                            int x = Random.Range(0,2);
                            enums.CellState cs = enums.CellState.Enmty;
                            if (x == 0) cs = enums.CellState.Color1;
                            if (x == 1) cs = enums.CellState.Color2;
                            //if (x == 2) cs = enums.CellState.Color3;
                            //enums.CellState cs = Random.value > 0.5f ? enums.CellState.Color1 : enums.CellState.Color2;
                            
                            Cells[i, j].GetComponent<Cell>().SetColor(cs, true);
                            //Debug.Log("AVTIVATION ON POSITION" + Cells[i, j].transform.position + "WITH COLOR : " + cs);
                            return;
                        }
                        else Cnt++;
                    }
                }
            }
        }
    }
    void AvtivateRandom(int N) {
        for (int i = 0; i < N; i++)
            AvtivateRandom();
    }


    public int GetAviableSpaces()
    {
        int Cnt = 0;
        if (Cells != null)
        {
            for (int i = 0; i < Cells.GetLength(0); i++)
            {
                for (int j = 0; j < Cells.GetLength(1); j++)
                {
                    if (Cells[i, j].GetComponent<Cell>().state == enums.CellState.Enmty) Cnt++;
                }
            }
        }
        return Cnt;
    }

    public void Move(Vector3 moveDir)
    {
        int totalCellsMoved = 0;
        if (moveDir == Vector3.right)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = Cells.GetLength(0) - 1; x >= 0; x--)
                {
                    if (Cells[x, y].GetComponent<Cell>().state != enums.CellState.Enmty)
                    {
                        for (int k = Cells.GetLength(0) - 1; k > x; k--)
                        {
                            if (Cells[k, y].GetComponent<Cell>().state == enums.CellState.Enmty)
                            {
                                //MOVE
                                //Debug.Log("MOVING CELL:  " + Cells[x, y].GetComponent<Cell>().state + "  X : " + x + y);
                                Cells[k, y].GetComponent<Cell>().SetColor(Cells[x, y].GetComponent<Cell>().state, false);
                                Cells[x, y].GetComponent<Cell>().SetColor(enums.CellState.Enmty, false);
                                totalCellsMoved++;
                                break;
                            }
                        }
                    }
                }
            }
        }
        if (moveDir == Vector3.left)
        {
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    if (Cells[x, y].GetComponent<Cell>().state != enums.CellState.Enmty)
                    {
                        for (int k = 0; k < x; k++)
                        {
                            if (Cells[k, y].GetComponent<Cell>().state == enums.CellState.Enmty)
                            {
                                //MOVE
                                Cells[k, y].GetComponent<Cell>().SetColor(Cells[x, y].GetComponent<Cell>().state,false);
                                Cells[x, y].GetComponent<Cell>().SetColor(enums.CellState.Enmty, false);
                                totalCellsMoved++;
                                //Debug.Log("MOVING CELL" + Cells[x, y].transform.position);
                                break;
                            }
                        }
                    }
                }
            }
        }
        if (moveDir == Vector3.up)
        {
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = Cells.GetLength(1) - 1; y >= 0; y--)
                {
                    if (Cells[x, y].GetComponent<Cell>().state != enums.CellState.Enmty)
                    {
                        for (int k = Cells.GetLength(1) - 1; k > y; k--)
                        {
                            if (Cells[x, k].GetComponent<Cell>().state == enums.CellState.Enmty)
                            {
                                //MOVE
                                Cells[x, k].GetComponent<Cell>().SetColor(Cells[x, y].GetComponent<Cell>().state,false);
                                Cells[x, y].GetComponent<Cell>().SetColor(enums.CellState.Enmty,false);
                                totalCellsMoved++;
                                //Debug.Log("MOVING CELL" + Cells[x, y].transform.position);
                                break;

                            }
                        }
                    }
                }
            }
        }
        if (moveDir == Vector3.down)
        {
            for (int x = 0; x < Cells.GetLength(0); x++)
            {
                for (int y = 0; y < Cells.GetLength(1); y++)
                {
                    if (Cells[x, y].GetComponent<Cell>().state != enums.CellState.Enmty)
                    {
                        for (int k = 0; k < y; k++)
                        {
                            if (Cells[x, k].GetComponent<Cell>().state == enums.CellState.Enmty)
                            {
                                //MOVE
                                Cells[x, k].GetComponent<Cell>().SetColor(Cells[x, y].GetComponent<Cell>().state,false);
                                Cells[x, y].GetComponent<Cell>().SetColor(enums.CellState.Enmty,false);
                                totalCellsMoved++;
                                //Debug.Log("MOVING CELL" + Cells[x, y].transform.position);
                                break;
                            }
                        }
                    }
                }
            }
        }

        if (totalCellsMoved > 0 || GetAviableSpaces() == Cells.GetLength(0) * Cells.GetLength(1)) AvtivateRandom();
        //Debug.Log(totalCellsMoved);
        CheckBoard();
    }

    int CountInRows(int N)
    {
        int cnt = 0;
        if (Cells != null)
        {
            for (int j = 0; j < Cells.GetLength(1); j++)
                if (Cells[N, j].GetComponent<Cell>().state != enums.CellState.Enmty) cnt++;
        }
        return cnt;
    }

    void Control()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || swipeControls.SwipeLeft)     Move(Vector3.left);
        if (Input.GetKeyDown(KeyCode.RightArrow) || swipeControls.SwipeRight)   Move(Vector3.right);
        if (Input.GetKeyDown(KeyCode.UpArrow) || swipeControls.SwipeUp)         Move(Vector3.up);
        if (Input.GetKeyDown(KeyCode.DownArrow) || swipeControls.SwipeDown)     Move(Vector3.down);
        //if (Input.GetKeyDown(KeyCode.R)) Application.LoadLevel(0);
    }

    void CheckBoard()
    {
        for (int x = 0; x < Cells.GetLength(0); x++)
        {
            enums.CellState firstState = Cells[x, 0].GetComponent<Cell>().state; 
            bool check = true;
            for (int y = 0; y < Cells.GetLength(1); y++)
            {
                if (Cells[x, y].GetComponent<Cell>().state != firstState) { check = false; break; }
                
            }
            if (check && firstState != enums.CellState.Enmty)
            {//DESTRoY ROW
                //Debug.Log(firstState + " " + check);
                for (int y = 0; y < Cells.GetLength(1); y++)
                {

                    GameObject tempCell = Instantiate(SuicideCell);
                    tempCell.transform.position = Cells[x, y].transform.position;
                    tempCell.GetComponent<Renderer>().material = Cells[x, y].GetComponent<Renderer>().material;
                    //Debug.Log("DELETING POSITION:" + Cells[x, y].transform.position + "COLOR : " + Cells[x, y].GetComponent<Cell>().state);
                    Cells[x, y].GetComponent<Cell>().SetColor(enums.CellState.Enmty,false);
                    AddPoints(1);
                }
            }
        }
        
        for (int y = 0; y < Cells.GetLength(1); y++)
        {
            enums.CellState firstState = Cells[0, y].GetComponent<Cell>().state;
            bool check = true;
            for (int x = 1; x < Cells.GetLength(0); x++)
            {
                if (Cells[x, y].GetComponent<Cell>().state != firstState) { check = false; break; }
            }
            if (check && firstState != enums.CellState.Enmty)
            {//DESTRoY ROW
                for (int x = 0; x < Cells.GetLength(0); x++)
                {
                    GameObject tempCell = Instantiate(SuicideCell);
                    tempCell.transform.position = Cells[x, y].transform.position;
                    tempCell.GetComponent<Renderer>().material = Cells[x, y].GetComponent<Renderer>().material;
                    Cells[x, y].GetComponent<Cell>().SetColor(enums.CellState.Enmty,false);
                    AddPoints(1);
                }
            }
        }

    }

    void ResetPoints()
    {
        points = 0;
        PointsText.text = points.ToString();
    }
    void AddPoints(int N)
    {
        points += N;
        PointsText.text = points.ToString();
        if (points > PlayerPrefs.GetInt("HighScore"))
        {
            PlayerPrefs.SetInt("HighScore", points);
            HighScoreText.text = points.ToString();
        }
    }
    


}
