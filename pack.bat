dotnet pack -o ./out --include-symbols --include-source
dotnet nuget push out\*.symbols* -s nuget.org -k 