using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    [Tooltip("Pause menüsü Canvas GameObject'inizi atayýn")]
    public GameObject pauseCanvas;

    public static bool IsPaused { get; private set; }

    void Start()
    {
        // Oyun baþladýðýnda menüyü kapalý tut
        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        // ESC tuþuna basýldý mý?
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        IsPaused = !IsPaused;

        // Canvas göster/gizle
        pauseCanvas.SetActive(IsPaused);

        // Zaman ölçeklendirmesini deðiþtir (0 = duraklat, 1 = normal hýz)
        Time.timeScale = IsPaused ? 0f : 1f;
        /*
        // Ýstersen fare imlecini serbest/kilitli yap
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused
            ? CursorLockMode.None
            : CursorLockMode.Locked;*/
    }

    // Eðer “Resume” butonuna baðlamak istersen:
    public void OnResumeButton()
    {
        TogglePause();
    }
}
