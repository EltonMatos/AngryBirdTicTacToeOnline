using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhereAmI : MonoBehaviour
{
    public int phase = -1;  

    public static WhereAmI instance;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += VerificaFase;
    }

    void VerificaFase(Scene cena, LoadSceneMode modo)
    {
        phase = SceneManager.GetActiveScene().buildIndex;
    }

    
    
}
