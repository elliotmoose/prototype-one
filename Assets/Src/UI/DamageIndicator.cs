using UnityEngine;
using System.Collections;

public class DamageIndicator : MonoBehaviour
{
  public GameObject damageIndicatorPrefab;
  void Awake()
  {
    this.GetComponent<Entity>().OnTakeDamageEvent += OnTakeDamage;
  }

  void OnDestroy()
  {
    this.GetComponent<Entity>().OnTakeDamageEvent -= OnTakeDamage;
  }

  void OnTakeDamage(TakeDamageInfo damageInfo)
  {
    // spawn text object, update text value to damage amount, and float upwards
    GameObject textObj = GameObject.Instantiate(damageIndicatorPrefab);
    TMPro.TextMeshPro tmp = textObj.GetComponent<TMPro.TextMeshPro>();
    tmp.text = damageInfo.effectiveDamage.ToString();
    tmp.color = damageInfo.isCritical ? new Color(0.737f, 0.07f, 0.07f) : Color.white;
    textObj.transform.position = this.transform.position;
    textObj.transform.rotation = Camera.main.transform.rotation;
    textObj.transform.position += Camera.main.transform.up * 4; // offset

    StartCoroutine(DestroyAfter(textObj, 0.6f));

    var renderers = this.GetComponentsInChildren<Renderer>();
    StartCoroutine(HitFlash(renderers, 0.4f));
  }

  IEnumerator DestroyAfter(GameObject textObj, float duration)
  {
    float timeElapsed = 0;
    while (timeElapsed < duration)
    {
      timeElapsed += Time.deltaTime;
      yield return null;
    }

    GameObject.Destroy(textObj);
  }

  IEnumerator HitFlash(Renderer[] renderers, float duration)
  {
    foreach (var renderer in renderers)
    {
      foreach (var material in renderer.materials)
      {
        material.EnableKeyword("_EMISSION");
      }
    }
    float timeElapsed = 0;
    while (timeElapsed < duration)
    {
      timeElapsed += Time.deltaTime;
      foreach (var renderer in renderers)
      {
        foreach (var material in renderer.materials)
        {
          var percentage = (duration - timeElapsed / duration) / duration * 0.5f + 0.3f;
          material.SetColor("_EmissionColor", Color.white * percentage);
        }
      }
      yield return null;
    }

    foreach (var renderer in renderers)
    {
      foreach (var material in renderer.materials)
      {
        material.SetColor("_EmissionColor", Color.black);
      }
    }
  }
}