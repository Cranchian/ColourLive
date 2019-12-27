using UnityEngine;
using UnityEngine.Events;
using System.Collections;

#if UNITY_EDITOR
#pragma warning disable 0414
#endif

public class RegionCapture : MonoBehaviour
{
    public bool UseCustomBackgroundMaterial;
    public Material CustomBackgroundMaterial;
    private Material RegionCaptureMaterial;

    public bool UseBackgroundPlane = true;
	public GameObject BackgroundPlane;

    public Texture VideoBackgroundTexure;

	public UnityEvent OutOfBounds;
	public UnityEvent ReturnInBounds;

	private bool PlaneIsOutOfBounds;
	private bool OutOfBounds_State;
	public bool Check_OutOfBounds;

	public bool HideFromARCamera;
	public bool FlipX, FlipY, Rotate90;

	public Camera ARCamera;

	Mesh RegionMesh;
	Vector3[] vertices;
	Vector2[] uvs, uvs_tmp;
	float KX, KY;

    private int tex_counter;

    private IEnumerator Start()
    {
        KX = 1.0f;
        KY = 1.0f;

        RegionCaptureMaterial = GetComponent<Renderer>().material;

        if (!UseCustomBackgroundMaterial) CustomBackgroundMaterial = null;

        yield return StartCoroutine(Initialize());
        StartCoroutine(Check_StateLoop());
    }

    private IEnumerator Initialize()
	{
        while (true)
        {
            string _notify = "";

            if (!ARCamera)
            {
                GameObject camObj;
                if ((camObj = GameObject.Find("ARCamera")) || (camObj = GameObject.FindWithTag("MainCamera")))
                {
                    ARCamera = camObj.GetComponent<Camera>();
                }
                else _notify = "ARCamera could not be found";
            }

            if (ARCamera)
            {
                if (HideFromARCamera)
                    ARCamera.cullingMask &= ~(1 << gameObject.layer);
            }

            if (!RegionMesh)
            {
                if (GetComponent<MeshFilter>())
                {
                    RegionMesh = GetComponent<MeshFilter>().mesh;
                    vertices = RegionMesh.vertices;
                    uvs = new Vector2[vertices.Length];
                    uvs_tmp = new Vector2[vertices.Length];
                }
                else _notify = "MeshFilter could not be found";
            }

            if (VideoBackgroundTexure)
            {
                GetComponent<MeshRenderer>().material.mainTexture = VideoBackgroundTexure;
            }

            if (UseBackgroundPlane)
            {
                if (!BackgroundPlane) BackgroundPlane = GameObject.Find("BackgroundPlane");

                if (BackgroundPlane && BackgroundPlane.GetComponent<MeshFilter>() && BackgroundPlane.GetComponent<MeshRenderer>())
                {
                    CustomBackgroundMaterial = BackgroundPlane.GetComponent<MeshRenderer>().sharedMaterial;
                }
                else _notify = "Searching BackgroundPlane";
            }

            tex_counter = 0;

            if (CustomBackgroundMaterial)
            {
                GetTextures();
            }
            Debug.Log("tex_counter outside = " + tex_counter);
            if (UseCustomBackgroundMaterial)
            {
                 if (!CustomBackgroundMaterial) _notify = "Searching CustomBackground material";
                 else if (tex_counter == 0) _notify = "CustomBackground material has no textures";
            }
            else
            {
                if (!CustomBackgroundMaterial) _notify = "Searching BackgroundPlane material";
                else if (tex_counter == 0) _notify = "BackgroundPlane material has no textures";
            }

            if (ARCamera && RegionMesh && (VideoBackgroundTexure || tex_counter > 0))
            {
                break;              /// Exit from init cycle
            }

            Debug.Log("Restart Initialize - " + _notify);
            yield return new WaitForEndOfFrame();
        }
	}

    public void GetTextures()
    {
        tex_counter = 0;

        if (CustomBackgroundMaterial && RegionCaptureMaterial)
        {
            string[] TextureName = CustomBackgroundMaterial.GetTexturePropertyNames();

            foreach (string name in TextureName)
            {
                if (
                    RegionCaptureMaterial.GetTexture(name) 
                    && RegionCaptureMaterial.GetTexture(name) != CustomBackgroundMaterial.GetTexture(name))
                    Destroy(RegionCaptureMaterial.GetTexture(name)
                    );

                if (CustomBackgroundMaterial.GetTexture(name))
                {
                    tex_counter++;
                    RegionCaptureMaterial.SetTexture(name, CustomBackgroundMaterial.GetTexture(name));
                }
            }
            Debug.Log("tex_counter inside = " + tex_counter);
            if (tex_counter > 0)
            {
                /// Check how many textures in background material
            }

            string[] Keywords = CustomBackgroundMaterial.shaderKeywords;

            foreach (string _keyword in Keywords)
            {
                if (CustomBackgroundMaterial.IsKeywordEnabled(_keyword))
                {
                    RegionCaptureMaterial.EnableKeyword(_keyword);
                }
            }
        }
    }

    private IEnumerator Check_StateLoop()			// 10 frames per second
	{
        while (true)
        {
            if (!UseCustomBackgroundMaterial && UseBackgroundPlane && BackgroundPlane)
                FindBackgroundPlaneBounds(BackgroundPlane);
            if (Check_OutOfBounds) On_OutOfBounds();
            yield return new WaitForSeconds(0.1f);
        }
	}

    private void Update()
    {
        if (ARCamera && RegionMesh)
            MeshUpdate();
    }

    private void MeshUpdate()
    {
        bool CheckComplete = false;

        for (int i = 0; i < uvs.Length; i++)
        {
            uvs[i] = ARCamera.WorldToViewportPoint(transform.TransformPoint(vertices[i]));

            uvs[i].x = (uvs[i].x - 0.5f) * KX + 0.5f;
            uvs[i].y = (uvs[i].y - 0.5f) * KY + 0.5f;

            if (FlipX)
                uvs[i].x = 1.0f - uvs[i].x;
            if (FlipY)
                uvs[i].y = 1.0f - uvs[i].y;

            if (Rotate90)
            {
                uvs_tmp[i].x = uvs[i].y;
                uvs_tmp[i].y = uvs[i].x;

                uvs[i].x = uvs_tmp[i].x;
                uvs[i].y = uvs_tmp[i].y;
            }

            if (Check_OutOfBounds && !CheckComplete)
            {
                if (uvs[i].x > 1.0f || uvs[i].y > 1.0f || uvs[i].x < 0.0f || uvs[i].y < 0.0f)
                {
                    PlaneIsOutOfBounds = true;
                    CheckComplete = true;
                }
                else PlaneIsOutOfBounds = false;
            }
        }
        RegionMesh.uv4 = uvs;
    }

	private void On_OutOfBounds()
	{
        if (OutOfBounds_State == PlaneIsOutOfBounds) return;
        OutOfBounds_State = PlaneIsOutOfBounds;

        if (enabled)
        {
            var selectEvent = OutOfBounds_State ? OutOfBounds : ReturnInBounds;
            if (selectEvent != null) selectEvent.Invoke();
        }
	}

	private void FindBackgroundPlaneBounds(GameObject plane)
	{
		Vector3[] vertices_tmp = plane.GetComponent<MeshFilter>().mesh.vertices;
		Vector2[] uvs_tmp = new Vector2[vertices_tmp.Length];

		float max_x_tmp = 0;
		float max_y_tmp = 0;
		float min_x_tmp = 0;
		float min_y_tmp = 0;

		for (int i = 0; i < uvs_tmp.Length; i++)
		{
			uvs_tmp [i] = ARCamera.WorldToViewportPoint (plane.transform.TransformPoint(vertices_tmp [i]));

			if (uvs_tmp [i].x > max_x_tmp) max_x_tmp = uvs_tmp [i].x;
			if (uvs_tmp [i].y > max_y_tmp) max_y_tmp = uvs_tmp [i].y;
			if (uvs_tmp [i].x < min_x_tmp) min_x_tmp = uvs_tmp [i].x;
			if (uvs_tmp [i].y < min_y_tmp) min_y_tmp = uvs_tmp [i].y;
		}

		KX = (1.0f / (((max_x_tmp - 1.0f) * 2.0f) + 1.0f));
		KY = (1.0f / (((max_y_tmp - 1.0f) * 2.0f) + 1.0f));
	}
}