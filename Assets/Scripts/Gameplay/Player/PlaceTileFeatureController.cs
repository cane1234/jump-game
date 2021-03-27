using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTileFeatureController : MonoBehaviour
{
    #region Private Fields
    private bool isOnCooldown;

    private float currentCooldown;
    #endregion

    #region Editor Fields
    [SerializeField]
    private float cooldownTime;

    public Text IsOnCooldownText;
    #endregion

    #region Constants
    private const string readyText = "Ready";
    private const string onCooldownText = "On Cooldown";
    private const float updateTextPeriod = 0.03f;
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
        GameObject newStep = BaseGameController.Instance.StepGeneratorController.CreateStep(x, y);
    }

    private IEnumerator StartCooldownTimer()
    {
        isOnCooldown = true;
        currentCooldown = cooldownTime;
        UpdateText();

        while (currentCooldown > 0)
        {
            yield return new WaitForSeconds(updateTextPeriod);
            currentCooldown-= updateTextPeriod;

            UpdateText();
        }
        

        isOnCooldown = false;
        UpdateText();
    }
    #endregion

    #region Presentation
    private void UpdateText()
    {
        if (isOnCooldown)
        {
            IsOnCooldownText.text = currentCooldown.ToString("#.##");
            return;
        }

        IsOnCooldownText.text = readyText;
    }
    #endregion
}
