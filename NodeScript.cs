using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeScript : MonoBehaviour
{
    public GameObject sensorLocation;
    public SensorData sensData;
    [SerializeField]

    private Camera cam;

    //Data from the node
    public double caution_limit;
    public string ci_identifier;
    public string ci_name;
    public double color;
    public double exceed_limit;
    public double goal_limit;
    public double latest_value;

    //Find the UIBehaviour
    private UIBehaviour infoScreen;

    //


    // Start is called before the first frame update
    void Start()
    {
        //initialize the data from the nodes onto the button nodes
        sensData = sensorLocation.GetComponent<SensorData>();
        infoScreen = GameObject.Find("DataStorage").GetComponent<UIBehaviour>();

        caution_limit = sensData.caution_limit;
        ci_identifier = sensData.ci_identifier;
        ci_name = sensData.ci_name;
        color = sensData.color;
        exceed_limit = sensData.exceed_limit;
        goal_limit = sensData.goal_limit;
        latest_value = sensData.latest_value;


        this.transform.SetParent(GameObject.Find("UI/Nodes").transform);

        cam = GameObject.Find("CamView").GetComponent<Camera>();

        if (color == 0)
        {
            this.GetComponent<Image>().color = new Color(0f, 0.7f, 0.0f);
        }
        if (color == 1)
        {
            this.GetComponent<Image>().color = new Color(1f, 0.5f, 0.0f);
        }
        if (color == 2)
        {
            this.GetComponent<Image>().color = new Color(1f, 0.0f, 0.0f);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (this.transform.position != null && sensorLocation != null)
        {
            this.transform.position = cam.WorldToScreenPoint(sensorLocation.transform.position);
        }
    }

    public void ShowData()
    {
        infoScreen.infoContainer.SetActive(true);
        GameObject.Find("UI/InfoContainer/caution_limit").GetComponent<Text>().text = "Caution Limit: " + caution_limit.ToString();
        GameObject.Find("UI/InfoContainer/ci_identifier").GetComponent<Text>().text = "Identifier: " + ci_identifier.ToString();
        GameObject.Find("UI/InfoContainer/ci_name").GetComponent<Text>().text = "Name: " + ci_name.ToString();

        //Status
        if (color == 0)
        {
            GameObject.Find("UI/InfoContainer/color").GetComponent<Text>().text = "Status: OK";
            GameObject.Find("UI/InfoContainer/color").GetComponent<Text>().color = new Color(0f, 0.7f, 0.0f);
        }
        if (color == 1)
        {
            GameObject.Find("UI/InfoContainer/color").GetComponent<Text>().text = "Status: LOOK INTO IT";
            GameObject.Find("UI/InfoContainer/color").GetComponent<Text>().color = new Color(1f, 0.5f, 0.0f);
        }
        if (color == 2)
        {
            GameObject.Find("UI/InfoContainer/color").GetComponent<Text>().text = "Status: CRITICAL ERROR";
            GameObject.Find("UI/InfoContainer/color").GetComponent<Text>().color = new Color(1f, 0.0f, 0.0f);
        }

        GameObject.Find("UI/InfoContainer/exceed_limit").GetComponent<Text>().text = "Exceed Limit: " + exceed_limit.ToString();
        GameObject.Find("UI/InfoContainer/goal_limit").GetComponent<Text>().text = "Goal Limit " + goal_limit.ToString();
        GameObject.Find("UI/InfoContainer/latest_value").GetComponent<Text>().text = "Latest Value: " + latest_value.ToString();
    }
    public void CloseData()
    {
        infoScreen.infoContainer.SetActive(false);
    }
}
