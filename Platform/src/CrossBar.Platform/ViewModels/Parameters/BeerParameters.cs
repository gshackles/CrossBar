namespace CrossBar.Platform.ViewModels.Parameters
{
    public class BeerParameters : ParametersBase
    {
        public int BeerId { get; private set; }

        public BeerParameters(int beerId)
        {
            BeerId = beerId;
        }
    }
}