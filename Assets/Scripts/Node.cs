using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public List<Vector2> availableDirections;
    public LayerMask obstacleLayer;
    // Start is called before the first frame update
    void Start()
    {
        this.availableDirections = new List<Vector2>();
        CheckAvailable(Vector2.up);
        CheckAvailable(Vector2.down);
        CheckAvailable(Vector2.left);
        CheckAvailable(Vector2.right);
    }

    public void CheckAvailable(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.50f, 0.0f, direction, 1f, this.obstacleLayer);
       if(hit.collider == null)
        {
            this.availableDirections.Add(direction);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
