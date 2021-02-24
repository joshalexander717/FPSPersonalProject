using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootExplosion : MonoBehaviour
{
    Vector3 trajectory;
    public float velocity;
    float y;
    public bool isExploding;
    public bool isChest;

    void Start()
    {
        isExploding = true;
        Vector3 traj = UnityEngine.Random.insideUnitSphere;
        if (Mathf.Abs(traj.y) < .75)
            traj = new Vector3(traj.x, .75f, traj.z);
        if (isChest)
            trajectory = new Vector3(traj.x * velocity * 1.5f, Mathf.Abs(traj.y) * velocity * 2, Mathf.Abs(traj.z) * velocity * 1.5f);
        else
            trajectory = new Vector3(traj.x * velocity * 1.5f, Mathf.Abs(traj.y) * velocity * 2, traj.z * velocity * 1.5f);
        y = trajectory.y;
    }

    void FixedUpdate()
    {
        if (isExploding)
        {
            y -= 9.81f * Time.deltaTime;
            transform.position += new Vector3(trajectory.x * Time.deltaTime, y * Time.deltaTime, trajectory.z * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            Debug.Log(name + "Collided with " + other.name);
            isExploding = false;
        }
    }
}
