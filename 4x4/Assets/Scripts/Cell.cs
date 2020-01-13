using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    
    public enums.CellState state;
    public Material Mat0;
    public Material Mat1;
    public Material Mat2;
    public Material Mat3;
    bool isAnimating = false;
    float animationStartTime;
    public float animationDuration;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, (Time.time - animationStartTime) / animationDuration);
        }
        if (Time.time > animationStartTime + animationDuration)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isAnimating = false;
        }

    }

    public void SetColor(enums.CellState st, bool animate)
    {
        if (animate)
        {
            isAnimating = true;
            animationStartTime = Time.time;
            transform.localScale = Vector3.zero;
        }
        if (st == enums.CellState.Enmty)  GetComponent<MeshRenderer>().material = Mat0;
        if (st == enums.CellState.Color1) GetComponent<MeshRenderer>().material = Mat1;
        if (st == enums.CellState.Color2) GetComponent<MeshRenderer>().material = Mat2;
        //if (st == enums.CellState.Color3) GetComponent<MeshRenderer>().material = Mat3;
        state = st;


    }


}
