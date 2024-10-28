namespace Recipes.Application.Interfaces.Services;

public interface IFileService
{
    void SaveFile(byte[] file, string fileName, string folder);
    void DeleteFile(string fileName, string folder);
    byte[] GetFile(string fileName, string folder);
}