

- Find if directory has .csproj
- Find if directory has .sln
    - If not, show warning message "no project found"
    - If yes, run `dotnet test -t` and check if exists: "The following Tests are available:"
        - If not exists, show warning message "no tests found"
        - If exists, continue with normal flow by taking the lines after the "The following Tests are available:" line.


- Test lines have the form of: {ProjectName}.{ClassName}.{TestName}

- Group by double dictionary, Project Key -> { Class Key -> Test Key }

- Execute them via `--filter` clause.
