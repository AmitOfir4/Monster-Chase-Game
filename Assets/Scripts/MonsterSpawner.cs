using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] monsterReference;
    [SerializeField] private Transform leftPos, rightPos;
    [SerializeField] private GameObject spawnedMonster;

    private int _randomIndex;
    private int _randomSide;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    IEnumerator SpawnMonsters()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 5));
            
            _randomIndex = Random.Range(0, monsterReference.Length);
            _randomSide = Random.Range(0, 2);

            spawnedMonster = Instantiate(monsterReference[_randomIndex]);

            if (_randomSide == 0)
            {
                spawnedMonster.transform.position = leftPos.position;
                spawnedMonster.GetComponent<Monster>().speed = Random.Range(4, 10);
            } //left
            else
            {
                spawnedMonster.transform.position = rightPos.position;
                spawnedMonster.GetComponent<Monster>().speed = -Random.Range(4, 10);
                spawnedMonster.transform.localScale = new Vector3(-1f, 1f, 1f);
            } //right
        }
    }
}
