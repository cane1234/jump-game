using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGeneratorController : MonoBehaviour
{
    #region Constants
    private const int wallCreationThreshold = 20;
    #endregion

    #region Editor Fields
    [SerializeField]
    private GameObject WallPrefab;

    [SerializeField]
    private GameObject currentHighestLeftWall;

    [SerializeField]
    private GameObject currentHighestRightWall;

    [SerializeField]
    private GameObject LeftWallRoot;

    [SerializeField]
    private GameObject RightWallRoot;
    #endregion

    #region Private Fields
    private WallController currentHighestLeftWallController;
    #endregion

    #region Unity Events
    // Start is called before the first frame update
    void Start()
    {
        currentHighestLeftWallController = currentHighestLeftWall.GetComponent<WallController>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWalls();   
    }
    #endregion

    #region Wall Creation
    private void UpdateWalls()
    {
        float currentPlayerPos = BaseGameController.Instance.GetPlayerBottomY();

        float currentHighestLeftWallPos = currentHighestLeftWallController.GetWallY();
        //float currentHighestRightWallPos = currentHighestRightWall.GetComponent<WallController>().GetWallY();

        if (currentHighestLeftWallPos - currentPlayerPos < wallCreationThreshold)
        {
            CreateWalls(currentHighestLeftWallPos, currentHighestLeftWall.transform.position.x, currentHighestRightWall.transform.position.x);
        }
    }

    private void CreateWalls(float y, float leftX, float rightX)
    {
        GameObject newLeftWall = Instantiate(WallPrefab, new Vector3(leftX, y, 0), Quaternion.identity);
        GameObject newRightWall = Instantiate(WallPrefab, new Vector3(rightX, y, 0), Quaternion.identity);

        newLeftWall.transform.parent = LeftWallRoot.transform;
        newRightWall.transform.parent = RightWallRoot.transform;

        currentHighestLeftWallController = newLeftWall.GetComponent<WallController>();

        currentHighestLeftWall = newLeftWall;
        currentHighestRightWall = newRightWall;
    }
    #endregion
}
