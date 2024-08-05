using Acr.UserDialogs;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DollarStore.Modules.Receiving.Model;
using MvvmHelpers;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Command = Xamarin.Forms.Command;

namespace DollarStore.Common.ExportDataExcel
{
    public class ExcelViewModel : BaseViewModel
    {
        public ExcelViewModel()
        {
            ExportToExcelCommand = new Command<IEnumerable<Object>>(async (DataList) => await ExportDataToExcelAsync(DataList));
        }

        #region PROPERTIES
        public Command ExportToExcelCommand { get; set; }
        public string _filePath; 
        public string FilePath
        {
            get => _filePath;
            set => SetProperty(ref _filePath, value);
        }
        #endregion

        #region CREATE CELL IN EXCEL
        private DocumentFormat.OpenXml.Spreadsheet.Cell ConstructCell(string value, CellValues dataType)
        {
            return new DocumentFormat.OpenXml.Spreadsheet.Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType)
            };
        }
        #endregion

        #region BUILD EXPORT
        /* Export the list to excel file at the location provide by DependencyService */
        public async System.Threading.Tasks.Task ExportDataToExcelAsync(IEnumerable<Object> DataList)
        {
            
            // Granted storage permission
            var storageStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<StoragePermission>();

            if (storageStatus != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                storageStatus = await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();
            }

            if (DataList.Count() > 0)
            {
                using (UserDialogs.Instance.Loading(""))
                {
                    try
                    {
                        string date = DateTime.Now.ToString();

                        date = date.Replace("/", "_");
                        date = date.Replace(" ", "_");
                        date = date.Replace(":", "_");

                        var name = "";

                        if (DataList.FirstOrDefault() is ReceivedItemDetailModel received)
                        {
                            name = "Receiving";
                        }


                        var path = DependencyService.Get<IExportFilesToLocation>().GetFolderLoaction() + name + date + ".xlsx";
                        FilePath = path;

                        using (SpreadsheetDocument document = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook))
                        {
                            WorkbookPart workbookPart = document.AddWorkbookPart();
                            workbookPart.Workbook = new Workbook();

                            WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                            worksheetPart.Worksheet = new Worksheet();

                            Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());

                            Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = name +" List" };

                            sheets.Append(sheet);

                            workbookPart.Workbook.Save();

                            SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());

                            if (DataList is ObservableRangeCollection<ReceivedItemDetailModel> datalis)
                            {
                                // Constructing headers
                                Row row = new Row();

                                row.Append(
                                    ConstructCell("Code article", CellValues.String),
                                    ConstructCell("Référence", CellValues.String),
                                    ConstructCell("Code barre", CellValues.String),
                                    ConstructCell("Nom article", CellValues.String),
                                    ConstructCell("Description", CellValues.String),
                                    ConstructCell("Qté achat", CellValues.String),
                                    ConstructCell("Prix achat", CellValues.String),
                                    ConstructCell("Prix de vente", CellValues.String)
                                    );

                                // Insert the header row to the Sheet Data
                                sheetData.AppendChild(row);

                                // Add each product
                                foreach (var d in datalis)
                                {
                                    row = new Row();

                                    row.Append(
                                        ConstructCell(d?.InternalId?.ToString(), CellValues.String),
                                        ConstructCell("", CellValues.String),
                                        ConstructCell(d?.ItemBarcode.ToString(), CellValues.String),
                                        ConstructCell(d?.ItemName.ToString(), CellValues.String),
                                        ConstructCell(d?.ItemName.ToString(), CellValues.String),
                                        ConstructCell(d?.Quantity.ToString(), CellValues.String),
                                        ConstructCell(d?.SellPrice.ToString(), CellValues.String),
                                        ConstructCell(d?.SellPrice.ToString(), CellValues.String));

                                    sheetData.AppendChild(row);
                                }

                                worksheetPart.Worksheet.Save();
                                

                                bool op =  await Application.Current.MainPage.DisplayAlert("Info", "Data exported Successfully. The location is : " + FilePath,"Open","ok");
                                if (op)
                                {
                                    await Launcher.OpenAsync(new OpenFileRequest()
                                    {
                                        File = new ReadOnlyFile(FilePath)
                                    });
                                }
                            }

                        }

                    }
                    catch (Exception e)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", e.Message, "ok");
                    }
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Info", "No Data To Export ", "ok");
            }

            

        }
        #endregion

    }

}
