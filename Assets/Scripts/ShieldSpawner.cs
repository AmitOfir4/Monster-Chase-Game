using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldSpawner : MonoBehaviour
{
    [SerializeField] private GameObject shieldReference;
    [SerializeField] private Transform leftPos, middlePos, rightPos;
    [SerializeField] private GameObject spawnedShield;
    
    private int _randomSide;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnShields());
    }

    IEnumerator SpawnShields()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            
            if (spawnedShield == null)
            {
                _randomSide = Random.Range(0, 2);
                spawnedShield = Instantiate(shieldReference);
                
                if (_randomSide == 0)
                {
                    spawnedShield.transform.position = leftPos.position;
                }

                else if (_randomSide == 1)
                {
                    spawnedShield.transform.position = middlePos.position;
                }

                else if (_randomSide == 2)
                {
                    spawnedShield.transform.position = rightPos.position;
                }
                
                while(spawnedShield != null)
                {
                    yield return null;
                }
            }
        } 
    }
    
}
