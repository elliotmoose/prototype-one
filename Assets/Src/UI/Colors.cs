using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colors : MonoBehaviour
{
    public static Color limegreen;
    public static Color green;
    public static Color gray;
    public static Color darkgray;
    public static Color red;
    public static Color blue;
    public static Color yellow;
    public static Color white;
    public static Color black;
    // Start is called before the first frame update
    void Awake()
    {
        InitializeColors();
    }

    private void InitializeColors() 
    {
        ColorUtility.TryParseHtmlString("#42DF48", out limegreen);
        ColorUtility.TryParseHtmlString("#2FB25C", out green);
        ColorUtility.TryParseHtmlString("#717171", out gray);
        ColorUtility.TryParseHtmlString("#3B3333", out darkgray);
        ColorUtility.TryParseHtmlString("#F24545", out red);
        ColorUtility.TryParseHtmlString("#FFB802", out yellow);
        ColorUtility.TryParseHtmlString("#7EA2FF", out blue);
        ColorUtility.TryParseHtmlString("#FFFFFF", out white);
        ColorUtility.TryParseHtmlString("#000000", out black);
    }
}
