#!/bin/bash
dotnet test ./src/BusHomework.Specs
livingdoc feature-folder src/BusHomework.Specs -t src/BusHomework.Specs/bin/Debug/net6.0/TestExecution.json --binding-assemblies src/BusHomework.Specs/bin/Debug/net6.0/BusHomework.Specs.dll
echo "==========="
echo "Open the LivingDoc.html file in your favorite browser to inspect the results!"
echo "==========="
