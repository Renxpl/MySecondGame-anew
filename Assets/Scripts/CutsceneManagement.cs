using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManagement : MonoBehaviour
{
    public string[] cutsceneNames;

    public bool[] cutscenes;

    
    
                

    public bool IsInCutscene { get; private set; }

   


    // Start is called before the first frame update
    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < cutscenes.Length;i++)
        {
            if (cutscenes[i])
            {

                IsInCutscene= true;
                PlayCutscene(cutsceneNames[i]);

            }


        }

    }

    void PlayCutscene(string cutsceneName)
    {






    }





}
