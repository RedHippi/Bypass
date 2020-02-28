using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInterpreter : MonoBehaviour
{
    public string MyGame;
    public TexturePasser tp;
    public int index;
    private Camera Subject;

    // On Start, we are opening our Game
    void Start()//TODO: Get camera to map its view to the image.
    {
        
        //SceneManager.LoadScene(MyGame,LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Test()
    {
        SceneManager.LoadScene(MyGame, LoadSceneMode.Additive);
    }

    
}
