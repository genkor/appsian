# Development of an Automation Pipeline

The application will receive as input a commands file and execute them one by one in
sequence.
## Stage 1:
- Implement a list of basic commands where each command can have different
parameters:
  - File Copy
    - Parameters: Source File, Destination File
  - File Delete
    - Parameters: File Path
  - Query Folder Files
    - Parameter: Folder Path
    - Output: print to console list of all files in folders
  - Create Folder
    - Parameter: Folder Path , new Folder Name
  - Download File
    - Parameters: Source url, output File
  - Wait
    - Parameter: wait time in seconds
  - Conditional Count Rows File:
    - Parameter: source file, string to search in rows
    - Output: Count of rows in the file that contain the search string

- reading of the command file from a plain text file
## Stage 2:
- Execute a command to run a command file
## In step 3
- Implement reading of the script file as an XML file

## In stage 4
- Allow the **customer** to build **execute** own commands (plugins / extensions)
