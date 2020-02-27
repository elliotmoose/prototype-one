using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{   
    public float mapSize = 10; //a map of size 10 has a width of size 100units

    private GameObject _map;
    public static MapManager GetInstance() 
    {
        GameObject gameManager = GameObject.Find("GameManager");
        if(gameManager == null) 
        {
            Debug.LogError("GameManager has not been instantiated yet");
            return null;
        }

        MapManager mapManager = gameManager.GetComponent<MapManager>();

        if(mapManager == null) 
        {
            Debug.LogError("GameManager has no component MapManager");
            return null;
        }

        return mapManager;
    }

    // Start is called before the first frame update
    void Start()
    {
        _map = GameObject.Find("Map");

        if(_map != null) 
        {
            _map.transform.localScale = new Vector3(mapSize, mapSize, mapSize);
        }
        else 
        {
            Debug.LogWarning("No map object with name \"Map\"");
        }
    }

    public static bool IsInMap(Vector3 position) {
        MapManager mapManager = GetInstance();
        Vector3 mapCenter = mapManager._map.transform.position;
        float mapSize = mapManager.mapSize;
        bool isXBounded = Mathf.Abs(position.x - mapCenter.x) < (mapSize*10)/2;
        bool isZBounded = Mathf.Abs(position.z - mapCenter.z) < (mapSize*10)/2;
        return isXBounded && isZBounded;
    }

    public GameObject GetMap()
    {
        return _map;
    }
}
