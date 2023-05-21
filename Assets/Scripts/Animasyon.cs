using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Animasyon : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float animasyonSuresi = 0.25f;
    public int oynat�lanAnimasyon;
    public bool dongu = true;
    private void Awake()
    {
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Oynat), this.animasyonSuresi, this.animasyonSuresi);
    }

    private void Oynat()
    {
        if (!this.spriteRenderer.enabled)
        {
            return;
        }
        this.oynat�lanAnimasyon++;
        if(this.oynat�lanAnimasyon >= this.sprites.Length && this.dongu)
        {
            this.oynat�lanAnimasyon = 0;
        }
        if(this.oynat�lanAnimasyon >= 0 && this.oynat�lanAnimasyon < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.oynat�lanAnimasyon];
        }

        
    }
    public void YenidenBasla()
    {
        this.oynat�lanAnimasyon = -1;
        Oynat();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
