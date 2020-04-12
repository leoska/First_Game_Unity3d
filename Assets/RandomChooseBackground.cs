using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChooseBackground : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int img = Random.Range(1, 4);
        string path = "Sprites/IMG_054" + img.ToString();
        Sprite sprite = Resources.Load<Sprite>(path);

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

        this.SetFullScreenSize(spriteRenderer);
    }

    private void SetFullScreenSize(SpriteRenderer spriteRenderer)
    {
        float cameraHeight = Camera.main.orthographicSize * 2;
        Vector2 cameraSize = new Vector2(Camera.main.aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y)
        { // Landscape (or equal)
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        { // Portrait
            scale *= cameraSize.y / spriteSize.y;
        }

        transform.localScale = scale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
