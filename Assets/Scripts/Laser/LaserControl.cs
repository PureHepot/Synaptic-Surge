using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    [SerializeField] private Color color = new Color(52/255, 191/255, 0);
    [SerializeField] private float colorIntensity = 1.5f;
    private float beamColorEnhance = 1f;

    [SerializeField] private float maxLength = 100;
    [SerializeField] private float thickness = 8;
    [SerializeField] private float noiseScale = 3;
    [SerializeField] private GameObject startVFX;
    [SerializeField] private GameObject endVFX;


    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponentInChildren<LineRenderer>();

        lineRenderer.material.color = color * colorIntensity;
        lineRenderer.material.SetFloat("_LaserThickness", thickness);
        lineRenderer.material.SetFloat("_LaserScale", noiseScale);

        ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem particle in particles)
        {
            Renderer r = particle.GetComponent<Renderer>();
            r.material.SetColor("_EmissionColor", color * (colorIntensity + beamColorEnhance));
        }
    }

    private void Start()
    {
        UpdateEndPosition();
        UpdateLaserPosition();
    }

    private void Update()
    {
        UpdateEndPosition();
        UpdateLaserPosition();
    }

    public void UpdatePosition(Vector2 startPosition, Vector2 direction)
    {
        direction = direction.normalized;
        transform.position = startPosition;
        float rotationZ = Mathf.Atan2(direction.y, direction.x);
        transform.rotation = Quaternion.Euler(0,0,rotationZ * Mathf.Rad2Deg);
    }

    public void UpdateLaserPosition()
    {
        lineRenderer.SetPosition(0,transform.position);
    }

    public void UpdateEndPosition()
    {
        Vector2 startPosition = transform.position;
        float rotationZ = transform.rotation.eulerAngles.z;
        rotationZ *= Mathf.Deg2Rad;

        Vector2 direction = new Vector2(Mathf.Cos(rotationZ), Mathf.Sin(rotationZ));

        RaycastHit2D hit = Physics2D.Raycast(startPosition, direction.normalized);

        float length = maxLength;
        float laserEndRotation = 180;

        if(hit)//通过反射检测获取两点之间的距离
        {
            length = (hit.point - startPosition).magnitude;
            laserEndRotation = Vector2.Angle(direction, hit.normal);//获取发射方向与法线的角度
                                                                    
            lineRenderer.SetPosition(1, hit.point);//设置激光落点
        }
        else 
        {
            lineRenderer.SetPosition(1, startPosition + length * direction);
        }

        Vector2 endPosition = startPosition + length * direction;
        startVFX.transform.position = startPosition;
        endVFX.transform.position = endPosition;
        endVFX.transform.rotation = Quaternion.Euler(0, 0, laserEndRotation);
    }
}
