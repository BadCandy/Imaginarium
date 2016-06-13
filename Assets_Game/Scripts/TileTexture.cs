using UnityEngine;
using System.Collections;

//[ExecuteInEditMode]
public class TileTexture : MonoBehaviour
{
	// Use this for initialization
	void Update()
    {
        GetComponent<MeshRenderer>().material.mainTextureScale = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2);
        //GetComponent<MeshRenderer>().sharedMaterial.mainTextureScale = new Vector2(transform.localScale.x * 2, transform.localScale.y * 2);
        //GetComponent<MeshRenderer>().sharedMaterial.SetTextureScale("_NormalMap", new Vector2(transform.localScale.x * 2, transform.localScale.y * 2));
	}
}
