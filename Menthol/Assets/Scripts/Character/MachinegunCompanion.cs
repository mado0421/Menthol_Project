public class MachinegunCompanion : Companion
{
    private void Awake()
    {
        hp = 1;
        actor = gameObject.AddComponent<MachinegunCompanionActor>();
    }
}
