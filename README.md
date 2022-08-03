# Ensek.EnergyManager.Api

## Notes:
- Service/Domain layer is in the Api file for simplicity, normally on enterprise projects these would be separated
- Using an im-memory DB for easy replication, DB is reseeded each time on startup
- Validation exceptions are being returned as a BadRequest (400)
- Tests have been shortened for simplicity - these could be expanded upon
- No auth has been added but this would be a requirement in a live situation
- end points are specified using minimal api, normally for larger applications this would be separarted out
- brief says "You should not be able to load the same entry twice" - assumed this to mean that if there are two VALID meter readings then only the first should be processed
	Additionally, the account has multiple meter readings as this made more sense from a Domain point of view
- some of the meter readings are numerical but not in the full NNNNN format, assuming proceeding 0s
- assuming tht all meter readings should be positive
- exception handling can be built into the pipeline to handle the thrown exceptions and fail gracefully without the bootstrapping of methods
- have utilised a service layer, CQRS and DDDs
- I have exclude logging for brevity, but this is an important part of an api.  Specifically a correlation on lgging to follow a request's story
- could have used AutoMapper for DTO mapping
