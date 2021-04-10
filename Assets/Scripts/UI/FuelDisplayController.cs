using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FuelDisplayController : MonoBehaviour
{
    public Text fuelNumberText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fuelNumberText.text = BaseGameController.Instance.PlayerController.fuel.ToString("#");
    }
}
