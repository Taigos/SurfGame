using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] GameObject[] powerupTables;
    [SerializeField] float[] powerupTablesY;
    [SerializeField] int powerupTablesNum = 4;
    [SerializeField] int powerupTablesYStep = 80;
    [SerializeField] GameObject powerupTable;
    [SerializeField] TextMeshProUGUI scoreTextObject;
    [SerializeField] TextMeshProUGUI gemsTextObject;

    // Start is called before the first frame update
    void Start()
    {
        powerupTables = new GameObject[powerupTablesNum];
        powerupTablesY = new float[powerupTablesNum];
        for (int i = 0; i < powerupTablesNum; i++)
        {
            powerupTablesY[i] = powerupTable.transform.position.y + i * powerupTablesYStep;
            //Debug.Log(i + ": " + powerupTables[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator ShowPowerupBar(GameObject powerup, float time)
    {
        GameObject newPowerupBar = Instantiate(powerupTable, GameObject.Find("Canvas").transform);
        Slider powerupSlider = newPowerupBar.GetComponentInChildren<Slider>();
        //newPowerupBar.//GetComponentInChildren(Image);
        newPowerupBar.GetComponentsInChildren<Image>()[4].color = powerup.GetComponent<Renderer>().sharedMaterial.color;
        //Debug.Log($"{powerup.GetComponent<Renderer>().sharedMaterial.color.r} {powerup.GetComponent<Renderer>().sharedMaterial.color.g} {powerup.GetComponent<Renderer>().sharedMaterial.color.b} {powerup.GetComponent<Renderer>().sharedMaterial.color.a}");
        powerupSlider.value = 1;

        // get current length of initialized array
        int y = GetNextPowerupNum();
        if (!(y == powerupTablesNum))
        {
            newPowerupBar.transform.position = new Vector3(newPowerupBar.transform.position.x, powerupTablesY[y], newPowerupBar.transform.position.z);
        }
        // add this game object to array
        powerupTables[y] = newPowerupBar;

        float step = 1 / time;
        for (int i = 0; i < time; i++)
        {
            yield return new WaitForSeconds(1);
            powerupSlider.value -= step;
        }
        Destroy(newPowerupBar);
        // remove this object from array
        powerupTables[y] = null;

        // check that all of them are in order
    }

    private void MovePowerupTablesDown()
    {
        //GameObject[] powerupTables = GameObject.Find("Canvas").GetComponentsInChildren
    }

    private int GetNextPowerupNum()
    {
        for (int i = 0; i < powerupTablesNum; i++)
        {
            if (powerupTables[i] == null)
            {
                return i;
            }
        }
        return powerupTablesNum;
    }

    public void SetScoreText(int score)
    {
        scoreTextObject.text = "Score: " + score;
    }

    public void UpdateGemsText(int gems)
    {
        gemsTextObject.text = $"Gems: {gems}";
    }
}
