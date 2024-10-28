using Recipes.Application.Interfaces.Services;

namespace Recipes.Application.Services;

public class FileService : IFileService
{
    public void SaveFile(byte[] file, string name, string folder)
    {
        Directory.CreateDirectory(Path.Combine("resources", folder));
        using var writer = new BinaryWriter(File.OpenWrite(Path.Combine("resources", folder, name)));
        writer.Write(file);
    }

    public void DeleteFile(string fileName, string folder)
    {
        File.Delete(Path.Combine("resources", folder, fileName));
    }

    public byte[] GetFile(string fileName, string folder)
    {
        using var reader = new BinaryReader(File.OpenRead(Path.Combine("resources", folder, fileName)));
        return reader.ReadBytes((int)reader.BaseStream.Length);
    }
}