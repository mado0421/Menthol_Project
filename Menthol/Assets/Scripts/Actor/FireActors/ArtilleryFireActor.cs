public class ArtilleryFireActor : FireActor
{
    private void Awake()
    {
        targetFinder = new ClosestTargetFinder();
    }

    public override void Act() { }

}
