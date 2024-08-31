using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] GameObject[] obstacles;
    public GameObject[] powerups;
    [SerializeField] GameObject[] buildings;
    [SerializeField] GameObject dots;
    [SerializeField] GameObject lightStolb;
    [SerializeField] GameObject roadFragment1;
    [SerializeField] GameObject roadFragment2;


    bool[] currentPowerups;

    private float[] xSpawnLocations = {-2.5f, 0, 2.5f};
    private float zSpawnLocation = 152;
    private float xLeftBuildingSpawnLocation = -10f;
    private float yBuildingSpawnLocation = 0f;

    [SerializeField] float obstacleSpawnTime;
    [SerializeField] float powerupSpawnTime;
    [SerializeField] float buildingSpawnTime;


    [SerializeField] float speed = 5;
    private float defoultSlotSize = 4;
    public float boundaryZ = -20;
    private float speedIncreaseTime = 3;
    private float speedIncreaseValue = 0.5f;
    private float maxSpeed = 30.0f;
    int score = 0;
    int scoreValue = 1;

    public UIHandler uiHandler;

    void Start()
    {
        currentPowerups = new bool[powerups.Length];
        InitializeCurrentPowerups();

        StartCoroutine(SpawnCoroutine(obstacles, obstacleSpawnTime));
        StartCoroutine(SpawnCoroutine(powerups, powerupSpawnTime));

        ///////////////////////////////////////////////////////////////
        // спавн короткого пунктира - 0,5 слот - в точке 0, как и дом 1
        // спавн короткого фонаря - 2 слота - y+0.5 x -5 z +2 Второй: x +6 z+8
        // спавн короткого здания - 1 слот - в точке 0
        // спавн длинного здания - 2 слота
        // спавн кустов - 
        // спавн неведомой фигни - 
        // СМЕНА УЧАСТКА ?? Город, река(мост), лес
        StartCoroutine(SpawnBuildings_Coroutine(1));
        //StartCoroutine(SpawnDots_Coroutine(0.5f));
        //StartCoroutine(SpawnLights_Coroutine(4)); //сразу 2 фонаря спавнит, чтобы не делать отдельно для 2-х сторон
        //StartCoroutine(SpawnCars_Coroutine(4));
        StartCoroutine(RePlaceRoad_Coroutine(38));
        StartCoroutine(IncreaseGameSpeed());

        uiHandler = GameObject.Find("Canvas").GetComponent<UIHandler>();
    }

    void InitializeCurrentPowerups()
    {
        for(int i = 0; i < currentPowerups.Length; i++)
        {
            currentPowerups[i] = false;
            // Debug.Log("currnt pwerups: " + currentPowerups[i]);
        }
    }

    private void FixedUpdate()
    {
        score += scoreValue;
        uiHandler.SetScoreText(score);
        Debug.Log("Speed: " + speed);
    }

    // 10 sec
    public void ScoreMultiplier()
    {
        scoreValue = 2;
        Debug.Log("Score doubled");
        StartCoroutine(uiHandler.ShowPowerupBar(powerups[1], 10));
        scoreValue = 1;
        Debug.Log("Score normal");
    }

    private IEnumerator IncreaseGameSpeed()
    {
        while (speed < maxSpeed) { 
            yield return new WaitForSeconds(speedIncreaseTime);
            speed += speedIncreaseValue;
        }
    }

    IEnumerator SpawnCoroutine(GameObject[] objectArray, float spawnTimeIndex)
    {
        float oldSpeed = speed;
        while (true)
        {
            yield return new WaitForSeconds(spawnTimeIndex);
            SpawnObject(objectArray);
            if (speed / oldSpeed > 5)
            {
                spawnTimeIndex -= 1;
                oldSpeed = speed;
            }
        }
    }
    IEnumerator SpawnCars_Coroutine(float slotSize)
    {
        while (true)
        {

            yield return new WaitForSeconds(defoultSlotSize*slotSize / speed);

            //spawnBuilding();
        }

    }
    IEnumerator SpawnDots_Coroutine(float slotSize)
    {
        while (true)
        {

            yield return new WaitForSeconds(defoultSlotSize*slotSize / speed);

            Vector3 dotsPosition = new Vector3(xLeftBuildingSpawnLocation+10, yBuildingSpawnLocation, zSpawnLocation);
            Instantiate(dots, dotsPosition, dots.gameObject.transform.rotation);
        }

    }
    IEnumerator SpawnLights_Coroutine(float slotSize)
    {
        while (true)
        {

            yield return new WaitForSeconds(defoultSlotSize * slotSize / speed);

            //здание для левой стороны
            Vector3 lightStolbPosition = new Vector3(xLeftBuildingSpawnLocation+4.9f, yBuildingSpawnLocation+0.5f, zSpawnLocation+2);
            Instantiate(lightStolb, lightStolbPosition, lightStolb.gameObject.transform.rotation);

            //здание для правой стороны
            lightStolbPosition = new Vector3(xLeftBuildingSpawnLocation+15.1f, yBuildingSpawnLocation + 0.5f, zSpawnLocation+6);
            Instantiate(lightStolb, lightStolbPosition,
                Quaternion.Euler(lightStolb.gameObject.transform.rotation.eulerAngles + new Vector3(0, 180, 0)));
        }

    }
    IEnumerator SpawnBuildings_Coroutine(float slotSize)
    {
        while (true)
        {
            
            yield return new WaitForSeconds(defoultSlotSize*slotSize / speed);

            spawnBuilding();
        }
        
    }
    private void spawnBuilding()
    {
        int randomBuilding = UnityEngine.Random.Range(0, buildings.Length);//здание для левой стороны
        Vector3 buildingPosition = new Vector3(xLeftBuildingSpawnLocation, yBuildingSpawnLocation, zSpawnLocation);
        Instantiate(buildings[randomBuilding], buildingPosition, buildings[randomBuilding].gameObject.transform.rotation);

        randomBuilding = UnityEngine.Random.Range(0, buildings.Length);//здание для правой стороны
        buildingPosition = new Vector3(-xLeftBuildingSpawnLocation, yBuildingSpawnLocation, zSpawnLocation);
        Instantiate(buildings[randomBuilding], buildingPosition,
            Quaternion.Euler(buildings[randomBuilding].gameObject.transform.rotation.eulerAngles + new Vector3(0, 180, 0)));
    }
    private void SpawnObject(GameObject[] objectGroup)
    {
        int randomX = UnityEngine.Random.Range(0, xSpawnLocations.Length);
        int randomObject = UnityEngine.Random.Range(0, objectGroup.Length);
        Vector3 objectPosition = new Vector3(xSpawnLocations[randomX], 
            objectGroup[randomObject].gameObject.transform.position.y, zSpawnLocation);
        
        Instantiate(objectGroup[randomObject], objectPosition, objectGroup[randomObject].gameObject.transform.rotation);
    }
    float CalculateTimeWithAcceleration(float startSpeed, float acceleration, float distance)
    {
        float discriminant = startSpeed * startSpeed + 2 * acceleration * distance;

        // Проверяем, есть ли решение (дискриминант должен быть неотрицательным)
        if (discriminant < 0)
        {
            throw new ArgumentException("Нет реальных решений для заданных параметров.");
        }

        // Рассчитываем два возможных времени
        float time1 = (-startSpeed + Mathf.Sqrt(discriminant)) / acceleration;
        float time2 = (-startSpeed - Mathf.Sqrt(discriminant)) / acceleration;

        // Выбираем положительное значение времени
        if (time1 >= 0 && time2 >= 0)
            return Mathf.Min(time1, time2); // Если оба времени положительные, возвращаем наименьшее
        else if (time1 >= 0)
            return time1;
        else if (time2 >= 0)
            return time2;
        else
            throw new ArgumentException("Решение не является положительным.");
    }
    IEnumerator RePlaceRoad_Coroutine(float slotCount)
    {
        while (true)
        {
            float timeToWait = CalculateTimeWithAcceleration(
                speed, speedIncreaseValue / speedIncreaseTime, defoultSlotSize * slotCount);
            yield return new WaitForSeconds(timeToWait);
            //yield return new WaitForSeconds(defoultSlotSize * slotCount / speed);
            if (roadFragment1.transform.position.z < roadFragment2.transform.position.z)
            {
                roadFragment1.transform.position = new Vector3(0, 0, 152);
            }
            else
            {
                roadFragment2.transform.position = new Vector3(0, 0, 152);
            }
        }

    }


    public float GetCurrentSpeed()
    {
        return speed;
    }

}
