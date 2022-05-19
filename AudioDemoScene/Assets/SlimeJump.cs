using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJump : MonoBehaviour
{
    public AudioSource clip;

    void Jump()
    {
        clip.PlayOneShot(clip.clip);
    }
}
