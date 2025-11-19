using UnityEngine;

public class targetmanager : MonoBehaviour
{
        public GameObject orb_middle;
        public GameObject orb_right;
        public GameObject orb_left;

        private GameObject activeOrbObj;
        private string activeOrbStr;

        public string getActiveOrbString() => activeOrbStr;
        public GameObject getActiveOrbObject() => activeOrbObj;
        public Texture2D blueTexture;
        public Texture2D originalTexture;

        // private trialmanager trialManager;

        void Start()
        {
        //   trialManager = GetComponent<trialmanager>();
        //   trialManager.OnTrialStarted += HandleTrialStart;      
        }

        public void ShowMiddleOrb()
        {
                orb_middle.SetActive(true);
                activeOrbObj = orb_middle;
                activeOrbStr = "middle";
        }

        public void ShowTargetOrb(string side)
        {
                activeOrbStr = side;
                if (side == "right")
                {
                        orb_right.SetActive(true);
                        activeOrbObj = orb_right;
                }
                else if (side == "left")
                {
                        orb_left.SetActive(true);
                        activeOrbObj = orb_left;
                }
        }

        public void HideAllOrbs()
        {
                orb_middle.SetActive(false);
                orb_right.SetActive(false);
                orb_left.SetActive(false);
        }

        void HandleTrialStart(string side)
        {
                // HideMiddleOrb();
                ShowTargetOrb(side);
        }

        public void MakeTransparent(GameObject orb)
        {
                var renderer = orb.GetComponentInChildren<Renderer>();
                if (renderer == null) return;

                var mat = renderer.material;

                // Shader transparent machen (wie zuvor)
                mat.SetFloat("_Mode", 3);
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                mat.SetInt("_ZWrite", 0);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.EnableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 3000;

                mat.SetTexture("_MainTex", blueTexture);
                Color c = new Color(0f, 0f, 1f, 0.5f); // RGB Blau, Alpha 0.5
                mat.color = c;
        }


        public void MakeOpaque(GameObject orb)
        {
                var renderer = orb.GetComponentInChildren<Renderer>();
                if (renderer == null) return;

                var mat = renderer.material;

                // Standard Shader: Opaque zurücksetzen
                mat.SetFloat("_Mode", 0); // Opaque
                mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
                mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
                mat.SetInt("_ZWrite", 1);
                mat.DisableKeyword("_ALPHATEST_ON");
                mat.DisableKeyword("_ALPHABLEND_ON");
                mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                mat.renderQueue = 2000;

                mat.SetTexture("_MainTex", originalTexture);

        }


        public void HideButKeepMiddleTransparent()
        {
                MakeTransparent(orb_middle);
                orb_left.SetActive(false);
                orb_right.SetActive(false);
        }





}