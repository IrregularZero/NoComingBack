using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnemiesSeekingProjectile : EnemiesProjectile
{
    protected override void Start()
    {
        ConstraintSource cs = new ConstraintSource();
        cs.sourceTransform = GameObject.FindGameObjectWithTag("Player").transform;
        cs.weight = 1.0f;
        GetComponent<LookAtConstraint>().AddSource(cs);
        GetComponent<LookAtConstraint>().constraintActive = true;

        base.Start();
    }
}
