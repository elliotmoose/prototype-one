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

  void OnTakeDamage(float amount)
  {
    // spawn text object, update text value to damage amount, and float upwards
    GameObject textObj = GameObject.Instantiate(damageIndicatorPrefab);
    textObj.GetComponent<TMPro.TextMeshPro>().text = amount.ToString();
    textObj.transform.position = this.transform.position;
    textObj.transform.rotation = Camera.main.transform.rotation;
    textObj.transform.position += Camera.main.transform.up * 4; // offset
    // textObj.transform.SetParent(Camera.main.transform);
    StartCoroutine(FloatText(textObj));
  }

  IEnumerator FloatText(GameObject textObj)
  {
    float duration = 0.6f;
    float timeElapsed = 0;
    while (timeElapsed < duration)
    {
      timeElapsed += Time.deltaTime;
      yield return null;
    }

    GameObject.Destroy(textObj);
  }
}