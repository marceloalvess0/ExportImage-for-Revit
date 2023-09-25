using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.IO;
using System.Xml.Linq;

namespace RevitExportViewImage
{
    [Transaction(TransactionMode.ReadOnly)]
    public class ExportViewImageCommand : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            try
            {
                // Obter o documento ativo do Revit
                UIDocument uidoc = commandData.Application.ActiveUIDocument;
                Document doc = uidoc.Document;

                // Obter a vista ativa
                View activeView = uidoc.ActiveView;

                // Verifique se a vista é válida para exportação
                if (activeView != null)
                {
                    // Configure as opções de exportação da imagem
                    ImageExportOptions options = new ImageExportOptions
                    {
                        FilePath = "C:\\Caminho\\Para\\Salvar\\Imagem.png", // Altere o caminho conforme necessário
                        ZoomType = ZoomFitType.FitToPage,
                        PixelSize = 1024, // Tamanho da imagem em pixels
                        FitDirection = FitDirectionType.Horizontal,
                    };

                    // Exporte a imagem da vista
                    doc.ExportImage(options);

                    return Result.Succeeded;
                }
                else
                {
                    TaskDialog.Show("Erro", "Não foi possível encontrar uma vista válida para exportação.");
                    return Result.Failed;
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
