using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
  float weaponHitWidth = 6;
  float hitBoxDistance = 2;
  private Vector3 GetHitHalfExtents()
  {
    var halfExtents = new Vector3(weaponHitWidth / 2, 2, this.GetWeaponRange() / 2);
    return halfExtents;
  }

  private Vector3 GetHitCenter()
  {
    return this._owner.transform.position + (this._owner.transform.rotation * Vector3.forward * (this.GetWeaponRange() / 2 + hitBoxDistance));
  }
  protected override void Fire(int comboIndex)
  {
    var halfExtents = GetHitHalfExtents();
    Collider[] collidersHit = Physics.OverlapBox(this.GetHitCenter(), halfExtents, this._owner.transform.rotation);


    float critChance = this._owner as Player != null ? (this._owner as Player).effectiveCritChance : 0;
    bool isCrit = Mathf.RoundToInt(Random.Range(0, 100)) <= critChance * 100;

    foreach (Collider collider in collidersHit)
    {
      if (collider.gameObject.tag != _owner.tag)
      {
        Entity entity = collider.gameObject.GetComponent<Entity>();
        if (entity != null)
        {
          entity.TakeDamage(new TakeDamageInfo(_owner, entity, _weaponData.damage[comboIndex], DamageType.NORMAL, isCrit));
          entity.TakeEffect(new KnockbackEffect(_owner, entity, _owner.transform.position, 0.3f, 0.2f));
        }
      }
    }
  }


  public override void WeaponUpdate()
  {
    base.WeaponUpdate();
    DrawHitBox();
    Debug.Log(this._weaponData.name);
  }

  void DrawHitBox()
  {
    var halfExtents = GetHitHalfExtents();
    DrawCubePoints(CubePoints(this.GetHitCenter(), halfExtents, this._owner.transform.rotation));
  }

  Vector3[] CubePoints(Vector3 center, Vector3 extents, Quaternion rotation)
  {
    Vector3[] points = new Vector3[8];
    points[0] = rotation * Vector3.Scale(extents, new Vector3(1, 1, 1)) + center;
    points[1] = rotation * Vector3.Scale(extents, new Vector3(1, 1, -1)) + center;
    points[2] = rotation * Vector3.Scale(extents, new Vector3(1, -1, 1)) + center;
    points[3] = rotation * Vector3.Scale(extents, new Vector3(1, -1, -1)) + center;
    points[4] = rotation * Vector3.Scale(extents, new Vector3(-1, 1, 1)) + center;
    points[5] = rotation * Vector3.Scale(extents, new Vector3(-1, 1, -1)) + center;
    points[6] = rotation * Vector3.Scale(extents, new Vector3(-1, -1, 1)) + center;
    points[7] = rotation * Vector3.Scale(extents, new Vector3(-1, -1, -1)) + center;

    return points;
  }

  void DrawCubePoints(Vector3[] points)
  {
    Debug.DrawLine(points[0], points[1], Color.red);
    Debug.DrawLine(points[0], points[2], Color.red);
    Debug.DrawLine(points[0], points[4], Color.red);

    Debug.DrawLine(points[7], points[6], Color.red);
    Debug.DrawLine(points[7], points[5], Color.red);
    Debug.DrawLine(points[7], points[3], Color.red);

    Debug.DrawLine(points[1], points[3], Color.red);
    Debug.DrawLine(points[1], points[5], Color.red);

    Debug.DrawLine(points[2], points[3], Color.red);
    Debug.DrawLine(points[2], points[6], Color.red);

    Debug.DrawLine(points[4], points[5], Color.red);
    Debug.DrawLine(points[4], points[6], Color.red);
  }
}
