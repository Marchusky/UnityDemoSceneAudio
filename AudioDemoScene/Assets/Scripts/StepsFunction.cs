using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepsFunction : MonoBehaviour
{
    private string surfaceType = "Sand";
    private int prevIndex = 0;
    public AudioClip[] sandFootsteps;
    public AudioClip[] hardFootsteps;
    private AudioSource source;
    private float pan = 0;
    [Range(1.0f, -1.0f)]
    public float deltaPan = 0;

    void Start()
    {
        source = gameObject.GetComponent<AudioSource>();
        pan = deltaPan;
    }

    void PlayFootstepSfx()
    {
        int index = 0;
        int maxLength = 0;
        switch (surfaceType)
        {
            case "Sand": 
                index = Random.Range(0, sandFootsteps.Length);
                source.clip = sandFootsteps[index];
                maxLength = sandFootsteps.Length;
                break;
            case "Hard":
                index = Random.Range(0, hardFootsteps.Length);
                source.clip = hardFootsteps[index];
                maxLength = hardFootsteps.Length;
                break;
        }

        if (index == prevIndex)
        {
            index++;
            if (index == maxLength) index = 0;
        }

        float mod = 0.0f;

        source.pitch = Random.Range(1.0f, 1.3f);
        source.volume = Random.Range(0.9f, 1.1f);
        source.panStereo = pan * -1;
        pan *= -1;

        //if (source.isPlaying) source.Stop();
        source.Play();
    }

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Sand":
            case "Hard":
                surfaceType = collision.gameObject.tag;
                break;

            default:
                break;
        }
    }
}
