using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyValuesManager : MonoBehaviour
{
    static Dictionary<GameObject,GameObject> enemyAndHpBars;
    public GameObject hpBar;
    static Dictionary<GameObject, GameObject> enemyAndStanceBars;
    public GameObject stanceBar;
    void Start()
    {
        enemyAndHpBars = new Dictionary<GameObject,GameObject>();
        enemyAndStanceBars= new Dictionary<GameObject,GameObject>();
        GameEvents.gameEvents.onRegisteringEnemiesToManager += RegisterEnemy;
    }

    // Update is called once per frame
    void Update()
    {
        foreach (KeyValuePair<GameObject, GameObject> pair in enemyAndHpBars)
        {
            pair.Value.transform.position = new Vector2( pair.Key.transform.position.x,pair.Key.transform.position.y+1.8f);
            if (pair.Key.CompareTag("SwordsmanEnemy"))
            {
                pair.Value.transform.GetChild(2).GetComponent<Image>().fillAmount = pair.Key.GetComponent<SwordsmanBehaviour>().HP / 9f;
            }
            if (!pair.Key.activeSelf)
            {
                if (pair.Value.activeSelf) pair.Value.SetActive(false);

            }
            else
            {
                if (!pair.Value.activeSelf) pair.Value.SetActive(true);
            }

        }
        foreach (KeyValuePair<GameObject, GameObject> pair in enemyAndStanceBars)
        {
            pair.Value.transform.position = new Vector2(pair.Key.transform.position.x, pair.Key.transform.position.y + 1.5f);
            if (pair.Key.CompareTag("SwordsmanEnemy"))
            {
                pair.Value.transform.GetChild(2).GetComponent<Image>().fillAmount = pair.Key.GetComponent<SwordsmanBehaviour>().stance / 3f;
            }

            if (!pair.Key.activeSelf)
            {
                if (pair.Value.activeSelf) pair.Value.SetActive(false);

            }
            else
            {
               if(!pair.Value.activeSelf) pair.Value.SetActive(true);
            }

        }



    }


    public void RegisterEnemy(GameObject enemy, string type,float amount)
    {
        if (enemyAndHpBars.ContainsKey(enemy))
        {
            Debug.Log("zaten enemy keyi mevcut");
            return;

        }
        GameObject newHPbar = Instantiate(hpBar, transform);
        newHPbar.transform.position = new Vector2(enemy.transform.position.x , enemy.transform.position.y +1.8f);
        enemyAndHpBars.Add(enemy, newHPbar);
       
        GameObject newStanceBar= Instantiate(stanceBar, transform);
        newStanceBar.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + 1.5f);
        enemyAndStanceBars.Add(enemy, newStanceBar);
    }




}
