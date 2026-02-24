using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class HumanSpawner : MonoBehaviour
{
    public void Spawn(float minX, float maxX, float minZ, float maxZ, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 target = new Vector3(Random.Range(minX, maxX), 0f, Random.Range(minZ, maxZ));

            NavMeshHit hit;

            if (NavMesh.SamplePosition(target, out hit, 100f, NavMesh.AllAreas))
            {
                string path = $"Humans/Human-{Random.Range(1, 6)}";

                Human human = Resources.Load<Human>(path);

                Instantiate(human, target, Quaternion.identity);
            }
        }
    }
}
