namespace Domain.Model.Directory
{
    using EasyErp.Core.Configuration;

    public class MeasureSettings : ISettings
    {
        public int BaseDimensionId { get; set; }

        public int BaseWeightId { get; set; }
    }
}