using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LevelController : MonoBehaviour
{
    public static LevelController instance { get; private set;}
    Animator animator;
    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
