using DiabetesApi.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace DiabetesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public PredictionController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        [HttpPost("predict")]
        public async Task<IActionResult> Predict([FromBody] PatientData data)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://prime-doria-lgharebhmedalat1-2586ea09.koyeb.app/predict", data);
                response.EnsureSuccessStatusCode();

                // استقبل الرد على هيئة PredictionResponse
                var result = await response.Content.ReadFromJsonAsync<PredictionResponse>();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
            [HttpPost("download")]
            public async Task<IActionResult> DownloadPDF([FromBody] PatientData patientData)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync("https://prime-doria-lgharebhmedalat1-2586ea09.koyeb.app/predict", patientData);
                    response.EnsureSuccessStatusCode();
                    var prediction = await response.Content.ReadFromJsonAsync<PredictionResponse>();

                    return GeneratePDF(patientData, prediction);
                }
                catch (Exception ex)
                {
                    return BadRequest(new { error = ex.Message });
                }
            }

        private IActionResult GeneratePDF(PatientData patientData, PredictionResponse prediction)
        {
            try
            {
                Document document = new Document(PageSize.A4, 36f, 36f, 36f, 36f); // هامش من كل الجوانب
                MemoryStream memoryStream = new MemoryStream();
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // إضافة خط مخصص (Arial أو Times New Roman)
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font titleFont = new Font(baseFont, 18, Font.BOLD, BaseColor.BLUE);
                Font sectionFont = new Font(baseFont, 14, Font.BOLD, BaseColor.DARK_GRAY);
                Font normalFont = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);
                Font linkFont = new Font(baseFont, 12, Font.UNDERLINE, BaseColor.GREEN);

                // إضافة العنوان
                Paragraph title = new Paragraph("My Reports", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                title.SpacingAfter = 20f;
                document.Add(title);

                // إضافة قسم التقارير الأخيرة
                Paragraph recentReports = new Paragraph("Recent Reports - Diabetes", sectionFont);
                recentReports.SpacingAfter = 10f;
                document.Add(recentReports);

                // إنشاء جدول لبيانات المريض
                PdfPTable patientTable = new PdfPTable(2); // 2 أعمدة
                patientTable.WidthPercentage = 100;
                patientTable.SetWidths(new float[] { 1f, 2f }); // نسب العرض

                // إعداد رأس الجدول
                patientTable.AddCell(new PdfPCell(new Phrase("Field", sectionFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase("Value", sectionFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 5 });

                // إضافة بيانات المريض إلى الجدول
                patientTable.AddCell(new PdfPCell(new Phrase("Gender", normalFont)) { Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase(patientData.Gender, normalFont)) { Padding = 5 });

                patientTable.AddCell(new PdfPCell(new Phrase("Age", normalFont)) { Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase(patientData.Age.ToString(), normalFont)) { Padding = 5 });

                patientTable.AddCell(new PdfPCell(new Phrase("Hypertension", normalFont)) { Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase(patientData.Hypertension.ToString(), normalFont)) { Padding = 5 });

                patientTable.AddCell(new PdfPCell(new Phrase("Heart Disease", normalFont)) { Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase(patientData.Heart_Disease.ToString(), normalFont)) { Padding = 5 });

                patientTable.AddCell(new PdfPCell(new Phrase("Smoking History", normalFont)) { Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase(patientData.Smoking_History, normalFont)) { Padding = 5 });

                patientTable.AddCell(new PdfPCell(new Phrase("BMI", normalFont)) { Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase(patientData.Bmi.ToString(), normalFont)) { Padding = 5 });

                patientTable.AddCell(new PdfPCell(new Phrase("HbA1c Level", normalFont)) { Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase(patientData.HbA1c_Level.ToString(), normalFont)) { Padding = 5 });

                patientTable.AddCell(new PdfPCell(new Phrase("Blood Glucose Level", normalFont)) { Padding = 5 });
                patientTable.AddCell(new PdfPCell(new Phrase(patientData.Blood_Glucose_Level.ToString(), normalFont)) { Padding = 5 });

                document.Add(patientTable);

                // إضافة مسافة
                document.Add(new Paragraph(" ", normalFont) { SpacingAfter = 20f });

                // إضافة نتيجة التنبؤ
                Paragraph predictionTitle = new Paragraph("Prediction Results", sectionFont);
                predictionTitle.SpacingAfter = 10f;
                document.Add(predictionTitle);

                PdfPTable predictionTable = new PdfPTable(2);
                predictionTable.WidthPercentage = 100;
                predictionTable.SetWidths(new float[] { 1f, 2f });

                predictionTable.AddCell(new PdfPCell(new Phrase("Field", sectionFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 5 });
                predictionTable.AddCell(new PdfPCell(new Phrase("Value", sectionFont)) { BackgroundColor = BaseColor.LIGHT_GRAY, Padding = 5 });

                predictionTable.AddCell(new PdfPCell(new Phrase("Risk Level", normalFont)) { Padding = 5 });
                predictionTable.AddCell(new PdfPCell(new Phrase($"{prediction.PredictionPercentage}%", normalFont)) { Padding = 5 });

                predictionTable.AddCell(new PdfPCell(new Phrase("Status", normalFont)) { Padding = 5 });
                predictionTable.AddCell(new PdfPCell(new Phrase(prediction.Recommendation.Message, normalFont)) { Padding = 5 });

                predictionTable.AddCell(new PdfPCell(new Phrase("Report Title", normalFont)) { Padding = 5 });
                predictionTable.AddCell(new PdfPCell(new Phrase("Diabetes check", normalFont)) { Padding = 5 });

                predictionTable.AddCell(new PdfPCell(new Phrase("Date & Time", normalFont)) { Padding = 5 });
                predictionTable.AddCell(new PdfPCell(new Phrase(DateTime.Now.ToString(), normalFont)) { Padding = 5 });

                predictionTable.AddCell(new PdfPCell(new Phrase("Risk Level Indicator", normalFont)) { Padding = 5 });
                predictionTable.AddCell(new PdfPCell(new Phrase(prediction.Recommendation.RiskLevel, normalFont)) { Padding = 5 });

                document.Add(predictionTable);

                // إضافة زر تحميل (كرابط نصي)
                Paragraph downloadLink = new Paragraph("Download PDF", linkFont);
                downloadLink.Alignment = Element.ALIGN_CENTER;
                downloadLink.SpacingBefore = 20f;
                document.Add(downloadLink);

                document.Close();

                byte[] bytes = memoryStream.ToArray();
                return File(bytes, "application/pdf", "DiabetesReport.pdf");
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
    }
