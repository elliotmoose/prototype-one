using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingManager : MonoBehaviour
{
  public static PostProcessingManager GetInstance()
  {
    GameObject gameManager = GameObject.Find("GameManager");
    if (gameManager == null)
    {
      Debug.LogError("GameManager has not been instantiated yet");
      return null;
    }

    PostProcessingManager postProcessingManager = gameManager.GetComponent<PostProcessingManager>();

    if (postProcessingManager == null)
    {
      Debug.LogError("GameManager has no component PostProcessingManager");
      return null;
    }

    return postProcessingManager;
  }

  PostProcessVolume volume;
  ColorGrading colorGradingLayer;

  void Awake()
  {
    volume = Camera.main.GetComponent<PostProcessVolume>();
    colorGradingLayer = null;
    volume.profile.TryGetSettings(out colorGradingLayer);
  }

  void Update()
  {
    UpdateInfectedApperance();
  }

  public void UpdateInfectedApperance()
  {
    // Player player = Player.GetInstance();
    // if(player == null || colorGradingLayer == null) 
    // {
    //     return;
    // }

    // bool isInfected = player.HasEffectOfType(typeof(InfectedEffect));

    // if(isInfected) 
    // {   
    //     colorGradingLayer.active = true;
    // }
    // else 
    // {
    //     colorGradingLayer.active = false;
    // }
  }

}