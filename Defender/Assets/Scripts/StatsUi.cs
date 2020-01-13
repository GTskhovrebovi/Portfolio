using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUi : MonoBehaviour
{
    
    [SerializeField] GameObject[] buttons;
    [SerializeField] Controller controller;
    [SerializeField] Manager manager;

    Animator anim;

    public Text pointsText;
    public Text levelText;



    public Action OnStartGamePressed;

    private void Awake() 
    {
        anim = GetComponent<Animator>();    
    }

    void Start()
    {
        manager.StartLevel += StartLevel;
        manager.EndLevel += EndLevel;
        RefreshUI();
    }

    // Update is called once per frame
    void Update()
    {
        RefreshPoints();
    }


    public void ButtonPress (GameObject button)
    {
        //Debug.Log(1);
        ResetAllButtons();
        button.GetComponent<Canvas>().sortingOrder = 101;
    }

    void ResetAllButtons()
    {
        foreach (GameObject go in buttons)
        {
            go.GetComponent<Canvas>().sortingOrder = 100;
        }
    }

    public void FadeIn()
    {
        RefreshUI();
        anim.SetBool("Active", true);
    }

    public void FadeOut()
    {
        RefreshUI();
        anim.SetBool("Active", false);
    }

    public void StartLevel()
    {
        FadeOut();
    }
    public void EndLevel()
    {   
        FadeIn();
    }


    public void StartGamePressed()
    {
        OnStartGamePressed();
        //manager.StartLevel();
    }


    public void damageLevelButtonPressed()
    {
        controller.stats.damageLevel++;
    }
    // public int critLevel;
    // public int cdReductionLevel;
    // public int castTimeLevel;

    void RefreshPoints()
    {
        pointsText.text = controller.stats.points.ToString();
    }

    void RefreshUI()
    {
        levelText.text = "Level " + controller.stats.currentLevel;
    }

}
