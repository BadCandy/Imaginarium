using UnityEngine;
using System.Collections;

namespace Blocks
{
    [ExecuteInEditMode]
    public class Texture : MonoBehaviour
    {
        void OnEnable()
        {
            Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
            Vector2[] UVs = new Vector2[mesh.vertices.Length];

            // Top
            UVs[4] = new Vector2(0.5f, 0.5f);
            UVs[5] = new Vector2(1.0f, 0.5f);
            UVs[8] = new Vector2(0.5f, 1.0f);
            UVs[9] = new Vector2(1.0f, 1.0f);
            // Bottom
            UVs[12] = new Vector2(0.5f, 0.0f);
            UVs[13] = new Vector2(0.5f, 0.5f);
            UVs[14] = new Vector2(0.0f, 0.5f);
            UVs[15] = new Vector2(0.0f, 0.0f);

            // Front
            UVs[0] = new Vector2(0.0f, 0.5f);
            UVs[1] = new Vector2(0.5f, 0.5f);
            UVs[2] = new Vector2(0.0f, 1.0f);
            UVs[3] = new Vector2(0.5f, 1.0f);
            // Back
            UVs[6] = new Vector2(0.5f, 0.5f);
            UVs[7] = new Vector2(0.0f, 0.5f);
            UVs[10] = new Vector2(0.5f, 1.0f);
            UVs[11] = new Vector2(0.0f, 1.0f);
            // Left
            UVs[16] = new Vector2(0.0f, 0.5f);
            UVs[17] = new Vector2(0.0f, 1.0f);
            UVs[18] = new Vector2(0.5f, 1.0f);
            UVs[19] = new Vector2(0.5f, 0.5f);
            // Right
            UVs[20] = new Vector2(0.0f, 0.5f);
            UVs[21] = new Vector2(0.0f, 1.0f);
            UVs[22] = new Vector2(0.5f, 1.0f);
            UVs[23] = new Vector2(0.5f, 0.5f);
            mesh.uv = UVs;
        }
    }
}