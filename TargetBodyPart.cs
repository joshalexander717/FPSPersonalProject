using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBodyPart : MonoBehaviour
{
    public bool criticalBody;

    public GameObject targetSource;

    Player player;

    // Start is called before the first frame update
    void Start()
    {
            player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BodyDamage(float damage)
    {
        if (criticalBody)
        {
            targetSource.GetComponent<Target>().TakeDamage(damage * (player.critPercentage / 100));
        }
        else
            targetSource.GetComponent<Target>().TakeDamage(damage);
    }

}
