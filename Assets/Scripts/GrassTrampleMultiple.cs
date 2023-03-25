using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GrassTrampleMultiple : MonoBehaviour
{
    [SerializeField] private Material material;
    [SerializeField] [Range(0, 10)] private float radius;
    [SerializeField] [Range(-1, 5)] private float heightOffset;

    public List<GameObject> cachedObjectsList = new List<GameObject>();
    public List<Vector4> charactersList = new List<Vector4>();

    private Vector4 tempPos = new Vector4();

    private void Awake()
    {
        cachedObjectsList.Clear();
        charactersList.Clear();
        cachedObjectsList = GameObject.FindGameObjectsWithTag("Interactable").ToList();

        for (int i = 0; i < cachedObjectsList.Count; i++)
        {
            charactersList.Add(new Vector4(cachedObjectsList[i].transform.position.x, cachedObjectsList[i].transform.position.y + heightOffset, cachedObjectsList[i].transform.position.z, radius));
        }

        material.SetInt("_TramplesCount", charactersList.Count);
    }

    private void Update()
    {
        if (material == null)
        {
            return;
        }

        for (int i = 0; i < charactersList.Count; i++)
        {
            tempPos.x = cachedObjectsList[i].transform.position.x;
            tempPos.y = cachedObjectsList[i].transform.position.y;
            tempPos.z = cachedObjectsList[i].transform.position.z;
            tempPos.w = radius;

            charactersList[i] = tempPos;
        }
        material.SetVectorArray("_MultipleTramples", charactersList);
    }
}