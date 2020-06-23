using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DataProcessor.API.Common.Dictionaries;
using DataProcessor.API.Common.Enums;
using DataProcessor.API.Common.Interfaces;
using DataProcessor.API.DTO;
using EventBus.Contracts.DTO;

namespace DataProcessor.API.Services
{
    /// <summary>
    /// Service for processing sensor data and generate reports on patient health.
    /// </summary>
    public class DataProcessorService : IDataProcessorService
    {
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor of service for processing telemetry data.
        /// </summary>
        /// <param name="mapper">AutoMapper service.</param>
        public DataProcessorService(IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc/>
        public async Task<(ReportDTO report, bool success)> ProcessData(IDataDTO dataDTO)
        {
            if(dataDTO == null)
            {
                return (null, false);
            }

            ReportDTO healthReport;
            try
            {
                healthReport = await GetHealthReport(dataDTO);
            }
            catch
            {
                return (null, false);
            }
            return (healthReport, true);
        }
        
        // Randomly generate health status of the patient.
        private Task<ReportDTO> GetHealthReport(IDataDTO dataDTO)
        {
            var generator = new Random();

            var arrayOfHealthStatuses = Enum.GetValues(typeof(HealthStatus));
            var healthStatus = (HealthStatus)arrayOfHealthStatuses.GetValue(generator.Next(0, arrayOfHealthStatuses.Length));

            var accuracy = generator.Next(50, 101);

            string diseases = null;
            if (healthStatus == HealthStatus.Diseased)
            {
                switch(dataDTO.SensorDeviceType)
                {
                    case "Temperature":
                        diseases = RecognizeDiseasesByTemperature();
                        break;

                    case "Acoustic":
                        diseases = RecognizeDiseasesByAcoustic();
                        break;

                    default:
                        break;
                }
            }

            var healthReport = _mapper.Map<IDataDTO, ReportDTO>(dataDTO);

            healthReport.HealthStatus = healthStatus.ToString();
            healthReport.HealthDescription = DataProcessorDictionary.GetHealthDescription(healthStatus);
            healthReport.Diseases = diseases;
            healthReport.Accuracy = accuracy;

            // Imitation of processing time. 
            Thread.Sleep(1000); 

            return Task.FromResult(healthReport);
        }

        // Recognize disease by temperature (randomly).
        private string RecognizeDiseasesByTemperature()
        {
            var diseasesTotalCount = DataProcessorDictionary.GetCommonDiseasesCount();
            var generator = new Random();

            var diseaseId = generator.Next(diseasesTotalCount);
            var disease = DataProcessorDictionary.GetCommonDisease(diseaseId);

            return disease;
        }

        // Recognize heart diseases by acoustic signal (randomly).
        private string RecognizeDiseasesByAcoustic()
        {
            var diseasesTotalCount = DataProcessorDictionary.GetHeartDiseasesCount();
            var generator = new Random();

            var RECOGNIZED_DISEASES_MAX_COUNT = 3;
            var recognizedDiseasesCount = generator.Next(1, RECOGNIZED_DISEASES_MAX_COUNT);

            // Create list of recognized diseases.
            var diseasesList = new List<string>();
            var count = 0;
            while (count < recognizedDiseasesCount)
            {
                var id = generator.Next(diseasesTotalCount);
                var disease = DataProcessorDictionary.GetHeartDisease(id);

                if (!diseasesList.Contains(disease))
                {
                    diseasesList.Add(disease);
                    count++;
                }
            }

            // Conver diseases list to string format.
            string diseases = null;
            foreach (var disease in diseasesList)
            {
                diseases += $"{disease}, ";
            }

            return diseases;
        }
    }
}