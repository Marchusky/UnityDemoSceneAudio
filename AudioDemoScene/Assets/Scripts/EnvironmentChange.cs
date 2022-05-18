using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnvironmentChange : MonoBehaviour
{

    public AudioMixerSnapshot changeSnapshot;
    public AudioMixerSnapshot defaultSnapshot;
    public string triggerTag;

    public float transitionTime = 0.5f;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag != triggerTag) return;
        changeSnapshot.TransitionTo(transitionTime);
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag != triggerTag) return;
        defaultSnapshot.TransitionTo(transitionTime);
    }
}
