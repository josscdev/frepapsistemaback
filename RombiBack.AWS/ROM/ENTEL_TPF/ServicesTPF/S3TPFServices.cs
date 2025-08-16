using Amazon.SecurityToken.Model;
using Amazon.SecurityToken;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3.Model;
using Amazon.S3;
using Amazon;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace RombiBack.AWS.ROM.ENTEL_TPF.ServicesTPF
{
    public class S3TPFServices
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3TPFServices(IConfiguration configuration)
        {
            var awsOptions = configuration.GetSection("AWSCredentials");
            var AWSAccessKey = awsOptions["AWSAccessKey"];
            var AWSSecretKey = awsOptions["AWSSecretKey"];
            var AWSRegion = awsOptions["AWSRegion"];
            _bucketName = awsOptions["AWSS3BucketName"];

            _s3Client = new AmazonS3Client(AWSAccessKey, AWSSecretKey, RegionEndpoint.USEast1);
        }

        public async Task<string> UploadFileToS3AsyncBundleTPF(IFormFile pdf)
        {
            if (pdf == null || pdf.Length == 0)
                throw new ArgumentException("No se ha proporcionado un archivo PDF.");

            try
            {
                using (var stream = new MemoryStream())
                {
                    await pdf.CopyToAsync(stream);
                    stream.Position = 0;

                    var key = $"BundlesRombiTPF/content/{pdf.FileName}";

                    var request = new PutObjectRequest
                    {
                        BucketName = _bucketName,
                        Key = key,
                        InputStream = stream,
                        ContentType = "application/pdf"
                    };

                    var response = await _s3Client.PutObjectAsync(request);

                    // Generar URL prefirmada
                    var urlRequest = new GetPreSignedUrlRequest
                    {
                        BucketName = _bucketName,
                        Key = key,
                        Expires = DateTime.Now.AddHours(1) // Duración de la URL prefirmada (1 hora en este ejemplo)
                    };

                    var preSignedUrl = _s3Client.GetPreSignedURL(urlRequest);
                    var base64String = Convert.ToBase64String(stream.ToArray());

                    var objectUrl = $"https://{_bucketName}.s3.amazonaws.com/BundlesRombiTPF/content/{pdf.FileName}";
                    var nombre = pdf.FileName;
                    // Crear un objeto JSON para la respuesta
                    var jsonResponse = new { status = "OK", message = "Archivo PDF cargado correctamente en Amazon S3.", nombrepdf = pdf.FileName, url = objectUrl, urlprefirmada = preSignedUrl, pdfBase64 = base64String };
                    return JsonConvert.SerializeObject(jsonResponse);
                }
            }
            catch (AmazonS3Exception ex)
            {
                throw new Exception($"Error al cargar el archivo PDF en Amazon S3: {ex.Message}");
            }
        }
        public async Task<string> UploadImageToS3AsyncBoletasTPF(string base64Image, string voucherName)
        {
            if (string.IsNullOrEmpty(base64Image) || string.IsNullOrEmpty(voucherName))
                throw new ArgumentException("No se ha proporcionado una imagen o nombre del voucher.");

            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64Image);
                using (var stream = new MemoryStream(imageBytes))
                {
                    var key = $"BoletasRombiTPF/content/{voucherName}";

                    var request = new PutObjectRequest
                    {
                        BucketName = _bucketName,
                        Key = key,
                        InputStream = stream,
                        ContentType = "image/png" // Ajusta esto si la imagen no es PNG
                    };

                    var response = await _s3Client.PutObjectAsync(request);

                    // Generar URL prefirmada
                    var urlRequest = new GetPreSignedUrlRequest
                    {
                        BucketName = _bucketName,
                        Key = key,
                        Expires = DateTime.Now.AddHours(1) // Duración de la URL prefirmada (1 hora en este ejemplo)
                    };

                    var preSignedUrl = _s3Client.GetPreSignedURL(urlRequest);
                    var base64String = Convert.ToBase64String(stream.ToArray());

                    var objectUrl = $"https://{_bucketName}.s3.amazonaws.com/{key}";

                    // Crear un objeto JSON para la respuesta
                    var jsonResponse = new
                    {
                        status = "OK",
                        message = "Imagen cargada correctamente en Amazon S3.",
                        nombreVoucher = voucherName,
                        url = objectUrl,
                        urlPrefirmada = preSignedUrl,
                        imagenBase64 = base64String
                    };
                    return JsonConvert.SerializeObject(jsonResponse);
                }
            }
            catch (AmazonS3Exception ex)
            {
                throw new Exception($"Error al cargar la imagen en Amazon S3: {ex.Message}");
            }
        }
        public string GetPreSignedUrlForVoucherTPF(string voucherName, int expirationMinutes = 15)
        {
            string objectKey = $"BoletasRombiTPF/content/{voucherName}";

            try
            {
                var request = new GetPreSignedUrlRequest
                {
                    BucketName = _bucketName,
                    Key = objectKey,
                    Expires = DateTime.Now.AddMinutes(expirationMinutes),
                    Protocol = Protocol.HTTPS
                };

                return _s3Client.GetPreSignedURL(request);
            }
            catch (AmazonS3Exception ex)
            {
                // Log the exception or handle it accordingly
                throw new Exception("Error generating pre-signed URL: " + ex.Message, ex);
            }
        }
    }
}
