using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadModel : MonoBehaviour
{
    //
    [SerializeField]//Determines the type 
    private string modelType;
    [SerializeField]//Determines the plane to load (tailnumber)
    private string modelToLoad;

    //Loads the pivot to place an object on
    private GameObject basePivot;
    [SerializeField] //Stores a current model for a null check from the DataStorage object in the scene
    private GameObject curModel;
    private LoadedModel modelData;

    //Looks for the DataStorage object to push JSON data to
    public ObjectJSON dataJSON;

    //Stores the camera position at the start
    private Camera worldCam;

    private Transform originalCamPos;

    //Making a list of all sensors attached to the model
    [SerializeField]
    private List<GameObject> sensors;

    void Start()
    {
        basePivot = GameObject.Find("ModelPivot");
        worldCam = GameObject.Find("CamView").GetComponent<Camera>();

        modelData = GameObject.Find("DataStorage").GetComponent<LoadedModel>();

        originalCamPos = worldCam.transform;

        dataJSON = GameObject.Find("DataStorage").GetComponent<ObjectJSON>();

    }

    // Update is called once per frame
    public void LoadModelButton()
    {
        //Clears any existing list and model
        sensors.Clear();

        //Disables the info about model if it was there
        GameObject.Find("DataStorage").GetComponent<UIBehaviour>().infoContainer.SetActive(false);
        if (modelData.currentModel == null)
        {
            //Load
            GameObject loadModel = Resources.Load<GameObject>("Model Prefab/" + modelType + "/" + modelToLoad);
            GameObject loadedModel = Instantiate(loadModel, basePivot.transform.position, Quaternion.identity);
            loadedModel.name = modelToLoad;

            //Sets the parent
            loadedModel.transform.SetParent(basePivot.transform, false);

            //Set current model
            curModel = loadedModel;

            //Sets the model in the DataStorage object in the scene
            modelData.currentModel = curModel;

            //Sets camera back to original position and rotation
            worldCam.transform.position = originalCamPos.transform.position;

            //Reset pivot rotation
            basePivot.transform.rotation = Quaternion.Euler(Vector3.zero);

            //Tells the DataStorage to load data into JSON file
            dataJSON.LoadData(modelType, modelToLoad);

            //Adds sensors to the sensor list
            AddSensors();

            //Add this data to the sensors on the model
            AddDataToSensor();
        }
        else
        {
            //Remove Model
            curModel = null;
            Destroy(basePivot.GetComponent<Transform>().GetChild(0).gameObject);

            //Load
            GameObject loadModel = Resources.Load<GameObject>("Model Prefab/" + modelType + "/" + modelToLoad);
            GameObject loadedModel = Instantiate(loadModel, basePivot.transform.position, Quaternion.identity);
            loadedModel.name = modelToLoad;

            //Sets the parent
            loadedModel.transform.SetParent(basePivot.transform, false);

            //Set current model
            curModel = loadedModel;

            //Sets the model in the DataStorage object in the scene
            modelData.currentModel = curModel;

            //Sets camera back to original position and rotation
            worldCam.transform.position = originalCamPos.transform.position;

            //Reset pivot rotation
            basePivot.transform.rotation = Quaternion.Euler(Vector3.zero);

            //Tells the DataStorage to load data from JSON file
            dataJSON.LoadData(modelType, modelToLoad);

            //Adds sensors to the sensor list
            AddSensors();

            //Add this data to the sensors on the model
            AddDataToSensor();

        }
    }

    private void AddSensors()
    {
        //Make the list of the available sensors
        Transform sensorLoc = GameObject.Find("ModelPivot/" + curModel.name + "/SensLoc").GetComponent<Transform>();

        foreach (Transform child in sensorLoc.transform)
        {
            sensors.Add(child.gameObject);
        }
        foreach (GameObject obj in sensors)
        {
            obj.AddComponent<SensorData>();
        }
    }

    private void AddDataToSensor()
    {
        //starting with int at 0
        int i = 0;

        //caution limit
        //NOTE this could be less code if I made strings of the JsonLoader class fields, but since this is a prototype and I do not have the full
        //real json data, this will do for now
        //Must add all properties from the JsonLoader (the names)

        foreach (var cautionlimit in dataJSON.jsonList.caution_limit)
        {
            sensors[i].GetComponent<SensorData>().caution_limit = dataJSON.jsonList.caution_limit[i];
            i++;
            if (i == dataJSON.jsonList.caution_limit.Capacity)
            {
                i = 0;
            }
        }

        //ci identifier
        foreach (var ci in dataJSON.jsonList.ci_identifier)
        {
            sensors[i].GetComponent<SensorData>().ci_identifier = dataJSON.jsonList.ci_identifier[i];
            i++;
            if (i == dataJSON.jsonList.ci_identifier.Capacity)
            {
                i = 0;
            }
        }

        //ci name
        foreach (var ciname in dataJSON.jsonList.ci_name)
        {
            sensors[i].GetComponent<SensorData>().ci_name = dataJSON.jsonList.ci_name[i];
            i++;
            if (i == dataJSON.jsonList.ci_name.Capacity)
            {
                i = 0;
            }
        }

        //color
        foreach (var color in dataJSON.jsonList.color)
        {
            sensors[i].GetComponent<SensorData>().color = dataJSON.jsonList.color[i];
            i++;
            if (i == dataJSON.jsonList.color.Capacity)
            {
                i = 0;
            }
        }

        //exceed limit
        foreach (var exceedlim in dataJSON.jsonList.exceed_limit)
        {
            sensors[i].GetComponent<SensorData>().exceed_limit = dataJSON.jsonList.exceed_limit[i];
            i++;
            if (i == dataJSON.jsonList.exceed_limit.Capacity)
            {
                i = 0;
            }
        }

        //goal limit
        foreach (var goallim in dataJSON.jsonList.goal_limit)
        {
            sensors[i].GetComponent<SensorData>().goal_limit = dataJSON.jsonList.goal_limit[i];
            i++;
            if (i == dataJSON.jsonList.goal_limit.Capacity)
            {
                i = 0;
            }
        }

        //Latest_value
        foreach (var latval in dataJSON.jsonList.latest_value)
        {
            sensors[i].GetComponent<SensorData>().latest_value = dataJSON.jsonList.latest_value[i];
            i++;
            if (i == dataJSON.jsonList.latest_value.Capacity)
            {
                i = 0;
            }
        }
        AddNodes();
    }
    public void AddNodes()
    {
        int i = 0;
        GameObject node = Resources.Load<GameObject>("UI Prefabs/Node");

        //First all exsisting nodes must be destroyed!
        foreach (Transform obj in GameObject.Find("UI/Nodes").transform)
        {
            GameObject.Destroy(obj.gameObject);
        }

        foreach (var addSensor in sensors)
        {

            GameObject nodes = Instantiate(node, new Vector3(0, 0, 0), Quaternion.identity);
            nodes.name = sensors[i].name;
            nodes.GetComponent<NodeScript>().sensorLocation = addSensor;


            i++;
        }
    }
}
