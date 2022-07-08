window.ExpeditorData = {};

$(function (expeditorGlobal) {

    var initialiseVariable = function () {
        expeditorGlobal.mainData = $('#mainData');
        expeditorGlobal.vendor = $('#vendor');
        expeditorGlobal.PO = $('#PO');
        expeditorGlobal.submitButton = $('#finalSubmit');
        expeditorGlobal.operation = $('#operation');
        expeditorGlobal.items = $('#form');
        expeditorGlobal.dict = {};
        expeditorGlobal.kendoNotification = $('#notification').kendoNotification({
            position: {
                top: 60
            },
            stacking: 'down',
            autoHideAfter: 1500
        });
        initialiseDropDowns();
    };
    var initialiseDropDowns = function () {
        expeditorGlobal.submitButton.on('click', onButtonPress);

        expeditorGlobal.vendor.kendoDropDownList({
            optionLabel: 'Please Select Vendor',
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
            optionLabel: 'Please Select PO',
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
            optionLabel: 'Please Select Operation',
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
            editable: "incell",
            width: "70%",
            columns: [
            {
                title: "TSPR Number / Model Number",
                template: '<div><div class="text-right"><strong>#=toolNo#-#=station#-#=positionNo#-#=reworkNo#</strong></div><div class="text-right">#=modelNo#</div></div>',
                editable: function () { return false; }
            },
            {
                field: "doneQuantity",
                title: "Quantity",
                width: "30%",
                template: '<div><input style="max-width: 60%; text-align:center;" class="kendo-editable-cell" value="#=doneQuantity#"></input><span> / #= givenQuantity# </span></div>',
                editor: function (container, options) {
                    var check = options.model.givenQuantity;
                    console.log(check);
                    $('<div><input  style="max-width: 60%; text-align:center;" type="number" data-bind="value:doneQuantity" class="k-textbox" required /><span> /' + check + '</span></div>').appendTo(container);
                }
            }],
            cellClose: function (event) {
                if (validateGrid(event.model.doneQuantity, event.model.givenQuantity)) {
                    var entry = {};

                    var today = new Date();
                    var date = today.getFullYear() + '-' + (today.getMonth() + 1) + '-' + today.getDate();
                    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
                    var dateTime = date + ' ' + time;

                    entry.POItemID = event.model.id;
                    entry.OperationId = expeditorGlobal.OperationID;
                    entry.totalQuantity = event.model.givenQuantity;
                    entry.doneQuantity = event.model.doneQuantity;
                    entry.entryBy = "Ankur.AD375";
                    entry.POId = expeditorGlobal.POID;
                    entry.isActive = true;
                    entry.isCompleted = false;
                    entry.entryDate = dateTime;
                    expeditorGlobal.dict[entry.POItemID] = entry;
                }
            }
        })
        
    };

    var validateGrid = function (done, given) {
        var isValid = true;
        if (done > given) {
            showErrorNotification("Quantity can not be more than total");
            isValid = false;
        }
        return isValid;
    };

    var showSuccessNotification = function (message) {
        expeditorGlobal.kendoNotification.data('kendoNotification').success(message);
    };

    var showErrorNotification = function (message) {
        if (!message) {
            message = 'Some error occured, please contact IT team';
        }
        expeditorGlobal.kendoNotification.data('kendoNotification').error(message);
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
        /*console.log(itemSelect.dataSource.transport.options.read.url);*/
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
    };


    var onButtonPress = function () {
        var checkList = [];
        for (var key in expeditorGlobal.dict) {
            checkList.push(expeditorGlobal.dict[key]);
        }
        if (checkList.length > 0) {
            $.ajax({
                url: expeditorGlobal.mainData.data('check'),
                type: 'POST',
                data: {
                    forms: checkList,
                },
                success: function () {
                    console.log("done");
                    showSuccessNotification("Form submitted!");
                    //dict clear
                    //for (var del in expeditorGlobal.dict) {
                    //    if (expeditorGlobal.dict.hasOwnProperty(del)) {
                    //        delete expeditorGlobal.dict[del];
                    //    }
                    //}
                },
                error: function (data) {
                    debugger;
                    console.log("error");
                    console.log(data);
                }
            })
        }
        else {
            showErrorNotification("Form not submitted");
        }
    };

    expeditorGlobal.onDocumentReady = function () {
        initialiseVariable();
    };
}(window.ExpeditorData));
