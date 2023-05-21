using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    
    public Hareket movement;
    public GhostHome home;
    public GhostChase chase;
    public GhostScatter scatter;
    public GhostVulnerable vulnerable;
    public GhostBehavior initialBehavior;
    public Transform target;
    public int points = 200;
    // Start is called before the first frame update
    void Awake()
    {
        this.movement = GetComponent<Hareket>();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScatter>();
        this.vulnerable = GetComponent<GhostVulnerable>();
        this.chase = GetComponent<GhostChase>();
    }
    private void Start()
    {
        ResetState();
    }
    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.ResetState();
        this.vulnerable.Disable();
        this.chase.Disable();
        this.scatter.Enable();
        if(this.home != this.initialBehavior)
        {
            this.home.Disable();
        }
        if (initialBehavior != null)
        {
            this.initialBehavior.Enable();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Pacman"))
        {
            if (this.vulnerable.enabled)
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
