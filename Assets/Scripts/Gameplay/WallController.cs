using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetWallY()
    {
        Bounds wallBounds = boxCollider.bounds;
        float wallTop = wallBounds.center.y + wallBounds.extents.y;

        return wallTop;
    }
}
