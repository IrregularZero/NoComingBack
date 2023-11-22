using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarterWeaponDispenser : MonoBehaviour
{
    [SerializeField]
    private int weaponOptionsAmount = 3;
    [SerializeField]
    private GameObject weaponOption0;
    [SerializeField]
    private GameObject weaponOption1;
    [SerializeField]
    private GameObject weaponOption2;

    [SerializeField]
    private float spawnDelay = 0.5f;

    void Start()
    {
        StartCoroutine(GenerateItem(Random.Range(0, weaponOptionsAmount)));
    }

    private IEnumerator GenerateItem(int generatedIndex)
    {
        yield return new WaitForSeconds(spawnDelay);

        switch (generatedIndex)
        {
            case 0: Instantiate(weaponOption0, transform.position, Quaternion.identity, transform.parent); break;
            case 1: Instantiate(weaponOption1, transform.position, Quaternion.identity, transform.parent); break;
            case 2: Instantiate(weaponOption2, transform.position, Quaternion.identity, transform.parent); break;
            default: Debug.Log("Generated index bigger than expected"); break;
        }

        Destroy(gameObject);
    }
}
