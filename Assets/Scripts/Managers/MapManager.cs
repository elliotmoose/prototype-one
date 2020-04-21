using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MapManager : MonoBehaviour
{   
    public float mapSize = 10; //a map of size 10 has a width of size 100units
    public float mapTextureHeight = 1f; //hieght of bumps
    public float mapResolution = 1;
    public float mapPositionVerticalOffset = -0.7f;
    public float perlinScale = 20f;
    public float healthZoneCooldown = 5;
    public float[] Cooldown;


    // public float zoneDuration = 30f;
    // public float zoneWait = 5f;

    // public float zoneWaitMin = 0;
    private float _zoneSpawnCooldown = 55f;
    private List<Zone> _zoneList = new List<Zone>();
    public GameObject map;

    private GameObject tutorialManager;

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

    public static GameObject getMap()
    {
        return GetInstance().map;
    }


    // Start is called before the first frame update
    void Start()
    {
        tutorialManager = TutorialManager.GetInstance().gameObject;
        Cooldown = new float[] {0f,35f};
        // GenerateMap();
        _zoneList.Add(new HealingZone());
        _zoneList.Add(new BuffMovementSpeedZone());
        // slowZone = new BuffAttackZone();
    }

    void Update(){
        for( int i = 0; i < map.transform.childCount; i++){
            Zone zone = map.transform.GetChild(i).GetComponent<Zone>();
            if(zone.active == false){
                Destroy(map.transform.GetChild(i).gameObject);
            }
        }
        if(!TutorialManager.active){
            SpawnZone(0);
            SpawnZone(1);
            // Debug.Log(Cooldown[0]);
        }

        // if (Input.GetKeyDown("space"))
        // {
        //     // Zone slowZone = new BuffAttackZone();
        //     CreateZone(_zoneList[0]);
        // }
    }


    void GenerateMap() 
    {
        map = GameObject.Find("Map");

        if(map != null) 
        {
            map.transform.localScale = new Vector3(mapSize, mapSize, mapSize);
        }
        else 
        {
            Debug.LogWarning("No map object with name \"Map\"");
        }

        //Create an empty gamobject
            GameObject plane = new GameObject("MapLowPoly");

            //Create Mesh Filter and Mesh Renderer components
            MeshFilter meshFilter = plane.AddComponent(typeof(MeshFilter)) as MeshFilter;
            MeshRenderer meshRenderer = plane.AddComponent((typeof(MeshRenderer))) as MeshRenderer;
            meshRenderer.sharedMaterial = Resources.Load("Materials/Map/MapMaterial") as Material;

            // //Generate a name for the mesh that will be created
            // string planeMeshAssetName = plane.name + widthSegments + "x" + heightSegments
            //                             + "W" + planeWidth + "H" + planeHeight + ".asset";

            // //Load the mesh from the save location
            // Mesh m = (Mesh)AssetDatabase.LoadAssetAtPath(assetSaveLocation + planeMeshAssetName, typeof(Mesh));

            //If there isn't a mesh located under assets, create the mesh
            float width = mapSize * 10;

            Mesh m = new Mesh();
            m.name = plane.name;

            int segmentCount = (int)(mapResolution * width);
            // int hCount2 = segmentCount + 1;
            int hCount2 = segmentCount + 1;
            int vCount2 = segmentCount + 1;
            int numTriangles = segmentCount * segmentCount * 6;
            int numVertices = hCount2 * vCount2;

            Vector3[] vertices = new Vector3[numVertices];
            Vector2[] uvs = new Vector2[numVertices];
            int[] triangles = new int[numTriangles];
            Vector4[] tangents = new Vector4[numVertices];
            Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
            Vector2 anchorOffset = Vector2.zero;

            int index = 0;
            float uvFactorX = 1.0f / segmentCount;
            float uvFactorY = 1.0f / segmentCount;
            float scaleX = width / segmentCount;
            float scaleY = width / segmentCount;

            //Generate the vertices
            for (float y = 0.0f; y < vCount2; y++)
            {
                for (float x = 0.0f; x < hCount2; x++)
                {
                    vertices[index] = new Vector3(x * scaleX - width / 2f - anchorOffset.x, 0.0f, y * scaleY - width / 2f - anchorOffset.y);

                    tangents[index] = tangent;
                    uvs[index++] = new Vector2(x * uvFactorX, y * uvFactorY);
                }
            }

            //Reset the index and generate triangles
            index = 0;
            for (int y = 0; y < segmentCount; y++)
            {
                for (int x = 0; x < segmentCount; x++)
                {
                    triangles[index] = (y * hCount2) + x;
                    triangles[index + 1] = ((y + 1) * hCount2) + x;
                    triangles[index + 2] = (y * hCount2) + x + 1;

                    triangles[index + 3] = ((y + 1) * hCount2) + x;
                    triangles[index + 4] = ((y + 1) * hCount2) + x + 1;
                    triangles[index + 5] = (y * hCount2) + x + 1;
                    index += 6;
                }
            }

            //Update the mesh properties (vertices, UVs, triangles, normals etc.)
            m.vertices = vertices;
            m.uv = uvs;
            m.triangles = triangles;
            m.tangents = tangents;
            m.RecalculateNormals();

            // //Save the newly created mesh under save location to reload later
            // AssetDatabase.CreateAsset(m, assetSaveLocation + planeMeshAssetName);
            // AssetDatabase.SaveAssets();


            //Update mesh
            meshFilter.sharedMesh = m;
            m.RecalculateBounds();

            // //If add collider is set to true, add a box collider
            // if (addCollider)
            //     plane.AddComponent(typeof(BoxCollider));

            //Add LowPolyWater as component
            MapTextureGenerator mapTextureGenerator = plane.AddComponent<MapTextureGenerator>();
            mapTextureGenerator.textureHeight = mapTextureHeight;
            mapTextureGenerator.scale = perlinScale;
            mapTextureGenerator.Build();
            plane.transform.position = new Vector3(0, mapPositionVerticalOffset, 0);            
    }


    public void SpawnZone(int i){
        Cooldown[i] -= Time.deltaTime;
        if(Cooldown[i] < 0){
            CreateZone(_zoneList[i]);
            Cooldown[i] = _zoneSpawnCooldown;
            if(i == 1){
                Cooldown[i] = Random.Range(60f,85f);
            }
        }
    }

    public static void CreateZone(Zone zone){
        // float zoneWidth = 1;
        Object zonePrefab = Resources.Load($"Prefabs/Zone/" + zone.name);
        GameObject map = GetInstance().GetMap();
        Vector3 spawnCoordinate = new Vector3(Random.Range(-(GetInstance().mapSize*10)/5, (GetInstance().mapSize*10)/5),0.5f, Random.Range(-(GetInstance().mapSize*10)/5, (GetInstance().mapSize*10)/5));
        // Vector3 spawnCoordinate = new Vector3(0,0.1f, 0);
        GameObject Zone = (GameObject)GameObject.Instantiate(zonePrefab, map.transform.position + spawnCoordinate, map.transform.rotation, map.transform);
        Zone.transform.localScale = new Vector3(1,1,1);
    }


    public static bool IsInMap(Vector3 position) {
        MapManager mapManager = GetInstance();
        Vector3 mapCenter = mapManager.map.transform.position;
        float mapSize = mapManager.mapSize;
        bool isXBounded = Mathf.Abs(position.x - mapCenter.x) < (mapSize*10)/2;
        bool isZBounded = Mathf.Abs(position.z - mapCenter.z) < (mapSize*10)/2;
        return isXBounded && isZBounded;
    }

    public GameObject GetMap()
    {
        return map;
    }


}
