using UnityEngine;
using System.Collections;


[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance
    {
        get;
        private set;
    }

    public string ResDir = "";

    void Awake()
    {
        Instance = this;

        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        Mute = PlayerPrefs.GetInt("Mute", 0) == 0 ? false : true;
    }

    #region BGM
    private AudioSource audioSource;

    public bool Mute
    {
        get { return audioSource.mute; }
        set
        {
            audioSource.mute = value;
            PlayerPrefs.SetInt("Mute", value ? 1 : 0);
        }
    }

    public float BGVolume //0-1
    {
        get { return audioSource.volume; }
        set { audioSource.volume = value; }
    }

    public void PlayBGM(string name)
    {
        string path = ResDir + "/" + name;
        if (ResManager.Instance.LoadPrefab(path) != null)
        {
            AudioClip ac = ResManager.Instance.LoadPrefab(path) as AudioClip;
            audioSource.clip = ac;
            audioSource.Play();
        }
    }

    public void StopBGM()
    {
        audioSource.clip = null;
        audioSource.Stop();
    }
    #endregion

    //Audio
    public void PlayAudio(string name)
    {
        if (Mute)
            return;

        string path = ResDir + "/" + name;

        if (ResManager.Instance.LoadPrefab(path) != null)
        {
            AudioClip ac = ResManager.Instance.LoadPrefab(path) as AudioClip;
            AudioSource.PlayClipAtPoint(ac, Camera.main.transform.position);
        }
    }
}
