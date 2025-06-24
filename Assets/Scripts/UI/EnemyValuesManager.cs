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
    public float offsetHp;
    public float offsetSta;
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
            pair.Value.transform.position = new Vector2( pair.Key.transform.position.x,pair.Key.transform.position.y + offsetHp);
            if (pair.Key.CompareTag("SwordsmanEnemy"))
            {
                pair.Value.transform.GetChild(2).GetComponent<Image>().fillAmount = (float)pair.Key.GetComponent<EnemyController>().CurrentHealth / pair.Key.GetComponent<EnemyController>().Stats.maxHealth;
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
            pair.Value.transform.position = new Vector2(pair.Key.transform.position.x, pair.Key.transform.position.y + offsetSta);
            if (pair.Key.CompareTag("SwordsmanEnemy"))
            {
                pair.Value.transform.GetChild(2).GetComponent<Image>().fillAmount = (float)pair.Key.GetComponent<EnemyController>().CurrentStance / pair.Key.GetComponent<EnemyController>().Stats.maxStance;
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
        newHPbar.transform.position = new Vector2(enemy.transform.position.x , enemy.transform.position.y + offsetHp);
        enemyAndHpBars.Add(enemy, newHPbar);
       
        GameObject newStanceBar= Instantiate(stanceBar, transform);
        newStanceBar.transform.position = new Vector2(enemy.transform.position.x, enemy.transform.position.y + offsetSta);
        enemyAndStanceBars.Add(enemy, newStanceBar);
    }




}
