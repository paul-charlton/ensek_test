namespace Ensek.EnergyManager.Api;

internal static class FileValidationExtensions
{

    private const long _fileSizeLimit = 5242880; // 5MB;

    // get this from config
    private static readonly Dictionary<string, List<string>> _allowedFileTypes = new()
    {
        ["text/csv"] = new List<string> { ".csv" },
    };

    /// <summary>
    /// Validates the file against the file type, checks the sufffix and the file size
    /// </summary>
    /// <param name="file"></param>
    /// <param name="fileType"></param>
    /// <returns></returns>
    public static bool TryValidateFile(this IFormFile file, string expectedFileType, out string? error)
    {

        error = ValidateFileSize(file);
        if (!string.IsNullOrEmpty(error))
            return false;

        error = ValidateFileExtension(file.FileName, file.ContentType, expectedFileType);
        if (!string.IsNullOrEmpty(error))
            return false;

        return true;
    }


    private static string? ValidateFileSize(IFormFile file)
    {
        return file.Length > _fileSizeLimit ? "File is too large" : null;
    }

    private static string? ValidateFileExtension(string filename, string contentType, string expectedFileType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            return $"Invalid content-type '{contentType}'";

        var ext = Path.GetExtension(filename);
        if (!_allowedFileTypes.TryGetValue(contentType, out var permittedExtensions) || string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext) || !string.Equals(ext, expectedFileType, StringComparison.OrdinalIgnoreCase))
            return $"Invalid file extension '{ext}'";

        return null;
    }
}
