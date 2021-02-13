﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGameController : Singleton<BaseGameController>
{

    #region Editor Fields

    [SerializeField]
    private PlayerController playerController;


    #endregion

    #region Unity methods
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public Fields


    public void PauseGame()
    {
        Time.timeScale = 0;
        playerController.PlayerInputEnabled = false;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        playerController.PlayerInputEnabled = true;
    }

    #endregion
}
