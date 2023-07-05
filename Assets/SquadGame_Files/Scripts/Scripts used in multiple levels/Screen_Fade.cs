using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Fade : MonoBehaviour
{
        [Tooltip("Define the duration of screen fade.")]
        public float fadeTime = 5.0f;
        public float fastFadeTime = 2.0f;
        [Tooltip("Define the color of screen fade.")]
        public Color fadeColor = new Color(0.0f, 0.0f, 0.0f, 1f);
        public int renderQueue = 5000;
        private MeshRenderer fadeMeshRenderer;
        private MeshFilter fadeMeshFilter;
        private Material fadeMaterial = null;
        private bool isFading = false;
        private float currentAlpha;
        private float nowFadeAlpha;

        void Awake()
        {
            CreateFadeMesh();
            SetCurrentAlpha(0);
        }
        void Start()
        {
            StartCoroutine(ScreenFade(1, 0));
        }
        void OnDestroy()
        {
            DestroyFadeMesh();
        }

        private void CreateFadeMesh()
        {
            fadeMaterial = new Material(Shader.Find("PXR_SDK/PXR_Fade"));
            fadeMeshFilter = gameObject.AddComponent<MeshFilter>();
            fadeMeshRenderer = gameObject.AddComponent<MeshRenderer>();

            var mesh = new Mesh();
            fadeMeshFilter.mesh = mesh;

            Vector3[] vertices = new Vector3[4];

            float width = 2f;
            float height = 2f;
            float depth = 1f;

            vertices[0] = new Vector3(-width, -height, depth);
            vertices[1] = new Vector3(width, -height, depth);
            vertices[2] = new Vector3(-width, height, depth);
            vertices[3] = new Vector3(width, height, depth);

            mesh.vertices = vertices;

            int[] tri = new int[6];

            tri[0] = 0;
            tri[1] = 2;
            tri[2] = 1;

            tri[3] = 2;
            tri[4] = 3;
            tri[5] = 1;

            mesh.triangles = tri;

            Vector3[] normals = new Vector3[4];

            normals[0] = -Vector3.forward;
            normals[1] = -Vector3.forward;
            normals[2] = -Vector3.forward;
            normals[3] = -Vector3.forward;

            mesh.normals = normals;

            Vector2[] uv = new Vector2[4];

            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(1, 0);
            uv[2] = new Vector2(0, 1);
            uv[3] = new Vector2(1, 1);

            mesh.uv = uv;
        }

        private void DestroyFadeMesh()
        {
            if (fadeMeshRenderer != null)
                Destroy(fadeMeshRenderer);

            if (fadeMaterial != null)
                Destroy(fadeMaterial);

            if (fadeMeshFilter != null)
                Destroy(fadeMeshFilter);
        }

        public void SetCurrentAlpha(float alpha)
        {
            currentAlpha = alpha;
            SetMaterialAlpha();
        }

        IEnumerator ScreenFade(float startAlpha, float endAlpha)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < fadeTime)
            {
                elapsedTime += Time.deltaTime;
                nowFadeAlpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.Clamp01(elapsedTime / fadeTime));
                SetMaterialAlpha();
                yield return new WaitForEndOfFrame();
            }
        }
        IEnumerator FastScreenFade(float startAlpha, float endAlpha)
        {
            float elapsedTime = 0.0f;
            while (elapsedTime < fastFadeTime)
            {
                elapsedTime += Time.deltaTime;
                nowFadeAlpha = Mathf.Lerp(startAlpha, endAlpha, Mathf.Clamp01(elapsedTime / fadeTime));
                SetMaterialAlpha();
                yield return new WaitForEndOfFrame();
            }
        }

        private void SetMaterialAlpha()
        {
            Color color = fadeColor;
            color.a = Mathf.Max(currentAlpha, nowFadeAlpha);
            isFading = color.a > 0;
            if (fadeMaterial != null)
            {
                fadeMaterial.color = color;
                fadeMaterial.renderQueue = renderQueue;
                fadeMeshRenderer.material = fadeMaterial;
                fadeMeshRenderer.enabled = isFading;
            }
        }
        public void FadeOut()
        {
            StartCoroutine(ScreenFade(0, 1));
        }

        public void FadeIn()
        {
            StartCoroutine(ScreenFade(1, 0));
        }
        public void FastFadeOut()
        {
            StartCoroutine(FastScreenFade(0, 1));
        }

        public void FastFadeIn()
        {
            StartCoroutine(FastScreenFade(1, 0));
        }
    }
