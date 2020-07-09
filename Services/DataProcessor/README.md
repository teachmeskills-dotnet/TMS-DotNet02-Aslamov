# DataProcessor Service

DataProcessor microservice is designed to process data received from telemetry sensors.

The following telemetry data is available:

- Temperature - for general patient health check.
- Acoustic - for recognizing heart diseaseds.

The health report is generating as a result of processing. Health reports are stored in Report.API microservice (transferring by EventBus).