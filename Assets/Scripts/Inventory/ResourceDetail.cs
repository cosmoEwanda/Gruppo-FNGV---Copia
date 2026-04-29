using UnityEngine;

[System.Serializable]
public class ResourceDetail
{
	public int CurrentAmount { get; private set; } = 0;
	public int MaxAmount { get; private set; }

	public ResourceDetail(int amount, int maxAmount)
	{
		MaxAmount = maxAmount;
		ChangeAmountBy(amount);
	}

	public int ChangeAmountBy(int delta)
	{
		int prevAmount = CurrentAmount;

		/* Prevent overlow. */
		long projectedAmount = (long)CurrentAmount + (long)delta;

		if (projectedAmount > MaxAmount)
		{
			CurrentAmount = MaxAmount;
		}
		else if (projectedAmount < 0)
		{
			CurrentAmount = 0;
		}
		else
		{
			CurrentAmount = (int)projectedAmount;
		}

		return CurrentAmount - prevAmount;
	}
}
