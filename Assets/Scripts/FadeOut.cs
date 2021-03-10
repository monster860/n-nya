using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float fadeness = 0;
    public bool fadingOut = false;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingOut) {
            fadeness = Mathf.Min(fadeness + Time.deltaTime / 5, 1);
            spriteRenderer.color = new Color(0, 0, 0, fadeness);
        }
    }
}
