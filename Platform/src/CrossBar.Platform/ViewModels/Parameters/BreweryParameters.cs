namespace CrossBar.Platform.ViewModels.Parameters
{
    public class BreweryParameters : ParametersBase
    {
        public int BreweryId { get; private set; }

        public BreweryParameters(int breweryId)
        {
            BreweryId = breweryId;
        }
    }
}