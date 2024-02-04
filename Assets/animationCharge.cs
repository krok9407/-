using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationCharge : MonoBehaviour
{
    public List<Texture> textureList;
    private MeshRenderer mesh;
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void OnEnable()
    {
        StartCoroutine(Charging());
    }
    private void OnDisable()
    {
        StopCoroutine(Charging());
    }
    int i = 0;
    IEnumerator Charging()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            mesh.material.mainTexture = textureList[i];
            i++;
            if (i >= textureList.Count) { i = 0; }
        }
    }
}
