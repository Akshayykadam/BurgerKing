using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform plateViusalPrefab;

    private LinkedList<GameObject> plateVisualGameObjectList;

    private void Awake()
    {
        plateVisualGameObjectList = new LinkedList<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawned += PlatesCounter_OnPlateSpawned;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        if (plateVisualGameObjectList.Count > 0)
        {
            GameObject plateGameObject = plateVisualGameObjectList.Last.Value;
            plateVisualGameObjectList.RemoveLast();
            Destroy(plateGameObject);
        }
    }

    private void PlatesCounter_OnPlateSpawned(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateViusalPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY * plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.AddLast(plateVisualTransform.gameObject);
    }
}
