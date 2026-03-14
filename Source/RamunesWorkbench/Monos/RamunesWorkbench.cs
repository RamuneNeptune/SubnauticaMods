﻿

namespace Ramune.RamunesWorkbench.Monos
{
    public class RamunesWorkbench : Workbench
    {
        public Renderer renderer;
        public Light light = new();
        public List<Coroutine> coroutines = new();

        public float duration = 3.50f;
        public float initial = 1.50f;
        public float result = -0.05f;
        public float elapsed = 0.00f;

        public bool isIncreasing = true;
        public bool fxSet = false;

        public Color initialColor = new(0.75f, 0.45f, 1.00f);


        public override void Awake()
        {
            base.Awake();

            if(Ramune.RamunesWorkbench.RamunesWorkbench.config.light)
                InitLight();

            fxLaserBeam?.ForEach(lb => 
            lb.GetComponent<MeshRenderer>().material.color = initialColor);

            CoroutineHost.StartCoroutine(WaitForFX());
        }


        public void Update()
        {
            if(renderer is null) return;

            if(!Ramune.RamunesWorkbench.RamunesWorkbench.config.animation)
                return;

            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / duration);

            var glowStrength = isIncreasing
                ? Mathf.Lerp(result, initial, t)
                : Mathf.Lerp(initial, result, t);

            //var currentColor = isIncreasing
            //    ? Color.Lerp(initialColor, targetColor, t)
            //    : Color.Lerp(targetColor, initialColor, t);

            renderer.material.SetFloat(ShaderPropertyID._GlowStrength, glowStrength);
            //renderer.material.SetColor(ShaderPropertyID._Color, currentColor);

            if(elapsed >= duration)
            {
                isIncreasing = !isIncreasing;
                elapsed = 0f;
            }
        }


        public void InitLight()
        {
            light = gameObject.EnsureComponent<Light>();
            light.transform.parent = gameObject.transform;
            light.intensity = 0f;
            light.range = 1.7f;
            light.color = initialColor;
            light.enabled = true;
        }


        public override void OnOpenedChanged(bool opened)
        {
            base.OnOpenedChanged(opened);

            if(animator != null) animator.SetBool(AnimatorHashID.open_workbench, opened);
            FMODUWE.PlayOneShot(opened ? openSound : closeSound, soundOrigin.position, 1f);

            if(Ramune.RamunesWorkbench.RamunesWorkbench.config.light is false)
                return;

            if(opened)
            {
                coroutines.Add(StartCoroutine(FadeLight(light.intensity, 10f, 1.7f)));
                //gameObject.EnsureComponent<DontLook>();
            }
            else
            {
                if(coroutines.Count > 0)
                {
                    Logfile.Warning(">> If it says \"Coroutine continue failure\", don't worry about it.");
                    coroutines.ForEach(CoroutineHost.StopCoroutine);
                }
                StartCoroutine(FadeLight(light.intensity, 0f, 0.4f));
                //DestroyImmediate(gameObject.GetComponent<DontLook>());
            }
        }


        public IEnumerator FadeLight(float num1, float num2, float duration)
        {
            float elapsedTime = 0f;

            while(elapsedTime < duration)
            {
                light.intensity = Mathf.Lerp(num1, num2, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            light.intensity = num2;
        }


        public IEnumerator WaitForFX()
        {
            while(fxSparksInstances is null)
                yield return null;
            
            SetColorsForFX();
            yield break;
        }


        public void SetColorsForFX()
        {            
            if(fxSparksInstances is null)
                return;
            
            foreach(var gameobject in fxSparksInstances)
            {
                if(gameobject is null)
                    break;
                
                var singleParticle = gameobject.GetComponent<ParticleSystem>();

                if(singleParticle is not null)
                    singleParticle.startColor = initialColor;

                foreach(var particle in gameobject.GetComponentsInChildren<ParticleSystem>(true))
                {
                    if(particle is null)
                        break;
                    
                    particle.startColor = initialColor;
                }
            }
        }
    }
}