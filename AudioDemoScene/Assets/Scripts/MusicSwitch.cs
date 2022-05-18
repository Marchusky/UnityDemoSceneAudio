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

    public static IEnumerator FadeIn(AudioSource audioSource, float FadeTime)
    {
        audioSource.Play();
        audioSource.volume = 0f;
        while (audioSource.volume < 1)
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
    public AudioSource well;

    public float fadeTime = 2.0f;

    private bool alertBool;
    private bool attackBool;

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Enter");

        if (collision.gameObject.tag == "Alert" && !attackBool)
        {
            Debug.Log("Alert");
            float volume = calm.volume;
            StartCoroutine(AudioHelper.FadeIn(alert, fadeTime));
            StartCoroutine(AudioHelper.FadeOut(calm, fadeTime));
            attack.Stop();
            alertBool = true;
        }

        if (collision.gameObject.tag == "Attack" && alertBool)
        {
            calm.Stop();
            alert.Stop();
            attack.Play();
            attackBool = true;
        }

        if (collision.gameObject.tag == "Well")
        {
            well.Play();
        }

    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Alert" && !attackBool)
        {
            calm.Play();
            alert.Stop();
            attack.Stop();
            alertBool = false;
        }

        if (collision.gameObject.tag == "Attack" && alertBool)
        {
            calm.Stop();
            alert.Play();
            attack.Stop();
            attackBool = false;
        }

        if (collision.gameObject.tag == "Well")
        {
            well.Stop();
        }
    }
}
