using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Vector3 orgPosition;
    Vector3 orgScale;
    float moveSpeed;
    float randomScale;
    Vector3 moveDir;

    public enum State
    {
        Idle,
        Colided,
        Reset,
    }
    public State state;
    void Start()
    {
        state = State.Idle;
    }
    
    void Update()
    {
        if (state == State.Colided)
        {
            if (moveSpeed <= 1.0f)
            {
                state = State.Reset;
            }
            else
            {
                transform.localScale += new Vector3(Time.deltaTime* randomScale , Time.deltaTime* randomScale, Time.deltaTime * randomScale);
                transform.localPosition += moveDir * moveSpeed * Time.deltaTime;
                moveSpeed -= Time.deltaTime;
            }
        }
        else if (state == State.Reset)
        {
            if (Vector3.Distance(transform.position, orgPosition) > 0.0f)
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, orgPosition, moveSpeed * Time.deltaTime);
                transform.localScale = Vector3.Lerp(transform.localScale, orgScale, Time.deltaTime);
            }
            else
            {
                state = State.Idle;
                transform.localScale = orgScale;
            }
        }
    }  

    public void UpdateMeshColor(Color col)
    {
        orgPosition = transform.localPosition;

        //Process Color
        List<Color> colors = new List<Color>();
        var mf = GetComponent<MeshFilter>();
        Mesh mesh = Instantiate(mf.sharedMesh);
        foreach (var v in mesh.vertices)
        { 
            colors.Add(col); 
        }
        //Add new color
        for (int i = 0; i < colors.Count; i++)
        {
            //Changing to RBG sequence
            colors[i] = new Color(col.r,col.b,col.g);
        }
        mesh.SetColors(colors);
        mf.sharedMesh = mesh;

        //Update Scale with Red color
        transform.localScale *= col.r;
        orgScale = transform.localScale;
    }

    public void IsColided(Vector3 pos, Vector3 dir, float speed, float randScale)
    {
        if (Vector3.Distance(pos, transform.position) < 0.15f)
        {
            state = State.Colided;
            moveSpeed = speed;
            moveDir = dir;
            randomScale = randScale;
        }
    }   
}
