
Test run

dotnet test

dotnet test --logger "console;verbosity=detailed"

dotnet test --logger "trx"

dotnet test --logger "html"

### Run the specific file/files

dotnet test --filter "Name=FileNameDoesExist"

dotnet test --filter "Name~FileName"

### Filter by Attribute

dotnet test --filter "Priority=1" 
 