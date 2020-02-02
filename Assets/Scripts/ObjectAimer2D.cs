using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectAimer2D : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] bool aimAtMouse = true;
    [SerializeField] Transform target;
    [SerializeField] bool onlyWhenGrowing = true;

    public void Aim()
    {
        Vector2 direction = Vector2.zero;
        if (onlyWhenGrowing && !Input.GetButton("Grow"))
            return;

        if(aimAtMouse)
        {
            direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        }
        else
        {
            direction = target.transform.position - transform.position;
        }
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);
    }
}
