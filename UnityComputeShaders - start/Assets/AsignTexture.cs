using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsignTexture : MonoBehaviour
{
    public ComputeShader shader;
    public int texResolution = 256;

    Renderer rend;
    RenderTexture outputTexture;
    int kernalHandle;


    // Start is called before the first frame update
    void Start()
    {
        outputTexture = new RenderTexture(texResolution, texResolution, 0);
        outputTexture.enableRandomWrite = true;
        outputTexture.Create();

        rend = GetComponent<Renderer>();
        rend.enabled = true;

        InitShader();
        
    }

    private void InitShader()
    {
        kernalHandle = shader.FindKernel("CSMain");
        shader.SetTexture(kernalHandle, "Result", outputTexture);
        rend.material.SetTexture("_MainTex", outputTexture);

        DispatchShader(texResolution / 16, texResolution / 16);
    }

    private void DispatchShader(int x, int y)
    {
        shader.Dispatch(kernalHandle, x, y, 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.U)) 
        { 
            DispatchShader(texResolution / 8, texResolution / 8);
        }
    }
}
