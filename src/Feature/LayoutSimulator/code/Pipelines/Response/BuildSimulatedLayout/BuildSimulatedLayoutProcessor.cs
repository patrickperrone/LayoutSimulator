namespace Sitecore.Feature.LayoutSimulator.Pipelines.Response.BuildSimulatedLayout
{
    public abstract class BuildSimulatedLayoutProcessor
    {
        protected BuildSimulatedLayoutProcessor()
        { }
        public abstract void Process(BuildSimulatedLayoutArgs args);
    }
}
