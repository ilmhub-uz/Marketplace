namespace Marketplace.Services.Organizations.FileServices;

public class FileService
{
	private const string Wwwroot = "wwwroot";

	private static void CheckDirectory(string folder)
	{
		if (!Directory.Exists(folder))
			Directory.CreateDirectory(folder);
	}

	public static async Task<string> SaveOrganizationLogo(IFormFile file)
	{
		return await SaveFile(file, "OrganizationLogos");
	}

	private static async Task<string> SaveFile(IFormFile file, string folder)
	{
		CheckDirectory(Path.Combine(Wwwroot, folder));
		var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
		var ms = new MemoryStream();
		await file.CopyToAsync(ms);
		await File.WriteAllBytesAsync(Path.Combine(Wwwroot, folder, fileName), ms.ToArray());
		return $"/{folder}/{fileName}";
	}
}