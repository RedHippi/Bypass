using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*  The SceneInterpreter is used for our game windows that relay
 *  information from another scene. The interpreter helps open and close
 *  this scene. 
                                                                            */
public class SceneInterpreter : MonoBehaviour
{
    public string MyGame;

    // On Start, we are opening our Game
    void Start()
    {
        SceneManager.LoadScene(MyGame,LoadSceneMode.Additive);


    }


    //When we go to close our window, this function is invoked to close the corresponding
    public void CloseMyGame()
    {
        SceneManager.UnloadSceneAsync(MyGame);
    }

    
}
