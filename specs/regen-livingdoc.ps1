dotnet test ./src/BusHomework.Specs
livingdoc feature-folder src/BusHomework.Specs -t src/BusHomework.Specs/bin/Debug/net6.0/TestExecution.json --binding-assemblies src/BusHomework.Specs/bin/Debug/net6.0/BusHomework.Specs.dll
$livingDocPath = Resolve-Path ./LivingDoc.html
chrome $livingDocPath