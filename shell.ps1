# Run StyleCopAnalyzers (or equivalent)
dotnet format
# Build the code (release and debug mode)
dotnet build -c Release
dotnet build -c Debug

# Build the documentation
doxygen Doxyfile

# Run the unit test(s)
dotnet test
#dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=opencover
