$LastMigrationFile = Get-ChildItem ./Migrations | Sort-Object Name | Where-Object { $_.Name -notlike '*ModelSnapshot.cs' -and $_ -notlike '*.designer*' } | Select-Object -Skip 1 -Last 1

$MigrationName = $LastMigrationFile -replace '.cs', ''

Write-Host "Reseting database to migration: $MigrationName"
dotnet ef database update $MigrationName --startup-project ../FloorPlanner.Api --context FloorPlannerDbContext
