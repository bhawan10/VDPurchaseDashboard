window.ExpeditorData = {};

$(function (expeditorGlobal) {

    var initialiseVariable = function () {
        expeditorGlobal.mainData = $('#mainData');
        expeditorGlobal.vendor = $('#vendor');
        expeditorGlobal.PO = $('#PO');
        console.log("initialize");
        initialiseDropDowns();
    };
    var initialiseDropDowns = function () {
        //console.log(expeditorGlobal.mainData.data());
        expeditorGlobal.vendor.kendoDropDownList({
            placeholder: 'Please Select Vendor',
            height: 300,
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
            //autoBind = false,
            dataSource: {
                transport: {
                    read: {
                        url: expeditorGlobal.mainData.data('getAllPurchaseOrders'),
                    }
                }
            }
        });
    };
    var onVendorSelect = function (event) {
        //console.log("hello")
        //console.log(event.sender.dataItem());
        var oldUrl = '', newUrl = '';
        var dataItem = event.sender.dataItem();
        var POSelect = expeditorGlobal.PO.data('kendoDropDownList');
        var VendorSelect = expeditorGlobal.vendor.data('kendoDropDownList');

        oldUrl = POSelect.dataSource.transport.options.read.url;
        console.log(oldUrl);
        //console.log(POSelect);
        //console.log(VendorSelect);
        console.log(dataItem);
        if (dataItem) {
            newUrl = oldUrl + '?vendor=' + dataItem;
        }
        else {
            newUrl = oldUrl + '?vendor=' + '';
        }

        POSelect.dataSource.transport.options.read.url = newUrl;
        console.log(POSelect.dataSource.transport.options.read.url);
        POSelect.dataSource.read();
        POSelect.dataSource.transport.options.read.url = oldUrl;
        POSelect.value('');
    };
    //var onVendorList = function (event) {
        
    //};

    expeditorGlobal.onDocumentReady = function () {
        initialiseVariable();
    };
}(window.ExpeditorData));
