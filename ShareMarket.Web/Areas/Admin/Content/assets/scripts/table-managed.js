var TableManaged = function () {

    return {

        //main function to initiate the module
        init: function () {

            if (!jQuery().dataTable) {
                return;
            }

            $('#sample_11').dataTable({
                "aoColumns": [
                  { "bSortable": false }

                ],
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 5,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });
            // begin first table
            $('#sample_1').dataTable({
                "aoColumns": [
                  { "bSortable": false },
                  null,
                  { "bSortable": false },
                  null,
                  { "bSortable": false },
                  { "bSortable": false }
                ],
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 5,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ records per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });

            jQuery('#sample_1 .group-checkable').change(function () {
                var set = jQuery(this).attr("data-set");
                var checked = jQuery(this).is(":checked");
                jQuery(set).each(function () {
                    if (checked) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
                jQuery.uniform.update(set);
            });

            jQuery('#sample_1_wrapper .dataTables_filter input').addClass("m-wrap medium"); // modify table search input
            jQuery('#sample_1_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            //jQuery('#sample_1_wrapper .dataTables_length select').select2(); // initialzie select2 dropdown

            // begin second table
            $('#sample_2').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 5,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });

            jQuery('#sample_2 .group-checkable').change(function () {
                var set = jQuery(this).attr("data-set");
                var checked = jQuery(this).is(":checked");
                jQuery(set).each(function () {
                    if (checked) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
                jQuery.uniform.update(set);
            });

            jQuery('#sample_2_wrapper .dataTables_filter input').addClass("m-wrap small"); // modify table search input
            jQuery('#sample_2_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#sample_2_wrapper .dataTables_length select').select2(); // initialzie select2 dropdown

            // begin: third table
            $('#sample_3').dataTable({
                "aLengthMenu": [
                    [5, 15, 20, -1],
                    [5, 15, 20, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 5,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [0]
                }
                ]
            });

            jQuery('#sample_3 .group-checkable').change(function () {
                var set = jQuery(this).attr("data-set");
                var checked = jQuery(this).is(":checked");
                jQuery(set).each(function () {
                    if (checked) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
                jQuery.uniform.update(set);
            });

            jQuery('#sample_3_wrapper .dataTables_filter input').addClass("m-wrap small"); // modify table search input
            jQuery('#sample_3_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#sample_3_wrapper .dataTables_length select').select2(); // initialzie select2 dropdown


            // begin: multi column table
            var multicolumn = $('#multiColumn').dataTable({
                "aLengthMenu": [
                    [50, 100, 200, -1],
                    [50, 100, 200, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 50,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "oLanguage": {
                    "sLengthMenu": "_MENU_ per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': [],
                }
                ]
            });

            jQuery('#multiColumn .group-checkable').change(function () {
                var set = jQuery(this).attr("data-set");
                var checked = jQuery(this).is(":checked");
                jQuery(set).each(function () {
                    if (checked) {
                        $(this).attr("checked", true);
                    } else {
                        $(this).attr("checked", false);
                    }
                });
                jQuery.uniform.update(set);
            });

            jQuery('#multiColumn_wrapper .dataTables_filter input').addClass("m-wrap small"); // modify table search input
            jQuery('#multiColumn_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#multiColumn_wrapper .dataTables_length select').select2(); // initialzie select2 dropdown            

            $("#multiColumn_wrapper thead input").change(function () {
                /* Filter on the column (the index) of this element */
                multicolumn.fnFilter(this.value, $("#multiColumn_wrapper thead input").index(this));
            });

            $("#multiColumn_wrapper thead input").click(function (event) {
                event.stopPropagation();
            });

            // begin: multi column table
            var inquiryTable = $('#inquiryTable').dataTable({
                "aLengthMenu": [
                    [50, 100, 200, -1],
                    [50, 100, 200, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 50,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "aaSorting": [[3, "desc"]],
                "bSortable": true,
                "oLanguage": {
                    "sLengthMenu": "_MENU_ per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': []
                }
                ]

            });

            jQuery('#inquiryTable_wrapper .dataTables_filter input').addClass("m-wrap small"); // modify table search input
            jQuery('#inquiryTable_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#inquiryTable_wrapper .dataTables_length select').select2(); // initialzie select2 dropdown           


            // begin: multi column table
            var queuedEmailTable = $('#queuedEmailTable').dataTable({
                "aLengthMenu": [
                    [50, 100, 200, -1],
                    [50, 100, 200, "All"] // change per page values here
                ],
                // set the initial value
                "iDisplayLength": 50,
                "sDom": "<'row-fluid'<'span6'l><'span6'f>r>t<'row-fluid'<'span6'i><'span6'p>>",
                "sPaginationType": "bootstrap",
                "aaSorting": [[7, "desc"]],
                "bSortable": true,
                "oLanguage": {
                    "sLengthMenu": "_MENU_ per page",
                    "oPaginate": {
                        "sPrevious": "Prev",
                        "sNext": "Next"
                    }
                },
                "aoColumnDefs": [{
                    'bSortable': false,
                    'aTargets': []
                }
                ]

            });

            jQuery('#queuedEmailTable_wrapper .dataTables_filter input').addClass("m-wrap small"); // modify table search input
            jQuery('#queuedEmailTable_wrapper .dataTables_length select').addClass("m-wrap small"); // modify table per page dropdown
            jQuery('#queuedEmailTable_wrapper .dataTables_length select').select2(); // initialzie select2 dropdown           


        }

    };

}();