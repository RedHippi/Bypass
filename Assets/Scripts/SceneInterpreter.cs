using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInterpreter : MonoBehaviour
{
    public string MyGame;
    private Camera Subject;


    // On Start, we are opening our Game
    void Start()
    {
        //SceneManager.LoadScene(MyGame,LoadSceneMode.Additive);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()//TODO: Get camera to map its view to the image.
    {
        SceneManager.LoadScene(MyGame, LoadSceneMode.Additive);
        GameObject[] g = SceneManager.GetSceneByName(MyGame).GetRootGameObjects();
        Subject = g[0].GetComponent<Camera>();
       // gameObject.GetComponent<Image>().material = 

    
}
