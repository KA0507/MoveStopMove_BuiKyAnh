using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Level : MonoBehaviour
{
    public Player player;
    [SerializeField] private Vector3 minPoint;
    [SerializeField] private Vector3 maxPoint;

    public int totalCharacter = 30;
    public int realBot = 5;

    // Tìm điểm ngẫu nhiên trên navmesh của level
    public Vector3 RandomPoint()
    {
        float x = Random.Range(minPoint.x + 1f, maxPoint.x - 1f);
        float z = Random.Range(minPoint.z + 1f, maxPoint.z - 1f);
        Vector3 randomPoint = x*Vector3.right + z*Vector3.forward;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPoint, out hit, float.PositiveInfinity, NavMesh.AllAreas);
        return hit.position;
    }
}
