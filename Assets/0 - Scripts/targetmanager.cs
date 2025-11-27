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
        
        public Material outlineShaderMaterial;
        
        // NEU: Nur für mittleren Orb
        private Material originalMiddleMaterial;

        void Start()
        {
                // Original-Material vom mittleren Orb speichern
                originalMiddleMaterial = orb_middle.GetComponentInChildren<Renderer>().material;
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
                ShowTargetOrb(side);
        }

        public void MakeTransparent(GameObject orb)
        {
                if (orb != orb_middle) return;
                
                var renderer = orb.GetComponentInChildren<Renderer>();
                if (renderer == null) return;

                if (outlineShaderMaterial != null)
                {
                        renderer.material = new Material(outlineShaderMaterial);
                        renderer.material.SetTexture("_MainTex", originalTexture);
                        renderer.material.SetColor("_OutlineColor", Color.red);
                        renderer.material.SetFloat("_OutlineWidth", 0.05f);
                        renderer.material.SetFloat("_EmissionStrength", 3f);
                }
        }

        public void MakeOpaque(GameObject orb)
        {
                if (orb != orb_middle) return;
                
                var renderer = orb.GetComponentInChildren<Renderer>();
                if (renderer == null) return;

                renderer.material = originalMiddleMaterial;
        }


        public void HideButKeepMiddleTransparent()
        {
                MakeTransparent(orb_middle);
                orb_left.SetActive(false);
                orb_right.SetActive(false);
        }
}