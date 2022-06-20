window.ExpeditorData = {};

$(function (expeditorGlobal) {

    var initialiseVariable = function () {
        expeditorGlobal.mainData = $('#mainData');
        expeditorGlobal.vendor = $('#vendor');
        expeditorGlobal.PO = $('#PO');
        expeditorGlobal.operation = $('#operation');
        expeditorGlobal.items = $('form');
        initialiseDropDowns();
    };
    var initialiseDropDowns = function () {
        //console.log(expeditorGlobal.mainData.data());
        expeditorGlobal.vendor.kendoDropDownList({
            placeholder: 'Please Select Vendor',
            height: 300,
            dataValueField: 'id',
            dataTextField: 'text',
            dataSource: {
                transport: {
                    read: {
                        url: expeditorGlobal.mainData.data('getAllVendorsList'),
                    }
                }
            },
            change: onVendorSelect
        });

        expeditorGlobal.PO.kendoDropDownList({
            placeholder: 'Please Select PO',
            height: 300,
            
            dataValueField: 'id',
            dataTextField: 'text',
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: expeditorGlobal.mainData.data('getAllPurchaseOrders'),
                    }
                }
            },
            change: onPOSelect
        });


        expeditorGlobal.operation.kendoDropDownList({
            placeholder: 'Please Select Operation',
            height: 300,

            dataValueField: 'id',
            dataTextField: 'text',
            autoBind: false,
            dataSource: {
                transport: {
                    read: {
                        url: expeditorGlobal.mainData.data('getAllDistinctCategory'),
                    }
                }
            },
            change: onOperationSelect
        });

        expeditorGlobal.items.kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: expeditorGlobal.mainData.data('getAllItemsList')
                    }
                }
            },
            autoBind: false,
            columns: [{
                field: "itemDescription",
                title: "Item Description"
            },
            {
                field: "toolNo",
                title: "Tool Number"
            },
            {
                field: "station",
                title: "Station"
            },
            {
                field: "positionNo",
                title: "Position Number"
            },
            {
                field: "reworkNo",
                title: "Rework Number"
            }
            ]
        })
        
    };
    var onVendorSelect = function (event) {

        var oldUrl = '', newUrl = '';
        var dataItem = event.sender.dataItem();

        var VendorSelect = expeditorGlobal.PO.data('kendoDropDownList');
        //var VendorSelect = expeditorGlobal.vendor.data('kendoDropDownList');
        oldUrl = VendorSelect.dataSource.transport.options.read.url;

        if (dataItem) {
            expeditorGlobal.VendorID = dataItem.id.toString();
            newUrl = oldUrl + '?vendor=' + dataItem.id.toString();
        }
        else {
            newUrl = oldUrl + '?vendor=' + '';
        }
        VendorSelect.dataSource.transport.options.read.url = newUrl;
        VendorSelect.dataSource.read();
        VendorSelect.dataSource.transport.options.read.url = oldUrl;
        
        VendorSelect.value('');
        expeditorGlobal.operation.data('kendoDropDownList').value('');
    };

    var onPOSelect = function (event) {

        var oldUrl = '', newUrl = '';
        var dataItem = event.sender.dataItem();
        var POSelect = expeditorGlobal.operation.data('kendoDropDownList');
        oldUrl = POSelect.dataSource.transport.options.read.url;

        if (dataItem) {
            //let check = dataItem.id;
            //console.log(check);
            expeditorGlobal.POID = dataItem.id.toString();
            newUrl = oldUrl + '?POId=' + dataItem.id.toString();
        }
        else {
            newUrl = oldUrl + '?POId=' + '';
        }
        POSelect.dataSource.transport.options.read.url = newUrl;
        POSelect.dataSource.read();
        POSelect.dataSource.transport.options.read.url = oldUrl;
        POSelect.value('');
        
    };

    var onOperationSelect = function (event) {

        var oldUrl = '', newUrl = '';
        var dataItem = event.sender.dataItem();
        var itemSelect = expeditorGlobal.items.data('kendoGrid');
        oldUrl = itemSelect.dataSource.transport.options.read.url;
        if (dataItem) {
            expeditorGlobal.OperationID = dataItem.id.toString();
            newUrl = oldUrl + '?operationId=' + expeditorGlobal.OperationID + '&poId=' + expeditorGlobal.POID;
        }
        else {
            newUrl = oldUrl + '?operationId=' + '' + '&poId=' + '';
        }
        itemSelect.dataSource.transport.options.read.url = newUrl;
        itemSelect.dataSource.read();
        itemSelect.dataSource.transport.options.read.url = oldUrl;
        console.log(itemSelect.dataSource.transport.options.read.url);
    };

    expeditorGlobal.onDocumentReady = function () {
        initialiseVariable();
    };
}(window.ExpeditorData));
