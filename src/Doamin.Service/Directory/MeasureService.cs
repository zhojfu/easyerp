namespace Doamin.Service.Directory
{
    using System;
    using System.Collections.Generic;
    using Domain.Model.Directory;
    using Nop.Core.Domain.Directory;

    /// <summary>
    /// Measure dimension service
    /// </summary>
    public class MeasureService : IMeasureService
    {
        public void DeleteMeasureDimension(MeasureDimension measureDimension)
        {
            throw new NotImplementedException();
        }

        public MeasureDimension GetMeasureDimensionById(int measureDimensionId)
        {
            throw new NotImplementedException();
        }

        public MeasureDimension GetMeasureDimensionBySystemKeyword(string systemKeyword)
        {
            throw new NotImplementedException();
        }

        public IList<MeasureDimension> GetAllMeasureDimensions()
        {
            throw new NotImplementedException();
        }

        public void InsertMeasureDimension(MeasureDimension measure)
        {
            throw new NotImplementedException();
        }

        public void UpdateMeasureDimension(MeasureDimension measure)
        {
            throw new NotImplementedException();
        }

        public decimal ConvertDimension(
            decimal value,
            MeasureDimension sourceMeasureDimension,
            MeasureDimension targetMeasureDimension,
            bool round = true)
        {
            throw new NotImplementedException();
        }

        public decimal ConvertToPrimaryMeasureDimension(decimal value, MeasureDimension sourceMeasureDimension)
        {
            throw new NotImplementedException();
        }

        public decimal ConvertFromPrimaryMeasureDimension(decimal value, MeasureDimension targetMeasureDimension)
        {
            throw new NotImplementedException();
        }

        public void DeleteMeasureWeight(MeasureWeight measureWeight)
        {
            throw new NotImplementedException();
        }

        public MeasureWeight GetMeasureWeightById(int measureWeightId)
        {
            throw new NotImplementedException();
        }

        public MeasureWeight GetMeasureWeightBySystemKeyword(string systemKeyword)
        {
            throw new NotImplementedException();
        }

        public IList<MeasureWeight> GetAllMeasureWeights()
        {
            throw new NotImplementedException();
        }

        public void InsertMeasureWeight(MeasureWeight measure)
        {
            throw new NotImplementedException();
        }

        public void UpdateMeasureWeight(MeasureWeight measure)
        {
            throw new NotImplementedException();
        }

        public decimal ConvertWeight(
            decimal value,
            MeasureWeight sourceMeasureWeight,
            MeasureWeight targetMeasureWeight,
            bool round = true)
        {
            throw new NotImplementedException();
        }

        public decimal ConvertToPrimaryMeasureWeight(decimal value, MeasureWeight sourceMeasureWeight)
        {
            throw new NotImplementedException();
        }

        public decimal ConvertFromPrimaryMeasureWeight(decimal value, MeasureWeight targetMeasureWeight)
        {
            throw new NotImplementedException();
        }
    }
}