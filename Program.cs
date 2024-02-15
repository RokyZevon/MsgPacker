using MessagePack;

if (args.Length != 1)
{
    Console.WriteLine("Usage: MsgPacker <filepath>");
    Environment.Exit(1);
}

var files = Directory.GetFiles(
    Path.GetDirectoryName(args[0]) ?? Environment.CurrentDirectory,
    Path.GetFileName(args[0]));

if (files.Length == 0)
{
    Console.WriteLine("No files found");
    Environment.Exit(1);
}

var outputDir = Path.Combine(Environment.CurrentDirectory, "output");
if (!Directory.Exists(outputDir)) Directory.CreateDirectory(outputDir);

foreach (var file in files)
{
    var (name, ext) = (Path.GetFileNameWithoutExtension(file), Path.GetExtension(file).ToLower());
    string outputPath;

    switch (ext)
    {
        case ".msg":
            outputPath = Path.Combine(outputDir, $"{name}.json");
            var bytes = await File.ReadAllBytesAsync(file);
            await File.WriteAllTextAsync(outputPath, MessagePackSerializer.ConvertToJson(bytes));
            Console.WriteLine($"Converted {name}.msg to {outputPath} ");
            break;

        case ".json":
            outputPath = Path.Combine(outputDir, $"{name}.msg");
            var text = await File.ReadAllTextAsync(file);
            File.WriteAllBytes(outputPath, MessagePackSerializer.ConvertFromJson(text));
            Console.WriteLine($"Converted {name}.json to {outputPath} ");
            break;

        default:
            Console.WriteLine("Invalid file extension");
            Environment.Exit(1);
            break;
    }
}

Console.WriteLine("All files have been converted successfully.");
