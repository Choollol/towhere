using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private static AudioPlayer instance;

    private static Dictionary<string, AudioSource> sounds;

    private void Awake()
    {
        sounds = new Dictionary<string, AudioSource>();

        GetSounds(transform);

        instance = this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="doRestartSound"></param>
    /// <param name="volume"></param>
    /// <param name="minPitch"></param>
    /// <param name="maxPitch"></param>
    /// <param name="doRoundSemitone"></param>
    /// <param name="position">Cannot be equal to a default-constructed Vector3, i.e. Vector3.zero.</param>
    public static void PlaySound(string key, bool doRestartSound = true, float volume = 1,
        float minPitch = 1, float maxPitch = 1, bool doRoundSemitone = false,
        Vector3 position = new Vector3())
    {
        if (!doRestartSound && sounds[key].isPlaying)
        {
            return;
        }
        if (minPitch != 1 || maxPitch != 1)
        {
            sounds[key].pitch = Random.Range(minPitch, maxPitch);
        }
        if (doRoundSemitone)
        {
            sounds[key].pitch = Mathf.Pow(1.05946f, (int)Mathf.Log(sounds[key].pitch, 1.05946f));
        }
        if (volume != 1)
        {
            sounds[key].volume = volume;
        }
        if (position != Vector3.zero)
        {
            sounds[key].transform.position = position;
        }
        sounds[key].Play();
    }

    /// <summary>
    /// Stops a currently sound.
    /// </summary>
    public static void StopSound(string key)
    {
        sounds[key].Stop();
    }

    /// <summary>
    /// Stops the sound if it's playing. Otherwise, do nothing.
    /// </summary>
    public static void StopSoundIfPlaying(string key)
    {
        if (IsSoundPlaying(key))
        {
            StopSound(key);
        }
    }

    /// <returns>The AudioSource component of a sound.</returns>
    public static AudioSource GetSound(string key)
    {
        return sounds[key];
    }

    /// <returns>Whether the sound with the given name is playing.</returns>
    public static bool IsSoundPlaying(string key)
    {
        return sounds[key].isPlaying;
    }

    /// <summary>
    /// Starts the FadeAudio Coroutine on the AudioPlayer object.
    /// </summary>
    public static void InstanceFadeAudio(string key, float duration, float targetVolume)
    {
        instance.StartCoroutine(FadeAudio(key, duration, targetVolume));
    }
    /// <summary>
    /// An IEnumerator to fade audio. Start the coroutine in the caller script.
    /// </summary>
    public static IEnumerator FadeAudio(string key, float duration, float targetVolume)
    {
        if (sounds[key].volume == targetVolume) { yield break; }
        float currentTime = 0;
        float start = sounds[key].volume;
        while (currentTime <= duration)
        {
            currentTime += Time.deltaTime;
            sounds[key].volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        sounds[key].volume = targetVolume;
        if (sounds[key].volume == 0)
        {
            sounds[key].Stop();
        }
        yield break;
    }

    /// <summary>
    /// Recursively get all sounds and add them into dictionary.
    /// </summary>
    private void GetSounds(Transform transform)
    {
        int childCount = transform.childCount;
        if (childCount > 0)
        {
            for (int i = 0; i < childCount; ++i)
            {
                GetSounds(transform.GetChild(i));
            }
        }
        else
        {
            sounds.Add(transform.name, transform.GetComponent<AudioSource>());
        }
    }
}