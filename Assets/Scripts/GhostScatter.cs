using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScatter : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if(node != null && this.enabled && !this.ghost.vulnerable.enabled)
        {
            int index = Random.Range(0, node.availableDirections.Count);

            if(node.availableDirections[index] == -this.ghost.movement.direction && node.availableDirections.Count > 1)
            {
                index++;
                    if(index >= node.availableDirections.Count)
                {
                    index = 0;
                }
            }
            this.ghost.movement.SetDirection(node.availableDirections[index]);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
