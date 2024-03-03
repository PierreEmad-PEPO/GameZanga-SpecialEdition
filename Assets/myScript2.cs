using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class myScript2 : MonoBehaviour
{
    // Start is called before the first frame update
   public void endGame()
    {
        Application.Quit();
    }
    public void backToMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }
}
