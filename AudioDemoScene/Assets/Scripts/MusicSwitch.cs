using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioHelper
{
    public static IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
        float startVolume = audioSource.volume;
        while (audioSource.volume > 0)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
            yield return null;
        }
        audioSource.Stop();
    }

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime, float maxVolume = 1, float time = 0.0f)
    {
        if (time != 0.0f) audioSource.time = time;
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < maxVolume)
        {
            audioSource.volume += Time.deltaTime / FadeTime;
            yield return null;
        }
    }

}

public class MusicSwitch : MonoBehaviour
{
    public AudioSource calm;
    public AudioSource alert;
    public AudioSource attack;

    public float fadeTime = 2.0f;
    [Range(0.05f, 1.0f)]
    public float volume = 1.0f;

    private bool alertBool;
    private bool attackBool;
    
    void Start()
    {
        calm.volume = volume;
        alert.volume = volume;
        attack.volume = volume;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Alert" && !attackBool)
        {
            float time = calm.time;
            StartCoroutine(AudioHelper.FadeOut(calm, fadeTime));
            StartCoroutine(AudioHelper.FadeIn(alert, fadeTime, volume, time));
            attack.Stop();
            alertBool = true;
        }

        if (collision.gameObject.tag == "Attack" && alertBool)
        {
            float time = alert.time;
            calm.Stop();
            StartCoroutine(AudioHelper.FadeOut(alert, fadeTime));
            StartCoroutine(AudioHelper.FadeIn(attack, fadeTime, volume, time));
            attackBool = true;
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Alert" && !attackBool)
        {
            float time = alert.time;
            StartCoroutine(AudioHelper.FadeOut(alert, fadeTime));
            StartCoroutine(AudioHelper.FadeIn(calm, fadeTime, volume, time));
            attack.Stop();
            alertBool = false;
        }

        if (collision.gameObject.tag == "Attack" && alertBool)
        {
            float time = attack.time;
            calm.Stop();
            StartCoroutine(AudioHelper.FadeOut(attack, fadeTime));
            StartCoroutine(AudioHelper.FadeIn(alert, fadeTime, volume, time));
            attackBool = false;
        }
    }
}
