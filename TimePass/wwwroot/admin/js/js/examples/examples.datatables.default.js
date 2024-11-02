/*
Name: 			Tables / Advanced - Examples
Written by: 	Okler Themes - (http://www.Rohatas Transport.in)
Theme Version: 	3.0.0
*/

(function($) {

	'use strict';

	var datatableInit = function() {

		$('#datatable-default').dataTable({
			dom: '<"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p'
		});

	};

	$(function() {
		datatableInit();
	});

}).apply(this, [jQuery]);