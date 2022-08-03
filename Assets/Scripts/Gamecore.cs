using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gamecore : MonoBehaviour
{
    public UnityEvent gameover_Event;
    public UnityEvent win_Event;
    
    public GameObject win_UI;
    public GameObject start_UI;
    public GameObject timer_UI;
    public GameObject menu_UI;
    public GameObject info_UI;
    public GameObject gameover_UI;
    public GameObject mom_dino;
    
    bool menuShowed;
    bool gameover;
    bool gamestarted;
    bool win;


    Slider slider;
    TextMeshProUGUI textMeshPro;
    DateTime start;
    DateTime delaytime;


    void Start()
    {
        menuShowed = false;
        slider = timer_UI.GetComponent<Slider>();
        textMeshPro = timer_UI.GetComponentInChildren<TextMeshProUGUI>();
        start = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {   
        if(!gameover && gamestarted && Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu(!menuShowed);
            menuShowed = !menuShowed;
        }
        if(!gameover && info_UI.activeSelf && Input.GetMouseButtonDown(0))
        {
            info_UI.SetActive(false);
        }

        if(gameover && DateTime.Now > delaytime)
        {
            Time.timeScale = 0;
        }

        if(gameover && Input.GetKey(KeyCode.Return))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if(!win && !gamestarted && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0 || Input.GetKey(KeyCode.Space)))
        {
            mom_dino.GetComponent<Movement>().enabled = true;
            start_UI.SetActive(false);
            timer_UI.SetActive(true);

            start = DateTime.Now;

            gamestarted = true;
        }

        if(!win && gamestarted && (DateTime.Now - start).TotalSeconds > 60)
        {
            Gameover(0);
        }

        if(!win &&  gamestarted && !gameover)
        {
            slider.value = 1.0f - (DateTime.Now - start).Seconds * (1f/60f);
            textMeshPro.text = (60 - (DateTime.Now - start).Seconds).ToString();
        }
        

    }
    
    public void ToggleMenu(bool show)
    {
        if(show)
        {
            Time.timeScale = 0;
            timer_UI.SetActive(false);
            menu_UI.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            timer_UI.SetActive(true);
            menu_UI.SetActive(false);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
    public void showInfo()
    {
        info_UI.SetActive(true);
    }

    public void Gameover(float delay)
    {
        gameover = true;
        gameover_UI.SetActive(true);

        delaytime = DateTime.Now.AddSeconds((double)delay);

        gameover_Event.Invoke();
    }

    public void Win()
    {
        win = true;
        Time.timeScale = 0;
        win_UI.SetActive(true);
        win_Event.Invoke();
    }
}
