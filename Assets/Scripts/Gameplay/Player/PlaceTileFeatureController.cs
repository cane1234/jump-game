using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceTileFeatureController : MonoBehaviour
{
    #region Private Fields
    private bool isOnCooldown;
    private long timeStarted;
    private float remainingCooldown;
    private Coroutine CooldownCoroutine;
    
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
            CooldownCoroutine = StartCoroutine(StartCooldownTimer(cooldownTime));
        }

        if (isOnCooldown && !BaseGameController.Instance.Pause)
        {
            remainingCooldown = remainingCooldown - Time.deltaTime;

            if(remainingCooldown <= 0)
            {
                remainingCooldown = 0;
            }
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

    private IEnumerator StartCooldownTimer(float duration)
    {
        remainingCooldown = duration;
        isOnCooldown = true;

        yield return new WaitForSeconds(duration);

        isOnCooldown = false;
        CooldownCoroutine = null;
    }

    public void Pause()
    {
        if (CooldownCoroutine != null)
        {
            StopCoroutine(CooldownCoroutine);
        }
    }

    public void Resume()
    {
        CooldownCoroutine = StartCoroutine(StartCooldownTimer(remainingCooldown));
    }
    #endregion

    #region Presentation
    private void UpdateText()
    {
        if (isOnCooldown)
        {
            IsOnCooldownText.text = (remainingCooldown).ToString("#.##");
            return;
        }

        IsOnCooldownText.text = readyText;
    }
    #endregion
}
