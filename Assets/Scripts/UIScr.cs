<<<<<<< HEAD
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class UIScr : MonoBehaviour
{
    public static bool isUIOpen = false;
    public static bool victoryed = false;
    const float lowpassNormal = 22000f;

    private static UIScr Instance;

    #region Public Fields
    public GameObject GOOptions;
    public GameObject GOVictory;
    public AudioMixer mixer;
    public float lowpassCutoff = 600f;
=======
using UnityEngine;

public class UIScr : MonoBehaviour
{
    private bool isUIOpen = false;
    #region Public Fields
    public GameObject GOOptions;
>>>>>>> origin/main
    #endregion

    #region Unity Methods
    private void Start()
    {
<<<<<<< HEAD
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        GOOptions.SetActive(false);
        GOVictory.SetActive(false);
    }
    void Update()
    {
        if (victoryed) return;
=======
        GOOptions.SetActive(isUIOpen);
    }
    void Update()
    {
>>>>>>> origin/main
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            isUIOpen = !isUIOpen;
            GOOptions.SetActive(isUIOpen);
<<<<<<< HEAD
            if (isUIOpen)
            {
                mixer.SetFloat("LowpassFreq", lowpassCutoff);
            }
            else
            {
                mixer.SetFloat("LowpassFreq", lowpassNormal);
            }
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        AudioManager.GetSound("BGM").source.volume = 1f;
        GOVictory.SetActive(false);
    }

    public static void Victory()
    {
        isUIOpen = false;
        Instance.GOOptions.SetActive(false);
        Instance.mixer.SetFloat("LowpassFreq", lowpassNormal);
        Instance.GOVictory.SetActive(true);

        Instance.StartCoroutine("BGMFade");
        AudioManager.GetSound("VictorySound").source.Play();
    }

    #endregion

    IEnumerator BGMFade()
    {
        AudioSource source = AudioManager.GetSound("BGM").source;
        for (int ft = 5; ft >= 0; ft -= 1)
        {
            source.volume = ft * 0.2f;
            yield return null;
        }
    }

=======
        }
    }
 
    #endregion
 
>>>>>>> origin/main
    #region Private Methods
    #endregion
}
