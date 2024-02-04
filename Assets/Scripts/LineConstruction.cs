using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[ExecuteInEditMode]
[RequireComponent(typeof(LineRenderer))]
public class LineConstruction : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] GameObject prefabLine;
    GameObject go;
    LineRenderer lr;
    Transform posOne;
    Transform posTwo;
    string tagLastTarget = "";
    bool click = false;
    bool draw = false;
    bool variant1 = false;
    public Dictionary<string, GameObject> lines = new Dictionary<string, GameObject>();	
    public List<string> lineCoordinates = new List<string>();
    [SerializeField] Material[] mat;
    int color = 0;

    Line line;
    Clemma clemma;

    void Update()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 15))
        {
            if (Input.GetMouseButtonDown(0) && (hit.collider.tag == "point" || hit.collider.tag == "clemma"))
            {
                if (!click && hit.collider.tag == "point")
                {
                    if (hit.transform.parent.gameObject.name == "Lines1"){
                        variant1 = true;
                    }
                    else{
                        variant1 = false;
                    }

                    line = hit.collider.gameObject.GetComponent<Line>();

                    if (!line.isSet)
                    {
                        posOne = hit.transform;
                        go = Instantiate(prefabLine);

                        lr = go.GetComponent<LineRenderer>();
                        lr.SetWidth(0.04f, 0.04f);
                        lr.SetPosition(0, posOne.position);

                        tagLastTarget = hit.collider.tag;

                        go.GetComponent<Renderer>().material = hit.collider.gameObject.GetComponent<MeshRenderer>().material;

                        click = !click;
                    }
                }
                else if (!click && hit.collider.tag == "clemma")
                {
                    clemma = hit.collider.gameObject.GetComponent<Clemma>();
                    if (clemma.isOpen){
                        clemma.isOpen = false;
                        Vector3 rotate = hit.transform.localEulerAngles;
                        rotate.z = 0;
                        hit.transform.localRotation = Quaternion.Euler(rotate);
                    }
                    else{
                        clemma.isOpen = true;
                        Vector3 rotate = hit.transform.localEulerAngles;
                        rotate.z = -60;
                        hit.transform.localRotation = Quaternion.Euler(rotate);
                    }
                }
                else if (click && hit.transform.position != posOne.position && hit.collider.tag != tagLastTarget)
                {
                    clemma = hit.collider.gameObject.GetComponent<Clemma>();
                    if (!clemma.isSet && clemma.isOpen)
                    {
                        posTwo = hit.transform;
                        draw = true;
                        line.isSet = true;
                        clemma.isSet = true;

                        if (go.GetComponent<Renderer>().material.name.Contains("синий"))
                        {
                            clemma.typeWire = "blue";
                        }
                    }
                    else
                    {
                        Destroy(go);
                    }
                    click = !click;
                }
                else if (click && hit.collider.tag == tagLastTarget)
                {
                    Destroy(go);
                    click = !click;
                }
            }

        }
        if (click)
        {
            Vector3 mousePos2D = Input.mousePosition;
            // print("inputMousePos" + mousePos2D);
            mousePos2D.z = -Camera.main.transform.position.z;
            
            Vector3 mousePos3D = Camera.main.ScreenToWorldPoint( mousePos2D );
            // print("pos1    " + posOne);
            mousePos3D.z = posOne.position.z;
            lr.SetPosition(1, mousePos3D);
        }
        if (draw)
        {
            if (variant1)
            {
                if (posOne.position.x == line.posCor){
                    lr.positionCount = 3;
                    Vector3 pos1 = new Vector3(line.posCor, posTwo.position.y, posOne.position.z);
                    lr.SetPosition(1, pos1);
                    Vector3 pos3 = new Vector3(posTwo.position.x, posTwo.position.y, posOne.position.z);
                    lr.SetPosition(2, pos3);
                }
                else{
                    lr.positionCount = 4;
                    Vector3 pos1 = new Vector3(line.posCor, posOne.position.y, posOne.position.z);
                    lr.SetPosition(1, pos1);
                    Vector3 pos2 = new Vector3(line.posCor, posTwo.position.y, posOne.position.z);
                    lr.SetPosition(2, pos2);
                    Vector3 pos3 = new Vector3(posTwo.position.x, posTwo.position.y, posOne.position.z);
                    lr.SetPosition(3, pos3);
                }
            }
            else
            {
                if (posOne.position.y == line.posCor){
                    lr.positionCount = 3;
                    Vector3 pos1 = new Vector3(posTwo.position.x, line.posCor, posOne.position.z);
                    lr.SetPosition(1, pos1);
                    Vector3 pos3 = new Vector3(posTwo.position.x, posTwo.position.y, posOne.position.z);
                    lr.SetPosition(2, pos3);
                }
                else{
                    lr.positionCount = 4;
                    Vector3 pos1 = new Vector3(posOne.position.x, line.posCor, posOne.position.z);
                    lr.SetPosition(1, pos1);
                    Vector3 pos2 = new Vector3(posTwo.position.x, line.posCor, posOne.position.z);
                    lr.SetPosition(2, pos2);
                    Vector3 pos3 = new Vector3(posTwo.position.x, posTwo.position.y, posOne.position.z);
                    lr.SetPosition(3, pos3);
                }
            }

            string ps = posOne.position.x.ToString() + posOne.position.y.ToString() + posTwo.position.x.ToString() + posTwo.position.y.ToString();
            string ps2 = posTwo.position.x.ToString() + posTwo.position.y.ToString() + posOne.position.x.ToString() + posOne.position.y.ToString();
            //print(ps);
            //print(ps2);
            // lineCoordinates.Add(ps);
            // lines.Add(ps, go);
            draw = false;

            // Lerp(posOne, posTwo, PointsCount);
            // lr.positionCount = PointsCount;
            // lr.SetPositions(points.ToArray());
        }
    }


    // private void Lerp(Transform Start, Transform End, int count)
    // {
    //     var rigidity = Mathf.Clamp01(Rigidity);
    //     var L = (Start.position - End.position);
    //     var D = L.magnitude + 0.001f;
    //     var DD = Mathf.Max(D, Length);
    //     var P0 = Start.position;
    //     var P1 = Start.position + Start.forward * DD * rigidity / 2;
    //     var P2 = End.position - End.forward * DD * rigidity / 2;
    //     var P3 = End.position;
    //     var overLength = Mathf.Max(0, Length - D);
 
    //     for (int i = 0; i < count; i++)
    //     {
    //         var t = (float)i / (count - 1);
 
    //         //Cubic Bezier
    //         var P01 = Vector3.Lerp(P0, P1, t);
    //         var P12 = Vector3.Lerp(P1, P2, t);
    //         var P23 = Vector3.Lerp(P2, P3, t);
    //         var P012 = Vector3.Lerp(P01, P12, t);
    //         var P123 = Vector3.Lerp(P12, P23, t);
    //         var P1234 = Vector3.Lerp(P012, P123, t);
 
    //         //add gravity
    //         var t1 = (t - 0.5f) * 2;//linear -1 : 0 : 1
    //         var t2 = t1 * t1;//parabola 1 : 0 : 1
    //         var t3 = 1 - t2;//parabola 0 : 1 : 0
    //         var gravity = Vector3.up * (t2 * t2 - 1) * Weight * t3 * (1 - rigidity) * overLength;
 
    //         P1234 += gravity;
 
    //         points[i] = P1234;
    //     }
    // }

}
