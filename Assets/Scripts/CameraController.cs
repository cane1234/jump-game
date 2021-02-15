using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private Transform player;

    [SerializeField]
    private Vector3 offset;

    [SerializeField]
    private float smoothSpeed;

    #endregion

    #region Unity Methods
    private void FixedUpdate()
    {
        Vector3 desiredPosition = new Vector3(transform.position.x, player.position.y, this.transform.position.z) + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        if (smoothedPosition.y > transform.position.y)
        {
            transform.position = smoothedPosition;
        }
    }
    #endregion

}
