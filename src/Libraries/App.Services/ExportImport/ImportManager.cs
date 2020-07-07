using App.Core;
using App.Core.Domain.Categorize;
using App.Services.Categorize;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace App.Services.ExportImport
{
    /// <summary>
    /// Import manager
    /// </summary>
    //public partial class ImportManager : IImportManager
    //{
    //    #region Fields

    //    private readonly IQuestionService _questionService;
    //    private readonly IWorkContext _workContext;

    //    #endregion

    //    #region Ctor

    //    public ImportManager(IQuestionService questionService,
    //        IWorkContext workContext)
    //    {
    //        _questionService = questionService;
    //        _workContext = workContext;
    //    }

    //    #endregion

    //    #region Utilities

    //    private ImportProductMetadata PrepareImportProductData(ExcelWorksheet worksheet)
    //    {
    //        //the columns
    //        var properties = GetPropertiesByExcelCells<Product>(worksheet);

    //        var manager = new PropertyManager<Product>(properties, _catalogSettings);

    //        var endRow = 2;
    //        var allCategories = new List<string>();
    //        var allSku = new List<string>();

    //        var tempProperty = manager.GetProperty("Categories");
    //        var categoryCellNum = tempProperty?.PropertyOrderPosition ?? -1;

    //        tempProperty = manager.GetProperty("SKU");
    //        var skuCellNum = tempProperty?.PropertyOrderPosition ?? -1;

    //        if (_catalogSettings.ExportImportUseDropdownlistsForAssociatedEntities)
    //        {
    //            productAttributeManager.SetSelectList("AttributeControlType", AttributeControlType.TextBox.ToSelectList(useLocalization: false));
    //            productAttributeManager.SetSelectList("AttributeValueType", AttributeValueType.Simple.ToSelectList(useLocalization: false));

    //            manager.SetSelectList("ProductType", ProductType.SimpleProduct.ToSelectList(useLocalization: false));

    //        }

    //        var allAttributeIds = new List<int>();
    //        var allSpecificationAttributeOptionIds = new List<int>();

    //        var attributeIdCellNum = 1 + ExportProductAttribute.ProducAttributeCellOffset;
    //        var specificationAttributeOptionIdCellNum =
    //            specificationAttributeManager.GetIndex("SpecificationAttributeOptionId") +
    //            ExportProductAttribute.ProducAttributeCellOffset;

    //        var productsInFile = new List<int>();

    //        //find end of data
    //        var typeOfExportedAttribute = ExportedAttributeType.NotSpecified;
    //        while (true)
    //        {
    //            var allColumnsAreEmpty = manager.GetProperties
    //                .Select(property => worksheet.Cells[endRow, property.PropertyOrderPosition])
    //                .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString()));

    //            if (allColumnsAreEmpty)
    //                break;

    //            if (new[] { 1, 2 }.Select(cellNum => worksheet.Cells[endRow, cellNum])
    //                    .All(cell => string.IsNullOrEmpty(cell?.Value?.ToString())) &&
    //                worksheet.Row(endRow).OutlineLevel == 0)
    //            {
    //                var cellValue = worksheet.Cells[endRow, attributeIdCellNum].Value;
    //                SetOutLineForProductAttributeRow(cellValue, worksheet, endRow);
    //                SetOutLineForSpecificationAttributeRow(cellValue, worksheet, endRow);
    //            }

    //            if (worksheet.Row(endRow).OutlineLevel != 0)
    //            {
    //                var newTypeOfExportedAttribute = GetTypeOfExportedAttribute(worksheet, productAttributeManager, specificationAttributeManager, endRow);

    //                //skip caption row
    //                if (newTypeOfExportedAttribute != ExportedAttributeType.NotSpecified && newTypeOfExportedAttribute != typeOfExportedAttribute)
    //                {
    //                    typeOfExportedAttribute = newTypeOfExportedAttribute;
    //                    endRow++;
    //                    continue;
    //                }

    //                endRow++;
    //                continue;
    //            }

    //            if (categoryCellNum > 0)
    //            {
    //                var categoryIds = worksheet.Cells[endRow, categoryCellNum].Value?.ToString() ?? string.Empty;

    //                if (!string.IsNullOrEmpty(categoryIds))
    //                    allCategories.AddRange(categoryIds
    //                        .Split(new[] { ";", ">>" }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim())
    //                        .Distinct());
    //            }

    //            if (skuCellNum > 0)
    //            {
    //                var sku = worksheet.Cells[endRow, skuCellNum].Value?.ToString() ?? string.Empty;

    //                if (!string.IsNullOrEmpty(sku))
    //                    allSku.Add(sku);
    //            }

    //            //counting the number of products
    //            productsInFile.Add(endRow);

    //            endRow++;
    //        }

    //        //performance optimization, the check for the existence of the categories in one SQL request
    //        var notExistingCategories = _categoryService.GetNotExistingCategories(allCategories.ToArray());
    //        if (notExistingCategories.Any())
    //        {
    //            throw new ArgumentException(string.Format(_localizationService.GetResource("Admin.Catalog.Products.Import.CategoriesDontExist"), string.Join(", ", notExistingCategories)));
    //        }

    //        return new ImportProductMetadata
    //        {
    //            EndRow = endRow,
    //            Manager = manager,
    //            Properties = properties,
    //            ProductsInFile = productsInFile,
    //            ProductAttributeManager = productAttributeManager,
    //            SpecificationAttributeManager = specificationAttributeManager,
    //            SkuCellNum = skuCellNum,
    //            AllSku = allSku
    //        };
    //    }

    //    #endregion

    //    #region Methods

    //    /// <summary>
    //    /// Import products from XLSX file
    //    /// </summary>
    //    /// <param name="stream">Stream</param>
    //    public virtual void ImportQuestionsFromXlsx(Stream stream)
    //    {
    //        using (var xlPackage = new ExcelPackage(stream))
    //        {
    //            // get the first worksheet in the workbook
    //            var worksheet = xlPackage.Workbook.Worksheets.FirstOrDefault();
    //            if (worksheet == null)
    //                throw new Exception("No worksheet found");

    //            var downloadedFiles = new List<string>();

    //            var metadata = PrepareImportQuestionData(worksheet);

    //            if (_catalogSettings.ExportImportSplitProductsFile && metadata.CountProductsInFile > _catalogSettings.ExportImportProductsCountInOneFile)
    //            {
    //                ImportProductsFromSplitedXlsx(worksheet, metadata);
    //                return;
    //            }

    //            //performance optimization, load all products by SKU in one SQL request
    //            var allProductsBySku = _questionService.GetProductsBySku(metadata.AllSku.ToArray(), _workContext.CurrentVendor?.Id ?? 0);

    //            //performance optimization, load all categories IDs for products in one SQL request
    //            var allProductsCategoryIds = _categoryService.GetProductCategoryIds(allProductsBySku.Select(p => p.Id).ToArray());

    //            //performance optimization, load all categories in one SQL request
    //            Dictionary<CategoryKey, Category> allCategories;
    //            try
    //            {
    //                var allCategoryList = _categoryService.GetAllCategories(showHidden: true, loadCacheableCopy: false);

    //                allCategories = allCategoryList
    //                    .ToDictionary(c => new CategoryKey(c, _categoryService, allCategoryList, _storeMappingService), c => c);
    //            }
    //            catch (ArgumentException)
    //            {
    //                //categories with the same name are not supported in the same category level
    //                throw new ArgumentException(_localizationService.GetResource("Admin.Catalog.Products.Import.CategoriesWithSameNameNotSupported"));
    //            }

    //            Product lastLoadedProduct = null;
    //            var typeOfExportedAttribute = ExportedAttributeType.NotSpecified;

    //            for (var iRow = 2; iRow < metadata.EndRow; iRow++)
    //            {
    //                //imports product attributes
    //                if (worksheet.Row(iRow).OutlineLevel != 0)
    //                {
    //                    if (lastLoadedProduct == null)
    //                        continue;

    //                    var newTypeOfExportedAttribute = GetTypeOfExportedAttribute(worksheet, metadata.ProductAttributeManager, metadata.SpecificationAttributeManager, iRow);

    //                    //skip caption row
    //                    if (newTypeOfExportedAttribute != ExportedAttributeType.NotSpecified &&
    //                        newTypeOfExportedAttribute != typeOfExportedAttribute)
    //                    {
    //                        typeOfExportedAttribute = newTypeOfExportedAttribute;
    //                        continue;
    //                    }

    //                    continue;
    //                }

    //                metadata.Manager.ReadFromXlsx(worksheet, iRow);

    //                var question = metadata.SkuCellNum > 0 ? allProductsBySku.FirstOrDefault(p => p.Sku == metadata.Manager.GetProperty("SKU").StringValue) : null;

    //                var isNew = question == null;

    //                question = question ?? new Question();

    //                //some of previous values
    //                var previousStockQuantity = question.StockQuantity;
    //                var previousWarehouseId = question.WarehouseId;

    //                if (isNew)
    //                    question.CreatedOnUtc = DateTime.UtcNow;

    //                foreach (var property in metadata.Manager.GetProperties)
    //                {
    //                    switch (property.PropertyName)
    //                    {
    //                        case "QuestionType":
    //                            question.QuestionTypeId = property.IntValue;
    //                            break;
    //                        case "Name":
    //                            question.Name = property.StringValue;
    //                            break;
    //                        case "Published":
    //                            question.Published = property.BooleanValue;
    //                            break;

    //                    }
    //                }

    //                //set some default values if not specified
    //                if (isNew && metadata.Properties.All(p => p.PropertyName != "ProductType"))
    //                    question.ProductType = ProductType.SimpleProduct;
    //                if (isNew && metadata.Properties.All(p => p.PropertyName != "VisibleIndividually"))
    //                    question.VisibleIndividually = true;
    //                if (isNew && metadata.Properties.All(p => p.PropertyName != "Published"))
    //                    question.Published = true;

    //                //sets the current vendor for the new product
    //                if (isNew && _workContext.CurrentVendor != null)
    //                    question.VendorId = _workContext.CurrentVendor.Id;

    //                question.UpdatedOnUtc = DateTime.UtcNow;

    //                if (isNew)
    //                {
    //                    _questionService.InsertProduct(question);
    //                }
    //                else
    //                {
    //                    _questionService.UpdateProduct(question);
    //                }

    //                //quantity change history
    //                if (isNew || previousWarehouseId == question.WarehouseId)
    //                {
    //                    _questionService.AddStockQuantityHistoryEntry(question, question.StockQuantity - previousStockQuantity, question.StockQuantity,
    //                        question.WarehouseId, _localizationService.GetResource("Admin.StockQuantityHistory.Messages.ImportProduct.Edit"));
    //                }
    //                //warehouse is changed 
    //                else
    //                {
    //                    //compose a message
    //                    var oldWarehouseMessage = string.Empty;
    //                    if (previousWarehouseId > 0)
    //                    {
    //                        var oldWarehouse = _shippingService.GetWarehouseById(previousWarehouseId);
    //                        if (oldWarehouse != null)
    //                            oldWarehouseMessage = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse.Old"), oldWarehouse.Name);
    //                    }

    //                    var newWarehouseMessage = string.Empty;
    //                    if (question.WarehouseId > 0)
    //                    {
    //                        var newWarehouse = _shippingService.GetWarehouseById(question.WarehouseId);
    //                        if (newWarehouse != null)
    //                            newWarehouseMessage = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.EditWarehouse.New"), newWarehouse.Name);
    //                    }

    //                    var message = string.Format(_localizationService.GetResource("Admin.StockQuantityHistory.Messages.ImportProduct.EditWarehouse"), oldWarehouseMessage, newWarehouseMessage);

    //                    //record history
    //                    _questionService.AddStockQuantityHistoryEntry(question, -previousStockQuantity, 0, previousWarehouseId, message);
    //                    _questionService.AddStockQuantityHistoryEntry(question, question.StockQuantity, question.StockQuantity, question.WarehouseId, message);
    //                }

    //                var tempProperty = metadata.Manager.GetProperty("SeName");

    //                //search engine name
    //                var seName = tempProperty?.StringValue ?? (isNew ? string.Empty : _urlRecordService.GetSeName(question, 0));
    //                _urlRecordService.SaveSlug(question, _urlRecordService.ValidateSeName(question, seName, question.Name, true), 0);

    //                tempProperty = metadata.Manager.GetProperty("Categories");

    //                if (tempProperty != null)
    //                {
    //                    var categoryList = tempProperty.StringValue;

    //                    //category mappings
    //                    var categories = isNew || !allProductsCategoryIds.ContainsKey(question.Id) ? new int[0] : allProductsCategoryIds[question.Id];

    //                    var importedCategories = categoryList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
    //                        .Select(categoryName => new CategoryKey(categoryName))
    //                        .Select(categoryKey =>
    //                        {
    //                            var rez = allCategories.ContainsKey(categoryKey) ? allCategories[categoryKey].Id : allCategories.Values.FirstOrDefault(c => c.Name == categoryKey.Key)?.Id;

    //                            if (!rez.HasValue && int.TryParse(categoryKey.Key, out var id))
    //                            {
    //                                rez = id;
    //                            }

    //                            if (!rez.HasValue)
    //                            {
    //                                //database doesn't contain the imported category
    //                                throw new ArgumentException(string.Format(_localizationService.GetResource("Admin.Catalog.Products.Import.DatabaseNotContainCategory"), categoryKey.Key));
    //                            }

    //                            return rez.Value;
    //                        }).ToList();

    //                    foreach (var categoryId in importedCategories)
    //                    {
    //                        if (categories.Any(c => c == categoryId))
    //                            continue;

    //                        var productCategory = new ProductCategory
    //                        {
    //                            ProductId = question.Id,
    //                            CategoryId = categoryId,
    //                            IsFeaturedProduct = false,
    //                            DisplayOrder = 1
    //                        };
    //                        _categoryService.InsertProductCategory(productCategory);
    //                    }

    //                    //delete product categories
    //                    var deletedProductCategories = categories.Where(categoryId => !importedCategories.Contains(categoryId))
    //                        .Select(categoryId => question.ProductCategories.First(pc => pc.CategoryId == categoryId));
    //                    foreach (var deletedProductCategory in deletedProductCategories)
    //                    {
    //                        _categoryService.DeleteProductCategory(deletedProductCategory);
    //                    }
    //                }

    //                tempProperty = metadata.Manager.GetProperty("Manufacturers");
    //                if (tempProperty != null)
    //                {
    //                    var manufacturerList = tempProperty.StringValue;

    //                    //manufacturer mappings
    //                    var manufacturers = isNew || !allProductsManufacturerIds.ContainsKey(question.Id) ? new int[0] : allProductsManufacturerIds[question.Id];
    //                    var importedManufacturers = manufacturerList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
    //                        .Select(x => allManufacturers.FirstOrDefault(m => m.Name == x.Trim())?.Id ?? int.Parse(x.Trim())).ToList();
    //                    foreach (var manufacturerId in importedManufacturers)
    //                    {
    //                        if (manufacturers.Any(c => c == manufacturerId))
    //                            continue;

    //                        var productManufacturer = new ProductManufacturer
    //                        {
    //                            ProductId = question.Id,
    //                            ManufacturerId = manufacturerId,
    //                            IsFeaturedProduct = false,
    //                            DisplayOrder = 1
    //                        };
    //                        _manufacturerService.InsertProductManufacturer(productManufacturer);
    //                    }

    //                    //delete product manufacturers
    //                    var deletedProductsManufacturers = manufacturers.Where(manufacturerId => !importedManufacturers.Contains(manufacturerId))
    //                        .Select(manufacturerId => question.ProductManufacturers.First(pc => pc.ManufacturerId == manufacturerId));
    //                    foreach (var deletedProductManufacturer in deletedProductsManufacturers)
    //                    {
    //                        _manufacturerService.DeleteProductManufacturer(deletedProductManufacturer);
    //                    }
    //                }

    //                tempProperty = metadata.Manager.GetProperty("ProductTags");
    //                if (tempProperty != null)
    //                {
    //                    var productTags = tempProperty.StringValue.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()).ToList();

    //                    //searching existing product tags by their id
    //                    var productTagIds = productTags.Where(pt => int.TryParse(pt, out var _)).Select(int.Parse);
    //                    var pruductTagsByIds = question.ProductProductTagMappings
    //                        .Select(mapping => mapping.ProductTag).Where(pt => productTagIds.Contains(pt.Id)).ToList();
    //                    productTags.AddRange(pruductTagsByIds.Select(pt => pt.Name));
    //                    var filter = pruductTagsByIds.Select(pt => pt.Id.ToString()).ToList();

    //                    //product tag mappings
    //                    _productTagService.UpdateProductTags(question, productTags.Where(pt => !filter.Contains(pt)).ToArray());
    //                }

    //                tempProperty = metadata.Manager.GetProperty("LimitedToStores");
    //                if (tempProperty != null)
    //                {
    //                    var limitedToStoresList = tempProperty.StringValue;

    //                    var importedStores = question.LimitedToStores ? limitedToStoresList.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
    //                        .Select(x => allStores.FirstOrDefault(store => store.Name == x.Trim())?.Id ?? int.Parse(x.Trim())).ToList() : new List<int>();

    //                    _questionService.UpdateProductStoreMappings(question, importedStores);
    //                }

    //                var picture1 = DownloadFile(metadata.Manager.GetProperty("Picture1")?.StringValue, downloadedFiles);
    //                var picture2 = DownloadFile(metadata.Manager.GetProperty("Picture2")?.StringValue, downloadedFiles);
    //                var picture3 = DownloadFile(metadata.Manager.GetProperty("Picture3")?.StringValue, downloadedFiles);

    //                productPictureMetadata.Add(new ProductPictureMetadata
    //                {
    //                    ProductItem = question,
    //                    Picture1Path = picture1,
    //                    Picture2Path = picture2,
    //                    Picture3Path = picture3,
    //                    IsNew = isNew
    //                });

    //                lastLoadedProduct = question;

    //                //update "HasTierPrices" and "HasDiscountsApplied" properties
    //                //_questionService.UpdateHasTierPricesProperty(product);
    //                //_questionService.UpdateHasDiscountsApplied(product);
    //            }

    //            if (_mediaSettings.ImportProductImagesUsingHash && _pictureService.StoreInDb && _dataProvider.SupportedLengthOfBinaryHash > 0)
    //                ImportProductImagesUsingHash(productPictureMetadata, allProductsBySku);
    //            else
    //                ImportProductImagesUsingServices(productPictureMetadata);

    //            foreach (var downloadedFile in downloadedFiles)
    //            {
    //                if (!_fileProvider.FileExists(downloadedFile))
    //                    continue;

    //                try
    //                {
    //                    _fileProvider.DeleteFile(downloadedFile);
    //                }
    //                catch
    //                {
    //                    // ignored
    //                }
    //            }

    //            //activity log
    //            _customerActivityService.InsertActivity("ImportProducts", string.Format(_localizationService.GetResource("ActivityLog.ImportProducts"), metadata.CountProductsInFile));
    //        }
    //    }

    //    #endregion
    //}
}
