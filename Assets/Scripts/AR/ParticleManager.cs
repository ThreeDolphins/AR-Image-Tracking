using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using Color = UnityEngine.Color;

public class ParticleManager : MonoBehaviour
{
    List<Particle> particleList = new List<Particle>();
    public GameObject particlePrefab = null;
    public Texture2D refTexture = null;

    //Mouse Colision
    Vector3 lastPos;
    float lastPosTime;
    bool mouseDown;

    void Start()
    {
        particleList.Clear();
        InitParticles();

        //Update color
        for (int k = 0; k < particleList.Count; k++)
        {
            Vector3 pos = particleList[k].gameObject.transform.position;
            float x = (pos.x + 0.5f) * 256.0f;
            float y = (pos.z + 0.5f) * 256.0f;
            Color col = refTexture.GetPixel((int)x, (int)y);
            particleList[k].UpdateMeshColor(col);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!mouseDown)
            {
                Vector3 worToScr = Camera.main.WorldToScreenPoint(transform.position);
                lastPos = Input.mousePosition;
                lastPos.z = worToScr.z;
                lastPosTime = Time.realtimeSinceStartup;
            }
            mouseDown = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            mouseDown = false;
        }
        if (mouseDown)
        {
            Vector3 currPos = Input.mousePosition;
            Vector3 worToScr = Camera.main.WorldToScreenPoint(transform.position);
            currPos.z = worToScr.z;
            float distance = Vector3.Distance(lastPos, currPos);
            if (distance > 0.001f)
            {
                Vector3 dir = (currPos - lastPos).normalized;
                Vector3 cross = Vector3.Cross(dir, Vector3.down).normalized;
                //float speed = (distance / (Time.realtimeSinceStartup - lastPosTime)) * 0.001f;
                
                Vector3 scrToWorld = Camera.main.ScreenToWorldPoint(currPos);
                for (int k = 0; k < particleList.Count; k++)
                {
                    particleList[k].IsColided(scrToWorld, cross, 1.0f + Random.Range(-0.2f,0.2f), Random.Range(0.001f, 0.25f));
                }
            }
        }
    }

    private void OnDestroy()
    {
        
    }

    #region init_particles
    void InitParticles()
    {
        //Add one point at center
        AddNewParticle(Vector3.zero);

        //Create circular points
        float thetaScale = 1.0f/6.0f;
        float radius = 0.025f;
        for (int k = 0; k < 40; k++)
        {            
            float theta = 0f;
            int Size = (int)((1f / thetaScale) + 1f);
            for (int i = 0; i < Size; i++)
            {
                theta += (2.0f * Mathf.PI * thetaScale);
                float x = radius * Mathf.Cos(theta);
                float y = radius * Mathf.Sin(theta);
                AddNewParticle(new Vector3(x, 0, y));
            }
            thetaScale = 1.0f / ((k+2)*6);
            radius += 0.025f;
        }
        
    }
    void AddNewParticle(Vector3 pos)
    {       
        if (IsPointInside(pos))
        {
            GameObject particleObj = Instantiate(particlePrefab);
            particleObj.transform.parent = transform;
            particleObj.transform.position = pos;
            particleObj.name = "Particle_" + particleList.Count.ToString();

            Particle particle = particleObj.GetComponent<Particle>();
            particleList.Add(particle);
        }
    }
    bool IsPointInside(Vector3 pos)
    {
        if (pos.x < -0.5f || pos.x > 0.5f 
            || pos.z < -0.5f || pos.z > 0.5f)
        {
            return false;
        }
        return true;
    }
    #endregion
}
