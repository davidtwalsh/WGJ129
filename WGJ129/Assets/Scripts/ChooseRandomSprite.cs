using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRandomSprite : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public List<Sprite> sprites;

    void Start()
    {
		spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Count)];
    }

    
}
