﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInterpreter : MonoBehaviour
{
    public string MyGame;

    // On Start, we are opening our Game
    void Start()
    {
        SceneManager.LoadScene(MyGame,LoadSceneMode.Additive);


    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Test()
    {
    }

    
}