using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Animasyon : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite[] sprites;
    public float animasyonSuresi = 0.25f;
    public int oynatýlanAnimasyon;
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
        this.oynatýlanAnimasyon++;
        if(this.oynatýlanAnimasyon >= this.sprites.Length && this.dongu)
        {
            this.oynatýlanAnimasyon = 0;
        }
        if(this.oynatýlanAnimasyon >= 0 && this.oynatýlanAnimasyon < this.sprites.Length)
        {
            this.spriteRenderer.sprite = this.sprites[this.oynatýlanAnimasyon];
        }

        
    }
    public void YenidenBasla()
    {
        this.oynatýlanAnimasyon = -1;
        Oynat();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
