using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RombiBack.Entities.ROM.FREPAPMODULE.MODULOREGISTROS;
using RombiBack.Repository.ROM.FREPAPMODULE.MODULOREGISTROS;

namespace RombiBack.Services.ROM.FREPAPMODULE.MODULOREGISTROS
{
    public class AfiliacionesServices: IAfiliacionesServices
    {
        private readonly IAfiliacionesRepository _afiliacionesRepository;

        public AfiliacionesServices(IAfiliacionesRepository afiliacionesRepository)
        {
            _afiliacionesRepository = afiliacionesRepository;
        }

        public Task<IEnumerable<ListarAfiliacion>> ListarAfiliaciones(FiltroAfiliacion filtro)
        {
            // Aquí podrías aplicar reglas adicionales (normalizar perfil/usuario, etc.)
            return _afiliacionesRepository.ListarAfiliaciones(filtro);
        }

        public Task<IEnumerable<ListarOpcionUbigeo>> ListarUbigeos(int? idemppaisnegcue, int? pais)
        => _afiliacionesRepository.ListarUbigeos(idemppaisnegcue, pais);

        private const string Root = @"C:\Archivos";
        private const int MaxImageKB = 200;
        private const int MaxPdfMB = 2;
        private static readonly string[] ImageTypes = { "image/png", "image/jpeg", "image/jpg", "image/webp" };
        private static readonly string[] PdfTypes = { "application/pdf" };
        // —— nuevo registrar ——
        public async Task<RegistrarAfiliacionResult> RegistrarAfiliacion(
            AfiliacionCreateDto model,
            IFormFile? foto,
            IFormFile? fichaafiliacionfile,
            IFormFile? hojadevida,
            IFormFile? copiadocumento)
        {
            var tempId = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var saved = new Dictionary<string, string?>();

            if (foto != null)
                saved["foto"] = await SaveToFolder(
                    foto, Path.Combine(Root, "Foto"), $"FOTO_{tempId}", ImageTypes, MaxImageKB * 1024);

            if (fichaafiliacionfile != null)
                saved["fichaafiliacionfile"] = await SaveToFolder(
                    fichaafiliacionfile, Path.Combine(Root, "FichaAfiliado"), $"FICHA_{tempId}", PdfTypes, MaxPdfMB * 1024 * 1024);

            if (hojadevida != null)
                saved["hojadevida"] = await SaveToFolder(
                    hojadevida, Path.Combine(Root, "HojaVida"), $"HV_{tempId}", PdfTypes, MaxPdfMB * 1024 * 1024);

            if (copiadocumento != null)
                saved["copiadocumento"] = await SaveToFolder(
                    copiadocumento, Path.Combine(Root, "CopiaDocumento"), $"COPIA_{tempId}", PdfTypes, MaxPdfMB * 1024 * 1024);

            // log opcional
            Console.WriteLine("[AFILIACION] " + JsonSerializer.Serialize(new { model, saved },
                new JsonSerializerOptions { WriteIndented = true }));

            return new RegistrarAfiliacionResult { Ok = true, Files = saved };
        }

        // ====== helpers privados ======
        private static async Task<string?> SaveToFolder(
            IFormFile file,
            string folder,
            string prefix,
            string[] allowedTypes,
            long maxBytes)
        {
            if (file.Length == 0) return null;
            if (!allowedTypes.Contains(file.ContentType))
                throw new InvalidOperationException($"Tipo no permitido: {file.ContentType}");
            if (file.Length > maxBytes)
                throw new InvalidOperationException($"Archivo supera el límite de {maxBytes} bytes.");

            Directory.CreateDirectory(folder);

            var ext = Path.GetExtension(file.FileName);
            var name = $"{prefix}_{DateTime.Now:yyyyMMddHHmmssfff}{ext}".ToLowerInvariant();
            var path = Path.Combine(folder, Sanitize(name));

            using (var fs = System.IO.File.Create(path))
                await file.CopyToAsync(fs);

            return path;
        }

        private static string Sanitize(string fileName)
        {
            foreach (var ch in Path.GetInvalidFileNameChars())
                fileName = fileName.Replace(ch, '_');
            return fileName;
        }
    }
}
