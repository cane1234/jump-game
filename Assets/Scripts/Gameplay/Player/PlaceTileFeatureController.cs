using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTileFeatureController : MonoBehaviour
{
    #region Private Fields
    private bool isOnCooldown;

    long timeStarted;
    #endregion

    #region Editor Fields
    [SerializeField]
    private float cooldownTime;

    public Text IsOnCooldownText;
    #endregion

    #region Constants
    private const string readyText = "Ready";
    #endregion

    #region Unity Methods
    // Start is called before the first frame update
    void Start()
    {
        isOnCooldown = false;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !isOnCooldown)
        {
            Tuple<float, float> tilePosition = CalculateTilePosition();
            PlaceTile(tilePosition.Item1, tilePosition.Item2);
            StartCoroutine(StartCooldownTimer());
        }

        UpdateText();

    }
    #endregion

    #region Logic
    private Tuple<float, float> CalculateTilePosition()
    {
        Tuple<float, float> playerPosition = BaseGameController.Instance.GetPlayerCenterPosition();

        return new Tuple<float, float>(playerPosition.Item1, playerPosition.Item2 - 4);
    }

    private void PlaceTile(float x, float y)
    {
        BaseGameController.Instance.StepGeneratorController.CreateStep(x, y);
    }

    private IEnumerator StartCooldownTimer()
    {
        timeStarted = DateTime.Now.Ticks;
        isOnCooldown = true;
        UpdateText();
       
        yield return new WaitForSeconds(cooldownTime);

        isOnCooldown = false;
        UpdateText();
    }
    #endregion

    #region Presentation
    private void UpdateText()
    {
        if (isOnCooldown)
        {
            long timeNow = DateTime.Now.Ticks;
            TimeSpan span = new TimeSpan(timeNow - timeStarted);
            IsOnCooldownText.text = (cooldownTime - span.TotalSeconds).ToString("#.#");
            return;
        }

        IsOnCooldownText.text = readyText;
    }
    #endregion
}
