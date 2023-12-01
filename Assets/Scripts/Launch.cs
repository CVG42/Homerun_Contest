using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Launch : MonoBehaviour
{
	[Header("Objetos")]
	public Rigidbody ball;
	public Transform target;
	public Transform launchDirection;
	public GameObject angle;
	public MagnusController magnus;
	public GameObject text;
	public TextMeshProUGUI scoreText;

	[Header("Variables de lanzamiento")]
	public int hits = 0;
	public float force = 0f;
	private bool canHit = true;
	private bool didOnce = false;
	private bool isLaunched = false;

	[Header("Variables fisicas")]
	public float h = 25;
	public float gravity = -18;
	public float magnusCoefficient = 4f;

	private float score = 0;

	public bool debugPath;

	void Start()
	{
		ball.useGravity = false;
	}

	void Update()
	{
		//Activar seleccion de angulo
		if (!didOnce)
		{
			didOnce = true;
			Invoke("SetAngle", 10f);
		}

		//Lanzar
		if (Input.GetKeyDown(KeyCode.E) && !magnus.magnusBarOn)
		{
			LaunchBall();
			text.SetActive(false);
		}

		//Incremento de la fuerza de lanzamiento
		if (canHit && Input.GetKeyDown(KeyCode.Space))
		{
			hits++;
			force += 2;

			if (Mathf.Approximately(force % 20f, 0f)) //Incrementa cada 20
			{
				force += 10f;
			}
		}

		if (debugPath)
		{
			DrawPath();
		}

		score = (int)(transform.position.x);
		scoreText.SetText(score + " m");
	}

    private void FixedUpdate()
    {
		//Efecto Magnus
		if(isLaunched)
        {
			Vector3 magnusForce = Vector3.Cross(ball.velocity, launchDirection.up) * magnus.currentCoefficient;
			ball.AddForce(magnusForce * Time.fixedDeltaTime);
        }
    }

    void SetAngle()
	{
		angle.SetActive(true);
		canHit = false;
	}

	void LaunchBall()
	{
		Physics.gravity = Vector3.up * gravity;
		ball.useGravity = true;
		ball.velocity = CalculateLaunchData().initialVelocity;
		isLaunched = true;
	}

	void TargetPosition()
    {
		target.position = new Vector3(force, 0f, 0f);
    }

	LaunchData CalculateLaunchData()
	{
		TargetPosition();
		float displacementY = target.position.y - ball.position.y;
		Vector3 displacementXZ = new Vector3(target.position.x - ball.position.x, 0, target.position.z - ball.position.z);
		float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity);
		Vector3 velocityY = launchDirection.up * Mathf.Sqrt(-2 * gravity * h);
		Vector3 velocityXZ = displacementXZ / time;

		Vector3 totalVelocity = velocityXZ + velocityY * -Mathf.Sign(gravity);

		return new LaunchData(totalVelocity, time);
	}

	void DrawPath()
	{
		LaunchData launchData = CalculateLaunchData();
		Vector3 previousDrawPoint = ball.position;

		int resolution = 30;
		for (int i = 1; i <= resolution; i++)
		{
			float simulationTime = i / (float)resolution * launchData.timeToTarget;
			Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * gravity * simulationTime * simulationTime / 2f;
			Vector3 drawPoint = ball.position + displacement;
			Debug.DrawLine(previousDrawPoint, drawPoint, Color.green);
			previousDrawPoint = drawPoint;
		}
	}

	struct LaunchData
	{
		public readonly Vector3 initialVelocity;
		public readonly float timeToTarget;

		public LaunchData(Vector3 initialVelocity, float timeToTarget)
		{
			this.initialVelocity = initialVelocity;
			this.timeToTarget = timeToTarget;
		}

	}
}
