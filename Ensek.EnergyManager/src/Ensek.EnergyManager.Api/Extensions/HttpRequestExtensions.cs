namespace Ensek.EnergyManager.Api;

public static class HttpRequestExtensions
{
    /// <summary>
    /// Validates the sent formfile and extracts from the request if nessecary
    /// </summary>
    /// <param name="request"></param>
    /// <param name="formFile"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    public static bool TryExtractFormFile(this HttpRequest request, out IFormFile? formFile)
    {
        formFile = null;

        if (!request.HasFormContentType)
        {
            return false;
        }

        formFile ??= request.Form.Files.Count > 0 ? request.Form.Files[0] : null;
        if (formFile == null)
        {
            return false;
        }

        return true;
    }
}
