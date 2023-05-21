using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVulnerable : GhostBehavior
{
    public SpriteRenderer Body;
    public SpriteRenderer Blue;
    public SpriteRenderer White;
    public bool eaten;

    public override void Enable(float duration)
    {
        base.Enable(duration);
        this.Body.enabled = false;
        this.Blue.enabled = true;
        this.White.enabled = false;
        Invoke(nameof(Flash), duration / 2f);

    }

    public override void Disable()
    {
        base.Disable();
      
        this.Body.enabled = true;
        this.Blue.enabled = false;
        this.White.enabled = false;
    }
    

    private void Flash()
    {
        if (!this.eaten)
        {
            this.Blue.enabled = false;
            this.White.enabled = true;
            this.White.GetComponent<Animasyon>().YenidenBasla();
        }
        
    }
    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
        this.eaten = false;
    }
    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1f;
        this.eaten = false;

    }
    private void Eaten()
    {
        this.eaten = true;
        Vector3 position = this.ghost.home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        this.ghost.home.Enable(this.duration);
        this.Body.enabled = false;
        this.Blue.enabled = false;
        this.White.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();
        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;
            foreach (Vector2 availableDirection in node.availableDirections)
            {
                Vector3 newPosition = this.transform.position + new Vector3(availableDirection.x, availableDirection.y, 0f);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }
            this.ghost.movement.SetDirection(direction);
        }
    }
        private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.enabled)
            {
                Eaten();
            }
            
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
