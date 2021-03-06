
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    #region Public Fields
    [SerializeField] Sound[] sounds;
    #endregion

    private void Start()
    {
<<<<<<< HEAD
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
=======
        Instance = this;
>>>>>>> origin/main
    }

    public static Sound GetSound(string name)
    {
        foreach (Sound sound in Instance.sounds)
        {
            if (sound.name == name) return sound;
        }
        return Instance.sounds[0];
    }
}