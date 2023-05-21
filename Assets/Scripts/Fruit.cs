using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    
    public int fruitPoints = 500;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Pacman"))
        {
            GameManager.Instance.FruitEaten(this);
            Destroy(gameObject);
        }
    }
}