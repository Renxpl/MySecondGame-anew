using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagement : MonoBehaviour
{
    [Tooltip("Pause men�s� Canvas GameObject'inizi atay�n")]
    public GameObject pauseCanvas;

    public static bool IsPaused { get; private set; }

    void Start()
    {
        // Oyun ba�lad���nda men�y� kapal� tut
        pauseCanvas.SetActive(false);
    }

    void Update()
    {
        // ESC tu�una bas�ld� m�?
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        IsPaused = !IsPaused;

        // Canvas g�ster/gizle
        pauseCanvas.SetActive(IsPaused);

        // Zaman �l�eklendirmesini de�i�tir (0 = duraklat, 1 = normal h�z)
        Time.timeScale = IsPaused ? 0f : 1f;
        /*
        // �stersen fare imlecini serbest/kilitli yap
        Cursor.visible = isPaused;
        Cursor.lockState = isPaused
            ? CursorLockMode.None
            : CursorLockMode.Locked;*/
    }

    // E�er �Resume� butonuna ba�lamak istersen:
    public void OnResumeButton()
    {
        TogglePause();
    }
}
