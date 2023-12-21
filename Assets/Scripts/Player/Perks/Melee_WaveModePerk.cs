using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Melee_WaveModePerk : BasePerk
{
    private bool canAttack = false;

    [SerializeField]
    private float damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float cooldownTime;
    [SerializeField]
    private float lifeTime;

    private Melee playersMelee;
    private GameObject player;
    [SerializeField]
    private GameObject slash;
    private GameObject activeSlash;

    private void Start()
    {
        playersMelee = GameObject.FindGameObjectWithTag("EmbededMelee").GetComponent<Melee>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (!active)
            return;

        if (canAttack)
        {
            if (playersMelee.IsAttacking)
            {
                activeSlash = Instantiate(slash, player.transform.position + player.transform.forward * 1.5f, GameObject.FindGameObjectWithTag("MainCamera").transform.rotation);
                activeSlash.GetComponent<WaveModePerk_ActiveComponent>().Setup(damage, speed);
                Destroy(activeSlash, lifeTime);
                StartCoroutine(Reload());
            }
        }
    }

    private IEnumerator Reload()
    {
        canAttack = false;
        yield return new WaitForSeconds(cooldownTime);
        canAttack = true;
    }

    public override void EnableEffect()
    {
        active = true;
        canAttack = true;
    }
    public override void DisableEffect()
    {
        active = false;
        canAttack = false;
    }
}
