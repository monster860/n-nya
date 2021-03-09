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
    public Transform followOverride;

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
        float player_dx = PlayerController.instance.transform.position.x - transform.position.x;
        if(followOverride) {
            float override_dx = followOverride.position.x - transform.position.x;
            if(Mathf.Abs(override_dx) > 3) outOfRangeOfPlayer = true;
            else if(Mathf.Abs(override_dx) < 2) outOfRangeOfPlayer = false;
            if(override_dx > 0 && outOfRangeOfPlayer) {
                charController.walkDir = Mathf.Min(override_dx / 3, 1);
            } else if(override_dx < 0 && outOfRangeOfPlayer) {
                charController.walkDir = Mathf.Max(override_dx / 3, -1);
            } else {
                charController.walkDir = 0;
                charController.faceRight = player_dx > 0;
            }
        } else if(followingPlayer) {
            if(Mathf.Abs(player_dx) > 3) outOfRangeOfPlayer = true;
            else if(Mathf.Abs(player_dx) < 2) outOfRangeOfPlayer = false;
            if(player_dx > 0 && outOfRangeOfPlayer) {
                charController.walkDir = Mathf.Min(player_dx / 3, 1);
            } else if(player_dx < 0 && outOfRangeOfPlayer) {
                charController.walkDir = Mathf.Max(player_dx / 3, -1);
            } else {
                charController.walkDir = 0;
                charController.faceRight = player_dx > 0;
            }
        }
        animator.SetInteger("Talking", source.isPlaying ? 1 : 0);
    }
}
