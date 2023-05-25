using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    public List<GameObject> terrainParts;
    public GameObject environment;
    public Transform player;
    public TorchController torch;

    [SerializeField]
    [Tooltip("Сдвиг по z, если игрок его проходит, то надо генерировать следующий блок")]
    private float zShift = 40;
    private float partLength = 80;
    [SerializeField]
    private int index = 0;
    private CharacterController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<CharacterController>();
        if (playerController == null)
            Debug.LogError("Не прицепился Character Controller");
        if (terrainParts == null || terrainParts.Count == 0)
            Debug.LogError("Нет возможности генерировать путь!!!");
        if (torch == null)
            Debug.LogError("Факел не присвоен!");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.z > (index * partLength - zShift))
            GenerateNewPart();
    }

    private void GenerateNewPart()
    {
        index++;
        var part = Instantiate(terrainParts[Random.Range(0, terrainParts.Count)], environment.transform);
        part.transform.position = Vector3.forward * index * partLength;
        for (int i = 0; i < part.transform.childCount; i++)
            if (part.transform.GetChild(i).CompareTag("Waterfall"))
            {
                var waterDetection = part.transform.GetChild(i).GetComponent<WaterDetector>();
                waterDetection.torch = torch;
                waterDetection.player = player;
            }
            else if (part.transform.GetChild(i).CompareTag("Fire"))
            {
                var fireDetection = part.transform.GetChild(i).GetComponent<FireController>();
                fireDetection.torch = torch;
                fireDetection.player = player;
            }
    }
}
