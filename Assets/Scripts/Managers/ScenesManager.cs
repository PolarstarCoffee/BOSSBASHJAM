using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    //static class makes it so this method is accessible in any way within any function. 
   public static ScenesManager instance;
    //SINGLETON gwagwa
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        instance = this;
    }

    public enum Scene //Levels are by index (mainMenu = 0, Level01 = 1, etc.. NOTE: MAKE SURE ENUM ENTRIES MATCH ORDER WITHIN UNITY'S BUILD SETTINGS, OR GAME WILL BLOW UP 
    {
        mainMenuSAMPLE,
        
    }

    // Load Scene method
    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString()); //scene enum needs to be converted to string to be readable by method
    }

    //load new game method
    public void LoadNewGame() 
    {
        //Put whichever scene here
        //SceneManager.LoadScene(Scene.LayOut_Test.ToString());
    }


    //Load next scene method (Does so by increasing index of Unity build index by 1)
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Loads main menu scene 
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.mainMenuSAMPLE.ToString());
    }
    //Exits application
    public void endGame()
    {
        Application.Quit();
    }
}
