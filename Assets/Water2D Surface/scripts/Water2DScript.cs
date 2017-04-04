// 2016 - Damien Mayance (@Valryon)
// Source: https://github.com/valryon/water2d-unity/

using UnityEngine;
using System;


/// <summary>
///     Water surface script (update the shader properties).
/// </summary>
public class Water2DScript : MonoBehaviour {
    private Material mat;

    [Tooltip("By default use a mesh, other times use a sprite?")]
    public bool material = true;

    private Renderer rend;
    private MeshFilter mf;
    public Vector2 speed = new Vector2(0.01f, 0f);

    [Tooltip("Drag the sprite you'd like a mesh off for the water terrain")]
    public Sprite theSprite;
    public bool generateNewMesh = false;

    void Awake() {

        //Convert the sprite to a mesh
        mf = this.GetComponent<MeshFilter>();
        mf.sharedMesh = SpriteToMesh(theSprite);

//Only run this in the editor since this piece uses Editor only pieces        
#if UNITY_EDITOR
        if (generateNewMesh) {
            var path = "Assets/Water2D Surface/demo/meshes/";
            var filename = string.Format("WaterHexMesh.asset");
            var fullpath = string.Format("{0}{1}", path, filename);
            UnityEditor.AssetDatabase.CreateAsset(SpriteToMesh(theSprite), fullpath);
        }
#endif


        rend = GetComponent<Renderer>();
        mat = rend.material;
    }

    private void LateUpdate() {
        var scroll = Time.deltaTime * speed;
        mat.mainTextureOffset += scroll;

    }

    private static Mesh SpriteToMesh(Sprite sprite) {
        Mesh mesh = new Mesh();
        mesh.vertices = Array.ConvertAll(sprite.vertices, i => (Vector3)i);
        mesh.uv = sprite.uv;
        mesh.triangles = Array.ConvertAll(sprite.triangles, i => (int)i);

        return mesh;
    }


}