using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatboyController : MonoBehaviour
{
    public static CatboyController instance { get; private set;}
    CharController charController;
    public bool followingPlayer = true;
    bool outOfRangeOfPlayer = false;
    Animator animator;
    AudioSource source;

    void Awake()
    {
        instance = this;
        charController = GetComponent<CharController>();
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(followingPlayer) {
            float dx = PlayerController.instance.transform.position.x - transform.position.x;
            if(Mathf.Abs(dx) > 3) outOfRangeOfPlayer = true;
            else if(Mathf.Abs(dx) < 2) outOfRangeOfPlayer = false;
            if(dx > 0 && outOfRangeOfPlayer) {
                charController.walkDir = Mathf.Min(dx / 3, 1);
            } else if(dx < 0 && outOfRangeOfPlayer) {
                charController.walkDir = Mathf.Max(dx / 3, -1);
            } else {
                charController.walkDir = 0;
                charController.faceRight = dx > 0;
            }
        }
        animator.SetInteger("Talking", source.isPlaying ? 1 : 0);
    }
}
