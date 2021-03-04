using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openness;
    public bool isOpen = false;
    public float openTime = 1;

    SpriteRenderer spriteRenderer;
    public Sprite[] states;
    BoxCollider2D boxCollider2D;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        openness = isOpen ? 0.99f : 0.01f;
    }

    // Update is called once per frame
    void Update()
    {
        bool changed = false;
        if(isOpen && openness < 1) {
            changed = true;
            openness = Mathf.Min(1, openness + Time.deltaTime / openTime);
        } else if(!isOpen && openness > 0) {
            changed = true;
            openness = Mathf.Max(0, openness - Time.deltaTime / openTime);
        }
        if(changed) {
            spriteRenderer.sprite = states[(int)Mathf.Min(Mathf.Floor(states.Length * openness), states.Length-1)];
            boxCollider2D.enabled = openness < 0.5f;
        }
    }
}
