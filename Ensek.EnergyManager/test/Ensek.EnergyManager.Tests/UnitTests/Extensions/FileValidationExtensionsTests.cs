using Microsoft.AspNetCore.Http;

namespace Ensek.EnergyManager.Tests.UnitTests.Extensions;
public class FileValidationExtensionsTests
{
    private const string _csvFileType = ".csv";

    private FormFileFake ValidFormFile()
    {
        return new FormFileFake()
        {
            ContentType = "text/csv",
            Length = 6,
            Name = "test",
            FileName = "test.csv",
        };
    }

    [Fact]
    public void TryValidateFile_WithLargeFileSize_ReturnsFalse()
    {
        // SETUP
        var invalidFile = ValidFormFile();
        invalidFile.Length = int.MaxValue;

        // TEST
        var valid = invalidFile.TryValidateFile(_csvFileType, out _);

        // ASSERT
        valid.Should().BeFalse();
    }

    [Fact]
    public void TryValidateFile_WithInvalidFileType_ReturnsFalse()
    {
        // SETUP
        var invalidFile = ValidFormFile();
        invalidFile.FileName = "test.txt";

        // TEST
        var valid = invalidFile.TryValidateFile(_csvFileType, out _);

        // ASSERT
        valid.Should().BeFalse();
    }
    [Fact]
    public void TryValidateFile_WithInvalidMimeType_ReturnsFalse()
    {
        // SETUP
        var invalidFile = ValidFormFile();
        invalidFile.ContentType = "application\\json";

        // TEST
        var valid = invalidFile.TryValidateFile(_csvFileType, out _);

        // ASSERT
        valid.Should().BeFalse();
    }

    [Fact]
    public void TryValidateFile_WithValidSizeMimeTypeAndFileType_ReturnsTrue()
    {
        // SETUP

        // TEST
        var valid = ValidFormFile().TryValidateFile(_csvFileType, out _);

        // ASSERT
        valid.Should().BeTrue();
    }

    private class FormFileFake : IFormFile
    {
        public string ContentType { get; set; } = "";
        public string ContentDisposition { get; set; } = "";
        public IHeaderDictionary Headers { get; set; } = new HeaderDictionary();

        public long Length { get; set; }
        public string Name { get; set; } = "";
        public string FileName { get; set; } = "";

        public void CopyTo(Stream target)
        {

        }

        public Task CopyToAsync(Stream target, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Stream OpenReadStream()
        {
            throw new NotImplementedException();
        }
    }
}
