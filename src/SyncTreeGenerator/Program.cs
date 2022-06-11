using CppAst;




string syncTreeDefFile = @"D:\Perso\fivem-proxy\src\Protocol\FiveM\SyncTreeFive_Raw.txt";

List<string> files = new List<string>
{
    @"D:\Perso\fivem\code\components\citizen-server-impl\include\state\SyncTrees_Header.h",
    @"D:\Perso\fivem\code\components\citizen-server-impl\include\state\SyncTrees_Five.h"
};

var compilation = CppParser.ParseFiles(files);

// var compilation = CppParser.ParseFile(syncTreeDefFile);

// Print diagnostic messages
foreach (var message in compilation.Diagnostics.Messages)
    Console.WriteLine(message);

// Print All enums
foreach (var cppEnum in compilation.Enums)
    Console.WriteLine(cppEnum);

// Print All functions
foreach (var cppFunction in compilation.Functions)
    Console.WriteLine(cppFunction);

// Print All classes, structs
foreach (var cppClass in compilation.Classes)
    Console.WriteLine(cppClass);

// Print All typedefs
foreach (var cppTypedef in compilation.Typedefs)
    Console.WriteLine(cppTypedef);


