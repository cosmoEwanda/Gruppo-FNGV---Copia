using UnityEngine;

public class TimeManager : MonoBehaviour
{
	[Range(0, 59)][SerializeField] private int minutes;
	[Range(0, 23)][SerializeField] private int hours;
	private int days;
	public int Minutes
	{
		get => minutes;
		set
		{
			minutes = value;
			HandleTimeOverflow();
			UpdateEnvironment();
		}
	}

	public int Hours
	{
		get => hours;
		set
		{
			hours = value;
			HandleTimeOverflow();
			UpdateEnvironment();
		}
	}

	public int Days { get => days; set => days = value; }

	[SerializeField] private float timeStep = 0.2f;
	[SerializeField] private Light directionalLight;
	[SerializeField] private AnimationCurve exposureCurve;
	[SerializeField] private Gradient lightColorGradient;

	[SerializeField] private bool testMode = false;

	private float timeSeconds;
	private Material skyboxMaterial;

	void Start()
	{
		if (RenderSettings.skybox != null)
		{
			skyboxMaterial = new Material(RenderSettings.skybox);	
			RenderSettings.skybox = skyboxMaterial;
		}
		UpdateEnvironment();
	}

	void Update()
	{
		timeSeconds += Time.deltaTime;

		if (timeSeconds >= timeStep)
		{
			Minutes += 1;
			timeSeconds = 0;
		}

		UpdateEnvironment();
	}

	private void HandleTimeOverflow()
	{

		if (minutes >= 60)
		{
			int extraHours = minutes / 60;
			minutes %= 60;
			hours += extraHours;

		}

		if (hours >= 24)
		{
			days += hours / 24;
			hours %= 24;
		}

		if (testMode)
		{
			Debug.Log($"Time: {hours:D2}:{minutes:D2} | Days: {days}");
		}

	}

	public void UpdateEnvironment()
	{
		if (directionalLight == null || skyboxMaterial == null) return;


		float minuteProgress = Application.isPlaying ? (timeSeconds / timeStep) : 0;
		float totalTimeDecimal = hours + (minutes + minuteProgress) / 60f;

		float t = totalTimeDecimal / 24f;
		float sunRotation = (totalTimeDecimal * 15f) - 90f;
		directionalLight.transform.rotation = Quaternion.Euler(sunRotation, 170f, 0f);

		Color currentColor = lightColorGradient.Evaluate(t);
		directionalLight.color = currentColor;

		if (skyboxMaterial != null)
		{
			skyboxMaterial.SetColor("_Tint", currentColor);
			skyboxMaterial.SetFloat("_Exposure", exposureCurve.Evaluate(t));
			skyboxMaterial.SetFloat("_Rotation", t * 360f);
		}

		if (Application.isPlaying && Time.frameCount % 20 == 0)
		{
			DynamicGI.UpdateEnvironment();
		}
	}

	private void OnDestroy()
	{
		if (skyboxMaterial != null)
		{
			Destroy(skyboxMaterial);
		}
	}

	private void OnValidate()
	{
		HandleTimeOverflow();
		UpdateEnvironment();
	}
}
