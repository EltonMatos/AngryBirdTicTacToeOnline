using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class UIMANAGER : MonoBehaviour
{
    public static UIMANAGER instance;

    private Animator panelWinAnim, panelLoseAnim, panelPigWinAnim, panelBirdWinAnim, panelTiedAnim;
    private Button winBtnMenu, winBtnAgain;
    private Button loseBtnMenu, loseBtnAgain;
    private Button birdBtn, pigBtn;   

    private Button BTN_Easy, BTN_Hard, BTN_Normal;
        
    public int dificuldade;   
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }        
        SceneManager.sceneLoaded += Carrega;
    }

    void Carrega(Scene cena, LoadSceneMode modo)
    {
        if(WhereAmI.instance.fase == 3)
        {
            //painel
            panelWinAnim = GameObject.Find("Menu_Win").GetComponent<Animator>();
            panelLoseAnim = GameObject.Find("Menu_Lose").GetComponent<Animator>();
            panelPigWinAnim = GameObject.Find("Menu_Pig").GetComponent<Animator>();
            panelBirdWinAnim = GameObject.Find("Menu_Bird").GetComponent<Animator>();
            panelTiedAnim = GameObject.Find("Menu_Tied").GetComponent<Animator>();

            //BTN_win
            winBtnAgain = GameObject.Find("BTN_Reiniciar").GetComponent<Button>();
            winBtnMenu = GameObject.Find("BTN_Menu").GetComponent<Button>();
            //BTN_Lose
            loseBtnAgain = GameObject.Find("BTN_Reiniciar_Lose").GetComponent<Button>();
            loseBtnMenu = GameObject.Find("BTN_Menu_Lose").GetComponent<Button>();
        }
    }

    void Start()
    {
        //fase LevelMenu
        if (WhereAmI.instance.fase == 5)
        {
            BTN_Easy = GameObject.FindWithTag("easymode").GetComponent<Button>();
            BTN_Normal = GameObject.FindWithTag("normalmode").GetComponent<Button>();
            BTN_Hard = GameObject.FindWithTag("hardmode").GetComponent<Button>();            
        }
    }

    void Update()
    {
        //fase PlayGame
        if (WhereAmI.instance.fase == 3)
        {
            AtivarAnimacao();
        }        
    }
    public int getDificuldade()
    {
        return dificuldade;
    }

    public void setDificuldade(int dificul)
    {
        dificuldade = dificul;
    }

    public void ReiniciarJogo()
    {
        if(BoardManager.Instance.level == Difficult.hard)
        {
            difficultHard();
        }
        if (BoardManager.Instance.level == Difficult.easy)
        {
            difficultEasy();
        }
        if(BoardManager.Instance.level == Difficult.normal)
        {
            difficultNormal();
        }
        if (BoardManager.Instance.level == Difficult.pvpLocal)
        {
            PvPLocal();
        }
        if (BoardManager.Instance.level == Difficult.pvpOn)
        {
            PvPOnline();
        }
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LevelMenu()
    {
        SceneManager.LoadScene("GamePlayLocal");                
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    public void difficultEasy()
    {
        dificuldade = 1;
        SceneManager.LoadScene("WhoStarts");        
    }

    public void difficultHard()
    {
        dificuldade = 2;
        SceneManager.LoadScene("WhoStarts");        
    }

    public void difficultNormal()
    {
        dificuldade = 3;
        SceneManager.LoadScene("WhoStarts");        
    }

    public void PvPLocal()
    {
        dificuldade = 4;
        SceneManager.LoadScene("WhoStarts");        
    }

    public void PvPOnline()
    {
        dificuldade = 5;
    }

    public void AtivarAnimacao()
    {
        if (BoardController.Instance.statusFinalGame == 1)
        {
            //derrota
            //AudioManager.instance.audioS.Stop();
            panelLoseAnim.Play("MenuLoseAnim");
        }
        if (BoardController.Instance.statusFinalGame == 2)
        {
            //vitoria
            //AudioManager.instance.audioS.Stop();
            panelWinAnim.Play("MenuWinAnim");
        }
        if (BoardController.Instance.statusFinalGame == 3)
        {
            //empate
            //AudioManager.instance.audioS.Stop();
            panelTiedAnim.Play("MenuTiedAnim");
        }
        if (BoardController.Instance.statusFinalGame == 4)
        {
            //pigWin
            //AudioManager.instance.audioS.Stop();
            panelPigWinAnim.Play("MenuPigWinAnim");
        }
        else if (BoardManager.Instance.getStatus() == 5)
        {
            //birdWin
            //AudioManager.instance.audioS.Stop();
            panelBirdWinAnim.Play("MenuBirdWinAnim");
        }
    }

    /*public void AtivarAnimacao()
    {
        if (BoardManager.Instance.getStatus() == 1)
        {
            //derrota
            AudioManager.instance.audioS.Stop();            
            panelLoseAnim.Play("MenuLoseAnim");
        }
        if (BoardManager.Instance.getStatus() == 2)
        {
            //vitoria
            AudioManager.instance.audioS.Stop();            
            panelWinAnim.Play("MenuWinAnim");
        }
        if (BoardManager.Instance.getStatus() == 3)
        {
            //empate
            AudioManager.instance.audioS.Stop();
            panelTiedAnim.Play("MenuTiedAnim");
        }
        if (BoardManager.Instance.getStatus() == 4)
        {
            //pigWin
            AudioManager.instance.audioS.Stop();            
            panelPigWinAnim.Play("MenuPigWinAnim");
        }
        else if (BoardManager.Instance.getStatus() == 5)
        {
            //birdWin
            AudioManager.instance.audioS.Stop();           
            panelBirdWinAnim.Play("MenuBirdWinAnim");
        }
    }*/

}
