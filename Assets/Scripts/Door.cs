using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public float openness;
    public bool isOpen = false;
    public float openTime = 1;
    public bool locked = false;

    SpriteRenderer spriteRenderer;
    public Sprite[] states;
    BoxCollider2D boxCollider2D;
    
    public string opennessProp;
    AudioSource audioSource;
    public AudioClip lockedClip;
    public AudioClip openClip;
    public AudioClip closeClip;
    public string lockedTrigger;
    public Door parentDoor;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        openness = isOpen ? 0.99f : 0.01f; // hacky way to make sure theres a bit of update
        Interactible interactible = GetComponent<Interactible>();
        if(interactible) {
            interactible.structs.Add(new InteractStruct(this.Interact, "Open", KeyCode.E));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(parentDoor) {
            isOpen = parentDoor.isOpen;
        }
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
            boxCollider2D.enabled = !isOpen || openness < 0.5f;
            if(opennessProp != null && opennessProp != "") {
                LevelController.instance.animator.SetFloat(opennessProp, openness);
            }
        }
    }

    public void Open() {
        if(!isOpen && audioSource && openClip) audioSource.PlayOneShot(openClip); 
        isOpen = true;
    }
    public void Close() {
        if(isOpen && audioSource && closeClip) audioSource.PlayOneShot(closeClip); 
        isOpen = false;
    }

    void Interact()
    {
        if(parentDoor) {
            parentDoor.Interact();
            return;
        }
        if(locked) {
            if(lockedTrigger != null && lockedTrigger != "") {
                LevelController.instance.animator.SetTrigger(lockedTrigger);
            }
            if(audioSource && lockedClip) {
                audioSource.PlayOneShot(lockedClip);
            }
        } else {
            if(!isOpen) Open();
            else Close();
        }
    }
}
