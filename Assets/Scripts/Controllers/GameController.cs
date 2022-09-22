using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Ant
{
    Ant1,
    Ant2
}

public class GameController : MonoBehaviour
{
    public static GameController GC;

    public Ant currentAnt;

    private void Awake()
    {
        Screen.SetResolution(160, 144, false);
        DontDestroyOnLoad(gameObject);
        GC = this;
        currentAnt = Ant.Ant1;

        #region BAD CODE DONT ENTER
        //VERY BAD CODE, BUT SHIT, ITS A JAM WHAT CAN I SAY?
        if (GameObject.FindGameObjectsWithTag("Controller").Length > 1)
        {
            Destroy(GameObject.FindGameObjectsWithTag("Controller")[0]);
        }
        //END OF VERY BAD CODE
        #endregion
    }


    private void Update()
    {
        ChangeAnts();
    }

    private void ChangeAnts()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            switch (currentAnt)
            {
                case Ant.Ant1:
                    currentAnt = Ant.Ant2;
                    break;
                case Ant.Ant2:
                    currentAnt = Ant.Ant1;
                    break;
            }
        }
    }

    public Ant GetCurrentAnt()
    {
        return currentAnt;
    }

    #region ButtonMethods
    public void LoadSceneIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion

}
