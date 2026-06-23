using System;

public static class PlayerOM
{
    public static Action<int> OnCoinCountChanged;

    public static void UpdateCoinCount(int coinCount)
    {
        OnCoinCountChanged?.Invoke(coinCount);
    }
}