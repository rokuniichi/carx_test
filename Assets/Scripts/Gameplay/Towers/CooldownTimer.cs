public class CooldownTimer : TimerBehaviour, ITowerSystem
{    public void Init(TowerData towerData)
    {
        duration = towerData.Cooldown;
    }
}
