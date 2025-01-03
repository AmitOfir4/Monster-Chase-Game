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
        
    }

    IEnumerator SpawnShields()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(3, 10));

            _randomSide = Random.Range(0, 2);

            spawnedShield = Instantiate(shieldReference);

            if (_randomSide == 0)
            {
                spawnedShield.transform.position = leftPos.position;
            }

        }
    }
}
