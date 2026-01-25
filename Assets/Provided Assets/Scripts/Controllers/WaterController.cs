using UnityEngine;

public class Water : MonoBehaviour
{
    public Vector2 waveSpeed = new Vector2(0.1f, 0.1f);

    public bool diffuse = true;
    public bool emissive = false;
    public bool normal = false;
    public bool opacity = false;

    private Renderer rend;
    private Material mat;

    void Start()
    {
        rend = GetComponent<Renderer>();
        if (rend != null)
            mat = rend.material; // creates instance (safe)
    }

    void Update()
    {
        if (mat == null) return;

        float dt = Time.deltaTime;

        if (diffuse)
        {
            Vector2 offset = mat.mainTextureOffset;
            offset += waveSpeed * dt;
            mat.mainTextureOffset = offset;
        }

        if (emissive)
        {
            Vector2 offset = mat.GetTextureOffset("_EmissionMap");
            offset += waveSpeed * dt;
            mat.SetTextureOffset("_EmissionMap", offset);
        }

        if (normal)
        {
            Vector2 offset = mat.GetTextureOffset("_BumpMap");
            offset += waveSpeed * dt;
            mat.SetTextureOffset("_BumpMap", offset);
        }

        if (opacity)
        {
            Vector2 offset = mat.GetTextureOffset("_MainTex"); // OR custom opacity map slot
            offset += waveSpeed * dt;
            mat.SetTextureOffset("_MainTex", offset);
        }
    }
}
